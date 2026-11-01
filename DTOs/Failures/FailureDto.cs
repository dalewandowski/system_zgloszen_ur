using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using system_zgloszen_ur.Entities;

namespace System_zgloszen_UR.DTOs.Failures
{
    public record FailureDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string FailureDescription { get; set; } = "";
        public string MachineName { get; set; } = "";
        public string Location { get; set; } = "";
        public FailureStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ReportedById { get; set; }
        public string ReportedBy { get; set; } = "";
    }
}