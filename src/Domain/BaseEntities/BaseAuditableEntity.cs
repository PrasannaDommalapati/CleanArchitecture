using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BaseEntities;

public abstract class BaseAuditableEntity : BaseEntity
{
    [Column("created")]
    public DateTimeOffset Created { get; set; }
    [Column("last_modified")]
    public DateTimeOffset LastModified { get; set; }
}
