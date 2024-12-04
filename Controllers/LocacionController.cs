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
    public class LocacionController
    {
        LocacionDAO locacionDAO = new LocacionDAO();

        public DataTable listarLocaciones()
        {
            return locacionDAO.ListarLocaciones();
        }

        public void agregarLocacion(Locacion loc)
        {
            locacionDAO.AgregarLocacion(loc);
        }

        public void editarLocacion(Locacion loc)
        {
            locacionDAO.EditarLocacion(loc);
        }

        public void eliminarLocacion(int idLocacion)
        {
            locacionDAO.EliminarLocacion(idLocacion);
        }

    }
}
