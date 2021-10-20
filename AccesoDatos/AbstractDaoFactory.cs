namespace Carpinteria.AccesoDatos
{
   abstract class AbstractDaoFactory
    {
        public abstract IPresupuestoDao CrearPresupuestoDao();
    }
}