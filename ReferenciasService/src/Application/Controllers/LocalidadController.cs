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
    public class LocalidadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LocalidadController> _logger;

        private readonly ILocalidadQueries _localidadQueries;

        public LocalidadController(
            IMediator mediator,
            ILogger<LocalidadController> logger,
            ILocalidadQueries localidad)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localidadQueries = localidad ?? throw new ArgumentNullException(nameof(localidad));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetLocalidadAsync(Guid id)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var localidad = await _localidadQueries.GetLocalidadAsync(id);

                return Ok(localidad);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("getByName/{descripcion}")]
        [HttpGet]
        public async Task<ActionResult> GetLocalidadByNameAsync(string descripcion)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var localidad = await _localidadQueries.GetLocalidadByNameAsync(descripcion);

                return Ok(localidad);
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
                var localidad = await _localidadQueries.GetAll();

                return Ok(localidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

         [Route("getByProvincia/{provinciaId}")]
        [HttpGet]
        public async Task<ActionResult> GetLocalidadByProvinciaAsync(Guid provinciaId)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var localidad = await _localidadQueries.GetLocalidadByProvincia(provinciaId);

                return Ok(localidad);
            }
            catch
            {
                return NotFound();
            }
        }

        

    }
}