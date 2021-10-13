using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{[ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var newobj = _context.CelestialObjects.Find(id);
            if (newobj == null)
            {
                return NotFound();
            }
            newobj.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();

            return Ok(newobj);

        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var newobj = _context.CelestialObjects.Where(c => c.Name == name).ToList();
            if (!newobj.Any())
            {
                return NotFound();
            }

            foreach (var newobjs in newobj)
            { newobjs.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == newobjs.Id).ToList(); }

            return Ok(newobj);

        }
        [HttpGet]
        public IActionResult GetAll() {
            var newobj = _context.CelestialObjects.ToList();
            if (!newobj.Any())
            {
                return NotFound();
            }

            foreach (var newobjs in newobj)
            { newobjs.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == newobjs.Id).ToList(); }

            return Ok(newobj);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject item)
        {
            _context.CelestialObjects.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = item.Id},item);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CelestialObject item)
        {
            var ex = _context.CelestialObjects.Find(id);
            if (ex == null)
            {
                return NotFound();
            }
            ex.Name = item.Name;
            ex.OrbitalPeriod = item.OrbitalPeriod;
            ex.OrbitedObjectId = item.OrbitedObjectId;
            _context.CelestialObjects.Update(ex);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id,string name) 
        {
            var ex = _context.CelestialObjects.Find(id);
            if (ex == null)
            {
                return NotFound();
            }
            ex.Name = name;
            _context.CelestialObjects.Update(ex);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ex = _context.CelestialObjects.Where(c=>c.Id==id || c.OrbitedObjectId==id).ToList();
            if (!ex.Any())
            {
                return NotFound();
            }
            _context.CelestialObjects.RemoveRange(ex);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
