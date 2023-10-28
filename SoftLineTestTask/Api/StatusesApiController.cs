using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftLineTestTask.Data;

namespace SoftLineTestTask.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
    
        public StatusesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // GET: api/Statuses
        [HttpGet]
        public JsonResult GetStatuses()
        {
            var tasks = _dbContext.Statuses.ToList();

            return new JsonResult(tasks);
        }
    }
}