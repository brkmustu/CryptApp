using Core.Shared.Messaging;

namespace Crypt.Events;

public class EncryptEvent : BaseEvent
{
    public string Context { get; set; }
}
