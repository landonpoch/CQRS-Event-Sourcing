using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using CQRS.Application.Services;
using CQRS.Domain.Common;
using CQRS.Infrastructure.Contract;
using CQRS.Messages.Events;

namespace CQRS.Infrastructure.EventStore
{
    public class EventStore : IEventStore
    {
        private const string GetEventsSprocName = "GetEvents";
        private const string WriteEventSprocName = "WriteEvent";
        private IApplicationSettings _settings;
        private IEventSerializer _serializer;
        private IBus _bus;

        public EventStore(IApplicationSettings settings, IEventSerializer serializer, IBus bus)
        {
            _settings = settings;
            _serializer = serializer;
            _bus = bus;
        }

        #region IEventStore Members

        public void SaveChanges(Guid aggregateId, Type aggregateType, int originatingVersion, IEnumerable<Event> events)
        {
            ExecuteProcedure(WriteEventSprocName, cmd =>
            {
                // TODO: Eventually, this call should be batched
                events.ToList().ForEach(e =>
                {
                    SaveAndPublish(cmd, e, aggregateId, aggregateType, originatingVersion);
                    originatingVersion++; // Increment the version for each event
                });
            });
        }

        // TODO: Implement snapshotting
        public IEnumerable<Event> GetEvents(Guid aggregateId)
        {
            var events = new List<Event>();
            ExecuteProcedure(GetEventsSprocName, cmd =>
            {
                cmd.Parameters.Add("@AggregateId", SqlDbType.UniqueIdentifier).Value = aggregateId;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            byte[] data = reader[1] as byte[];
                            Event @event = _serializer.Deserialize(data);
                            events.Add(@event);
                        }
                    }
                }
            });
            return events;
        }

        #endregion

        private void ExecuteProcedure(string sprocName, Action<SqlCommand> sqlAction)
        {
            using (var conn = new SqlConnection(_settings.EventStoreConnectionString))
            using (var command = new SqlCommand(sprocName, conn) 
                { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                sqlAction.Invoke(command);
            }
        }

        private void SaveAndPublish(SqlCommand cmd, Event @event, Guid aggregateId, Type aggregateType, int originatingVersion)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    @event.Version = originatingVersion;
                    cmd.Parameters.Add("@AggregateId", SqlDbType.UniqueIdentifier).Value = aggregateId;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = aggregateType.ToString();
                    cmd.Parameters.Add("@Data", SqlDbType.VarBinary).Value = _serializer.Serialize(@event);
                    cmd.Parameters.Add("@ExpectedVersion", SqlDbType.Int).Value = originatingVersion;

                    cmd.ExecuteNonQuery(); // Insert into the DB
                    _bus.Publish(@event); // Publish to the enterprise
                    scope.Complete();
                }
            }
            catch (SqlException e)
            {
                throw new Exception("An error occured while trying to insert into the event log.", e);
            }
        }
    }
}
