using TerceraEntrega;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TerceraEntrega.Models
{
    public class Servicios
    {
        public static Servicios instancia;
        public ActualizarInfo actualizar  = new ActualizarInfo ();
        public FuncionesCalculo funciones = new FuncionesCalculo ();
        //public VefiricarInfo verificar = new VefiricarInfo ();
        public EliminarInfo eliminar = new EliminarInfo();

        public static Servicios ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new Servicios();
            }

            return instancia;
        }

        public static List<ListaUsuario> Usuarios = new List<ListaUsuario>();
        public List<ListaUsuario> ListaDeUsuarios { get => Usuarios; set => Usuarios = value; }
        public ActualizarInfo Actualizar { get => actualizar; set => actualizar = value; }
        public FuncionesCalculo Funciones { get => funciones; set => funciones = value; }
        public ListaUsuario Verificar { get; set; }
        public EliminarInfo Eliminar { get => eliminar; set => eliminar = value; }

        /*
        public ListaUsuario UsuarioPorCc(int Cc)
        {
            foreach (ListaUsuario Usuario in Usuarios)
            {
                if (Usuario.Cedula == Cc)
                {
                    return Usuario;
                }
            }

            return null;
        }*/

        /*
        public class VefiricarInfo
        {
            List<ListaUsuario> Usuarios = Servicios.Usuarios;

            public bool Verificar_Usuario(int cedula)
            {
                foreach (ListaUsuario usuario in Usuarios)
                {
                    if (usuario.cedula == cedula)
                    {
                        return true;
                    }

                }
                return false;
            }


        }*/

        public ListaUsuario Verificar_Usuario(int cedula)
        {
            foreach (ListaUsuario usuario in Usuarios)
            {
                if (usuario.cedula == cedula)
                {
                    return usuario;
                }
            }
            return null; 
        }



        public void ingresarUsuario(ListaUsuario UsuarioNuevo)
        {
            ListaDeUsuarios.Add(UsuarioNuevo);

        }

        /*
        public void ActualizarUsuario(ListaUsuario usuarioActualizado)
        {
            
            ListaUsuario usuarioExistente = Verificar_Usuario(usuarioActualizado.Cedula);

            if (usuarioExistente != null)
            {
                // Actualizar los campos del usuario existente con los valores del usuario actualizado
                usuarioExistente.nombre = usuarioActualizado.nombre;
                usuarioExistente.apellido = usuarioActualizado.apellido;
                usuarioExistente.periodo_consumo = usuarioActualizado.periodo_consumo;
                usuarioExistente.estrato = usuarioActualizado.estrato;
                usuarioExistente.meta_ahorro_energia = usuarioActualizado.meta_ahorro_energia;
                usuarioExistente.consumo_actual_energia = usuarioActualizado.consumo_actual_energia;
                usuarioExistente.promedio_consumo_agua = usuarioActualizado.promedio_consumo_agua;
                usuarioExistente.consumo_actual_agua = usuarioActualizado.consumo_actual_agua;
            }
            else
            {
                // Si el usuario no se encuentra en la lista, puedes lanzar una excepción o manejarlo según lo necesites
                throw new InvalidOperationException("El usuario no existe en la lista");
            }
        }*/


    }
}