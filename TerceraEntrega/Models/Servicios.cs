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
    }
}