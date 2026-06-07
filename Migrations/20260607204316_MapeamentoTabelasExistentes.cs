using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proj_OrionBeacon.Migrations
{
    /// <summary>
    /// Migration de mapeamento: as tabelas TB_* já existem no Oracle (schema RM565568).
    /// Up/Down intencionalmente vazios para não alterar a estrutura do banco.
    /// </summary>
    public partial class MapeamentoTabelasExistentes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
