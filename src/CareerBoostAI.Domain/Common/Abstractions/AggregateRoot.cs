using CareerBoostAI.Domain.Abstractions;

namespace CareerBoostAI.Domain.Common.Abstractions;

public abstract class AggregateRoot<T> : Entity<T>
{
    public int Version { get; protected set; }
    public IEnumerable<IDomainEvent> Events => _events;
    
    private readonly List<IDomainEvent> _events = new(); 
    private bool _versionIncremented;
    
    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any() && !_versionIncremented)
        {
            Version++;
            _versionIncremented = true;
        }
            
        _events.Add(@event);
    }

    public void ClearEvents() => _events.Clear();    

    protected void IncrementVersion()
    {
        if (_versionIncremented)
        {
            return;
        }

        Version++;
        _versionIncremented = true;
    }
}