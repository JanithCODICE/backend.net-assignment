using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.Model.Request
{
    public class StudentCreateRequest
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
        public string? Address { get; set; } 
        public required int ClassId { get; set; }
        public List<int>? SubjectIds { get; set; }
    }
}
