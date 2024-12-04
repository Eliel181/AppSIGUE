using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class EspTecnicaController
    {
        EspTecnicaDAO espTecnicaDao = new EspTecnicaDAO();

        public DataTable ListarEspTecnicaes()
        {
            return espTecnicaDao.listarEspTecnicas();
        }

        public void AgregarEspTecnica(EspTecnica espTecnica)
        {
            espTecnicaDao.agregarEspTecnica(espTecnica);
        }

        public void EditarEspTecnica(EspTecnica espTecnica)
        {
            espTecnicaDao.editarEspTecnica(espTecnica);
        }

        public void EliminarEspTecnica(int idEspTecnica)
        {
            espTecnicaDao.eliminarEspTecnica(idEspTecnica);
        }

    }
}
