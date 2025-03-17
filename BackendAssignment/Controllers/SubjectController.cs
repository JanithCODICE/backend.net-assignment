using BackendAssignment.Managers.SubjectManager;
using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using BackendASsignment.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace BackendAssignment.Controllers
{
    [ApiController]
    public class SubjectController : CoreController
    {
        private readonly ISubjectManager _subjectManager;

        public SubjectController(ISubjectManager subjectManager)
        {
            _subjectManager = subjectManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            return Ok(await _subjectManager.GetAllSubjects(page, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _subjectManager.GetSubjectById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubjectCreateRequest subjectDto)
        {
            return Ok(await _subjectManager.AddSubject(subjectDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(SubjectDto subjectDto)
        {
            return Ok(await _subjectManager.UpdateSubject(subjectDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _subjectManager.DeleteSubject(id));
        }

        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetStudents(int id, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            return Ok(await _subjectManager.GetStudentsBySubject(id, page, pageSize));
        }
    }
}
