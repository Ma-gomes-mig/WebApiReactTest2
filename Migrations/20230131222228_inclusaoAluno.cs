using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlunosApi.Migrations
{
    /// <inheritdoc />
    public partial class inclusaoAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Alunos(Nome,Email,Idade) VALUES('Reny','Reny_correa@yahoo.com',53)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
