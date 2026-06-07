using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj_OrionBeacon.Models
{
    [Table("TB_CORPO_CELESTE")]
    public class CorpoCeleste
    {
        [Key]
        [Column("ID_CORPO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCorpo { get; set; }

        [Required]
        [StringLength(50)]
        [Column("NOME")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("DESCRICAO")]
        public string? Descricao { get; set; }

        public ICollection<AreaAnalisada> Areas { get; set; } = new List<AreaAnalisada>();
    }
}
