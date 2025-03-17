using BackendAssignment.DataAccessLayer.Repositories.SubjectRepository;
using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using BackendASsignment.Model.Response;
using static BackendAssignment.Core.Exceptions.UserDefinedException;

namespace BackendAssignment.Managers.SubjectManager
{
    public class SubjectManager : ISubjectManager
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectManager(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<BaseResponse<SubjectDto>> AddSubject(SubjectCreateRequest subjectDto)
        {
            if (subjectDto == null)
            {
                throw new UDValiationException("subjectDto is null");
            }
            SubjectDto subject = await _subjectRepository.Add(subjectDto);
            return new BaseResponse<SubjectDto>
            {
                Success = true,
                Message = "Subject added successfully",
                Data = subject
            };
        }

        public async Task<BaseResponse<SubjectDto>> DeleteSubject(int id)
        {
            var subjectEntity = await _subjectRepository.Delete(id);
            if (subjectEntity == null)
            {
                throw new UDNotFoundException("Subject not found");
            }
            return new BaseResponse<SubjectDto>
            {
                Success = true,
                Message = "Subject deleted successfully",
                Data = subjectEntity
            };
        }

        public async Task<BaseResponse<PaginatedResult<SubjectDto>>> GetAllSubjects(int page, int pageSize)
        {
            var subjects = await _subjectRepository.GetAll(page, pageSize);
            if (subjects == null || subjects.Items == null)
            {
                throw new UDNotFoundException("No subjects found");
            }
            return new BaseResponse<PaginatedResult<SubjectDto>>
            {
                Success = true,
                Message = "Subjects retrieved successfully",
                Data = new PaginatedResult<SubjectDto>
                {
                    Entities = subjects.Items.ToArray(),
                    Pagination = new Pagination
                    {
                        PageSize = subjects.PageSize,
                        Length = subjects.TotalCount
                    }
                }
            };
        }

        public async Task<BaseResponse<PaginatedResult<StudentDto>>> GetStudentsBySubject(int subjectId, int page, int pageSize)
        {
            var students = await _subjectRepository.GetStudentsBySubjectId(subjectId, page, pageSize);
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

        public async Task<BaseResponse<SubjectDto>> GetSubjectById(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            if (subject == null)
            {
                throw new UDNotFoundException("Subject not found");
            }
            return new BaseResponse<SubjectDto>
            {
                Success = true,
                Message = "Subject retrieved successfully",
                Data = subject
            };
        }

        public async Task<BaseResponse<SubjectDto>> UpdateSubject(SubjectDto subjectDto)
        {
            var updatedSubject = await _subjectRepository.Update(subjectDto);
            if (updatedSubject == null)
            {
                throw new UDNotFoundException("Subject not found");
            }
            return new BaseResponse<SubjectDto>
            {
                Success = true,
                Message = "Subject updated successfully",
                Data = updatedSubject
            };
        }
    }
}
