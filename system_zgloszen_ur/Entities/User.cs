using System;

namespace system_zgloszen_ur.Entities;

public class User
{
 public int Id { get; set; }
 public string Name { get; set; } = "";
 public string Email { get; set; } = "";
 public string Password { get; set; } = "";
 public UserRole Role { get; set; } = UserRole.User;
 public int FailureId { get; set; }
 public List<Failure> Failures { get; set; } = null!;
}

public enum UserRole
{
    Admin,
    User
}
