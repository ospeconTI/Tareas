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
    public class PlanController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlanController> _logger;

        private readonly IPlanQueries _planQueries;

        public PlanController(
            IMediator mediator,
            ILogger<PlanController> logger,
            IPlanQueries plan)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _planQueries = plan ?? throw new ArgumentNullException(nameof(plan));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetPlanAsync(Guid id)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var plan = await _planQueries.GetPlanAsync(id);

                return Ok(plan);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("getByName/{descripcion}")]
        [HttpGet]
        public async Task<ActionResult> GetPlanByNameAsync(string descripcion)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var plan = await _planQueries.GetPlanByNameAsync(descripcion);

                return Ok(plan);
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
                var plan = await _planQueries.GetAll();

                return Ok(plan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> addPlanAsync([FromBody] AddPlanCommand command)
        {

            Guid UID = await _mediator.Send(command);

            return Ok(UID);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> updatePlanAsync([FromBody] UpdatePlanCommand command)
        {
            bool commandResult = false;

            commandResult = await _mediator.Send(command);

            return Ok();
        }

        

    }
}