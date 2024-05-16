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
    public class EnergiaConsumoesController : Controller
    {
        private ENERGIAEntities db = new ENERGIAEntities();

        // GET: EnergiaConsumoes
        public ActionResult Index()
        {
            var tbEnergiaConsumoes = db.tbEnergiaConsumoes.Include(t => t.tbUsuario);
            return View(tbEnergiaConsumoes.ToList());
        }

        // GET: EnergiaConsumoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergiaConsumo tbEnergiaConsumo = db.tbEnergiaConsumoes.Find(id);
            if (tbEnergiaConsumo == null)
            {
                return HttpNotFound();
            }
            return View(tbEnergiaConsumo);
        }

        // GET: EnergiaConsumoes/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre");
            return View();
        }

        // POST: EnergiaConsumoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,Cedula,Periodo_consumo,Meta_ahorro_energia,Consumo_actual_energia")] tbEnergiaConsumo tbEnergiaConsumo)
        {
            if (ModelState.IsValid)
            {
                db.tbEnergiaConsumoes.Add(tbEnergiaConsumo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbEnergiaConsumo.Cedula);
            return View(tbEnergiaConsumo);
        }

        // GET: EnergiaConsumoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergiaConsumo tbEnergiaConsumo = db.tbEnergiaConsumoes.Find(id);
            if (tbEnergiaConsumo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbEnergiaConsumo.Cedula);
            return View(tbEnergiaConsumo);
        }

        // POST: EnergiaConsumoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,Cedula,Periodo_consumo,Meta_ahorro_energia,Consumo_actual_energia")] tbEnergiaConsumo tbEnergiaConsumo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEnergiaConsumo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.tbUsuarios, "Cedula", "Nombre", tbEnergiaConsumo.Cedula);
            return View(tbEnergiaConsumo);
        }

        // GET: EnergiaConsumoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergiaConsumo tbEnergiaConsumo = db.tbEnergiaConsumoes.Find(id);
            if (tbEnergiaConsumo == null)
            {
                return HttpNotFound();
            }
            return View(tbEnergiaConsumo);
        }

        // POST: EnergiaConsumoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEnergiaConsumo tbEnergiaConsumo = db.tbEnergiaConsumoes.Find(id);
            db.tbEnergiaConsumoes.Remove(tbEnergiaConsumo);
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

        public int CalcularValorPagarEnergia(int cedula)
        {
            var clienteEncontrado = db.tbEnergiaConsumoes.FirstOrDefault(x => x.Cedula == cedula);

            if (clienteEncontrado != null)
            {
                return ValorPagarEnergia(clienteEncontrado.Meta_ahorro_energia, clienteEncontrado.Consumo_actual_energia);
            }
            else
            {
                // Devolvemos un valor predeterminado en caso de que el cliente no sea encontrado
                return 0;
            }
        }

        public int ValorPagarEnergia(int metaAhorroEnergia, int consumoActualEnergia)
        {
            int costoKilovatio = 850;
            int valorParcial = consumoActualEnergia * costoKilovatio;
            int valorIncentivo = (metaAhorroEnergia - consumoActualEnergia) * costoKilovatio;
            return valorParcial - valorIncentivo;
        }

    }
}
