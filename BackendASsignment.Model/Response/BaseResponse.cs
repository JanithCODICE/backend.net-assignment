using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendASsignment.Model.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

    public class PaginatedResult<T>
    {
        public required T[] Entities { get; set; }
        public required Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public long Length { get; set; }
        public int PageSize { get; set; }
    }
}
