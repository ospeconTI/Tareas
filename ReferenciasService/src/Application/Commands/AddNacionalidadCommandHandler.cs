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
    public class AddNacionalidadCommandHandler : IRequestHandler<AddNacionalidadCommand, Guid>
    {
        private readonly INacionalidadRepository _nacionalidadRepository;
        private readonly IEventBus _eventBus;

        public AddNacionalidadCommandHandler(INacionalidadRepository nacionalidadRepository, IEventBus eventBus)
        {
            _nacionalidadRepository = nacionalidadRepository;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(AddNacionalidadCommand command, CancellationToken cancellationToken)
        {

            Nacionalidad nacionalidad = new Nacionalidad(command.Descripcion, command.CodigoSSS);

            _nacionalidadRepository.Add(nacionalidad);

            await _nacionalidadRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            /* AfiliadoCreadoIntegrationEvent evento = new AfiliadoCreadoIntegrationEvent(nacionalidad.Id);
            _eventBus.Publish(evento); */
            return nacionalidad.Id;
        }
    }
}