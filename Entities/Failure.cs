using System;


namespace system_zgloszen_ur.Entities;

public class Failure
{
public int Id { get; set; }
public string Title { get; set; } = "";
public string FailureDescription { get; set; } = "";
public string  MachineName { get; set; } = "";
public string  Location { get; set; } = "";
public FailureStatus Status { get; set; } = FailureStatus.New;
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

public int ReportedById { get; set; }
public User ReportedBy { get; set; } = null!;
public List<Repair> Repairs { get; set; } = [];
}

public enum FailureStatus
{
    New,
    InProgress,
    Resolved
}