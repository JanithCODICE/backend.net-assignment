using BackendAssignment.Model.Dtos;
using BackendAssignment.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.DataAccess.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        Task<PaginatedResultDto<StudentDto>> GetAll(int page, int pageSize);
        Task<StudentDto?> GetById(int id);
        Task<StudentDto> Add(StudentCreateRequest student);
        Task<StudentDto?> Update(StudentDto student);
        Task<StudentDto?> Delete(int id);
        Task<PaginatedResultDto<StudentDto>> GetByClassId(int classId, int page, int pageSize);
        Task<PaginatedResultDto<StudentDto>> GetBySubjectId(int subjectId, int page, int pageSize);
    }
}
