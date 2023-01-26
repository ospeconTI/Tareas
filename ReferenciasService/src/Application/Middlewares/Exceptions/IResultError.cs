using Newtonsoft.Json;
using System;
namespace OSPeConTI.ReferenciasService.Application.Middlewares
{
    public interface IResultError
    {
        int StatusCode { get; set; }
        string Message { get; set; }


        string Detail { get; set; }
        string ToJson() => JsonConvert.SerializeObject(this);
        void Map(Exception ex);
    }
}