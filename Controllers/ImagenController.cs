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
    public class ImagenController
    {
        ImagenDAO imagenDao = new ImagenDAO();

        public DataTable ListarImagenes()
        {
            return imagenDao.listarImagenes();
        }

        public void AgregarImagen(Imagen imagen)
        {
            imagenDao.agregarImagen(imagen);
        }

        public void EditarImagen(Imagen imagen)
        {
            imagenDao.editarImagen(imagen);
        }

        public void EliminarImagen(int idImagen)
        {
            imagenDao.eliminarImagen(idImagen);
        }

        public byte[] ObtenerFoto(int idImagen)
        {
            return imagenDao.obtenerFoto(idImagen);
        }
    }
}
