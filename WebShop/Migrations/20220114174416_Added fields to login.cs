using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class Addedfieldstologin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ff0ad4f-2881-4d7c-b886-f880788c1d51");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "bca30e6e-d0a9-467d-8bc9-b04b642de320", "0ec1fc05-bac5-4f62-a533-86294d89d3da" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ec1fc05-bac5-4f62-a533-86294d89d3da");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bca30e6e-d0a9-467d-8bc9-b04b642de320");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "495c2840-32bd-4125-9974-9adba4249655", "e0cf3dcf-7fce-4525-bd2d-9e271eacd788", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37301ef6-ab25-4433-a950-b53e5a1973ce", "bedf1bb2-3b34-4eef-85cf-60c4bd8693e8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "City", "FirstName", "LastName", "Street", "ZipCode" },
                values: new object[] { "cf2dc03f-6d80-4ced-88b9-0604c0ecf2f1", 0, "451cd2e1-8fe6-468e-a40b-e2bb8cc9a5bc", "ApplicationUser", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEDZQy9yFbst/mDSdnblBE81WZOH8yblKJ/pm+9DAkbjxZ0lozHE+wgzfC6jlJs7yHA==", "070-123 45 67", false, "cd2eb284-85aa-4c80-bd10-4b6777d98ab0", false, "admin@admin.com", "Göteborg", "Admin", "Adminsson", "Storgatan 3", "123 45" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "cf2dc03f-6d80-4ced-88b9-0604c0ecf2f1", "495c2840-32bd-4125-9974-9adba4249655" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37301ef6-ab25-4433-a950-b53e5a1973ce");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "cf2dc03f-6d80-4ced-88b9-0604c0ecf2f1", "495c2840-32bd-4125-9974-9adba4249655" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "495c2840-32bd-4125-9974-9adba4249655");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf2dc03f-6d80-4ced-88b9-0604c0ecf2f1");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ec1fc05-bac5-4f62-a533-86294d89d3da", "ea942395-0e16-4f12-8c8b-aca967a2aff5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7ff0ad4f-2881-4d7c-b886-f880788c1d51", "c00de314-057e-4fb1-b3bc-c1b6ffdaabf4", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Name" },
                values: new object[] { "bca30e6e-d0a9-467d-8bc9-b04b642de320", 0, "377096fa-a7f9-4869-ba18-7cb0233ecc92", "ApplicationUser", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEHf/yK+VzyIUjRlc97IU8SNjcUa3JGMya8ON3S3bSskWXFf3zAtFyyO2ojVO0iQJiA==", "070-123 45 67", false, "71463433-8b37-4f30-b393-25406b3dd9a2", false, "admin@admin.com", "Admin Adminsson" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "bca30e6e-d0a9-467d-8bc9-b04b642de320", "0ec1fc05-bac5-4f62-a533-86294d89d3da" });
        }
    }
}
