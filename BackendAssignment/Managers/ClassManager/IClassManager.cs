using BackendAssignment.Model.Dtos;
using BackendASsignment.Model;
using BackendASsignment.Model.Request;
using BackendASsignment.Model.Response;

namespace BackendAssignment.Managers.ClassManager
{
    public interface IClassManager
    {
        Task<BaseResponse<PaginatedResult<ClassDto>>> GetAllClasses(int page, int pageSize);
        Task<BaseResponse<ClassDto>> GetClassById(int id);
        Task<BaseResponse<ClassDto>> AddClass(ClassCreateRequest classDto);
        Task<BaseResponse<ClassDto>> UpdateClass(ClassDto classDto);
        Task<BaseResponse<ClassDto>> DeleteClass(int id);
        Task<BaseResponse<PaginatedResult<StudentDto>>> GetStudentsInClass(int classId, int page, int pageSize);
    }
}
