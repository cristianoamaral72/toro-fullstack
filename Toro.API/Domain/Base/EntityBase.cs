using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Toro.Domain.Base;

public abstract class EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; protected set; }

    [Required]
    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

    public DateTime? ModifiedDate { get; private set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    public void MarkAsModified()
    {
        ModifiedDate = DateTime.UtcNow;
    }

    public override bool Equals(object obj)
    {
        if (obj is not EntityBase other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }

    public static bool operator ==(EntityBase a, EntityBase b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityBase a, EntityBase b)
    {
        return !(a == b);
    }
}