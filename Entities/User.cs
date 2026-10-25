using System;

namespace system_zgloszen_ur.Entities;

public class User
{
 public int Id { get; set; }
 public string Name { get; set; } = "";
 public string Email { get; set; } = "";
 public string PasswordHash { get; set; } = "";
 public UserRole Role { get; set; } = UserRole.User;
 
 public List<Failure> Failures { get; set; } = [];
 public List<Repair> Repairs { get; set; } = [];
}


// user Roles enum
public enum UserRole
{
    Admin,
    User
}
