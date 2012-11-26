using CQRS.Messages.Events;

namespace CQRS.Infrastructure.Contract
{
    public interface IEventSerializer
    {
        byte[] Serialize(Event @event);
        Event Deserialize(byte[] @event);
    }
}
