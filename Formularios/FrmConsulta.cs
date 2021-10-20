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
using Carpinteria.Servicios;
using Carpinteria.AccesoDatos;

namespace Carpinteria.Formularios
{
    public partial class FrmConsulta : Form
    {
        private GestorPresupuesto gestor;
        private Presupuesto nuevoP;
        public FrmConsulta()
        {
            InitializeComponent();
            gestor = new GestorPresupuesto(new DaoFactory());
            nuevoP = new Presupuesto();
        }

        
        
        private void FrmConsulta_Load(object sender, EventArgs e)
        {
           
        }
        private void btnConsultasPresupuest_Click(object sender, EventArgs e)
        {
            ConsultarPresupuestos();
        }

        
       private void ConsultarPresupuestos()
        {
                List<Presupuesto> lst = gestor.ObtenerPresupuestos();

                
                dgrConsultas.Rows.Clear();
                foreach (Presupuesto oPresupuesto in lst)
                { 
                   if (oPresupuesto.Fecha >= dtfDesde.Value && oPresupuesto.Fecha <= dtfHasta.Value)
                   {
                        dgrConsultas.Rows.Add(new object[]{
                                        oPresupuesto.PresupuestoNro,
                                        oPresupuesto.Fecha.ToString("dd/MM/yyyy"),
                                        oPresupuesto.Cliente,
                                        oPresupuesto.Total,
                                        oPresupuesto.Descuento,
                                        oPresupuesto.FechaBaja.ToString("dd/MM/yyyy")}); ;
                        
                   }
               }
               foreach (DataGridViewRow row in dgrConsultas.Rows)
               {
                if (Convert.ToString(row.Cells[5].Value) != "01/01/0001")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.Visible = false;

                    if (chkBoxConsultas.Checked)
                    {
                        row.Visible = true;
                    }
                }
              }


        }

        private void btnConsultasEditar_Click(object sender, EventArgs e)
        {
            if (dgrConsultas.RowCount > 0)
            {
                int nroPresupuesto = Convert.ToInt32(dgrConsultas.CurrentRow.Cells[0].Value.ToString());
                FrmNuevoPresupuesto nuevoForm = new FrmNuevoPresupuesto(Accion.UPDATE, nroPresupuesto);
                nuevoForm.ShowDialog();
            }
            else
            {
              MessageBox.Show("No hay presupuestos en consulta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgrConsultas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrConsultas.CurrentCell.ColumnIndex == 7)
            {
                if (Convert.ToString(dgrConsultas.CurrentRow.Cells[5].Value) == "01/01/0001")
                {
                    int nro_presupuesto = Convert.ToInt32(dgrConsultas.CurrentRow.Cells[0].Value);
                    if (MessageBox.Show("Seguro que desea eliminar el presupuesto seleccionado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool respuesta = gestor.BajaPresupuesto(nro_presupuesto);

                        if (respuesta)
                        {

                            MessageBox.Show("Presupuesto eliminado!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgrConsultas.Rows.Clear();
                            ConsultarPresupuestos();
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar borrar el presupuesto!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El formulario ya esta dado de baja!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if(dgrConsultas.CurrentCell.ColumnIndex == 6)
            {
                int nroPresupuesto = Convert.ToInt32(dgrConsultas.CurrentRow.Cells[0].Value.ToString());
                FrmNuevoPresupuesto nuevoForm = new FrmNuevoPresupuesto(Accion.READ, nroPresupuesto);
                nuevoForm.ShowDialog();
                
            }

        }

            private void btnAtrasConsultas_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
