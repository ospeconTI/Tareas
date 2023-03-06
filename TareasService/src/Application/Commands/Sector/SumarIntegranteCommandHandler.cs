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
    public class SumarIntegranteCommandHandler : IRequestHandler<SumarIntegranteCommand, bool>
    {
        private readonly SectorRepository _sectorRepository;

        public SumarIntegranteCommandHandler(SectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<bool> Handle(SumarIntegranteCommand command, CancellationToken cancellationToken)
        {

            Sector Sector = await _sectorRepository.GetAsync(command.SectorId);

            if (Sector == null) throw new NotFoundException();

            Usuario usuario = new Usuario(command.Apellido, command.Nombre, command.Email, command.Identificador);

            Sector.SumarIntegrante(usuario);

            _sectorRepository.Update(Sector);

            await _sectorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}