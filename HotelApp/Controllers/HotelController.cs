using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelApp.API.Controllers
{
    using System.Threading;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Model.Hotel;

    [ApiController]
    [Route("api/hotel")]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public HotelController(ApplicationDbContext context, ILogger<HotelController> logger, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> Post(CreateHotelResource model, CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                //map model to entity
                var entity = this._mapper.Map<Hotel>(model);
                this._context.Hotels.Add(entity);

                await this._context.SaveChangesAsync();

                var cts = new CancellationTokenSource();
                _memoryCache.Set($"_HOTEL_{entity.Id}", cts);

                this.CreatedAtAction("Get", new { id = entity.Id }, this._mapper.Map<HotelResource>(entity));
            }

            return Ok();
        }
        // GET: api/hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels(CancellationToken token)
        {
            this._logger.LogInformation("HotelsController-GetHotels hit");

            return await this._context.Hotels.ToListAsync(token);
        }

        [HttpGet("{id}")]
        [ResponseCache(VaryByQueryKeys = new[] { "id" }, Duration = 300)]
        public async Task<ActionResult<HotelResource>> Get(int id, CancellationToken token)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative Hotel id exception");
            }
            var entity = await this._context.Hotels.FindAsync(new object[] { id }, token);
            if (entity == null)
            {
                return this.NotFound();
            }

            this._logger.LogInformation("HotelsController-Get(id) hit");

            return this._mapper.Map<HotelResource>(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateHotelResource model, CancellationToken token)
        {
            var entity = await this._context.Hotels.FindAsync(new object[] { id }, token);
            if (entity == null)
            {
                return this.NotFound();
            }

            entity.City = model.City;
            this._context.Hotels.Update(entity);
            await this._context.SaveChangesAsync(token);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> Delete(int id, CancellationToken token)
        {
            try {
                var hotel = await this._context.Hotels.FindAsync(new object[] { id }, token);
                if (hotel == null)
                {
                    return this.NotFound();
                }

                var rooms = await this._context.Rooms.Include(x => x.Hotel).Where(x => x.Hotel.Id == id).ToListAsync(token);
                foreach (var room in rooms)
                {
                    this._context.Rooms.Remove(room);
                }

                this._context.Hotels.Remove(hotel);
                await this._context.SaveChangesAsync(token);

                var cts = this._memoryCache.Get<CancellationTokenSource>($"_HOTEL_{id}");
                cts?.Cancel();

                return hotel;
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Task canceled!");

            }
            return NoContent();

        }
    }
}
