using DepositoInd.servicio.impl;
using DepositoInd.servicio.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositoInd.servicio
{
    public class FabricaServicioImpl : FabricaServicio
    {
        public override IServicio CrearServicio()
        {
            return new ServicioImpl();
        }
    }
}
