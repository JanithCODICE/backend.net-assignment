using BackendAssignment.Model.Dtos;
using BackendAssignment.Model.Request;
using BackendASsignment.Model.Response;

namespace BackendAssignment.Managers.StudentManager
{
    public interface IStudentManager
    {
        Task<BaseResponse<PaginatedResult<StudentDto>>> GetAllStudents(int page, int pageSize);
        Task<BaseResponse<StudentDto>> GetStudentById(int id);
        Task<BaseResponse<StudentDto>> AddStudent(StudentCreateRequest studentDto);
        Task<BaseResponse<StudentDto>> UpdateStudent(StudentDto studentDto);
        Task<BaseResponse<StudentDto>> DeleteStudent(int id);
        Task<BaseResponse<PaginatedResult<StudentDto>>> GetStudentsByClass(int classId, int page, int pageSize);
        Task<BaseResponse<PaginatedResult<StudentDto>>> GetStudentsBySubject(int subjectId, int page, int pageSize);
    }
}
