using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainingAPI.Connectors.Migrations
{
    public partial class CreateTrainingPresetAndTrainingGroupWithRelationsMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PersonalizeFit.Training");

            migrationBuilder.CreateTable(
                name: "TrainingPresets",
                schema: "PersonalizeFit.Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PresetDefaultFlag = table.Column<bool>(type: "boolean", nullable: false),
                    TrainerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPresets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentHasTrainingPreset",
                schema: "PersonalizeFit.Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AcquisitionType = table.Column<string>(type: "text", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: false),
                    TrainingPresetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHasTrainingPreset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHasTrainingPreset_TrainingPresets_TrainingPresetId",
                        column: x => x.TrainingPresetId,
                        principalSchema: "PersonalizeFit.Training",
                        principalTable: "TrainingPresets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingGroups",
                schema: "PersonalizeFit.Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainingPresetId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingGroups_TrainingPresets_TrainingPresetId",
                        column: x => x.TrainingPresetId,
                        principalSchema: "PersonalizeFit.Training",
                        principalTable: "TrainingPresets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingGroupHasExercise",
                schema: "PersonalizeFit.Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainingGroupId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false),
                    Observation = table.Column<string>(type: "text", nullable: false),
                    TrainingSetJsonString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingGroupHasExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingGroupHasExercise_TrainingGroups_TrainingGroupId",
                        column: x => x.TrainingGroupId,
                        principalSchema: "PersonalizeFit.Training",
                        principalTable: "TrainingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentHasTrainingPreset_TrainingPresetId",
                schema: "PersonalizeFit.Training",
                table: "StudentHasTrainingPreset",
                column: "TrainingPresetId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingGroupHasExercise_TrainingGroupId",
                schema: "PersonalizeFit.Training",
                table: "TrainingGroupHasExercise",
                column: "TrainingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingGroups_TrainingPresetId",
                schema: "PersonalizeFit.Training",
                table: "TrainingGroups",
                column: "TrainingPresetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentHasTrainingPreset",
                schema: "PersonalizeFit.Training");

            migrationBuilder.DropTable(
                name: "TrainingGroupHasExercise",
                schema: "PersonalizeFit.Training");

            migrationBuilder.DropTable(
                name: "TrainingGroups",
                schema: "PersonalizeFit.Training");

            migrationBuilder.DropTable(
                name: "TrainingPresets",
                schema: "PersonalizeFit.Training");
        }
    }
}
