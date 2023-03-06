using MediatR;
using OSPeConTI.Tareas.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using OSPeConTI.Tareas.Domain.Repositories;
using OSPeConTI.Tareas.Infrastructure.Repositories;
using OSPeConTI.Tareas.Application.Exceptions;

namespace OSPeConTI.Tareas.Application.Commands
{
    // Regular CommandHandler
    public class QuitarIntegranteCommandHandler : IRequestHandler<QuitarIntegranteCommand, bool>
    {
        private readonly SectorRepository _sectorRepository;

        public QuitarIntegranteCommandHandler(SectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<bool> Handle(QuitarIntegranteCommand command, CancellationToken cancellationToken)
        {

            Sector sector = await _sectorRepository.GetAsync(command.SectorId);

            if (sector == null) throw new NotFoundException();

            Usuario usuario = sector.Usuarios.Find(u => u.Identificacion == command.Identificador);

            if (usuario == null) throw new NotFoundException();

            sector.QuitarIntegrante(usuario);

            _sectorRepository.Update(sector);

            await _sectorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}