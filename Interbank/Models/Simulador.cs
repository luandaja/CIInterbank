using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interbank.Models
{
    #region Colecciones 
    public enum Cuotas
    {
        [Display(Name = "24 Meses")]
        meses24,
        [Display(Name = "36 Meses")]
        meses36
    }
    public enum TipoSeguro
    {
        Individual,
        Macomunado
    }
    public enum TipoCuenta
    {
        Ordinaria,
        Extraordinaria
    }
    public enum TipoCuotaIncial
    {
        [Display(Name = "20%")]
        por20,
        [Display(Name = "30%")]
        por30
    }
    #endregion

    public class Simulador
    {

        public int SimuladorID { get; set; }

        [Required]
        public Cuotas plazo { get; set; }
        [Required]
        public TipoSeguro tipoSeguro { get; set; }
        [Required]
        public TipoCuotaIncial tipoCuotaIncial { get; set; }

        [Required]
        public double montoDelCredito { get; set; }
        public double importeAsegurado { get; set; }

        public double val_deuda { get; set; }
        public double val_CuotaIncial { get; set; }
        //public double val_SeguroDesgravamen { get; set; }
        //public double val_SeguroVehicular { get; set; }
        public int NCuotas { get; set; }

        public float por_CuotaIncial { get; set; }
        public float por_SeguroDesgravamen { get; set; }
        public float por_SeguroVehicular { get; set; }

        public float tea { get; set; }
        public float ted { get; set; }
        public float tem { get; set; }
    }
    public class SimuladorContext : DbContext
    {
        public DbSet<Simulador> Simuladores { get; set; }
    }
}
