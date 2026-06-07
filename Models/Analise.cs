using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj_OrionBeacon.Models
{
    [Table("TB_ANALISE")]
    public class Analise
    {
        [Key]
        [Column("ID_ANALISE")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdAnalise { get; set; }

        [Required]
        [Column("DATA_ANALISE")]
        public DateTime DataAnalise { get; set; }

        [Required]
        [StringLength(20)]
        [Column("CLASSIFICACAO_FINAL")]
        public string ClassificacaoFinal { get; set; } = string.Empty;

        [StringLength(500)]
        [Column("OBSERVACOES")]
        public string? Observacoes { get; set; }

        [Required]
        [Column("ID_AREA")]
        public int IdArea { get; set; }

        [ForeignKey(nameof(IdArea))]
        public AreaAnalisada? Area { get; set; }

        public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();
    }
}
