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
    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, bool>
    {
        private readonly IPlanRepository _planRepository;

        private readonly ReferenciasContext _ReferenciasContext;

        private readonly IAfiliacionIntegrationEventService _afiliacionIntegrationEventService;

        public UpdatePlanCommandHandler(IPlanRepository planRepository, ReferenciasContext ReferenciasContext, IAfiliacionIntegrationEventService afiliacionIntegrationEventService)
        {
            _planRepository = planRepository;

            _ReferenciasContext = ReferenciasContext;

            _afiliacionIntegrationEventService = afiliacionIntegrationEventService;

        }

        public async Task<bool> Handle(UpdatePlanCommand command, CancellationToken cancellationToken)
        {

            var planToUpdate = await _planRepository.GetByIdAsync(command.Id);

            if (planToUpdate == null) throw new NotFoundException();

            planToUpdate.Update(command.Id, command.Descripcion, command.CodigoSSS);

            _planRepository.Update(planToUpdate);

            await _planRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            //AfiliadoModificadoIntegrationEvent evento = new AfiliadoModificadoIntegrationEvent(command.Id);


            Guid transactionId = Guid.NewGuid();
           // await _afiliacionIntegrationEventService.AddAndSaveEventAsync(evento, transactionId);
            //await _afiliacionIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);


            return true;
        }
    }
}