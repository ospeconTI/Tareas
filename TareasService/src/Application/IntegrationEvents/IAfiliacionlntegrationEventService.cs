using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using OSPeConTI.Tareas.BuildingBlocks.EventBus.Events;

namespace OSPeConTI.Tareas.Application.IntegrationEvents
{

    public interface IAfiliacionIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt, Guid transacationId);
    }
}