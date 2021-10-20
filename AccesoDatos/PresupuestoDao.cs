using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpinteria.AccesoDatos
{
    class PresupuestoDao: IPresupuestoDao
    {
        public bool Crear(Presupuesto oPresupuesto) 
        {

            HelperDao helper = HelperDao.ObtenerInstancia();
            return helper.InsertarMaestroDet(oPresupuesto, "SP_INSERTAR_MAESTRO", "SP_INSERTAR_DETALLE");
        }

        public bool Editar(Presupuesto oPresupuesto)
        {
            HelperDao helper = HelperDao.ObtenerInstancia();
            return helper.EditarMaestroDet(oPresupuesto, "SP_EDITAR_MAESTRO", "SP_INSERTAR_DETALLE");
        }

        public bool RegistrarBajaPresupuesto(int nro_presupuesto) 
        {
            HelperDao helper = HelperDao.ObtenerInstancia();
            return helper.BajaPresupuesto(nro_presupuesto);
        }

        public DataTable ListarProductos()
        {
            
            HelperDao helper = HelperDao.ObtenerInstancia();
            return helper.ConsultaSQL2("SP_CONSULTAR_PRODUCTOS");


        }

        public int ObtenerProximoNumero()
        {
            HelperDao helper = HelperDao.ObtenerInstancia();
            return helper.ObtenerProximoNumeroHelper("SP_PROXIMO_ID");
            

        }

        public List<Presupuesto> ListarPresupuestos()//Ahora con list tambien trabaja con datatable como el Listar productos
        {
            HelperDao helper = HelperDao.ObtenerInstancia();
            return helper.ConsultaSQL("SP_CONSULTAR_PRESUPUESTOS");
        }

        public Presupuesto PresupuestoPorId(int nro)
        {
            HelperDao helper = HelperDao.ObtenerInstancia();
            return helper.PresupuestoId(nro);
        }

        
    }
}
