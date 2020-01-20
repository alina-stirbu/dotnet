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
    using Microsoft.EntityFrameworkCore;
    using Model.Hotel;

    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(ApplicationDbContext context, ILogger<HotelController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> Post(CreateHotelResource model)
        {
            //map model to entity
            var entity = this._mapper.Map<Hotel>(model);
            this._context.Hotels.Add(entity);

            await this._context.SaveChangesAsync();

            return this.CreatedAtAction("Get", new { id = entity.Id }, this._mapper.Map<HotelResource>(entity));
        }
        // GET: api/hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            this._logger.LogInformation("HotelsController-GetHotels hit");

            return await this._context.Hotels.ToListAsync();
        }

        [HttpGet("{id}")]
        //[ResponseCache(VaryByQueryKeys = new[] { "id" }, Duration = 30)]
        public async Task<ActionResult<HotelResource>> Get(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative Hotel id exception");
            }

            var entity = await this._context.Hotels.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            this._logger.LogInformation("HotelsController-Get(id) hit");

            return this._mapper.Map<HotelResource>(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateHotelResource model)
        {
            var entity = await this._context.Hotels.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            entity.City = model.City;
            this._context.Hotels.Update(entity);
            await this._context.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> Delete(int id)
        {
            var hotel = await this._context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return this.NotFound();
            }

            var rooms = await this._context.Rooms.Include(x => x.Hotel).Where(x => x.Hotel.Id == id).ToListAsync();
            foreach (var room in rooms)
            {
                this._context.Rooms.Remove(room);
            }

            this._context.Hotels.Remove(hotel);
            await this._context.SaveChangesAsync();

            return hotel;
        }
    }
}
