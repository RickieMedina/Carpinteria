
namespace Carpinteria.Formularios
{
    partial class FrmConsulta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAtrasConsultas = new System.Windows.Forms.Button();
            this.chkBoxConsultas = new System.Windows.Forms.CheckBox();
            this.btnConsultasPresupuest = new System.Windows.Forms.Button();
            this.dgrConsultas = new System.Windows.Forms.DataGridView();
            this.btnConsultasEditar = new System.Windows.Forms.Button();
            this.dtfDesde = new System.Windows.Forms.DateTimePicker();
            this.dtfHasta = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.cPresupuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFecha_baja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAcciones = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgrConsultas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAtrasConsultas
            // 
            this.btnAtrasConsultas.Location = new System.Drawing.Point(12, 403);
            this.btnAtrasConsultas.Name = "btnAtrasConsultas";
            this.btnAtrasConsultas.Size = new System.Drawing.Size(86, 23);
            this.btnAtrasConsultas.TabIndex = 7;
            this.btnAtrasConsultas.Text = "Atras";
            this.btnAtrasConsultas.UseVisualStyleBackColor = true;
            this.btnAtrasConsultas.Click += new System.EventHandler(this.btnAtrasConsultas_Click);
            // 
            // chkBoxConsultas
            // 
            this.chkBoxConsultas.AutoSize = true;
            this.chkBoxConsultas.Location = new System.Drawing.Point(112, 83);
            this.chkBoxConsultas.Name = "chkBoxConsultas";
            this.chkBoxConsultas.Size = new System.Drawing.Size(73, 17);
            this.chkBoxConsultas.TabIndex = 1;
            this.chkBoxConsultas.Text = "Con bajas";
            this.chkBoxConsultas.UseVisualStyleBackColor = true;
            // 
            // btnConsultasPresupuest
            // 
            this.btnConsultasPresupuest.Location = new System.Drawing.Point(29, 39);
            this.btnConsultasPresupuest.Name = "btnConsultasPresupuest";
            this.btnConsultasPresupuest.Size = new System.Drawing.Size(167, 23);
            this.btnConsultasPresupuest.TabIndex = 0;
            this.btnConsultasPresupuest.Text = "Consultar Presupuestos";
            this.btnConsultasPresupuest.UseVisualStyleBackColor = true;
            this.btnConsultasPresupuest.Click += new System.EventHandler(this.btnConsultasPresupuest_Click);
            // 
            // dgrConsultas
            // 
            this.dgrConsultas.AllowUserToAddRows = false;
            this.dgrConsultas.AllowUserToDeleteRows = false;
            this.dgrConsultas.AllowUserToResizeColumns = false;
            this.dgrConsultas.AllowUserToResizeRows = false;
            this.dgrConsultas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrConsultas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cPresupuesto,
            this.cFecha,
            this.cCliente,
            this.cTotal,
            this.cDescuento,
            this.cFecha_baja,
            this.cAcciones,
            this.cEliminar});
            this.dgrConsultas.Location = new System.Drawing.Point(12, 107);
            this.dgrConsultas.Name = "dgrConsultas";
            this.dgrConsultas.ReadOnly = true;
            this.dgrConsultas.RowHeadersVisible = false;
            this.dgrConsultas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrConsultas.Size = new System.Drawing.Size(609, 290);
            this.dgrConsultas.TabIndex = 6;
            this.dgrConsultas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrConsultas_CellContentClick);
            // 
            // btnConsultasEditar
            // 
            this.btnConsultasEditar.Location = new System.Drawing.Point(127, 403);
            this.btnConsultasEditar.Name = "btnConsultasEditar";
            this.btnConsultasEditar.Size = new System.Drawing.Size(79, 23);
            this.btnConsultasEditar.TabIndex = 8;
            this.btnConsultasEditar.Text = "Editar";
            this.btnConsultasEditar.UseVisualStyleBackColor = true;
            this.btnConsultasEditar.Click += new System.EventHandler(this.btnConsultasEditar_Click);
            // 
            // dtfDesde
            // 
            this.dtfDesde.Location = new System.Drawing.Point(271, 81);
            this.dtfDesde.Name = "dtfDesde";
            this.dtfDesde.Size = new System.Drawing.Size(142, 20);
            this.dtfDesde.TabIndex = 3;
            // 
            // dtfHasta
            // 
            this.dtfHasta.Location = new System.Drawing.Point(479, 81);
            this.dtfHasta.Name = "dtfHasta";
            this.dtfHasta.Size = new System.Drawing.Size(142, 20);
            this.dtfHasta.TabIndex = 5;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(227, 85);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(38, 13);
            this.lblDesde.TabIndex = 2;
            this.lblDesde.Text = "Desde";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(438, 85);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(35, 13);
            this.lblHasta.TabIndex = 4;
            this.lblHasta.Text = "Hasta";
            // 
            // cPresupuesto
            // 
            this.cPresupuesto.Frozen = true;
            this.cPresupuesto.HeaderText = "Presupuesto";
            this.cPresupuesto.Name = "cPresupuesto";
            this.cPresupuesto.ReadOnly = true;
            this.cPresupuesto.Width = 91;
            // 
            // cFecha
            // 
            this.cFecha.Frozen = true;
            this.cFecha.HeaderText = "Fecha";
            this.cFecha.Name = "cFecha";
            this.cFecha.ReadOnly = true;
            this.cFecha.Width = 90;
            // 
            // cCliente
            // 
            this.cCliente.Frozen = true;
            this.cCliente.HeaderText = "Cliente";
            this.cCliente.Name = "cCliente";
            this.cCliente.ReadOnly = true;
            // 
            // cTotal
            // 
            this.cTotal.Frozen = true;
            this.cTotal.HeaderText = "Total";
            this.cTotal.Name = "cTotal";
            this.cTotal.ReadOnly = true;
            this.cTotal.Width = 60;
            // 
            // cDescuento
            // 
            this.cDescuento.Frozen = true;
            this.cDescuento.HeaderText = "Descuento";
            this.cDescuento.Name = "cDescuento";
            this.cDescuento.ReadOnly = true;
            this.cDescuento.Width = 60;
            // 
            // cFecha_baja
            // 
            this.cFecha_baja.Frozen = true;
            this.cFecha_baja.HeaderText = "Fecha_baja";
            this.cFecha_baja.Name = "cFecha_baja";
            this.cFecha_baja.ReadOnly = true;
            this.cFecha_baja.Width = 90;
            // 
            // cAcciones
            // 
            this.cAcciones.Frozen = true;
            this.cAcciones.HeaderText = "Acciones";
            this.cAcciones.Name = "cAcciones";
            this.cAcciones.ReadOnly = true;
            this.cAcciones.Text = "Ver";
            this.cAcciones.UseColumnTextForButtonValue = true;
            this.cAcciones.Width = 57;
            // 
            // cEliminar
            // 
            this.cEliminar.HeaderText = "";
            this.cEliminar.Name = "cEliminar";
            this.cEliminar.ReadOnly = true;
            this.cEliminar.Text = "Eliminar";
            this.cEliminar.UseColumnTextForButtonValue = true;
            this.cEliminar.Width = 56;
            // 
            // FrmConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 445);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.dtfHasta);
            this.Controls.Add(this.dtfDesde);
            this.Controls.Add(this.btnConsultasEditar);
            this.Controls.Add(this.btnAtrasConsultas);
            this.Controls.Add(this.chkBoxConsultas);
            this.Controls.Add(this.btnConsultasPresupuest);
            this.Controls.Add(this.dgrConsultas);
            this.Name = "FrmConsulta";
            this.Text = "Consultas";
            this.Load += new System.EventHandler(this.FrmConsulta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrConsultas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAtrasConsultas;
        private System.Windows.Forms.CheckBox chkBoxConsultas;
        private System.Windows.Forms.Button btnConsultasPresupuest;
        private System.Windows.Forms.DataGridView dgrConsultas;
        private System.Windows.Forms.Button btnConsultasEditar;
        private System.Windows.Forms.DateTimePicker dtfDesde;
        private System.Windows.Forms.DateTimePicker dtfHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPresupuesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFecha_baja;
        private System.Windows.Forms.DataGridViewButtonColumn cAcciones;
        private System.Windows.Forms.DataGridViewButtonColumn cEliminar;
    }
}