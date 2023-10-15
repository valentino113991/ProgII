using CarpinteriaApp_1w3.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp_1w3.Datos
{
    internal class DBHelper
    {
        SqlConnection conexion;
        
        public DBHelper()
        {
            conexion = new SqlConnection(@"Data Source = localhost; Initial Catalog = facturacion; Integrated Security = True");
        }

        public int ProximaFactura()
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_PROXIMO_ID";
            SqlParameter param = new SqlParameter("@next", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            comando.Parameters.Add(param);
            comando.ExecuteNonQuery();
            conexion.Close();
            return (int)param.Value;
        }

        public DataTable Consultar(string cadenaSp)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = cadenaSp;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }

        public DataTable Consultar(string cadenaSp, List<Parametro> lstParametros)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = cadenaSp;
            comando.Parameters.Clear();
            foreach (Parametro p in lstParametros)
            {
                comando.Parameters.AddWithValue(p.Nombre, p.Valor);
            }
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }

        public bool ConfirmarFactura(Factura factura)
        {
            bool resultado = true;
            SqlTransaction t = null;
            try
            {
                conexion.Open();
                t = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand("SP_INSERTAR_MAESTRO", conexion, t);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@fecha", factura.Fecha);
                comando.Parameters.AddWithValue("@cliente", factura.Cliente);
                comando.Parameters.AddWithValue("@nro_forma_pago", factura.FormaPago.NroFormaPago);
                SqlParameter param = new SqlParameter("@nro_factura", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery();

                int nroFac = (int)param.Value;
                SqlCommand cmdDetalle = null;
                foreach (DetalleFactura dF in factura.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", conexion, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@cantidad", dF.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", nroFac);
                    cmdDetalle.Parameters.AddWithValue("@nro_articulo", dF.Articulo.NroArticulo);
                    cmdDetalle.ExecuteNonQuery();
                }
                t.Commit();
            }
            catch
            {
                if (t != null)
                {
                    t.Rollback();
                    resultado = false;
                }
            }
            finally 
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return resultado;
        }
    }
}
