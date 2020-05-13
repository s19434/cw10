using cw10.Models;
using cw10.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace cw10.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentDbService _dbService;

        public StudentsController(IStudentDbService _dbService)
        {
            this._dbService = _dbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            IActionResult response;
            try
            {
                response = Ok(_dbService.GetStudents());
            }
            catch (Exception exc)
            {
                response = BadRequest("ERROR with geting student" + exc.StackTrace);
            }

            return response;
        }

        [HttpGet("{index}")]
        public IActionResult GetStudent(string index)
        {
            IActionResult response;
            try
            {
                response = Ok(_dbService.GetStudent(index));
            }
            catch (Exception exc)
            {
                response = BadRequest("ERROR with geting student" + exc.StackTrace);
            }

            return response;

        }

        [HttpPost("enroll")]
        public IActionResult AddStudent(Student student)
        {
            IActionResult response;
            try
            {
                response = Ok(_dbService.AddStudent(student)); ;
            }
            catch (Exception exc)
            {
                response = BadRequest("ERROR with enrollment of student" + exc.StackTrace);
            }
            return response;
        }

        [HttpPut]
        public IActionResult ModifyStudent(Student student)
        {
            IActionResult response;
            try
            {
                response = Ok(_dbService.ModifyStudent(student));
            }
            catch (Exception exc)
            {
                response = BadRequest("ERROR with putting of student" + exc.StackTrace);
            }
            return response;

        }

        [HttpPost("promote")]
        public IActionResult PromoteStudents()
        {
            return Ok("Student is promoted");
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteStudent(string index)
        {
            if (_dbService.DeleteStudent(index) == null)
            {
                return NotFound("Student has already deleted or not found");
            }


            return NoContent();
        }



    }
}