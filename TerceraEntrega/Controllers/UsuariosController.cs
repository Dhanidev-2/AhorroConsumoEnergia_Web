using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TerceraEntrega;

namespace TerceraEntrega.Controllers
{
    public class UsuariosController : Controller
    {
        private ENERGIAEntities db = new ENERGIAEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.tbUsuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuarios.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cedula,Nombre,Apellido,Estrato")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                db.tbUsuarios.Add(tbUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbUsuario);
        }

        // GET: Usuarios/Edit/5
        /*public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuarios.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }*/

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = null;
            try
            {
                tbUsuario = db.tbUsuarios.Find(id);
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción como consideres necesario
                Console.WriteLine("Error al buscar el usuario: " + ex.Message);
            }
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public ActionResult Edit([Bind(Include = "Cedula,Nombre,Apellido,Estrato")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbUsuario);
        }*/

        // GET: Usuarios/Delete/5

        public ActionResult Edit([Bind(Include = "Cedula,Nombre,Apellido,Estrato")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(tbUsuario).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Aquí puedes manejar la excepción como consideres necesario
                    Console.WriteLine("Error al editar el usuario: " + ex.Message);
                    return View(tbUsuario);
                }
                return RedirectToAction("Index");
            }
            return View(tbUsuario);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuarios.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        /*public ActionResult DeleteConfirmed(int id)
        {
            tbUsuario tbUsuario = db.tbUsuarios.Find(id);
            db.tbUsuarios.Remove(tbUsuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

        public ActionResult DeleteConfirmed(int id)
        {
            tbUsuario tbUsuario = db.tbUsuarios.Find(id);

            // Encuentra y elimina primero los registros relacionados en tbEnergiaConsumo
            var energiaConsumoRelacionados = db.tbEnergiaConsumoes.Where(ec => ec.Cedula == tbUsuario.Cedula);
            db.tbEnergiaConsumoes.RemoveRange(energiaConsumoRelacionados);

            // Ahora puedes eliminar el usuario
            db.tbUsuarios.Remove(tbUsuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
