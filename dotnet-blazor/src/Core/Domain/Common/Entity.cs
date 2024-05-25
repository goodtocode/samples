namespace Coffee.Core.Domain.Common;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity()
    {
    }

    protected Entity(Guid key)
        : this()
    {
        Key = key;
    }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    public Guid Key { get; }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Entity other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetRealType() != other.GetRealType())
            return false;

        if (Key == Guid.Empty || other.Key == Guid.Empty)
            return false;

        return Key == other.Key;
    }

    public static bool operator ==(Entity? a, Entity b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetRealType().ToString() + Key).GetHashCode();
    }

    private Type GetRealType()
    {
        var type = GetType();

        if (type.ToString().Contains("Aacn.Events"))
            return type.BaseType;

        return type;
    }
}