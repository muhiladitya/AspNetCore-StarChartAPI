using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;

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
        [HttpGet("{id:int}", Name="GetById")]
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
            var newobj = _context.CelestialObjects.Where(c => c.Name== name).ToList();
            if (!newobj.Any())
            {
                return NotFound();
            }

            foreach(var newobjs in newobj)
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


    }
}
