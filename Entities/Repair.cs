using System;

namespace system_zgloszen_ur.Entities;

public class Repair
{
    public int Id { get; set; }
    public string? RepairDescription { get; set; } 
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ResolvedAt { get; set; }

    public int FailureId { get; set; }
    public Failure Failure { get; set; } = null!;
    public int PerformedById { get; set; }
    public User PerformedBy { get; set; } = null!;

}
    