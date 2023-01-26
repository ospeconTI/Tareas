using MediatR;
using OSPeConTI.ReferenciasService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using OSPeConTI.ReferenciasService.Application.Exceptions;
using OSPeConTI.ReferenciasService.BuildingBlocks.EventBus.Events;
using OSPeConTI.ReferenciasService.BuildingBlocks.EventBus.Abstractions;
using OSPeConTI.ReferenciasService.Application.IntegrationEvents;
using OSPeConTI.ReferenciasService.BuildingBlocks.IntegrationEventLogEF.Services;
using System.Data.Common;
using OSPeConTI.ReferenciasService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace OSPeConTI.ReferenciasService.Application.Commands
{
    // Regular CommandHandler
    public class UpdateTipoDocumentoCommandHandler : IRequestHandler<UpdateTipoDocumentoCommand, bool>
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        private readonly ReferenciasContext _ReferenciasContext;

        private readonly IAfiliacionIntegrationEventService _afiliacionIntegrationEventService;

        public UpdateTipoDocumentoCommandHandler(ITipoDocumentoRepository tipoDocumentoRepository, ReferenciasContext ReferenciasContext, IAfiliacionIntegrationEventService afiliacionIntegrationEventService)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;

            _ReferenciasContext = ReferenciasContext;

            _afiliacionIntegrationEventService = afiliacionIntegrationEventService;

        }

        public async Task<bool> Handle(UpdateTipoDocumentoCommand command, CancellationToken cancellationToken)
        {

            var tipoDocumentoToUpdate = await _tipoDocumentoRepository.GetByIdAsync(command.Id);

            if (tipoDocumentoToUpdate == null) throw new NotFoundException();

            tipoDocumentoToUpdate.Update(command.Id, command.Descripcion, command.CodigoSSS);

            _tipoDocumentoRepository.Update(tipoDocumentoToUpdate);

            await _tipoDocumentoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            //AfiliadoModificadoIntegrationEvent evento = new AfiliadoModificadoIntegrationEvent(command.Id);


            Guid transactionId = Guid.NewGuid();
           // await _afiliacionIntegrationEventService.AddAndSaveEventAsync(evento, transactionId);
            //await _afiliacionIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);


            return true;
        }
    }
}