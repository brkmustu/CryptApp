using Core.Shared.Messaging;

namespace Crypt.Events;

public class DecryptEvent : BaseEvent
{
    public string Context { get; set; }
}
