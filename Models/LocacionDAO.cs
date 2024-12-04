using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LocacionDAO : ConnectionSQL
    {
        //metodo para listar las locaciones
        public DataTable ListarLocaciones()
        {
            DataTable listLocaciones = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarLocaciones";
                    command.CommandType = CommandType.StoredProcedure;
                    leerfilas = command.ExecuteReader();
                    listLocaciones.Load(leerfilas);
                    leerfilas.Close();
                    return listLocaciones;
                }
            }
        }

        //metodo para agregar locacion
        public void AgregarLocacion(Locacion loc)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@descripcion", loc.Descripcion);
                    
                    command.CommandText = "sp_AgregarLocacion";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        //metodo para editar locaciones
        public void EditarLocacion(Locacion loc)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idLocacion", loc.IdLocacion);
                    command.Parameters.AddWithValue("@descripcion", loc.Descripcion);
                    
                    command.CommandText = "sp_EditarLocacion";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        //metodo para eliminar locaciones
        public void EliminarLocacion(int IdLocacion)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idLocacion", IdLocacion);
                    command.CommandText = "sp_EliminarLocacion";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
