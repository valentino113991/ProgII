using DepositoInd.servicio.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositoInd.servicio
{
    public abstract class FabricaServicio
    {
        public abstract IServicio CrearServicio();
    }
}
