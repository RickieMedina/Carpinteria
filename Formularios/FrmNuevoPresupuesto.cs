using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Carpinteria.Formularios;
using Carpinteria.Servicios;
using Carpinteria.AccesoDatos;

namespace Carpinteria.Formularios
{
    public enum Accion
    {
        CREATE,
        READ,
        UPDATE,
        DELETE
    }
    public partial class FrmNuevoPresupuesto : Form
    {
        Presupuesto oPresupuesto;
        public Accion modo;
        private GestorPresupuesto gestor;


        public FrmNuevoPresupuesto(Accion modo, int nro)
        {
            InitializeComponent();
            oPresupuesto= new Presupuesto();
            gestor = new GestorPresupuesto(new DaoFactory());
            this.modo = modo;

            

            if (modo.Equals(Accion.READ))  // Esto agregar a un GrOupbox
            {
                txtFecha.Enabled = false;
                txtCliente.Enabled = false;
                txtDescuento.Enabled = false;
                cboProductos.Enabled = false;
                txtCantidad.Enabled = false;
                btnAceptar.Enabled = false;
                btnAgregar.Enabled = false;
                dgvDetalles.Enabled = false;
                txtSubTotal.Enabled = false;
                txtTotal.Enabled = false;
                this.Text = "Ver Presupuesto";
                this.Cargar_presupuesto(nro);
               
                
                
            }
           

            if(modo.Equals(Accion.UPDATE))
            {
                
                txtFecha.Enabled = false;
                this.Text = "Editar Presupuesto";
                this.Cargar_presupuesto(nro);              
                  
            }

        }

        private void Cargar_presupuesto(int nro)
        {
            this.oPresupuesto = gestor.ObtenerPresupuestoPorID(nro);
            txtCliente.Text = oPresupuesto.Cliente;
            txtFecha.Text = oPresupuesto.Fecha.ToString("dd/MM/yyyy");
            txtDescuento.Text = oPresupuesto.Descuento.ToString();
            LblNroPresupuesto.Text = "Presupuesto Nro: " + oPresupuesto.PresupuestoNro.ToString();

            dgvDetalles.Rows.Clear();
            foreach (DetallePresupuesto oDetalle in oPresupuesto.Detalles)
            {
                dgvDetalles.Rows.Add(new object[] { "", oDetalle.Producto.Nombre, oDetalle.Producto.Precio, oDetalle.Cantidad });//, oDetalle.CalcularSubtotal() }); 
            }
            CalcularTotales();
        }




        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
          
            CargarProductos();
            if (modo.Equals(Accion.CREATE))
            {
                txtFecha.Enabled = false;
                LblNroPresupuesto.Text += gestor.ProximoPresupuesto();
                txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtCliente.Text = "Consumidor final";
                txtDescuento.Text = "0";
                txtCantidad.Text = "1";
            }

        }


        private void CargarProductos()
        {
            DataTable tabla = gestor.ObtenerProductos();

            cboProductos.DataSource = tabla;
            cboProductos.ValueMember = tabla.Columns[0].ColumnName; //num de mi columna en el sql from productos
            cboProductos.DisplayMember = tabla.Columns[1].ColumnName;
        }

        private void lblDescuento_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        { //DETALLEPresupuesto con producto y cantidad
            //validaciones
            if(string.IsNullOrEmpty(txtDescuento.Text) || !int.TryParse(txtDescuento.Text, out _))
            {
                MessageBox.Show("Debe ingresar un número de descuento válido...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cboProductos.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe seleccionar un Producto...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad válida...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["ColProd"].Value.ToString().Equals(cboProductos.Text))
                {
                    MessageBox.Show("Este producto ya está presupuestado...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
          

            DataRowView item = (DataRowView)cboProductos.SelectedItem; // como mi combo tiene el select * from prod me trae todas sus columnas
            int nro = Convert.ToInt32(item.Row.ItemArray[0]);
            string nomb = Convert.ToString(item.Row.ItemArray[1]);
            double pre = Convert.ToDouble(item.Row.ItemArray[2]);

            Producto p = new Producto(nro, nomb, pre);  // tengo mi PRODUCTO
            int cant =Convert.ToInt32(txtCantidad.Text);

            DetallePresupuesto detalle = new DetallePresupuesto(p, cant); //tengo el detalle y lo agrego a presupu.

            oPresupuesto.AgregarDetalle(detalle);
            dgvDetalles.Rows.Add(new object[] {nro, nomb, pre, cant});//Agrega el detalle como un objeto

            CalcularTotales();

        }

        private void CalcularTotales()
        {
            txtSubTotal.Text = oPresupuesto.CalcularTotal().ToString();//El presupuesto  calcula su total
            double desc = oPresupuesto.CalcularTotal() * Convert.ToDouble(txtDescuento.Text) / 100;
            txtTotal.Text = (oPresupuesto.CalcularTotal() - desc).ToString();
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex==4)
            {
                oPresupuesto.QuitarDetalle(dgvDetalles.CurrentRow.Index);//el presupuesto quita sus detalles
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                CalcularTotales();

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCliente.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un cliente...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCliente.Focus();
                return;
            }
            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar un detalle al menos...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboProductos.Focus();
                return;
            }

            GuardarPresupuesto();
        }

        private void GuardarPresupuesto() // le envío lo que tiene mis columnas del presupuesto ...
        {   
            oPresupuesto.Fecha = Convert.ToDateTime(txtFecha.Text);
            oPresupuesto.Cliente = txtCliente.Text;
            oPresupuesto.Descuento =Convert.ToDouble(txtDescuento.Text);
            oPresupuesto.Total = Convert.ToDouble(txtTotal.Text);
            if (modo.Equals(Accion.UPDATE))
            {
                if (gestor.EditarPresupuesto(oPresupuesto))
                {
                    MessageBox.Show("El presupuesto de actualizo correctamente!!!", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("El presupuesto NO se pudo actualizar!!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (gestor.ConfirmarPresupuesto(oPresupuesto))
                {
                    MessageBox.Show("El presupuesto de grabó correctamente!!!", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("El presupuesto NO se pudo grabar!!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }    


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
