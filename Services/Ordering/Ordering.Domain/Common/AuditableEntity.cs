namespace Ordering.Domain.Common;

public abstract class AuditableEntity
{
    public int Id { get; protected set; }
    public string? CreatedBy { get; set; }
    public DateTime? Created { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }
}