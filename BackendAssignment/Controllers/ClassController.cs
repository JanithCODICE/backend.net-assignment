using BackendAssignment.Managers.ClassManager;
using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using BackendASsignment.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAssignment.Controllers
{
    [ApiController]
    public class ClassController : CoreController
    {
        private readonly IClassManager _classManager;

        public ClassController(IClassManager classManager)
        {
            _classManager = classManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            return Ok(await _classManager.GetAllClasses(page, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _classManager.GetClassById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ClassCreateRequest classDto)
        {
            return Ok(await _classManager.AddClass(classDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClassDto classDto)
        {
            return Ok(await _classManager.UpdateClass(classDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _classManager.DeleteClass(id));
        }

        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetStudents(int id, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            return Ok(await _classManager.GetStudentsInClass(id, page, pageSize));
        }
    }
}
