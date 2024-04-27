using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TerceraEntrega.Models;

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

            /*cedula = Convert.ToInt32(Request.Form["cedula"]);
            nombre = Convert.ToString(Request.Form["nombre"]);
            apellido = Convert.ToString(Request.Form["apellido"]);
            periodo_consumo = Convert.ToInt32(Request.Form["Pconsumo"]);
            estrato = Convert.ToInt32(Request.Form["estrato"]);
            meta_ahorro_energia = Convert.ToInt32(Request.Form["MHenergia"]);
            consumo_actual_energia = Convert.ToInt32(Request.Form["CAenergia"]);
            promedio_consumo_agua = Convert.ToInt32(Request.Form["PCagua"]);
            consumo_actual_agua = Convert.ToInt32(Request.Form["CAagua"]);*/

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
                TempData["UsuarioActualizar"] = usuario; // Guarda el usuario en TempData para usarlo en la vista de confirmación
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

        /*

        public ActionResult ActualizarInformacion(int cedula, string nombre, string apellido, int periodoConsumo, int estrato, int metaAhorroEnergia, int consumoActualEnergia, int promedioConsumoAgua, int consumoActualAgua)
        {
            // Lógica para actualizar la información del usuario en la base de datos
            try
            {
                // Obtener el usuario a actualizar
                ListaUsuario usuario = Servicios.Verificar_Usuario(cedula);

                if (usuario != null)
                {
                    // Actualizar los campos del usuario con los nuevos valores recibidos
                    usuario.Nombre = nombre;
                    usuario.Apellido = apellido;
                    usuario.periodo_consumo = periodoConsumo;
                    usuario.Estrato = estrato;
                    usuario.meta_ahorro_energia = metaAhorroEnergia;
                    usuario.consumo_actual_energia = consumoActualEnergia;
                    usuario.promedio_consumo_agua = promedioConsumoAgua;
                    usuario.consumo_actual_agua = consumoActualAgua;

                    // Guardar los cambios en la base de datos
                    Servicios.ActualizarUsuario(usuario);

                    // Establecer un mensaje de éxito en TempData
                    TempData["ActualizacionExitosa"] = "El usuario se actualizó correctamente";
                }
                else
                {
                    TempData["ActualizacionFallida"] = "No se pudo encontrar el usuario en la base de datos";
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la actualización
                TempData["ActualizacionFallida"] = "Ocurrió un error al intentar actualizar la información del usuario";
                // Registrar o mostrar la excepción para fines de depuración
                Console.WriteLine($"Error al actualizar el usuario: {ex.Message}");
            }

            // Redirigir a la acción ConfirmacionActualizacion
            return RedirectToAction("ConfirmacionActualizacion");
        }
        */


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