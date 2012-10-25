using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using MassTransit;
using Pariveda.Infrastructure.Contract;
using Pariveda.Domain;

namespace Parivda.EventStore
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

        public void SaveChanges(Guid aggregateId, int originatingVersion, IEnumerable<Event> events)
        {
            ExecuteProcedure(WriteEventSprocName, cmd =>
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        events.ToList().ForEach(e =>
                        {
                            cmd.Parameters.Add("@AggregateId", SqlDbType.UniqueIdentifier).Value = aggregateId;
                            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = e.GetType().ToString();
                            cmd.Parameters.Add("@Data", SqlDbType.VarBinary).Value = _serializer.Serialize(e);
                            cmd.Parameters.Add("@ExpectedVersion", SqlDbType.Int).Value = originatingVersion;

                            cmd.ExecuteNonQuery(); // Insert into the DB
                            _bus.Publish(e); // Publish to the enterprise
                        });
                        scope.Complete();
                    }
                    catch (SqlException e)
                    {
                        // TODO: Figure out exception strategy for concurrency issues
                    }
                }
            });
        }

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
    }
}
