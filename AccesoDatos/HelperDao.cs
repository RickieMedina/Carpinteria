using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpinteria.AccesoDatos
{
    class HelperDao
    {
        private static HelperDao instancia;
        private string connectionString; // cadenaconexion
        private HelperDao() // hacemos privado el constructor
        {
            connectionString = @"Data Source=HOME\SQLEXPRESS;Initial Catalog=CarpinteriaUlt;Integrated Security=True";
        }

        public static HelperDao ObtenerInstancia()//metodo de creación estatico que usas mi helper private y me dice si la instancia esta abierta
        {
            if (instancia == null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }
        public List<Presupuesto> ConsultaSQL(string storeName) // nombre sp
        {   
            
            List<Presupuesto> lst = new List<Presupuesto>();
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            DataTable tabla = new DataTable();
            try
            {
                cnn.ConnectionString = connectionString; 
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storeName;
                tabla.Load(cmd.ExecuteReader());
                foreach (DataRow row in tabla.Rows)
                {
                    //Por cada registro creamos un objeto del dominio
                    Presupuesto oPresupuesto = new Presupuesto();
                    oPresupuesto.Cliente = row["Cliente"].ToString();
                    oPresupuesto.Fecha = Convert.ToDateTime(row["Fecha"].ToString());
                    oPresupuesto.Descuento = Convert.ToDouble(row["Descuento"].ToString());
                    oPresupuesto.PresupuestoNro = Convert.ToInt32(row["Presupuesto"].ToString());
                    oPresupuesto.Total = Convert.ToDouble(row["Total"].ToString());
                    //validar que fecha_baja no es null:
                    if (!row["Fecha_baja"].Equals(DBNull.Value))
                        oPresupuesto.FechaBaja =Convert.ToDateTime(row["Fecha_baja"].ToString());

                    lst.Add(oPresupuesto);
                }



            }
            catch (SqlException ex)
            {
                throw (ex);
            }
            finally
            {
                this.CloseConnection(cnn); 
            }                    
            return lst;
        }

        

        public Presupuesto PresupuestoId(int nro)
        {

            Presupuesto oPresupuesto = new Presupuesto();
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cnn.ConnectionString = connectionString;//@"Data Source=HOME\SQLEXPRESS;Initial Catalog=Carpinteria;Integrated Security=True"; 
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CONSULTAR_PRESUPUESTO_POR_ID";
                cmd.Parameters.AddWithValue("@nro", nro);
                SqlDataReader reader = cmd.ExecuteReader();
                bool esPrimerRegistro = true;

                while (reader.Read())
                {
                    if (esPrimerRegistro)
                    {
                        //solo para el primer resultado recuperamos los datos del MAESTRO:
                        oPresupuesto.PresupuestoNro = Convert.ToInt32(reader["presupuesto_nro"].ToString());
                        oPresupuesto.Cliente = reader["cliente"].ToString();
                        oPresupuesto.Fecha = Convert.ToDateTime(reader["fecha"].ToString());
                        oPresupuesto.Descuento = Convert.ToDouble(reader["descuento"].ToString());
                        oPresupuesto.PresupuestoNro = Convert.ToInt32(reader["presupuesto_nro"].ToString());
                        oPresupuesto.Total = Convert.ToDouble(reader["total"].ToString());
                        esPrimerRegistro = false;
                    }

                    DetallePresupuesto oDetalle = new DetallePresupuesto();
                    Producto oProducto = new Producto();
                    oProducto.ProductoNro = Convert.ToInt32(reader["id_producto"].ToString());
                    oProducto.Nombre = reader["n_producto"].ToString();
                    oProducto.Precio = Convert.ToDouble(reader["precio"].ToString());
                    oProducto.Activo = reader["activo"].ToString().Equals("S");
                    oDetalle.Producto = oProducto;
                    oDetalle.Cantidad = Convert.ToInt32(reader["cantidad"].ToString());
                    esPrimerRegistro = false;
                    oPresupuesto.AgregarDetalle(oDetalle);
                }

            }
            catch (SqlException ex)
            {
                throw (ex);
            }
            finally
            {
                this.CloseConnection(cnn); 
            }
            return oPresupuesto;

        
      }
            
            
        public  DataTable ConsultaSQL2(string storeName)
        {
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            DataTable tabla = new DataTable();
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storeName;
                tabla.Load(cmd.ExecuteReader());
                return tabla;


            }
            catch (SqlException ex)
            {
                throw (ex);
            }
            finally
            {
                this.CloseConnection(cnn); 
            }                   
        }

        public bool InsertarMaestroDet(Presupuesto oPresupuesto, string storeName1, string storename2)
        {
            bool estado = true;
            SqlConnection conexion = new SqlConnection();
            SqlTransaction transaccion = null;


            try
            {

                conexion.ConnectionString = connectionString;
                conexion.Open();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = storeName1;
                comando.Parameters.AddWithValue("@cliente", oPresupuesto.Cliente);                                       
                comando.Parameters.AddWithValue("@dto", oPresupuesto.Descuento);
                comando.Parameters.AddWithValue("@total", oPresupuesto.Total);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@presupuesto_nro";
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.Output;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery();
                oPresupuesto.PresupuestoNro = (int)param.Value;



                int DetalleNro = 1;
                foreach (DetallePresupuesto item in oPresupuesto.Detalles)
                {
                    SqlCommand comandoDetalle = new SqlCommand();
                    comandoDetalle.Connection = conexion;
                    comandoDetalle.Transaction = transaccion;
                    comandoDetalle.CommandType = CommandType.StoredProcedure;
                    comandoDetalle.CommandText = storename2;
                    comandoDetalle.Parameters.AddWithValue("@presupuesto_nro", oPresupuesto.PresupuestoNro);
                    comandoDetalle.Parameters.AddWithValue("@detalle", DetalleNro);
                    comandoDetalle.Parameters.AddWithValue("@id_producto", item.Producto.ProductoNro);
                    comandoDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    comandoDetalle.ExecuteNonQuery();
                    DetalleNro++;
                }

                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                estado = false;
            }
            finally
            {
                this.CloseConnection(conexion);
            }

            return estado;

        }

        public bool EditarMaestroDet(Presupuesto oPresupuesto, string storeName1, string storename2) //Trae un parametro mas. Ya viene con nro presup.
        {
            bool estado = true;
            SqlConnection conexion = new SqlConnection();
            SqlTransaction transaccion = null;

            try
            {

                conexion.ConnectionString = connectionString;
                conexion.Open();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = storeName1;
                comando.Parameters.AddWithValue("@nro_presupuesto", oPresupuesto.PresupuestoNro);
                comando.Parameters.AddWithValue("@cliente", oPresupuesto.Cliente);
                comando.Parameters.AddWithValue("@dto", oPresupuesto.Descuento);
                comando.Parameters.AddWithValue("@total", oPresupuesto.Total);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@presupuesto_nro";
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.Output;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery();
                oPresupuesto.PresupuestoNro = (int)param.Value;


                int DetalleNro = 1;
                foreach (DetallePresupuesto item in oPresupuesto.Detalles)
                {
                    SqlCommand comandoDetalle = new SqlCommand();
                    comandoDetalle.Connection = conexion;
                    comandoDetalle.Transaction = transaccion;
                    comandoDetalle.CommandType = CommandType.StoredProcedure;
                    comandoDetalle.CommandText = storename2; //Este sp recibe un "@presupuesto_nro",que es el output de mi insert
                    comandoDetalle.Parameters.AddWithValue("@presupuesto_nro", oPresupuesto.PresupuestoNro);// por eso tengo que seguir usando el output, ver si puedo sacarlo
                    comandoDetalle.Parameters.AddWithValue("@detalle", DetalleNro);
                    comandoDetalle.Parameters.AddWithValue("@id_producto", item.Producto.ProductoNro);
                    comandoDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    comandoDetalle.ExecuteNonQuery();
                    DetalleNro++;
                }

                transaccion.Commit();
            }
            catch (Exception)
            {
                transaccion.Rollback();
                estado = false;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return estado;
        }

        public bool BajaPresupuesto(int nro_presupuesto)
        {
            SqlConnection conexion = new SqlConnection();
            SqlTransaction t = null;
            int affected = 0;
            try
            {
                conexion.ConnectionString = connectionString;
                conexion.Open();
                t = conexion.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_REGISTRAR_BAJA_PRESUPUESTOS", conexion, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@presupuesto_nro", nro_presupuesto);
                affected = cmd.ExecuteNonQuery();
                t.Commit();



            }
            catch (SqlException ex)
            {
                t.Rollback();
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            if (affected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int ObtenerProximoNumeroHelper(string storeName)
        {
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            DataTable tabla = new DataTable();
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storeName;
                SqlParameter param = new SqlParameter("@next", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                return (int)param.Value;


            }
            catch (SqlException ex)
            {
                throw (ex);
            }
            finally
            {
                this.CloseConnection(cnn); 
            }                
        }



        private void CloseConnection(SqlConnection cnn)
        {
            if (cnn != null && cnn.State == ConnectionState.Open)
            {
                cnn.Close();
            }

        }


      
    }
}
    

