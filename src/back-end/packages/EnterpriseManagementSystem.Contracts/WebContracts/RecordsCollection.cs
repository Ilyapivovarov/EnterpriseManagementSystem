namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed class RecordsCollection<T> : List<T>
{
    public RecordsCollection()
        : base(ArraySegment<T>.Empty)
    { }

    public RecordsCollection(params T[]? values)
        : base(values ?? ArraySegment<T>.Empty)
    { }

    public RecordsCollection(IList<T>? list)
        : base(list ?? ArraySegment<T>.Empty)
    { }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (ReferenceEquals(this, obj)) return true;

        return obj is RecordsCollection<T> collection &&
               collection.SequenceEqual(this);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var item in this) hashCode.Add(item);

        return hashCode.ToHashCode();
    }
}