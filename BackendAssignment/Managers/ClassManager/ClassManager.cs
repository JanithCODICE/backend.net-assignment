using BackendAssignment.DataAccessLayer.Repositories.Class;
using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using BackendASsignment.Model.Response;
using static BackendAssignment.Core.Exceptions.UserDefinedException;

namespace BackendAssignment.Managers.ClassManager
{
    public class ClassManager: IClassManager
    {
        private readonly IClassRepository _classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<BaseResponse<ClassDto>> AddClass(ClassCreateRequest classDto)
        {

            if (classDto == null)
            {
                throw new UDValiationException("classDto is null");
            }

            ClassDto classEntity = await _classRepository.Add(classDto);
            return new BaseResponse<ClassDto>
            {
                Success = true,
                Message = "Class added successfully",
                Data = classEntity
            };
        }

        public async Task<BaseResponse<ClassDto>> DeleteClass(int id)
        {
            var classEntity = await _classRepository.Delete(id);
            if(classEntity == null)
            {
                throw new UDNotFoundException("Class not found");
            }

            return new BaseResponse<ClassDto>
            {
                Success = true,
                Message = "Class deleted successfully",
                Data = classEntity
            };
        }

        public async Task<BaseResponse<PaginatedResult<ClassDto>>> GetAllClasses(int page, int pageSize)
        {
            var classes = await _classRepository.GetAllClasses(page, pageSize);
            if (classes == null || classes.Items == null)
            {
                throw new UDNotFoundException("No classes found");
            }
            return new BaseResponse<PaginatedResult<ClassDto>>
            {
                Success = true,
                Message = "Classes retrieved successfully",
                Data = new PaginatedResult<ClassDto>
                {
                    Entities = classes.Items.ToArray(),
                    Pagination = new Pagination
                    {
                        PageSize = classes.PageSize,
                        Length = classes.TotalCount
                    }
                }
            };
        }

        public async Task<BaseResponse<ClassDto>> GetClassById(int id)
        {
            var classEntity = await _classRepository.GetById(id);
            if (classEntity == null)
            {
                throw new UDNotFoundException("Class not found");
            }
            return new BaseResponse<ClassDto>
            {
                Success = true,
                Message = "Class retrieved successfully",
                Data = classEntity
            };
        }

        public async Task<BaseResponse<PaginatedResult<StudentDto>>> GetStudentsInClass(int classId, int page, int pageSize)
        {
            var students = await _classRepository.GetStudentsByClassId(classId, page, pageSize);
            if (students == null || students.Items == null)
            {
                throw new BadHttpRequestException("No students found");
            }
            return new BaseResponse<PaginatedResult<StudentDto>>
            {
                Success = true,
                Message = "Students retrieved successfully",
                Data = new PaginatedResult<StudentDto>
                {
                    Entities = students.Items.ToArray(),
                    Pagination = new Pagination
                    {
                        PageSize = students.PageSize,
                        Length = students.TotalCount
                    }
                }
            };
        }

        public async Task<BaseResponse<ClassDto>> UpdateClass(ClassDto classDto)
        {
            var classEntity = await _classRepository.Update(classDto);
            if(classEntity == null)
            {
                throw new UDNotFoundException("Class not found");
            }
            return new BaseResponse<ClassDto>
            {
                Success = true,
                Message = "Class updated successfully",
                Data = classEntity
            };
        }
    }
}
