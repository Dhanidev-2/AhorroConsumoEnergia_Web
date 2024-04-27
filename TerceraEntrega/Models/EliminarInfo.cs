using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TerceraEntrega.Models
{
    public class EliminarInfo
    {
        List<ListaUsuario> Usuarios = Servicios.Usuarios;

        public void EliminarUsuarioPorCc(int Cc)
        {
            ListaUsuario usuarioAEliminar = null;

            foreach (ListaUsuario usuario in Usuarios)
            {
                if (usuario.Cedula == Cc)
                {
                    usuarioAEliminar = usuario;
                    break;
                }
            }

            if (usuarioAEliminar != null)
            {
                Usuarios.Remove(usuarioAEliminar);
                Console.WriteLine($"El usuario con cédula {Cc} ha sido eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("El usuario no se encuentra en el sistema.");
            }
        }
    }


}