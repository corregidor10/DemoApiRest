using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApiRest.Extensiones
{
    public class LogHandler:DelegatingHandler
    {
        //TODO MESSAGEHANDLERS 001. Creamos un metodo para capturar las peticiones que llegan a la API.

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken tokencancelacion)
        {
            using (var d=File.AppendText(@"c:\log\logapi.txt"))
            {
                d.WriteLine("{0:d} Request-> {1}", DateTime.Now, request);
            }

            //TODO MESSAGEHANDLERS 002 en el return le ponemos en el base para que llame al padre.
            return base.SendAsync(request, tokencancelacion);
        }
    }
}

//TODO ODATA 001. IMmplementamos el Modelo de Datos