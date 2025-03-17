using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.DataAccessLayer.Repositories.SubjectRepository
{
    public interface ISubjectRepository
    {
        Task<PaginatedResultDto<SubjectDto>> GetAll(int page, int pageSize);
        Task<SubjectDto?> GetById(int id);
        Task<SubjectDto> Add(SubjectCreateRequest subject);
        Task<SubjectDto?> Update(SubjectDto subject);
        Task<SubjectDto?> Delete(int id);
        Task<PaginatedResultDto<StudentDto>> GetStudentsBySubjectId(int subjectId, int page, int pageSize);
    }
}
