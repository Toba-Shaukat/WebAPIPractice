using Microsoft.EntityFrameworkCore.Migrations;

namespace AWebAPIPractice.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { 7, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 8, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 9, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 10, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 11, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 12, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 13, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 14, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 15, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 16, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 17, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 18, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 19, "Generic Address", "We Dont know", "Generic IT Company" },
                    { 20, "Generic Address", "We Dont know", "Generic IT Company" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { 6, 26, 7, "Generic Name", "Software Developer" },
                    { 7, 26, 7, "Generic Name", "Software Developer" },
                    { 8, 26, 7, "Generic Name", "Software Developer" },
                    { 9, 26, 7, "Generic Name", "Software Developer" },
                    { 10, 26, 7, "Generic Name", "Software Developer" },
                    { 11, 26, 7, "Generic Name", "Software Developer" },
                    { 12, 26, 7, "Generic Name", "Software Developer" },
                    { 13, 26, 7, "Generic Name", "Software Developer" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
