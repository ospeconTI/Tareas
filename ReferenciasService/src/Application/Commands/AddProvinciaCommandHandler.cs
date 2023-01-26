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
    public class AddProvinciaCommandHandler : IRequestHandler<AddProvinciaCommand, Guid>
    {
        private readonly IProvinciaRepository _ProvinciaRepository;
        private readonly IEventBus _eventBus;

        public AddProvinciaCommandHandler(IProvinciaRepository ProvinciaRepository, IEventBus eventBus)
        {
            _ProvinciaRepository = ProvinciaRepository;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(AddProvinciaCommand command, CancellationToken cancellationToken)
        {

            Provincia Provincia = new Provincia(command.Descripcion, command.CodigoSSS);

            _ProvinciaRepository.Add(Provincia);

            await _ProvinciaRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            /* AfiliadoCreadoIntegrationEvent evento = new AfiliadoCreadoIntegrationEvent(Provincia.Id);
            _eventBus.Publish(evento); */
            return Provincia.Id;
        }
    }
}