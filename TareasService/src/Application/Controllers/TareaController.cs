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
    public class TareaController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ITareaQueries _TareaQueries;


        public TareaController(
            IMediator mediator,
            ITareaQueries tarea)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _TareaQueries = tarea ?? throw new ArgumentNullException(nameof(tarea));
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<TareaDTO> GetAsync(Guid id)
        {
            var retorno = await _TareaQueries.GetAsync(id);
            return retorno;
        }

        [Route("ByIdReferencia{idReferencia}")]
        [HttpGet]
        public async Task<IEnumerable<TareaDTO>> GetByIdReferenciaAsync(Guid idReferencia)
        {
            var retorno = await _TareaQueries.GetByIdReferenciaAsync(idReferencia);
            return retorno;
        }

        [Route("crearSimple")]
        [HttpPost]
        public async Task<IActionResult> crearSimple([FromBody] CrearSimpleCommand command)
        {
            Guid UID = await _mediator.Send(command);
            return Ok(UID);
        }
    }
}