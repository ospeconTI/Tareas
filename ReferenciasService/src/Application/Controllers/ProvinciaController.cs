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
    public class ProvinciaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProvinciaController> _logger;

        private readonly IProvinciaQueries _provinciaQueries;

        public ProvinciaController(
            IMediator mediator,
            ILogger<ProvinciaController> logger,
            IProvinciaQueries provincia)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _provinciaQueries = provincia ?? throw new ArgumentNullException(nameof(provincia));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetProvinciaAsync(Guid id)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var provincia = await _provinciaQueries.GetProvinciaAsync(id);

                return Ok(provincia);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("getByName/{descripcion}")]
        [HttpGet]
        public async Task<ActionResult> GetProvinciaByNameAsync(string descripcion)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var provincia = await _provinciaQueries.GetProvinciaByNameAsync(descripcion);

                return Ok(provincia);
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
                var provincia = await _provinciaQueries.GetAll();

                return Ok(provincia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        

    }
}