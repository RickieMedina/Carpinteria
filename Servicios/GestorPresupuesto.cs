using Carpinteria.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpinteria.Servicios
{
    class GestorPresupuesto
    {
        private IPresupuestoDao dao; //Este va a ser mi objeto dentro del gestor y va a hacer lo que permite su interfaz dao
        public GestorPresupuesto(AbstractDaoFactory factory) // un gestorPresup que tiene un constructor, que tiene un factory y ese f creapresup dao
        {
            dao = factory.CrearPresupuestoDao();
        }

        public int ProximoPresupuesto()
        {
            return dao.ObtenerProximoNumero();
        }

        public DataTable ObtenerProductos() 
        {
            return dao.ListarProductos();
        }

        public bool ConfirmarPresupuesto(Presupuesto oPresupuesto)
        {
            return dao.Crear(oPresupuesto);
        }

        public bool EditarPresupuesto(Presupuesto oPresupuesto)
        {
            return dao.Editar(oPresupuesto);
        }

        public List<Presupuesto> ObtenerPresupuestos()
        {
            return dao.ListarPresupuestos();
        }

        public bool BajaPresupuesto(int nro_presupuesto)
        {
            return dao.RegistrarBajaPresupuesto(nro_presupuesto);
        }

        public Presupuesto ObtenerPresupuestoPorID(int nro)
        {
            return dao.PresupuestoPorId(nro);
        }
    }
}
