using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artisan_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddArtisanSpecialityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtisanSpeciality",
                table: "ArtisanProfiles",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessAddress",
                table: "ArtisanProfiles",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalBio",
                table: "ArtisanProfiles",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtisanSpeciality",
                table: "ArtisanProfiles");

            migrationBuilder.DropColumn(
                name: "BusinessAddress",
                table: "ArtisanProfiles");

            migrationBuilder.DropColumn(
                name: "ProfessionalBio",
                table: "ArtisanProfiles");
        }
    }
}
