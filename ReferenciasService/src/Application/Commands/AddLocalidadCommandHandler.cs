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
    public class AddLocalidadCommandHandler : IRequestHandler<AddLocalidadCommand, Guid>
    {
        private readonly ILocalidadRepository _LocalidadRepository;
        private readonly IEventBus _eventBus;

        public AddLocalidadCommandHandler(ILocalidadRepository LocalidadRepository, IEventBus eventBus)
        {
            _LocalidadRepository = LocalidadRepository;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(AddLocalidadCommand command, CancellationToken cancellationToken)
        {

            Localidad Localidad = new Localidad(command.Descripcion, command.ProvinciaId, command.CodigoPostal,  command.CodigoSSS);

            _LocalidadRepository.Add(Localidad);

            await _LocalidadRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            /* LocalidadCreadoIntegrationEvent evento = new LocalidadCreadoIntegrationEvent(Localidad.Id);
            _eventBus.Publish(evento); */
            return Localidad.Id;
        }
    }
}