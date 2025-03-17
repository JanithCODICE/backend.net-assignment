using BackendAssignment.Managers.StudentManager;
using BackendAssignment.Model.Dtos;
using BackendAssignment.Model.Request;
using BackendASsignment.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace BackendAssignment.Controllers
{
    [ApiController]
    public class StudentController: CoreController
    {
        private readonly IStudentManager _studentManager;

        public StudentController(IStudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            return Ok(await _studentManager.GetAllStudents(page, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _studentManager.GetStudentById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentCreateRequest studentRequest)
        {
            return Ok(await _studentManager.AddStudent(studentRequest));
        }

        [HttpPut]
        public async Task<IActionResult> Update(StudentDto studentDto)
        {
            return Ok(await _studentManager.UpdateStudent(studentDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _studentManager.DeleteStudent(id));
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetByClass(int classId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            return Ok(await _studentManager.GetStudentsByClass(classId, page, pageSize));
        }

        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetBySubject(int subjectId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            return Ok(await _studentManager.GetStudentsBySubject(subjectId, page, pageSize));
        }
    }
}
