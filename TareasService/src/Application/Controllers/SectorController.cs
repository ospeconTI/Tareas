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
        private readonly ILogger<SectorController> _logger;

        private readonly ISectorQueries _SectorQueries;

        public SectorController(
            IMediator mediator,
            ILogger<SectorController> logger,
            ISectorQueries Sector)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _SectorQueries = Sector ?? throw new ArgumentNullException(nameof(Sector));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetSectorAsync(Guid id)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var Sector = await _SectorQueries.GetSectorAsync(id);

                return Ok(Sector);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("getByName/{descripcion}")]
        [HttpGet]
        public async Task<ActionResult> GetSectorByNameAsync(string descripcion)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var Sector = await _SectorQueries.GetSectorByNameAsync(descripcion);

                return Ok(Sector);
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
                var Sector = await _SectorQueries.GetAll();

                return Ok(Sector);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> updateSectorAsync([FromBody] UpdateSectorCommand command)
        {
            bool commandResult = false;

            commandResult = await _mediator.Send(command);

            return Ok();
        }

    }
}