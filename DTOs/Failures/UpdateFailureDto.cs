using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace System_zgloszen_UR.DTOs.Failures
{
    public class UpdateFailureDto
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
        int ReportedById { get; set; }
    }
}