using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.Model.Dtos
{
    public class StudentDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public List<int>? SubjectIds { get; set; } = [];
    }
}
