using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QrCodeGeneratorWebAppMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddQrCodeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QrCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardNickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardOrg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardOrgTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardMobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardWorkPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardBirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VCardWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardHouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardStateRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCardNote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QrCode", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QrCode");
        }
    }
}
