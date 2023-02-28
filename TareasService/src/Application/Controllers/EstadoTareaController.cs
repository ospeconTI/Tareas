using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSPeConTI.Tareas.Application.Commands;
using OSPeConTI.Tareas.Application.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Application
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EstadoTareaController : ControllerBase
    {

        public EstadoTareaController()

        {

        }


        [Route("all")]
        [HttpGet]
        public IEnumerable<EstadoTarea> GetAll()
        {
            return EstadoTarea.List();
        }
    }
}