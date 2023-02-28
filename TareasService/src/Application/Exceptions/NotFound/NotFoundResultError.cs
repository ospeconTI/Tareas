using System;
using OSPeConTI.Tareas.Application.Attributes;
using OSPeConTI.Tareas.Application.Middlewares;

namespace OSPeConTI.Tareas.Application.Exceptions
{
    public class NotFoundResultError : IResultError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        [NotShowInProduction]
        public string Detail { get; set; }
        public string ID { get; set; }

        public void Map(Exception ex)
        {
            Message = ex.Message;
            StatusCode = 404;
            Detail = ex.StackTrace;
            ID = ((INotFoundException)ex).ID;
        }

    }


}