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

        //------------Actualizar estrato--------------

        public ActionResult ActualizarInformacion()
        {
            ViewBag.Notificacion = TempData["ActualizarInformacion"];
            return View();
        }

        public ActionResult BuscarPorCedula(int cedula)
        {
            ListaUsuario usuario = Servicios.Verificar_Usuario(cedula);
            if (usuario == null)
            {
                TempData["Notificacion"] = "El usuario no se encuentra en el sistema";
                return RedirectToAction("ActualizarInformacion");
            }
            else
            {
                TempData["idMomentaneoEstrato"] = usuario.Cedula.ToString();
                TempData["UsuarioEncontrado"] = usuario; // Almacena el usuario encontrado en TempData
                return RedirectToAction("MostrarActualizarUsuario");
            }
        }

        public ActionResult BorrarUsuarioYActualizarlo()
        {
            // Verificr si el usuario fue encontrado en la función BuscarPorCedula
            if (TempData["UsuarioEncontrado"] != null)
            {
                ListaUsuario usuario = (ListaUsuario)TempData["UsuarioEncontrado"];

                // Elimina el usuario encontrado
                Servicios.Eliminar.EliminarUsuarioPorCc(usuario.Cedula); // Utiliza el método para eliminar usuario por cédula

                // Obtiene los campos actualizados del formulario
                string nombre = Request.Form["nombre"];
                string apellido = Request.Form["apellido"];
                string periodoConsumoStr = Request.Form["Pconsumo"];
                int? periodoConsumo = !string.IsNullOrEmpty(periodoConsumoStr) ? Convert.ToInt32(periodoConsumoStr) : (int?)null;
                string estratoStr = Request.Form["estrato"];
                int? estrato = !string.IsNullOrEmpty(estratoStr) ? Convert.ToInt32(estratoStr) : (int?)null;
                string metaAhorroEnergiaStr = Request.Form["MHenergia"];
                int? metaAhorroEnergia = !string.IsNullOrEmpty(metaAhorroEnergiaStr) ? Convert.ToInt32(metaAhorroEnergiaStr) : (int?)null;
                string consumoActualEnergiaStr = Request.Form["CAenergia"];
                int? consumoActualEnergia = !string.IsNullOrEmpty(consumoActualEnergiaStr) ? Convert.ToInt32(consumoActualEnergiaStr) : (int?)null;
                string promedioConsumoAguaStr = Request.Form["PCagua"];
                int? promedioConsumoAgua = !string.IsNullOrEmpty(promedioConsumoAguaStr) ? Convert.ToInt32(promedioConsumoAguaStr) : (int?)null;
                string consumoActualAguaStr = Request.Form["CAagua"];
                int? consumoActualAgua = !string.IsNullOrEmpty(consumoActualAguaStr) ? Convert.ToInt32(consumoActualAguaStr) : (int?)null;

                ListaUsuario nuevoUsuario = new ListaUsuario(
                    usuario.Cedula,
                    string.IsNullOrEmpty(nombre) ? usuario.Nombre : nombre,
                    string.IsNullOrEmpty(apellido) ? usuario.Apellido : apellido,
                    periodoConsumo ?? usuario.Periodo_consumo,
                    estrato ?? usuario.Estrato,
                    metaAhorroEnergia ?? usuario.Meta_ahorro_energia,
                    consumoActualEnergia ?? usuario.Consumo_actual_energia,
                    promedioConsumoAgua ?? usuario.Promedio_consumo_agua,
                    consumoActualAgua ?? usuario.Consumo_actual_agua
                );


                // Agrega el nuevo usuario a la lista de usuarios
                Servicios.ingresarUsuario(nuevoUsuario);

                TempData["Notificacion"] = "El usuario se eliminó correctamente y se creó un nuevo usuario";
            }
            else
            {
                TempData["Notificacion"] = "No se encontró al usuario";
            }

            return RedirectToAction("ActualizarInformacion");
        }

        public ActionResult MostrarActualizarUsuario()
        {
            // Obtiene el usuario encontrado almacenado en TempData
            ListaUsuario usuario = (ListaUsuario)TempData["UsuarioEncontrado"];

            // Verifica si el usuario no es nulo antes de pasar a la vista
            if (usuario != null)
            {
                // Pasa el usuario encontrado a la vista MostrarActualizarUsuario
                return View(usuario);
            }
            else
            {
                // Si el usuario es nulo, redirige o muestra un mensaje de error
                return RedirectToAction("ActualizarInformacion"); // o cualquier otra acción adecuada
            }
            /*// Obtener el usuario encontrado almacenado en TempData
            ListaUsuario usuario = (ListaUsuario)TempData["UsuarioEncontrado"];
            TempData["Notificacion"] = "El usuario ha sido actualizado";

            // Pasar el usuario encontrado a la vista MostrarActualizarUsuario
            return View(usuario);
            ViewBag.Notificacion = TempData["UsuarioEncontrado"];
            return View();*/
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