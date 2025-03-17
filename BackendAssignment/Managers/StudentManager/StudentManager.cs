using BackendAssignment.DataAccess.Repositories.StudentRepository;
using BackendAssignment.Model.Dtos;
using BackendAssignment.Model.Request;
using BackendASsignment.Model.Response;
using static BackendAssignment.Core.Exceptions.UserDefinedException;

namespace BackendAssignment.Managers.StudentManager
{
    public class StudentManager : IStudentManager
    {
        private readonly IStudentRepository _studentRepository;

        public StudentManager(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<BaseResponse<StudentDto>> AddStudent(StudentCreateRequest studentDto)
        {
            if (studentDto == null)
            {
                throw new UDValiationException("StudentDto is null");
            }
            StudentDto student = await _studentRepository.Add(studentDto);
            return new BaseResponse<StudentDto>
            {
                Success = true,
                Message = "Student added successfully",
                Data = student
            };
        }

        public async Task<BaseResponse<StudentDto>> DeleteStudent(int id)
        {
           var student = await _studentRepository.Delete(id);
            if (student == null)
            {
                throw new UDNotFoundException("Student not found");
            }
            return new BaseResponse<StudentDto>
            {
                Success = true,
                Message = "Student deleted successfully",
                Data = student
            };
        }

        public async Task<BaseResponse<PaginatedResult<StudentDto>>> GetAllStudents(int page, int pageSize)
        {
            var students = await _studentRepository.GetAll(page, pageSize);
            if (students == null || students.Items == null)
            {
                throw new UDNotFoundException("No students found");
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

        public async Task<BaseResponse<StudentDto>> GetStudentById(int id)
        {
            var student = await _studentRepository.GetById(id);
            if (student == null)
            {
                throw new UDNotFoundException("Student not found");
            }
            return new BaseResponse<StudentDto>
            {
                Success = true,
                Message = "Student retrieved successfully",
                Data = student
            };
        }

        public async Task<BaseResponse<PaginatedResult<StudentDto>>> GetStudentsByClass(int classId, int page, int pageSize)
        {
            var students = await _studentRepository.GetByClassId(classId, page, pageSize);
            if (students == null || students.Items == null)
            {
                throw new UDNotFoundException("No students found for the given class.");
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

        public async Task<BaseResponse<PaginatedResult<StudentDto>>> GetStudentsBySubject(int subjectId, int page, int pageSize)
        {
            var students = await _studentRepository.GetBySubjectId(subjectId, page, pageSize);
            if (students == null || students.Items == null)
            {
                throw new UDNotFoundException("No students found for this subject.");
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

        public async Task<BaseResponse<StudentDto>> UpdateStudent(StudentDto studentDto)
        {
            var student = await _studentRepository.Update(studentDto);
            if (student == null)
            {
                throw new UDNotFoundException("Student not found");
            }
            return new BaseResponse<StudentDto>
            {
                Success = true,
                Message = "Student updated successfully",
                Data = student
            };
        }
    }
}
