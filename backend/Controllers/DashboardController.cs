using Microsoft.AspNetCore.Mvc;
using backend.Repositories;

namespace backend.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardRepository _repository;

        public DashboardController(DashboardRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var data = await _repository.GetDashboardData();

            return Ok(data);
        }
    }
}