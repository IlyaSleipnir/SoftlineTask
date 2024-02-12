using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using SoftlineTask.Server.Data;
using SoftlineTask.Server.Models.Entities;
using SoftlineTask.Server.Services.Interfaces;

namespace SoftlineTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IStatusService _service;

        public StatusController(IStatusService service)
        {
            _service = service;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            var statuses = await _service.GetStatuses();
            return statuses.ToList();
        }

        //// GET: api/Status/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Status>> GetStatus(int id)
        //{
        //    var status = await _context.Statuses.FindAsync(id);

        //    if (status == null)
        //    {
        //        return NotFound();
        //    }

        //    return status;
        //}

        // PUT: api/Status/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateStatus(status);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Status
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            await _service.AddStatus(status);

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            try
            {
                await _service.DeleteStatus(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        
    }
}
