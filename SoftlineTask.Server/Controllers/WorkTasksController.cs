using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftlineTask.Server.Data;
using SoftlineTask.Server.Models.Entities;
using SoftlineTask.Server.Models.ViewModels;
using SoftlineTask.Server.Services.Interfaces;

namespace SoftlineTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTasksController : ControllerBase
    {
        private readonly IWorkTaskService _service;

        public WorkTasksController(AppDbContext context, IWorkTaskService service)
        {
            _service = service;
        }

        // GET: api/WorkTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetWorkTasks()
        {
            var tasks = await _service.GetAllTasks();
            return tasks.ToList();
        }

        // GET: api/WorkTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTask>> GetWorkTask(int id)
        {
            try
            {
                return await _service.GetTask(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/WorkTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkTask(int id, WorkTask workTask)
        {
            if (id != workTask.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateTask(workTask);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/WorkTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkTask>> PostWorkTask(WorkTask workTask)
        {
            try
            {
                await _service.AddTask(workTask);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return CreatedAtAction("GetWorkTask", new { id = workTask.Id }, workTask);
        }

        // DELETE: api/WorkTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTask(int id)
        {
            try
            {
                await _service.DeleteTask(id);
            }
            catch(Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
