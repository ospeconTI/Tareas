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
    public class AddSectorCommandHandler : IRequestHandler<AddSectorCommand, Guid>
    {
        private readonly SectorRepository _SectorRepository;

        public AddSectorCommandHandler(SectorRepository SectorRepository)
        {
            _SectorRepository = SectorRepository;
        }

        public async Task<Guid> Handle(AddSectorCommand command, CancellationToken cancellationToken)
        {

            Sector Sector = new Sector(command.Descripcion, command.CodigoSSS);

            _SectorRepository.Add(Sector);

            await _SectorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Sector.Id;
        }
    }
}