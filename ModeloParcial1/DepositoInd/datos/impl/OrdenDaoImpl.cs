using DepositoInd.datos.interfaz;
using DepositoInd.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositoInd.datos.impl
{
    public class OrdenDaoImpl : IOrdenDao
    {
        public bool CrearOrden(Orden o)
        {
            bool resultado = true;
            SqlTransaction t = null;
            SqlConnection conexion = HelperDB.ObtenerInstancia().ObtenerConexion();
            try
            {

                conexion.Open();
                t = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand("SP_INSERTAR_ORDEN", conexion, t);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@responsable", o.Responsable);
                SqlParameter param = new SqlParameter("@nro", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery();
                int nro = (int)param.Value;
                int detalleNro = 1;
                SqlCommand cmdDetalle;

                foreach (DetalleOrden dp in o.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@nro_orden", nro);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@codigo", dp.Material.Codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", dp.Cantidad);

                    cmdDetalle.ExecuteNonQuery();
                    detalleNro++;
                }
                t.Commit();
            }
            catch
            {
                if (t != null)
                {
                    t.Rollback();
                }
                resultado = false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return resultado;
        }

        public List<Material> ObtenerMateriales()
        {
            List<Material> materiales = new List<Material>();
            DataTable tabla = HelperDB.ObtenerInstancia().ConsultarTabla("SP_CONSULTAR_MATERIALES");
            foreach (DataRow fila in tabla.Rows)
            {
                int codigo = Convert.ToInt32(fila[0]);
                string nombre = fila[1].ToString();
                int stock = Convert.ToInt32(fila[2]);
                Material m = new Material(codigo, nombre, stock);
                materiales.Add(m);
            }
            return materiales;
        }
    }
}
