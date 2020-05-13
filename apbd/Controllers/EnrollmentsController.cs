using System;
using cw10.DTOs.Responses;
using cw10.DTOs.Requests;
using cw10.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw10.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController: ControllerBase
    {
        private readonly IEnrollmentDbService _enrollmentService;

        public EnrollmentsController(IEnrollmentDbService enrollmentService)
        {
            this._enrollmentService = enrollmentService;
        }

        [HttpPost("promotions")]
        public IActionResult Promote(PromoteStudentRequest request)
        {
            if (!_enrollmentService.EnrollmentExists(request.Studies, request.Semester))
            {
                return NotFound("Student does not found");
            }

            return Ok(_enrollmentService.Promote(request));
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest enrollment)
        {
            if (!_enrollmentService.IsExist(enrollment.Studies))
            {
                return BadRequest("Your student does not exist or request is bad");
            }

            var posted = _enrollmentService.EnrollStudent(enrollment);

            if (posted == null)
            {
                return BadRequest();
            }
            return Created("http://localhost:51341/api/enrollments", posted);
        }


    }
}