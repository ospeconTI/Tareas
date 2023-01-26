using MediatR;
using OSPeConTI.ReferenciasService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace OSPeConTI.ReferenciasService.Application.Commands
{
    // Regular CommandHandler
    public class AddEstadoCivilCommandHandler : IRequestHandler<AddEstadoCivilCommand, Guid>
    {
        private readonly IEstadoCivilRepository _estadoCivilRepository;

        public AddEstadoCivilCommandHandler(IEstadoCivilRepository estadoCivilRepository)
        {
            _estadoCivilRepository = estadoCivilRepository;
        }

        public async Task<Guid> Handle(AddEstadoCivilCommand command, CancellationToken cancellationToken)
        {

            EstadoCivil estadoCivil = new EstadoCivil(command.Descripcion, command.CodigoSSS);

            _estadoCivilRepository.Add(estadoCivil);

            await _estadoCivilRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return estadoCivil.Id;
        }
    }
}