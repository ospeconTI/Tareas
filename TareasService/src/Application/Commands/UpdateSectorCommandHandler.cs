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
    public class UpdateSectorCommandHandler : IRequestHandler<UpdateSectorCommand, bool>
    {
        private readonly SectorRepository _SectorRepository;

        public UpdateSectorCommandHandler(SectorRepository SectorRepository)
        {
            _SectorRepository = SectorRepository;
        }

        public async Task<bool> Handle(UpdateSectorCommand command, CancellationToken cancellationToken)
        {

            Sector Sector = new Sector(command.Descripcion);

            _SectorRepository.Add(Sector);

            await _SectorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}