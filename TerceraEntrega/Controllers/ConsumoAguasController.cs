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
    public class ConsumoAguasController : Controller
    {
        private ENERGIAEntities db = new ENERGIAEntities();

        // GET: ConsumoAguas
        public ActionResult Index()
        {
            var tbConsumoAguas = db.tbConsumoAguas.Include(t => t.tbUsuario);
            return View(tbConsumoAguas.ToList());
        }

        // GET: ConsumoAguas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumoAgua tbConsumoAgua = db.tbConsumoAguas.Find(id);
            if (tbConsumoAgua == null)
            {
                return HttpNotFound();
            }
            return View(tbConsumoAgua);
        }

        // GET: ConsumoAguas/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre");
            return View();
        }

        // POST: ConsumoAguas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,Cedula,Promedio_consumo_agua,Consumo_actual_agua")] tbConsumoAgua tbConsumoAgua)
        {
            if (ModelState.IsValid)
            {
                db.tbConsumoAguas.Add(tbConsumoAgua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbConsumoAgua.Cedula);
            return View(tbConsumoAgua);
        }

        // GET: ConsumoAguas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumoAgua tbConsumoAgua = db.tbConsumoAguas.Find(id);
            if (tbConsumoAgua == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbConsumoAgua.Cedula);
            return View(tbConsumoAgua);
        }

        // POST: ConsumoAguas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,Cedula,Promedio_consumo_agua,Consumo_actual_agua")] tbConsumoAgua tbConsumoAgua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbConsumoAgua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbConsumoAgua.Cedula);
            return View(tbConsumoAgua);
        }

        // GET: ConsumoAguas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumoAgua tbConsumoAgua = db.tbConsumoAguas.Find(id);
            if (tbConsumoAgua == null)
            {
                return HttpNotFound();
            }
            return View(tbConsumoAgua);
        }

        // POST: ConsumoAguas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbConsumoAgua tbConsumoAgua = db.tbConsumoAguas.Find(id);
            db.tbConsumoAguas.Remove(tbConsumoAgua);
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
        }
    }
}
