using System;
using System.ComponentModel.DataAnnotations;
using system_zgloszen_ur.Entities;

namespace system_zgloszen_ur.Models;

public class CreateFailureDto
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = "";
    [Required]
    [MaxLength(1000)]
    public string FailureDescription { get; set; } = "";
    [Required]
    [MaxLength(255)]
    public string MachineName { get; set; } = "";
    [Required]
    [MaxLength(255)]
    public string Location { get; set; } = "";


}
