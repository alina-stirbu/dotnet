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
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using Model.Rate;
    using Newtonsoft.Json;
    using System.Threading;

    [Route("api/room/{roomId}/rate")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RateController> _logger;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public RateController(ApplicationDbContext context, ILogger<RateController> logger, IMapper mapper, IDistributedCache dcache)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _cache = dcache;
        }

        [HttpPost]
        public async Task<ActionResult<Rate>> Post(int roomId, CreateRateResource model, CancellationToken token)
        {
            //map model to entity
            var room = await this._context.Rooms.FindAsync(new object[] { roomId }, token);

            if (room == null)
            {
                return this.NotFound();
            }

            var entity = this._mapper.Map<Rate>(model);
            entity.Room = room;

            if (token.IsCancellationRequested) return this.NoContent();

            this._context.Rates.Add(entity);

            await this._context.SaveChangesAsync(token);
            await this._cache.RemoveAsync($"_RATES_ROOM_{roomId}", token);

            return this.CreatedAtAction("Get", new {roomId, id = entity.Id }, this._mapper.Map<RateResource>(entity));
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<RateResource>>> GetRates(int roomId, CancellationToken token)
        {
            this._logger.LogInformation("RatesController-GetRates hit");

            var key = $"_RATES_ROOM_{roomId}";

            var rates = this._cache.GetString(key);

            if (!string.IsNullOrEmpty(rates))
            {
                this._logger.LogInformation("DistributedCachedRateController-Get(roomId) cache hit");

                var ratesList = Deserialize<List<RateResource>>(rates);

                return ratesList;
            }
            else
            {
                this._logger.LogInformation("DistributedCachedRateController-Get(roomId) db hit");

                var list = await this._context.Rates
                .Include(r => r.Room)
                .Where(r => r.Room.Id == roomId)
                .ToListAsync(token);

                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(TimeSpan.FromSeconds(3));

                var data = list.Select(r => this._mapper.Map<RateResource>(r));
                this._cache.SetString(key, Serialize(data), options);
                return Ok(data);
            }
        }
        [HttpGet("{id}")]
        //[ResponseCache(VaryByQueryKeys = new[] { "id" }, Duration = 30)]
        public async Task<ActionResult<RateResource>> Get(int roomId, int id, CancellationToken token)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative Rate id exception");
            }

            var room = await this._context.Rooms.FindAsync(new object[] { roomId }, token);

            if (room == null)
            {
                return this.NotFound();
            }

            var entity = await this._context.Rates.FindAsync(new object[] { id }, token);
            if (entity == null)
            {
                return this.NotFound();
            }

            this._logger.LogInformation("RatesController-Get(id) hit");

            return this._mapper.Map<RateResource>(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int roomId, int id, UpdateRateResource model, CancellationToken token)
        {
            var room = await this._context.Rooms.FindAsync(new object[] { roomId }, token);

            if (room == null)
            {
                return this.NotFound();
            }
            var entity = await this._context.Rates.FindAsync(new object[] { id }, token);
            if (entity == null)
            {
                return this.NotFound();
            }

            entity.Amount = model.Amount;
            entity.Currency = model.Currency;
            entity.Day = model.Day;
            
            if (token.IsCancellationRequested) return this.NoContent();

            this._context.Rates.Update(entity);
            await this._context.SaveChangesAsync(token);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Rate>> Delete(int roomId, int id, CancellationToken token)
        {
            var room = await this._context.Rooms.FindAsync(new object[] { roomId }, token);
            if (room == null)
            {
                return this.NotFound();
            }
            var Rate = await this._context.Rates.FindAsync(new object[] { id }, token);
            if (Rate == null)
            {
                return this.NotFound();
            }
            if (token.IsCancellationRequested) return this.NoContent();

            this._context.Rates.Remove(Rate);
            await this._context.SaveChangesAsync(token);
            await this._cache.RemoveAsync($"_RATES_ROOM_{roomId}", token);
            return Rate;
        }

        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}