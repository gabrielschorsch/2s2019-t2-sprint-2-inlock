﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Repositories;

namespace Senai.InLock.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosController : ControllerBase {

        EstudioRepository estudioRepository = new EstudioRepository();

        [HttpGet]
        public IEnumerable<Estudios> ListarEstudios () {
            return estudioRepository.ListarEstudios();
        }

    }
}