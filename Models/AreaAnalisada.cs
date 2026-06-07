using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj_OrionBeacon.Models
{
    [Table("TB_AREA_ANALISADA")]
    public class AreaAnalisada
    {
        [Key]
        [Column("ID_AREA")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdArea { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NOME_AREA")]
        public string NomeArea { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("REGIAO")]
        public string Regiao { get; set; } = string.Empty;

        [Column("LATITUDE", TypeName = "NUMBER(10,6)")]
        public decimal? Latitude { get; set; }

        [Column("LONGITUDE", TypeName = "NUMBER(10,6)")]
        public decimal? Longitude { get; set; }

        [Required]
        [Column("ID_CORPO")]
        public int IdCorpo { get; set; }

        [StringLength(100)]
        [Column("TIPO_TERRENO")]
        public string? TipoTerreno { get; set; }

        [Column("SCORE_RANKING", TypeName = "NUMBER(5,2)")]
        public decimal ScoreRanking { get; set; }

        [ForeignKey(nameof(IdCorpo))]
        public CorpoCeleste? CorpoCeleste { get; set; }

        public ICollection<Missao> Missoes { get; set; } = new List<Missao>();
        public ICollection<Analise> Analises { get; set; } = new List<Analise>();
    }
}
