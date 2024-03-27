using Ecomm_project.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecomm_project.DataAcess.Migrations
{
    public partial class AddSptocov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE CreateCov
                                     @name varchar(50)
                                    AS
	                                insert CoverTypes values(@name)");
            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateCov
                                     @name varchar(50),
                                       @id int
                                    AS
	                                Update CoverTypes set name=@name where id = @id");
            migrationBuilder.Sql(@"CREATE PROCEDURE DeleteCov
                                     @id int
                                    AS
	                                delete from CoverTypes where id=@id ");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCovertypes
                                    AS
	                                select * from CoverTypes ");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCovertype
                                     @id int
                                    AS
	                                select * from CoverTypes where id = @id ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
