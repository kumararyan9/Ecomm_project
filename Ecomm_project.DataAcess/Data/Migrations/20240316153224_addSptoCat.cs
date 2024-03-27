using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecomm_project.DataAcess.Migrations
{
    public partial class addSptoCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE CreateCategory
                                     @name varchar(50)
                                    AS
	                                insert Categories values(@name)");
            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateCategory
                                     @name varchar(50),
                                       @id int
                                    AS
	                                Update Categories set name=@name where id = @id");
            migrationBuilder.Sql(@"CREATE PROCEDURE DeleteCategory
                                     @id int
                                    AS
	                                delete from Categories where id=@id ");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCategories
                                    AS
	                                select * from Categories ");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCategory
                                     @id int
                                    AS
	                                select * from Categories where id = @id ");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
