using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.Model.Dtos
{
    public class ClassDto
    {
        public int? Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string TeacherInCharge { get; set; } = string.Empty;
    }
}
