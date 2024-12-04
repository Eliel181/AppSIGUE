using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class PersonaController
    {
        PersonaDAO personaDAO = new PersonaDAO();

        public DataTable listarPersonas()
        {
            return personaDAO.ListarPersonas();
        }

        public void agregarPersona(Persona per)
        {
            personaDAO.AgregarPersona(per);
        }

        public void editarPersona(Persona per)
        {
            personaDAO.EditarPersona(per);
        }

        public void eliminarPersona(int idPersona)
        {
            personaDAO.EliminarPersona(idPersona);
        }

        public byte[] obtenerFoto(int idPersona)
        {
            return personaDAO.ObtenerFoto(idPersona);
        }
    }
}
