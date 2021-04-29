using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Postgres_Core_Docker.DbContexts;
using Postgres_Core_Docker.Models;

namespace Postgres_Core_Docker.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {


        private readonly ILogger<ItemsController> _logger;
        private readonly ItemsDbContext _context;
        public ItemsController(ILogger<ItemsController> logger, ItemsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> AllItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> AddItem([FromBody] Item item)
        {
            if (item == null)
                return BadRequest();
            var entity = await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(entity.Entity);
        }
    }
}
