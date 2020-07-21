using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthCatalystService.Migrations
{
    public partial class InsertInitialEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"INSERT INTO People (FirstName, LastName, AddressLine1, AddressLine2, City, State, Zipcode, Age, Interests, PicturePath)
                 SELECT 'John', 'Doe', '1 Main St', null, 'Los Angeles', 'CA', '91540', 56, 'Football, Poker', 'Picture\John.jpg' UNION ALL
                 SELECT 'Aaron', 'Dev', '27 Wall St', null, 'New York', 'NY', '11004', 56, 'Baseball', 'Picture\Aaron.jpg' UNION ALL
                 SELECT 'James', 'Bond', '102 Hidden Blvd', 'Apt 101', 'Salt Lake City', 'UT', '91540', 56, null, 'Picture\James.jpg' UNION ALL
                 SELECT 'Samantha', 'Bee', '43 Cross Ave', null, 'San Francisco', 'CA', '52154', 28, 'Cooking, Dancing', 'Picture\Samantha.jpg' 
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
