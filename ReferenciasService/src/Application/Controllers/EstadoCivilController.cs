using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSPeConTI.ReferenciasService.Application.Commands;
using OSPeConTI.ReferenciasService.Application.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OSPeConTI.ReferenciasService.Application
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EstadoCivilController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EstadoCivilController> _logger;

        private readonly IEstadoCivilQueries _estadoCivilQueries;

        public EstadoCivilController(
            IMediator mediator,
            ILogger<EstadoCivilController> logger,
            IEstadoCivilQueries estadoCivil)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _estadoCivilQueries = estadoCivil ?? throw new ArgumentNullException(nameof(estadoCivil));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetEstadoCivilAsync(Guid id)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var estadoCivil = await _estadoCivilQueries.GetEstadoCivilAsync(id);

                return Ok(estadoCivil);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("getByName/{descripcion}")]
        [HttpGet]
        public async Task<ActionResult> GetEstadoCivilByNameAsync(string descripcion)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var estadoCivil = await _estadoCivilQueries.GetEstadoCivilByNameAsync(descripcion);

                return Ok(estadoCivil);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var estadoCivil = await _estadoCivilQueries.GetAll();

                return Ok(estadoCivil);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> addEstadoCivilAsync([FromBody] AddEstadoCivilCommand command)
        {

            Guid UID = await _mediator.Send(command);

            return Ok(UID);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> updateEstadoCivilAsync([FromBody] UpdateEstadoCivilCommand command)
        {
            bool commandResult = false;

            commandResult = await _mediator.Send(command);

            return Ok();
        }

    }
}