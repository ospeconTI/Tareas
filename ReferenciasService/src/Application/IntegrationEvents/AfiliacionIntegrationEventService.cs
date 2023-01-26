using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using OSPeConTI.ReferenciasService.BuildingBlocks.EventBus.Abstractions;
using OSPeConTI.ReferenciasService.BuildingBlocks.EventBus.Events;
using OSPeConTI.ReferenciasService.BuildingBlocks.IntegrationEventLogEF;
using OSPeConTI.ReferenciasService.BuildingBlocks.IntegrationEventLogEF.Services;
using OSPeConTI.ReferenciasService.Infrastructure;

namespace OSPeConTI.ReferenciasService.Application.IntegrationEvents
{
    public class AfiliacionIntegrationEventService : IAfiliacionIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly ReferenciasContext _ReferenciasContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<AfiliacionIntegrationEventService> _logger;

        public AfiliacionIntegrationEventService(IEventBus eventBus,
            ReferenciasContext ReferenciasContext,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<AfiliacionIntegrationEventService> logger)
        {
            _ReferenciasContext = ReferenciasContext ?? throw new ArgumentNullException(nameof(ReferenciasContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_ReferenciasContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, Program.AppName);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt, Guid transactionId)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            //IDbContextTransaction transactionId = _Afiliaciones.Context.GetCurrentTransaction();

            await _eventLogService.SaveEventAsync(evt, transactionId);

        }


    }
}