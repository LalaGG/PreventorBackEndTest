using Microsoft.AspNetCore.Mvc;
using PreventorTest.Application.Students.Domain;
using PreventorTest.Application.Students.Interfaces;

namespace PreventorTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
            private readonly IStudentRepository _repository;

            public StudentsController(IStudentRepository repository)
            {
                this._repository = repository;
            }

            [HttpGet]
            public async Task<ActionResult> Get()
            {
                var result = await _repository.Get();
                return Ok(result);
            }

            [HttpGet("{studentId}")]
            public async Task<ActionResult> Get(int studentId)
            {
                var result = await _repository.GetById(studentId);
                if (result == null) return NotFound();
                else return Ok(result);
            }

            [HttpPost]
            public async Task<ActionResult> Post(StudentDTO student)
            {
                var result = await _repository.Add(student);
                return Ok(result);
            }

            [HttpPut("{studentId}")]
            public async Task<ActionResult> Put(StudentDTO student, int studentId)
            {
                var result = await _repository.Update(student, studentId);
                if (result == null) return NotFound();
                else return Ok();
            }

            [HttpDelete("{studentId}")]
            public async Task<ActionResult> Delete(int studentId)
            {
                var result = await _repository.DeleteById(studentId);
                if(!result) return NotFound();
                else return Ok();
            }
        }
}
