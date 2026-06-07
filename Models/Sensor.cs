using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj_OrionBeacon.Models
{
    [Table("TB_SENSOR")]
    public class Sensor
    {
        [Key]
        [Column("ID_SENSOR")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSensor { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NOME_SENSOR")]
        public string NomeSensor { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("TIPO_SENSOR")]
        public string TipoSensor { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("DESCRICAO")]
        public string? Descricao { get; set; }

        public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();
    }
}
