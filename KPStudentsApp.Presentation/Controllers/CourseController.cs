using KPStudentsApp.Application.Common.Models;
using KPStudentsApp.Application.Interfaces;
using KPStudentsApp.Application.Models.RequestModels;
using KPStudentsApp.Application.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KPStudentsApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseService _courseService;

        public CourseController(
            ILogger<CourseController> logger,
            ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        /// <summary>
        /// This endpoint is used to Create a Course
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<CourseResponseModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Response<CourseResponseModel>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCourse(CreateCourseModel model)
        {
            _logger.LogInformation($"{nameof(CreateCourse)} Controller Method Initiated");

            var result = await _courseService.CreateCourse(model);

            _logger.LogInformation($"{nameof(CreateCourse)} Controller Method Completed");

            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// This endpoint is used to Update a Course
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response<CourseResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<CourseResponseModel>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCourse(UpdateCourseModel model)
        {
            _logger.LogInformation($"{nameof(UpdateCourse)} Controller Method Initiated");

            var result = await _courseService.UpdateCourse(model);

            _logger.LogInformation($"{nameof(UpdateCourse)} Controller Method Completed");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// This endpoint is used to retrieve a Course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<CourseResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<CourseResponseModel>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCourse([FromRoute] int id)
        {
            _logger.LogInformation($"{nameof(GetCourse)} Controller Method Initiated");

            var result = await _courseService.GetCourse(id);

            _logger.LogInformation($"{nameof(GetCourse)} Controller Method Completed");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// This endpoint is used to retrieve a List of courses according to the search criteria
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(SearchResponse<CourseResponseModel>), (int)HttpStatusCode.OK)]
        public IActionResult SearchCourses([FromQuery] SearchRequest<string> searchRequest)
        {
            _logger.LogInformation($"{nameof(SearchCourses)} Controller Method Initiated");

            var result = _courseService.SearchCourses(searchRequest);

            _logger.LogInformation($"{nameof(SearchCourses)} Controller Method Completed");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// This endpoint is used to Delete a Course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<CourseResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<CourseResponseModel>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            _logger.LogInformation($"{nameof(DeleteCourse)} Controller Method Initiated");

            var result = await _courseService.DeleteCourse(id);

            _logger.LogInformation($"{nameof(DeleteCourse)} Controller Method Completed");

            return StatusCode((int)HttpStatusCode.OK, result);
        }
    }
}
