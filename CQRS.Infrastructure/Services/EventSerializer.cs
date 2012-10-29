using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Parivda.EventStore
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
