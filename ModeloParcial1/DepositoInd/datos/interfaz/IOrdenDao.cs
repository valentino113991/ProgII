using DepositoInd.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositoInd.datos.interfaz
{
    public interface IOrdenDao
    {
        List<Material> ObtenerMateriales();
        bool CrearOrden(Orden o);
    }
}
