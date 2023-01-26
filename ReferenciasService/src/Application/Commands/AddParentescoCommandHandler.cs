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
    public class AddParentescoCommandHandler : IRequestHandler<AddParentescoCommand, Guid>
    {
        private readonly IParentescoRepository _parentescoRepository;
        private readonly IEventBus _eventBus;

        public AddParentescoCommandHandler(IParentescoRepository parentescoRepository, IEventBus eventBus)
        {
            _parentescoRepository = parentescoRepository;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(AddParentescoCommand command, CancellationToken cancellationToken)
        {

            Parentesco parentesco = new Parentesco(command.Descripcion, command.CodigoSSS);

            _parentescoRepository.Add(parentesco);

            await _parentescoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            /* AfiliadoCreadoIntegrationEvent evento = new AfiliadoCreadoIntegrationEvent(parentesco.Id);
            _eventBus.Publish(evento); */
            return parentesco.Id;
        }
    }
}