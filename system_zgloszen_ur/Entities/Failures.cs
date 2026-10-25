using System;
using System.ComponentModel.DataAnnotations;

namespace system_zgloszen_ur.Entities;

public class Failure
{
public int Id { get; set; }
public string Title { get; set; } = "";
public string Description { get; set; } = "";
public string  MachineName { get; set; } = "";
public string  Location { get; set; } = "";
public FailureStatus Status { get; set; } = FailureStatus.New;
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
public DateTime? ResolvedAt { get; set; }
public int UserId { get; set; }
public User User { get; set; } = null!;
}

public enum FailureStatus
{
    New,
    InProgress,
    Resolved
}