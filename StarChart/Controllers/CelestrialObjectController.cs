﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;

namespace StarChart.Controllers
{[ApiController]
    [Route("")]
    public class CelestrialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestrialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        
    }
}
