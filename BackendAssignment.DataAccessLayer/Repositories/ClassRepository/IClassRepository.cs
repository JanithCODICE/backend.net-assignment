using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.DataAccessLayer.Repositories.Class
{
    public interface IClassRepository
    {
        Task<PaginatedResultDto<ClassDto>> GetAllClasses(int page, int pageSize);
        Task<ClassDto?> GetById(int id);
        Task<ClassDto> Add(ClassCreateRequest classEntity);
        Task<ClassDto?> Update(ClassDto classEntity);
        Task<ClassDto?> Delete(int id);
        Task<PaginatedResultDto<StudentDto>> GetStudentsByClassId(int classId, int page, int pageSize);
    }
}
