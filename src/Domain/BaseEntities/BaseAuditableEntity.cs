using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BaseEntities;

public abstract class BaseAuditableEntity : BaseEntity
{
    [Column("created")]
    public DateTimeOffset Created { get; set; }
    [Column("created_by")]
    public string? CreatedBy { get; set; }
    [Column("last_modified")]
    public DateTimeOffset LastModified { get; set; }
    [Column("last_modified_by")]
    public string? LastModifiedBy { get; set; }
}
