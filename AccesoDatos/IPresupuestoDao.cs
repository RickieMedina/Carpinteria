using System.Collections.Generic;
using System.Data;

namespace Carpinteria.AccesoDatos
{
     interface IPresupuestoDao
    {

        int ObtenerProximoNumero();
        DataTable ListarProductos();
        bool Crear(Presupuesto oPresupuesto);

        bool Editar(Presupuesto oPresupuesto);

        bool RegistrarBajaPresupuesto(int nro_presupuesto);

        List<Presupuesto> ListarPresupuestos();
        Presupuesto PresupuestoPorId(int nro);
        
    }
}