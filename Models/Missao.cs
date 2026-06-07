using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj_OrionBeacon.Models
{
    [Table("TB_MISSAO")]
    public class Missao
    {
        [Key]
        [Column("ID_MISSAO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMissao { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NOME_MISSAO")]
        public string NomeMissao { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column("STATUS")]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Column("ID_AREA")]
        public int IdArea { get; set; }

        [Column("DATA_INICIO")]
        public DateTime? DataInicio { get; set; }

        [Column("DATA_FIM")]
        public DateTime? DataFim { get; set; }

        [ForeignKey(nameof(IdArea))]
        public AreaAnalisada? Area { get; set; }
    }
}
