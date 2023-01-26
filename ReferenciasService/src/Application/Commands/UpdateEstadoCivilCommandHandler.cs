using MediatR;
using OSPeConTI.ReferenciasService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace OSPeConTI.ReferenciasService.Application.Commands
{
    // Regular CommandHandler
    public class UpdateEstadoCivilCommandHandler : IRequestHandler<UpdateEstadoCivilCommand, bool>
    {
        private readonly IEstadoCivilRepository _estadoCivilRepository;

        public UpdateEstadoCivilCommandHandler(IEstadoCivilRepository estadoCivilRepository)
        {
            _estadoCivilRepository = estadoCivilRepository;
        }

        public async Task<bool> Handle(UpdateEstadoCivilCommand command, CancellationToken cancellationToken)
        {

            var estadoCivilToUpdate = await _estadoCivilRepository.GetByIdAsync(command.Id);

            if (estadoCivilToUpdate == null)
            {
                return false;
            }

            estadoCivilToUpdate.Update(command.Id, command.Descripcion, command.CodigoSSS);

            _estadoCivilRepository.Update(estadoCivilToUpdate);

            return await _estadoCivilRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}