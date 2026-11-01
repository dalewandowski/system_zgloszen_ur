using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System_zgloszen_UR.DTOs.Failures
{
    public class UpdateFailureDto
    {
        public string Title { get; set; } = "";
        public string FailureDescription { get; set; } = "";
        public string MachineName { get; set; } = "";
        public string Location { get; set; } = "";
    }
}