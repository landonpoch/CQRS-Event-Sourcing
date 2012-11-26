using System.Text;
using CQRS.Infrastructure.Contract;
using CQRS.Messages.Events;
using Newtonsoft.Json;

namespace CQRS.Infrastructure.Services
{
    public class EventSerializer : IEventSerializer
    {
        JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        #region IEventSerializer Members

        public byte[] Serialize(Event @event)
        {
            string jsonEvent = JsonConvert.SerializeObject(@event, _settings);
            return Encoding.ASCII.GetBytes(jsonEvent);
        }

        public Event Deserialize(byte[] @event)
        {
            string jsonString = Encoding.ASCII.GetString(@event);
            return JsonConvert.DeserializeObject(jsonString, _settings) as Event;
        }

        #endregion
    }
}
