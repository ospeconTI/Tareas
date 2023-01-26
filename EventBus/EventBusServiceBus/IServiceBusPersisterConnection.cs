namespace OSPeConTI.ReferenciasService.BuildingBlocks.EventBusServiceBus;

public interface IServiceBusPersisterConnection : IDisposable
{
    ServiceBusClient TopicClient { get; }
    ServiceBusAdministrationClient AdministrationClient { get; }
}