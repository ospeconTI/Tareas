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
    public class AddTipoDocumentoCommandHandler : IRequestHandler<AddTipoDocumentoCommand, Guid>
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;
        private readonly IEventBus _eventBus;

        public AddTipoDocumentoCommandHandler(ITipoDocumentoRepository tipoDocumentoRepository, IEventBus eventBus)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(AddTipoDocumentoCommand command, CancellationToken cancellationToken)
        {

            TipoDocumento tipoDocumento = new TipoDocumento(command.Descripcion, command.CodigoSSS);

            _tipoDocumentoRepository.Add(tipoDocumento);

            await _tipoDocumentoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            /* AfiliadoCreadoIntegrationEvent evento = new AfiliadoCreadoIntegrationEvent(tipoDocumento.Id);
            _eventBus.Publish(evento); */
            return tipoDocumento.Id;
        }
    }
}