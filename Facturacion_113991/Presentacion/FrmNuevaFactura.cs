using CarpinteriaApp_1w3.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpinteriaApp_1w3.Presentacion
{
    public partial class FrmNuevaFactura : Form
    {
        Factura factura;
        public FrmNuevaFactura()
        {
            InitializeComponent();
            factura = new Factura();
        }

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            lblFacturaId.Text += ProximaFactura();
            CargarArticulos();
            CargarFormasPago();
            txtCliente.Text = "Consumidor Final";
            txtCantidad.Text = "1";
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void CargarFormasPago()
        {
            string cadenaConexion = @"Data Source = localhost; Initial Catalog = facturacion; Integrated Security = True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_CONSULTAR_FORMAS_PAGO";
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            cboFormaPago.DataSource = tabla;
            cboFormaPago.ValueMember = "nro_forma_pago";
            cboFormaPago.DisplayMember = "nombre";
            conexion.Close();
        }

        private void CargarArticulos()
        {
            string cadenaConexion = @"Data Source = localhost; Initial Catalog = facturacion; Integrated Security = True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_CONSULTAR_ART";
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            cboArticulo.DataSource = tabla;
            cboArticulo.ValueMember = "nro_articulo";
            cboArticulo.DisplayMember = "nombre";
            conexion.Close();
        }



        private int ProximaFactura()
        {
            string cadenaConexion = @"Data Source = localhost; Initial Catalog = facturacion; Integrated Security = True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (cboArticulo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un artículo"
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }
            if (cboFormaPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una forma de pago"
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }
            if (txtCantidad.Text == "" || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Seleccione una cantidad"
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["ColArticulo"].Value.ToString().Equals(cboArticulo.Text))
                {
                    MessageBox.Show("Artículo ya seleccionado"
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                    return;
                }
            }

            DataRowView item = (DataRowView) cboArticulo.SelectedItem;
            int nro_articulo = Convert.ToInt32(item.Row.ItemArray[0]);
            string n_articulo = Convert.ToString(item.Row.ItemArray[1]);
            double precio = Convert.ToDouble(item.Row.ItemArray[2]);
            Articulo a = new Articulo(nro_articulo, n_articulo, precio);

            DataRowView itemFormaPago = (DataRowView)cboFormaPago.SelectedItem;
            int nro_forma_pago = Convert.ToInt32(itemFormaPago.Row.ItemArray[0]);
            string n_forma_pago = Convert.ToString(itemFormaPago.Row.ItemArray[1]);
            FormaPago fp = new FormaPago(n_forma_pago);

            int cantidad = Convert.ToInt32(txtCantidad.Text);

            DetalleFactura dF = new DetalleFactura(a, cantidad);

            factura.AgregarDetalle(dF);


            dgvDetalles.Rows.Add(new Object[] { nro_articulo, n_articulo,precio, cantidad, n_forma_pago, "Quitar" });

            calcularTotales();
        }

        private void calcularTotales()
        {
            double total = factura.CalcularTotal();
            txtTotal.Text = total.ToString();
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalles.CurrentCell.ColumnIndex == 5)
            {
                factura.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
                calcularTotales();
            }
        }
    } 
}
