using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj_OrionBeacon.Models
{
    [Table("TB_LOG_ANALISE")]
    public class LogAnalise
    {
        [Key]
        [Column("ID_LOG")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdLog { get; set; }

        [Column("DATA_LOG")]
        public DateTime? DataLog { get; set; }

        [StringLength(200)]
        [Column("MENSAGEM")]
        public string? Mensagem { get; set; }
    }
}
