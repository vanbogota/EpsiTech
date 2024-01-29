using Microsoft.AspNetCore.Mvc;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyTasksController : ControllerBase
    {
        private readonly ILogger<MyTasksController> _logger;
        private readonly ITaskTrackerService<MyTask> _service;

        public MyTasksController(ILogger<MyTasksController> logger, ITaskTrackerService<MyTask> service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// GET запрос на получение задачи по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public IActionResult GetById([FromQuery] int id)
        {
            return Ok(_service.GetTaskById(id));            
        }

        /// <summary>
        /// GET запрос на получение всех задач.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllTasks());
        }

        /// <summary>
        /// POST запрос на создание новой задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] MyTask task)
        {
            try
            {
                _service.CreateTask(task);
                return Created();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e, "Invoked method - Create.");
                return BadRequest($"Null value of {typeof(MyTask)}");
            }            
        }

        /// <summary>
        /// PUT запрос на обновление задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult Update([FromBody] MyTask task)
        {
            try
            {
                _service.UpdateTask(task);
                return Created();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e, "Invoked method - Update.");
                return BadRequest($"Null value of {typeof(MyTask)}");
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, $"Invoked method - Update. Requested Task ID - {task.Id}");
                return NotFound($"Incorrect request with ID - {task.Id}");
            }
        }

        /// <summary>
        /// DELETE запрос на удаление задачи по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                _service.DeleteTask(id);
                return Ok();
            }            
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, $"Invoked method - Delete. Requested Task ID - {id}");
                return NotFound($"Incorrect request with ID - {id}.");
            }            
        }
    }
}
