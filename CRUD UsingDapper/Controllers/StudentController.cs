using CRUD_UsingDapper.Model;
using CRUD_UsingDapper.Service;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_UsingDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var response = await _studentService.GetAllStudents();
                if (response == null)
                    return NotFound();
                return Ok(response);
            }catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(StudentDto student)
        {
            try
            {
                if (student == null)
                    return BadRequest();
                var response = await _studentService.AddStudent(student);
                if (response == null)
                    return BadRequest();
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            try
            {
                if (student == null)
                    return BadRequest();
                var response = await _studentService.UpdateStudent(student);
                return Ok(response);
            }catch( Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                if (id == 0 || id==null)
                    return BadRequest();
               await _studentService.DeleteStudent(id);
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
