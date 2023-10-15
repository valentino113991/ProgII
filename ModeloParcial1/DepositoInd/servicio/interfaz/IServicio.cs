using DepositoInd.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositoInd.servicio.interfaz
{
    public interface IServicio
    {
        bool Grabar(Orden o);
        List<Material> TraerMateriales();
    }
}
