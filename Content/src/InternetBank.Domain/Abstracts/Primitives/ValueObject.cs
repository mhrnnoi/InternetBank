namespace InternetBank.Domain.Abstracts.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValue();

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValuesAreEqual(other);
    }

    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesAreEqual(other);
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValue().SequenceEqual(other.GetAtomicValue());
    }



    public override int GetHashCode()
    {
        return GetAtomicValue().GetHashCode();
    }
}