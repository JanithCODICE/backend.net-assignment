using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using BackendASsignment.Model.Response;

namespace BackendAssignment.Managers.SubjectManager
{
    public interface ISubjectManager
    {
        Task<BaseResponse<PaginatedResult<SubjectDto>>> GetAllSubjects(int page, int pageSize);
        Task<BaseResponse<SubjectDto>> GetSubjectById(int id);
        Task<BaseResponse<SubjectDto>> AddSubject(SubjectCreateRequest subjectDto);
        Task<BaseResponse<SubjectDto>> UpdateSubject(SubjectDto subjectDto);
        Task<BaseResponse<SubjectDto>> DeleteSubject(int id);
        Task<BaseResponse<PaginatedResult<StudentDto>>> GetStudentsBySubject(int subjectId, int page, int pageSize);
    }
}
