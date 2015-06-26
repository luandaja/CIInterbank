using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.EntityClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace InterbankV2.Models
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
        [Display(Name = "Nro de cuotas")]
        public Cuotas plazo { get; set; }
        [Required]
        [Display(Name = "Tipo de seguro")]
        public TipoSeguro tipoSeguro { get; set; }
        [Required]
        [Display(Name = "Cuota inicial")]
        public TipoCuotaIncial tipoCuotaIncial { get; set; }

        [Required]
        [Display(Name = "Monto del crédito")]
        [Range(18000, 240000, ErrorMessage = "El valor debe estar entre 18 mil y 240 mil")]
        public double montoDelCredito { get; set; }

        [Display(Name = "Monto asegurado")]
        public double importeAsegurado { get; set; }

        [Display(Name = "Valor de deuda")]
        public double val_deuda { get; set; }
        [Display(Name = "Valor de la cuota incial")]
        public double val_CuotaIncial { get; set; }
        [Display(Name = "Valor de la cuota final")]
        public double val_CuotaFinal { get; set; }

        //public double val_SeguroDesgravamen { get; set; }
        //public double val_SeguroVehicular { get; set; }
        public int NCuotas { get; set; }

        public float por_CuotaIncial { get; set; }
        [Display(Name = "Tasa seguro de desgravamen")]
        public float por_SeguroDesgravamen { get; set; }
        [Display(Name = "Tasa seguro vehicular")]
        public float por_SeguroVehicular { get; set; }
        public float por_cuotaFinal { get; set; }

        [Display(Name = "TEA")]
        public float tea { get; set; }
        [Display(Name = "TED")]
        public float ted { get; set; }
        [Display(Name = "TEM")]
        public float tem { get; set; }

        [ForeignKey("cliente")]
        public int IdCliente { get; set; }
        public virtual Usuario cliente { get; set; }
    }
}
