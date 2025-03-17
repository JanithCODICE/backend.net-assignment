using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendASsignment.Model.Request
{
    public class SubjectCreateRequest
    {
        public required string Name { get; set; }

        public required string Code { get; set; }

        public required string Teacher { get; set; }
    }
}
