using KPStudentsApp.Application.Common.Models;
using KPStudentsApp.Application.Interfaces;
using KPStudentsApp.Application.Models.RequestModels;
using KPStudentsApp.Application.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KPStudentsApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;

        public StudentController(
            ILogger<StudentController> logger,
            IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        /// <summary>
        /// This endpoint is used to Create a Student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<StudentResponseModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Response<StudentResponseModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateStudent(CreateStudentModel model)
        {
            _logger.LogInformation($"{nameof(CreateStudent)} Controller Method Initiated");

            var result = await _studentService.CreateStudent(model);

            if (result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.Created, result);
            }

            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }

        /// <summary>
        /// This endpoint is used to Update a Student's information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response<StudentResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<StudentResponseModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateStudent(UpdateStudentModel model)
        {
            _logger.LogInformation($"{nameof(UpdateStudent)} Controller Method Initiated");

            var result = await _studentService.UpdateStudent(model);

            if (result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.OK, result);
            }

            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }

        /// <summary>
        /// This endpoint is used to retrieve a Student's details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<StudentResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<StudentResponseModel>), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            _logger.LogInformation($"{nameof(GetStudent)} Controller Method Initiated");

            var result = await _studentService.GetStudent(id);

            if (result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.OK, result);
            }

            return StatusCode((int)HttpStatusCode.NotFound, result);
        }

        /// <summary>
        /// This endpoint is used to retrieve a List of students according to the search criteria
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(SearchResponse<StudentResponseModelSlim>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SearchStudents([FromQuery] SearchRequest<string> searchRequest)
        {
            _logger.LogInformation($"{nameof(SearchStudents)} Controller Method Initiated");

            var result = await _studentService.SearchStudents(searchRequest);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// This endpoint is used to Delete a Student by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            _logger.LogInformation($"{nameof(DeleteStudent)} Controller Method Initiated");

            var result = await _studentService.DeleteStudent(id);
            if (result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.OK, result);
            }

            return StatusCode((int)HttpStatusCode.NotFound, result);
        }
    }
}
