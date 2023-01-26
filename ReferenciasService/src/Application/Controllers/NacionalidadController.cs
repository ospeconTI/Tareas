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
    public class NacionalidadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NacionalidadController> _logger;

        private readonly INacionalidadQueries _nacionalidadQueries;

        public NacionalidadController(
            IMediator mediator,
            ILogger<NacionalidadController> logger,
            INacionalidadQueries nacionalidad)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _nacionalidadQueries = nacionalidad ?? throw new ArgumentNullException(nameof(nacionalidad));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetNacionalidadAsync(Guid id)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var nacionalidad = await _nacionalidadQueries.GetNacionalidadAsync(id);

                return Ok(nacionalidad);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("getByName/{descripcion}")]
        [HttpGet]
        public async Task<ActionResult> GetNacionalidadByNameAsync(string descripcion)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var nacionalidad = await _nacionalidadQueries.GetNacionalidadByNameAsync(descripcion);

                return Ok(nacionalidad);
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
                var nacionalidad = await _nacionalidadQueries.GetAll();

                return Ok(nacionalidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

         [Route("add")]
        [HttpPost]
        public async Task<IActionResult> addNacionalidadAsync([FromBody] AddNacionalidadCommand command)
        {

            Guid UID = await _mediator.Send(command);

            return Ok(UID);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> updateNacionalidadAsync([FromBody] UpdateNacionalidadCommand command)
        {
            bool commandResult = false;

            commandResult = await _mediator.Send(command);

            return Ok();
        }

    }
}