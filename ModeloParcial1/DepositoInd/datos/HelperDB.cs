using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositoInd.datos
{
    public class HelperDB
    {
        private SqlConnection conexion;
        private static HelperDB instancia = null;
        private HelperDB()
        {
            conexion = new SqlConnection(@"Data Source=localhost;Initial Catalog=db_ordenes;Integrated Security=True");
        }
        public static HelperDB ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new HelperDB();
            }
            return instancia;
        }

        public SqlConnection ObtenerConexion()
        {
            return conexion;
        }

        public DataTable ConsultarTabla(string nombreSP)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand(nombreSP, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }
    }
}
