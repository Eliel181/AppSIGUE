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
    public class PersonaDAO : ConnectionSQL
    {
        //metodo para listar a las Personas
        public DataTable ListarPersonas()
        {
            DataTable listPersonas = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarPersonas";
                    command.CommandType = CommandType.StoredProcedure;
                    leerfilas = command.ExecuteReader();
                    listPersonas.Load(leerfilas);
                    leerfilas.Close();
                    return listPersonas;
                }
            }
        }

        //metodo para agregar Personas
        public void AgregarPersona(Persona per)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@nombre", per.Nombre);
                    command.Parameters.AddWithValue("@apellido", per.Apellido);
                    command.Parameters.AddWithValue("@cuit", per.Cuil);
                    command.Parameters.AddWithValue("@domicilio", per.Domicilio);
                    command.Parameters.AddWithValue("@telefono", per.Telefono);
                    command.Parameters.AddWithValue("@tipo", per.Tipo);
                    command.Parameters.AddWithValue("@locacion", per.IdLocacion);
                    command.Parameters.AddWithValue("@foto", per.Foto);
                    command.CommandText = "sp_AgregarPersona";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        //metodo para obtener la foto de una BD
        public byte[] ObtenerFoto(int idPersona)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select Foto from PERSONAS where IdPersona = @IdPersona";
                    command.Parameters.AddWithValue("idPersona", idPersona);
                    command.CommandType = CommandType.StoredProcedure;
                    leerfilas = command.ExecuteReader();
                    if (leerfilas.Read())
                    {
                        fotoDB = (byte[])leerfilas.GetValue(0);
                    }
                    leerfilas.Close();
                    return fotoDB;
                }
            }
        }

        //metodo para editar personas
        public void EditarPersona(Persona per)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idPersona", per.IdPersona);
                    command.Parameters.AddWithValue("@apellido", per.Apellido);
                    command.Parameters.AddWithValue("@cuit", per.Cuil);
                    command.Parameters.AddWithValue("@domicilio", per.Domicilio);
                    command.Parameters.AddWithValue("@telefono", per.Telefono);
                    command.Parameters.AddWithValue("@tipo", per.Tipo);
                    command.Parameters.AddWithValue("@locacion", per.IdLocacion);
                    command.Parameters.AddWithValue("@foto", per.Foto);
                    command.CommandText = "sp_EditarPersona";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        //metodo para eliminar personas
        public void EliminarPersona(int IdPersona)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idPersona", IdPersona);
                    command.CommandText = "sp_EliminarPersona";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
