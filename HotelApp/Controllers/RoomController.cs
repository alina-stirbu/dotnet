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
    using Model.Room;
    using Model.Hotel;
    using AutoMapper.QueryableExtensions;
    using Microsoft.Extensions.Caching.Memory;
    using System.Threading;

    [Route("api/hotel/{hotelId}/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RoomController> _logger;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public RoomController(ApplicationDbContext context, ILogger<RoomController> logger, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Post(int hotelId, CreateRoomResource model, CancellationToken token)
        {
            //map model to entity
            var hotel = await this._context.Hotels.FindAsync(new object[] { hotelId }, token);

            if (hotel == null)
            {
                return this.NotFound();
            }

            var entity = this._mapper.Map<Room>(model);
            entity.Hotel = hotel;

            if (token.IsCancellationRequested) return this.NoContent();

            this._context.Rooms.Add(entity);

            await this._context.SaveChangesAsync(token);

            this._memoryCache.Remove($"_HOTEL_ROOMS_{hotelId}");

            return this.CreatedAtAction("Get", new { hotelId, id = entity.Id }, this._mapper.Map<RoomResource>(entity));
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<RoomResource>>> GetRooms(int hotelId, CancellationToken token)
        {
            this._logger.LogInformation("RoomController-GetRooms hit");

            var key = $"_HOTEL_ROOMS_{hotelId}";
            if (_memoryCache.TryGetValue(key, out List<Room> list))
            {
                this._logger.LogInformation("RoomsController-GetRooms(hotelId) cache hit");
            }
            else
            {
                list = await this._context.Rooms.Include(h => h.Hotel).Where(h => h.Hotel.Id == hotelId).ToListAsync(token);

                this._logger.LogInformation("RoomsController-GetRooms(hotelId) DB hit");

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(40));

                this._memoryCache.Set(key, list, cacheEntryOptions);
            }

            var data = list.Select(r => this._mapper.Map<RoomResource>(r));
            return Ok(data);
        }
        [HttpGet("{id}")]
        //[ResponseCache(VaryByQueryKeys = new[] { "id" }, Duration = 30)]
        public async Task<ActionResult<RoomResource>> Get(int hotelId, int id, CancellationToken token)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative Room id exception");
            }

            var hotel = await this._context.Hotels
                    .Include(i => i.Rooms)
                    .FirstOrDefaultAsync(i => i.Id == hotelId, token);

            if (hotel == null) return this.NotFound();

            var entity = await this._context.Rooms.FindAsync(new object[] { id }, token);
            if (entity == null)
            {
                return this.NotFound();
            }

            this._logger.LogInformation("RoomsController-Get(id) hit");

            return this._mapper.Map<RoomResource>(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int hotelId, int id, UpdateRoomResource model, CancellationToken token)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative Room id exception");
            }

            var hotel = await this._context.Hotels
                    .Include(i => i.Rooms)
                    .FirstOrDefaultAsync(i => i.Id == hotelId, token);

            if (hotel == null) return this.NotFound();

            var entity = await this._context.Rooms.FindAsync(new object[] { id }, token);
            if (entity == null)
            {
                return this.NotFound();
            }

            entity.AdultsNumber = model.AdultsNumber;
            entity.ChildrenNumber = model.ChildrenNumber;
            entity.RoomName = model.RoomName;
            this._context.Rooms.Update(entity);
            await this._context.SaveChangesAsync(token);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> Delete(int hotelId, int id, CancellationToken token)
        {
            var hotel = await this._context.Hotels
                    .Include(i => i.Rooms)
                    .FirstOrDefaultAsync(i => i.Id == hotelId, token);

            if (hotel == null) return this.NotFound();

            var room = await this._context.Rooms.FindAsync(new object[] { id }, token);
            if (room == null)
            {
                return this.NotFound();
            }

            this._context.Rooms.Remove(room);
            await this._context.SaveChangesAsync(token);

            //inmemory cache invalidation
            this._memoryCache.Remove($"_HOTEL_ROOMS_{hotelId}");

            return room;
        }
    }
}