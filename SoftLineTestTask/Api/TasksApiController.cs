using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoftLineTestTask.Data;
using SoftLineTask = SoftLineTestTask.Models.Task;

namespace SoftLineTestTask.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
    
        public TasksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        // GET: api/Tasks
        [HttpGet]
        public JsonResult GetTasks()
        {
            var tasks = _dbContext.Tasks.Include(t => t.Status).ToList();

            return new JsonResult(tasks);
        }
    
        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public JsonResult GetTask(int id)
        {
            var task = _dbContext.Tasks.Include(t => t.Status).FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                NotFound();
            }
            return new JsonResult(task);
        }
    
        // PUT: api/Tasks/5
        [HttpPut]
        public IActionResult PutTask([FromBody] SoftLineTask task)
        {
            // if (id != task.Id)
            // {
            //     return BadRequest();
            // }
    
            _dbContext.Entry(task).State = EntityState.Modified;
    
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // if (!TaskExists(id))
                // {
                //     return NotFound();
                // }
                // else
                // {
                //     throw;
                // }
            }
    
            return Ok();
        }
    
        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] SoftLineTask task)
        {
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
    
            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }
    
        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _dbContext.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
    
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
    
            return Ok();
        }
    
        private bool TaskExists(int id)
        {
            return _dbContext.Tasks.Any(e => e.Id == id);
        }
    }
}