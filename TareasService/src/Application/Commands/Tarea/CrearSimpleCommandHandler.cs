using MediatR;
using OSPeConTI.Tareas.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using OSPeConTI.Tareas.Domain.Repositories;
using OSPeConTI.Tareas.Infrastructure.Repositories;

namespace OSPeConTI.Tareas.Application.Commands
{
    // Regular CommandHandler
    public class CrearSimpleCommandHandler : IRequestHandler<CrearSimpleCommand, Guid>
    {
        private readonly TareaRepository _tareaRepository;

        public CrearSimpleCommandHandler(TareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<Guid> Handle(CrearSimpleCommand command, CancellationToken cancellationToken)
        {

            Tarea tarea = Tarea.CrearSimple(command.ReferenciaId, command.Creador, command.Ejecutor, command.VigenteDesde, command.VenceEn, command.Alerta, command.Descripcion, command.Instrucciones);

            _tareaRepository.Add(tarea);

            await _tareaRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return tarea.Id;
        }
    }
}