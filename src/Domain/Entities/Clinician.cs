using System.ComponentModel.DataAnnotations;
using Domain.BaseEntities;

namespace Domain.Entities;

public sealed class Clinician : BaseAuditableEntity
{
    [Key]
    public Guid ClinicianId { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
}
