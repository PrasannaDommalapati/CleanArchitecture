using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;

namespace Domain.Entities;

[Table("applicant", Schema ="job")]
public sealed class Applicant : BaseAuditableEntity
{
    [MaxLength(100)]
    [Column("first_name")]
    public required string FirstName { get; set; }
    [MaxLength(100)]
    [Column("last_name")]
    public required string LastName { get; set; }
}
