using DepositoInd.datos.impl;
using DepositoInd.datos.interfaz;
using DepositoInd.dominio;
using DepositoInd.servicio.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositoInd.servicio.impl
{
    public class ServicioImpl : IServicio
    {
        IOrdenDao dao = null;
        public ServicioImpl()
        {
            dao = new OrdenDaoImpl();
        }
        public bool Grabar(Orden o)
        {
            return dao.CrearOrden(o);
        }

        public List<Material> TraerMateriales()
        {
            return dao.ObtenerMateriales();
        }
    }
}
