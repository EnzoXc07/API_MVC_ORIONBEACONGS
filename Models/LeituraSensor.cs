using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj_OrionBeacon.Models
{
    [Table("TB_LEITURA_SENSOR")]
    public class LeituraSensor
    {
        [Key]
        [Column("ID_LEITURA")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdLeitura { get; set; }

        [Required]
        [Column("VALOR_LIDO", TypeName = "NUMBER(10,2)")]
        public decimal ValorLido { get; set; }

        [StringLength(100)]
        [Column("INTERPRETACAO")]
        public string? Interpretacao { get; set; }

        [Required]
        [Column("ID_ANALISE")]
        public int IdAnalise { get; set; }

        [Required]
        [Column("ID_SENSOR")]
        public int IdSensor { get; set; }

        [ForeignKey(nameof(IdAnalise))]
        public Analise? Analise { get; set; }

        [ForeignKey(nameof(IdSensor))]
        public Sensor? Sensor { get; set; }
    }
}
