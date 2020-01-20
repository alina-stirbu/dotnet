using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controllers
{
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Model.Rate;

    [Route("api/room/{roomId}/rate")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RateController> _logger;
        private readonly IMapper _mapper;

        public RateController(ApplicationDbContext context, ILogger<RateController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Rate>> Post(int roomId, CreateRateResource model)
        {
            //map model to entity
            var room = await this._context.Rooms.FindAsync(roomId);

            if (room == null)
            {
                return this.NotFound();
            }

            var entity = this._mapper.Map<Rate>(model);
            entity.Room = room;
            this._context.Rates.Add(entity);

            await this._context.SaveChangesAsync();

            return this.CreatedAtAction("Get", new {roomId, id = entity.Id }, this._mapper.Map<RateResource>(entity));
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<RateResource>>> GetRates(int roomId)
        {
            this._logger.LogInformation("RatesController-GetRates hit");

            var list = await this._context.Rates
                .Include(r => r.Room)
                .Where(r => r.Room.Id == roomId)
                .ToListAsync();

            var data = list.Select(r => this._mapper.Map<RateResource>(r));
            return Ok(data);
        }
        [HttpGet("{id}")]
        //[ResponseCache(VaryByQueryKeys = new[] { "id" }, Duration = 30)]
        public async Task<ActionResult<RateResource>> Get(int roomId, int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative Rate id exception");
            }

            var entity = await this._context.Rates.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            this._logger.LogInformation("RatesController-Get(id) hit");

            return this._mapper.Map<RateResource>(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateRateResource model)
        {
            var entity = await this._context.Rates.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            entity.Amount = model.Amount;
            entity.Currency = model.Currency;
            entity.Day = model.Day;
            this._context.Rates.Update(entity);
            await this._context.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Rate>> Delete(int id)
        {
            var Rate = await this._context.Rates.FindAsync(id);
            if (Rate == null)
            {
                return this.NotFound();
            }

            this._context.Rates.Remove(Rate);
            await this._context.SaveChangesAsync();

            return Rate;
        }
    }
}