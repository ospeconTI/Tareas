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
    public class UpdateParentescoCommandHandler : IRequestHandler<UpdateParentescoCommand, bool>
    {
        private readonly IParentescoRepository _parentescoRepository;

        private readonly ReferenciasContext _ReferenciasContext;

        private readonly IAfiliacionIntegrationEventService _afiliacionIntegrationEventService;

        public UpdateParentescoCommandHandler(IParentescoRepository parentescoRepository, ReferenciasContext ReferenciasContext, IAfiliacionIntegrationEventService afiliacionIntegrationEventService)
        {
            _parentescoRepository = parentescoRepository;

            _ReferenciasContext = ReferenciasContext;

            _afiliacionIntegrationEventService = afiliacionIntegrationEventService;

        }

        public async Task<bool> Handle(UpdateParentescoCommand command, CancellationToken cancellationToken)
        {

            var parentescoToUpdate = await _parentescoRepository.GetByIdAsync(command.Id);

            if (parentescoToUpdate == null) throw new NotFoundException();

            parentescoToUpdate.Update(command.Id, command.Descripcion, command.CodigoSSS);

            _parentescoRepository.Update(parentescoToUpdate);

            await _parentescoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            //AfiliadoModificadoIntegrationEvent evento = new AfiliadoModificadoIntegrationEvent(command.Id);


            Guid transactionId = Guid.NewGuid();
           // await _afiliacionIntegrationEventService.AddAndSaveEventAsync(evento, transactionId);
            //await _afiliacionIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);


            return true;
        }
    }
}