namespace Core.Shared.Messaging;

public abstract class BaseEvent
{
    public BaseEvent()
    {
        this.EventId = Guid.NewGuid().ToString();
        this.CreationDate = DateTime.Now;
    }
    public BaseEvent(string identifier)
    {
        this.EventId = Guid.NewGuid().ToString();
        this.CreationDate = DateTime.Now;
        this.Identifier = identifier;
    }
    public string EventId { get; private set; }
    public DateTime CreationDate { get; private set; }
    public string Identifier { get; set; }
}
