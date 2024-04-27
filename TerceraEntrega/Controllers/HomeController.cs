using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TerceraEntrega.Models;

//ESTE ES

namespace TerceraEntrega.Controllers
{
    public class HomeController : Controller
    {
        Servicios Servicios = Servicios.ObtenerInstancia();
        public ActionResult MenuAplicacion()
        {
            return View();
        }

        //------------Crear registro--------------

        public ActionResult CrearRegistro()
        {
            ViewBag.Notificacion = TempData["Usuario existente"];

            return View();
        }

        public ActionResult Estadisticas()
        {
            if (Servicios.Usuarios.Count > 0)
            {
                return View(Servicios);
            }
            else
            {
                return RedirectToAction("UsuarioSinRegistro");
            }

        }

        public ActionResult UsuarioSinRegistro()
        {
            return View();
        }


        public ActionResult Registrar()
        {
            int cedula;
            string nombre;
            string apellido;
            int periodo_consumo;
            int estrato;
            int meta_ahorro_energia;
            int consumo_actual_energia;
            int promedio_consumo_agua;
            int consumo_actual_agua;

            cedula = Convert.ToInt32(Request.Form["cedula"]);
            nombre = Convert.ToString(Request.Form["nombre"]);
            apellido = Convert.ToString(Request.Form["apellido"]);
            periodo_consumo = string.IsNullOrEmpty(Request.Form["Pconsumo"]) ? 0 : Convert.ToInt32(Request.Form["Pconsumo"]);
            estrato = string.IsNullOrEmpty(Request.Form["estrato"]) ? 0 : Convert.ToInt32(Request.Form["estrato"]);
            meta_ahorro_energia = string.IsNullOrEmpty(Request.Form["MHenergia"]) ? 0 : Convert.ToInt32(Request.Form["MHenergia"]);
            consumo_actual_energia = string.IsNullOrEmpty(Request.Form["CAenergia"]) ? 0 : Convert.ToInt32(Request.Form["CAenergia"]);
            promedio_consumo_agua = string.IsNullOrEmpty(Request.Form["PCagua"]) ? 0 : Convert.ToInt32(Request.Form["PCagua"]);
            consumo_actual_agua = string.IsNullOrEmpty(Request.Form["CAagua"]) ? 0 : Convert.ToInt32(Request.Form["CAagua"]);



            ListaUsuario usuarioExistente = Servicios.Verificar_Usuario(cedula);
            if (usuarioExistente != null)
            {
                TempData["Usuario existente"] = "El usuario con cedula " + cedula + " ya se encuentra en el sistema";
                return RedirectToAction("CrearRegistro");
            }

            ListaUsuario usuario = new ListaUsuario(cedula, nombre, apellido, periodo_consumo, estrato, meta_ahorro_energia, consumo_actual_energia, promedio_consumo_agua, consumo_actual_agua);
            Servicios.ingresarUsuario(usuario);
            return View(usuario);
        }

        //------------Actualizar usuario--------------
        public ActionResult ModificarInformacion()
        {
            return View();
        }

        //------------Actualizar información--------------

        public ActionResult ActualizarInformacion()
        {
            ViewBag.Notificacion = TempData["ActualizarInformacion"];
            return View();
        }

        public ActionResult VerificarActualizarUsuario(int cedula)
        {
            ListaUsuario usuario = Servicios.Verificar_Usuario(cedula);
            if (usuario == null)
            {
                TempData["ActualizarInformacion"] = "El usuario no se encuentra en el sistema";
                return RedirectToAction("ActualizarInformacion");
            }
            else
            {
                TempData["UsuarioActualizar"] = usuario; 
                return RedirectToAction("ConfirmacionActualizacion");
            }
        }



        public ActionResult ConfirmacionActualizacion()
        {
            ListaUsuario usuario = TempData["UsuarioActualizar"] as ListaUsuario;
            if (usuario != null)
            {
                return View(usuario);
            }
            else
            {
                return RedirectToAction("ActualizarInformacion");
            }
        }


        //------------Eliminar usuario--------------
        public ActionResult EliminarUsuario()
        {
            ViewBag.Notificacion = TempData["EliminarUsuario"];
            return View();
        }

        public ActionResult VerificarEliminarUsuario()
        {
            int cedula;
            cedula = Convert.ToInt32(Request.Form["cedula"]);

            ListaUsuario usuario = Servicios.Verificar_Usuario(cedula);
            if (usuario == null)
            {
                TempData["EliminarUsuario"] = "El usuario no se encuentra en el sistema";
            }
            else
            {
                Servicios.Eliminar.EliminarUsuarioPorCc(cedula);
                TempData["EliminacionExitosa"] = "El usuario se eliminó correctamente";
                return RedirectToAction("ConfirmacionEliminacion");
            }
            return RedirectToAction("EliminarUsuario");
        }

        public ActionResult ConfirmacionEliminacion()
        {
            ViewBag.Notificacion = TempData["EliminacionExitosa"];
            return View();
        }
       



    }
}