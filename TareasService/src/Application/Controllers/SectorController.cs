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

namespace OSPeConTI.Tareas.Application
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISectorQueries _SectorQueries;


        public SectorController(
            IMediator mediator,
            ISectorQueries Sector)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _SectorQueries = Sector ?? throw new ArgumentNullException(nameof(Sector));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<SectorDTO> GetSectorAsync(Guid id)
        {
            var retorno = await _SectorQueries.GetAsync(id);
            return retorno;
        }


        [Route("getByDescripcion/{descripcion}")]
        [HttpGet]
        public async Task<IEnumerable<SectorDTO>> GetSectorByDescripcionAsync(string descripcion)
        {
            var retorno = await _SectorQueries.GetByDescripcionAsync(descripcion);
            return retorno;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IEnumerable<SectorDTO>> GetAll()
        {
            var retorno = await _SectorQueries.GetAll();
            return retorno;
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> addSectorAsync([FromBody] AddSectorCommand command)
        {
            Guid UID = await _mediator.Send(command);
            return Ok(UID);
        }

        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> updateSectorAsync([FromBody] UpdateSectorCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }


        [Route("SumarIntegrante")]
        [HttpPut]
        public async Task<IActionResult> SumarIntegranteAsync([FromBody] SumarIntegranteCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Route("QuitarIntegrante")]
        [HttpPut]
        public async Task<IActionResult> QuitarIntegrante([FromBody] QuitarIntegranteCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}