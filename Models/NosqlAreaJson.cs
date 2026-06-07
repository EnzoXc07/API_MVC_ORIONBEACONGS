using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj_OrionBeacon.Models
{
    [Table("TB_NOSQL_AREA_JSON")]
    public class NosqlAreaJson
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DOCUMENTO", TypeName = "CLOB")]
        public string? Documento { get; set; }
    }
}
