using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class EntidadDuplicada : System.Exception
    {
        public string Mensaje { get; }
        public EntidadDuplicada(string mensaje) {
            Mensaje = mensaje;
        }
    }
}
