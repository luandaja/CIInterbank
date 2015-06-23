using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InterbankV2.Models;

namespace InterbankV2.Controllers
{
    public class SimuladorsController : Controller
    {
        private UsuariosContext db = new UsuariosContext();
        private int nroDias = 360;
        // GET: Simuladors
        public ActionResult Index(int id)
        {
            var simuladors = db.Simuladors.ToList().FindAll(x=>x.IdCliente == id);
            ViewBag.id = id;
            return View(simuladors.ToList());
        }

        public ActionResult Cronograma(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulador simulador = db.Simuladors.Find(id);
            if (simulador == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Usuarios, "Id", "UserName", simulador.IdCliente);
            return View(simulador);
        }
        // GET: Simuladors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulador simulador = db.Simuladors.Find(id);
            if (simulador == null)
            {
                return HttpNotFound();
            }
            return View(simulador);
        }

        // GET: Simuladors/Create
        public ActionResult Create(int id)
        {
            ViewBag.IdCliente = id;
            return View();
        }

        // POST: Simuladors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SimuladorID,plazo,tipoSeguro,tipoCuotaIncial,montoDelCredito,importeAsegurado,val_deuda,val_CuotaIncial,val_CuotaFinal,NCuotas,por_CuotaIncial,por_SeguroDesgravamen,por_SeguroVehicular,por_cuotaFinal,tea,ted,tem,IdCliente")] Simulador simulador)
        {
            if (ModelState.IsValid)
            {
                calcularValoresImplicitos(simulador);
                db.Simuladors.Add(simulador);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = simulador.IdCliente });
            }

            ViewBag.IdCliente = new SelectList(db.Usuarios, "Id", "UserName", simulador.IdCliente);

            return View(simulador);
        }

        // GET: Simuladors/Edit/5
        public ActionResult Edit(int? id, int idC)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulador simulador = db.Simuladors.Find(id);
            if (simulador == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = idC;
            return View(simulador);
        }

        // POST: Simuladors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SimuladorID,plazo,tipoSeguro,tipoCuotaIncial,montoDelCredito,importeAsegurado,val_deuda,val_CuotaIncial,val_CuotaFinal,NCuotas,por_CuotaIncial,por_SeguroDesgravamen,por_SeguroVehicular,por_cuotaFinal,tea,ted,tem,IdCliente")] Simulador simulador)
        {
            if (ModelState.IsValid)
            {
                calcularValoresImplicitos(simulador);

                db.Entry(simulador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = simulador.IdCliente });

            }
            ViewBag.IdCliente = new SelectList(db.Usuarios, "Id", "UserName", simulador.IdCliente);
            return View(simulador);
        }

        // GET: Simuladors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulador simulador = db.Simuladors.Find(id);
            if (simulador == null)
            {
                return HttpNotFound();
            }
            return View(simulador);
        }

        // POST: Simuladors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Simulador simulador = db.Simuladors.Find(id);
            db.Simuladors.Remove(simulador);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = simulador.IdCliente });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void calcularValoresImplicitos(Simulador s)
        {
            if (s.plazo == Cuotas.meses24)
            {
                s.NCuotas = 24;
            }
            if (s.plazo == Cuotas.meses36)
            {
                s.NCuotas = 36;
            }
            if (s.tipoSeguro == TipoSeguro.Individual)
            {
                s.por_SeguroDesgravamen = 0.035f / 100;
            }
            if (s.tipoSeguro == TipoSeguro.Macomunado)
            {
                s.por_SeguroDesgravamen = 0.065f / 100;
            }
            if (s.tipoCuotaIncial == TipoCuotaIncial.por20)
            {
                s.por_CuotaIncial = 0.20f;
            }
            if (s.tipoCuotaIncial == TipoCuotaIncial.por30)
            {
                s.por_CuotaIncial = 0.30f;
            }
            //s.montoDelCredito
            //s.importeAsegurado
            s.val_CuotaIncial = s.por_CuotaIncial * s.montoDelCredito;
            //s.val_SeguroDesgravamen = 
            //s.val_SeguroVehicular
            //s.NCuotas
            //s.por_CuotaIncial
            //s.por_SeguroDesgravamen
            //s.por_SeguroVehicular
            s.tea = 13.50f / 100;
            s.ted = (float)Math.Pow(1 + s.tea, 1 / (float)nroDias) - 1;
            //s.tem
            s.por_cuotaFinal = 0.50f;
            s.val_CuotaFinal = s.por_cuotaFinal * s.montoDelCredito;
            s.val_deuda = s.montoDelCredito - s.val_CuotaIncial - s.val_CuotaFinal;


        }
        public float calcularTEM(Simulador s, int diasMes)
        {
            return (float)Math.Pow(1 + s.ted, diasMes) - 1;
        }
        public double calcularInteresMensual(Simulador s, int diasMes, double monto)
        {
            float tasa = calcularTEM(s, diasMes);
            return (monto) * tasa;
        }
        public double calcularSeguroDesgMensual(Simulador s, int diasMes, double monto)
        {
            return (monto) * s.por_SeguroDesgravamen * diasMes / 30;
        }
        public double calcularSeguroVehiMensual(Simulador s, int diasMes)
        {
            return s.por_SeguroVehicular / 12 * s.importeAsegurado;
        }
        public double calcularAnualidad(Simulador s, int diasMes)
        {
            float tasa = calcularTEM(s, diasMes) + s.por_SeguroDesgravamen;
            return (s.montoDelCredito * s.por_cuotaFinal * (1 - s.por_CuotaIncial)) * tasa / (1 - Math.Pow(1 + tasa, -s.NCuotas));

        }
        public double ajustarAnualidad(Simulador s, int diasMes)
        {
            float tasa = calcularTEM(s, diasMes) + s.por_SeguroDesgravamen;
            float tasa2 = (float)tasa / (float)(1 - Math.Pow(1 + tasa, -s.NCuotas));
            return (s.montoDelCredito * s.por_cuotaFinal * (1 - s.por_CuotaIncial)) * tasa2 / nroDias;
        }
    }
}
