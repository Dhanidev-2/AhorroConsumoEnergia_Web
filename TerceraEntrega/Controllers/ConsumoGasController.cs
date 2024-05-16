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
    public class ConsumoGasController : Controller
    {
        private ENERGIAEntities db = new ENERGIAEntities();

        // GET: ConsumoGas
        public ActionResult Index()
        {
            var tbConsumoGas = db.tbConsumoGas.Include(t => t.tbUsuario);
            return View(tbConsumoGas.ToList());
        }

        // GET: ConsumoGas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumoGa tbConsumoGa = db.tbConsumoGas.Find(id);
            if (tbConsumoGa == null)
            {
                return HttpNotFound();
            }
            return View(tbConsumoGa);
        }

        // GET: ConsumoGas/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre");
            return View();
        }

        // POST: ConsumoGas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,Cedula,Consumo_gas")] tbConsumoGa tbConsumoGa)
        {
            if (ModelState.IsValid)
            {
                db.tbConsumoGas.Add(tbConsumoGa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbConsumoGa.Cedula);
            return View(tbConsumoGa);
        }

        // GET: ConsumoGas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumoGa tbConsumoGa = db.tbConsumoGas.Find(id);
            if (tbConsumoGa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbConsumoGa.Cedula);
            return View(tbConsumoGa);
        }

        // POST: ConsumoGas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,Cedula,Consumo_gas")] tbConsumoGa tbConsumoGa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbConsumoGa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbConsumoGa.Cedula);
            return View(tbConsumoGa);
        }

        // GET: ConsumoGas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumoGa tbConsumoGa = db.tbConsumoGas.Find(id);
            if (tbConsumoGa == null)
            {
                return HttpNotFound();
            }
            return View(tbConsumoGa);
        }

        // POST: ConsumoGas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbConsumoGa tbConsumoGa = db.tbConsumoGas.Find(id);
            db.tbConsumoGas.Remove(tbConsumoGa);
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
