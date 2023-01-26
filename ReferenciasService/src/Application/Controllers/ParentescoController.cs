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
    public class ParentescoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ParentescoController> _logger;

        private readonly IParentescoQueries _parentescoQueries;

        public ParentescoController(
            IMediator mediator,
            ILogger<ParentescoController> logger,
            IParentescoQueries parentesco)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _parentescoQueries = parentesco ?? throw new ArgumentNullException(nameof(parentesco));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetParentescoAsync(Guid id)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var parentesco = await _parentescoQueries.GetParentescoAsync(id);

                return Ok(parentesco);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("getByName/{descripcion}")]
        [HttpGet]
        public async Task<ActionResult> GetParentescoByNameAsync(string descripcion)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var parentesco = await _parentescoQueries.GetParentescoByNameAsync(descripcion);

                return Ok(parentesco);
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
                var parentesco = await _parentescoQueries.GetAll();

                return Ok(parentesco);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }
 [Route("add")]
        [HttpPost]
        public async Task<IActionResult> addParentescoAsync([FromBody] AddParentescoCommand command)
        {

            Guid UID = await _mediator.Send(command);

            return Ok(UID);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> updateParentescoAsync([FromBody] UpdateParentescoCommand command)
        {
            bool commandResult = false;

            commandResult = await _mediator.Send(command);

            return Ok();
        }
        

    }
}