using System;

namespace OSPeConTI.Tareas.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class TareaDomainException : Exception
    {
        public TareaDomainException()
        { }

        public TareaDomainException(string message)
            : base(message)
        { }

        public TareaDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}