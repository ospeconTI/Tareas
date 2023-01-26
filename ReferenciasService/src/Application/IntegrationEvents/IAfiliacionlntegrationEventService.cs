using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using OSPeConTI.ReferenciasService.BuildingBlocks.EventBus.Events;

namespace OSPeConTI.ReferenciasService.Application.IntegrationEvents
{

    public interface IAfiliacionIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt, Guid transacationId);
    }
}