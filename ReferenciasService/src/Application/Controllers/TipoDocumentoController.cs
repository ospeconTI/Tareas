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
    public class TipoDocumentoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TipoDocumentoController> _logger;

        private readonly ITipoDocumentoQueries _tipoDocumentosQueries;

        public TipoDocumentoController(
            IMediator mediator,
            ILogger<TipoDocumentoController> logger,
            ITipoDocumentoQueries tipoDocumentos)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tipoDocumentosQueries = tipoDocumentos ?? throw new ArgumentNullException(nameof(tipoDocumentos));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetTipoDocumentoAsync(Guid id)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var tipoDocumento = await _tipoDocumentosQueries.GetTipoDocumentoAsync(id);

                return Ok(tipoDocumento);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("getByName/{descripcion}")]
        [HttpGet]
        public async Task<ActionResult> GetTipoDocumentoByNameAsync(string descripcion)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var tipoDocumento = await _tipoDocumentosQueries.GetTipoDocumentoByNameAsync(descripcion);

                return Ok(tipoDocumento);
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
                var tipoDocumentos = await _tipoDocumentosQueries.GetAll();

                return Ok(tipoDocumentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

         [Route("add")]
        [HttpPost]
        public async Task<IActionResult> addTipoDocumentoAsync([FromBody] AddTipoDocumentoCommand command)
        {

            Guid UID = await _mediator.Send(command);

            return Ok(UID);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> updateTipoDocumentoAsync([FromBody] UpdateTipoDocumentoCommand command)
        {
            bool commandResult = false;

            commandResult = await _mediator.Send(command);

            return Ok();
        }

        

    }
}