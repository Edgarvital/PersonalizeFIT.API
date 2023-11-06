using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExerciseAPI.Connectors.Migrations
{
    public partial class CreateExerciseAndMuscularGroupMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PersonalizeFit.Exercise");

            migrationBuilder.CreateTable(
                name: "MuscularGroups",
                schema: "PersonalizeFit.Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscularGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                schema: "PersonalizeFit.Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    TrainerId = table.Column<string>(type: "text", nullable: false),
                    MuscularGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_MuscularGroups_MuscularGroupId",
                        column: x => x.MuscularGroupId,
                        principalSchema: "PersonalizeFit.Exercise",
                        principalTable: "MuscularGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseHasEquivalentExercise",
                schema: "PersonalizeFit.Exercise",
                columns: table => new
                {
                    EquivalentExerciseId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseHasEquivalentExercise", x => new { x.EquivalentExerciseId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_ExerciseHasEquivalentExercise_Exercises_EquivalentExerciseId",
                        column: x => x.EquivalentExerciseId,
                        principalSchema: "PersonalizeFit.Exercise",
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseHasEquivalentExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "PersonalizeFit.Exercise",
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseHasEquivalentExercise_ExerciseId",
                schema: "PersonalizeFit.Exercise",
                table: "ExerciseHasEquivalentExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_MuscularGroupId",
                schema: "PersonalizeFit.Exercise",
                table: "Exercises",
                column: "MuscularGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseHasEquivalentExercise",
                schema: "PersonalizeFit.Exercise");

            migrationBuilder.DropTable(
                name: "Exercises",
                schema: "PersonalizeFit.Exercise");

            migrationBuilder.DropTable(
                name: "MuscularGroups",
                schema: "PersonalizeFit.Exercise");
        }
    }
}
