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
    public class UpdateNacionalidadCommandHandler : IRequestHandler<UpdateNacionalidadCommand, bool>
    {
        private readonly INacionalidadRepository _nacionalidadRepository;

        private readonly ReferenciasContext _ReferenciasContext;

        private readonly IAfiliacionIntegrationEventService _afiliacionIntegrationEventService;

        public UpdateNacionalidadCommandHandler(INacionalidadRepository nacionalidadRepository, ReferenciasContext ReferenciasContext, IAfiliacionIntegrationEventService afiliacionIntegrationEventService)
        {
            _nacionalidadRepository = nacionalidadRepository;

            _ReferenciasContext = ReferenciasContext;

            _afiliacionIntegrationEventService = afiliacionIntegrationEventService;

        }

        public async Task<bool> Handle(UpdateNacionalidadCommand command, CancellationToken cancellationToken)
        {

            var nacionalidadToUpdate = await _nacionalidadRepository.GetByIdAsync(command.Id);

            if (nacionalidadToUpdate == null) throw new NotFoundException();

            nacionalidadToUpdate.Update(command.Id, command.Descripcion, command.CodigoSSS);

            _nacionalidadRepository.Update(nacionalidadToUpdate);

            await _nacionalidadRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            //AfiliadoModificadoIntegrationEvent evento = new AfiliadoModificadoIntegrationEvent(command.Id);


            Guid transactionId = Guid.NewGuid();
           // await _afiliacionIntegrationEventService.AddAndSaveEventAsync(evento, transactionId);
            //await _afiliacionIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);


            return true;
        }
    }
}