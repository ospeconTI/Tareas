using MediatR;
using OSPeConTI.ReferenciasService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using OSPeConTI.ReferenciasService.BuildingBlocks.EventBus.Abstractions;
using OSPeConTI.ReferenciasService.Application.IntegrationEvents;

namespace OSPeConTI.ReferenciasService.Application.Commands
{
    // Regular CommandHandler
    public class AddPlanCommandHandler : IRequestHandler<AddPlanCommand, Guid>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IEventBus _eventBus;

        public AddPlanCommandHandler(IPlanRepository planRepository, IEventBus eventBus)
        {
            _planRepository = planRepository;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(AddPlanCommand command, CancellationToken cancellationToken)
        {

            Plan plan = new Plan(command.Descripcion, command.CodigoSSS);

            _planRepository.Add(plan);

            await _planRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
/*             AfiliadoCreadoIntegrationEvent evento = new AfiliadoCreadoIntegrationEvent(plan.Id);
            _eventBus.Publish(evento); */
            return plan.Id;
        }
    }
}