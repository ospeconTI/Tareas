using System;

namespace OSPeConTI.Tareas.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class SectorDomainException : Exception
    {
        public SectorDomainException()
        { }

        public SectorDomainException(string message)
            : base(message)
        { }

        public SectorDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}