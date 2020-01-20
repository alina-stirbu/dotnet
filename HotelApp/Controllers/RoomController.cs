﻿using System;
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

    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RoomController> _logger;
        private readonly IMapper _mapper;

        public RoomController(ApplicationDbContext context, ILogger<RoomController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Post(CreateRoomResource model)
        {
            //map model to entity
            var entity = this._mapper.Map<Room>(model);
            this._context.Rooms.Add(entity);

            await this._context.SaveChangesAsync();

            return this.CreatedAtAction("Get", new { id = entity.Id }, this._mapper.Map<RoomResource>(entity));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            this._logger.LogInformation("RoomController-GetRooms hit");

            return await this._context.Rooms.ToListAsync();
        }
        [HttpGet("{id}")]
        //[ResponseCache(VaryByQueryKeys = new[] { "id" }, Duration = 30)]
        public async Task<ActionResult<RoomResource>> Get(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative Room id exception");
            }

            var entity = await this._context.Rooms.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            this._logger.LogInformation("RoomsController-Get(id) hit");

            return this._mapper.Map<RoomResource>(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateRoomResource model)
        {
            var entity = await this._context.Rooms.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            entity.AdultsNumber = model.AdultsNumber;
            entity.ChildrenNumber = model.ChildrenNumber;
            entity.RoomName = model.RoomName;
            this._context.Rooms.Update(entity);
            await this._context.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> Delete(int id)
        {
            var room = await this._context.Rooms.FindAsync(id);
            if (room == null)
            {
                return this.NotFound();
            }

            this._context.Rooms.Remove(room);
            await this._context.SaveChangesAsync();

            return room;
        }
    }
}