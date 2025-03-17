using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendASsignment.Model.Request
{
    public class ClassCreateRequest
    {
        public required string ClassName { get; set; }
        public required string Grade { get; set; }
        public required string TeacherInCharge { get; set; } 
    }
}
