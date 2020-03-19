using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XiaoQi.Study.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ButtonInfos",
                columns: table => new
                {
                    ButtonId = table.Column<string>(nullable: false),
                    ButtonName = table.Column<string>(nullable: true),
                    ButtonIcon = table.Column<string>(nullable: true),
                    ButtonDes = table.Column<string>(nullable: true),
                    SetTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ButtonInfos", x => x.ButtonId);
                });

            migrationBuilder.CreateTable(
                name: "MenuButton_Rs",
                columns: table => new
                {
                    MenuButtonId = table.Column<string>(nullable: false),
                    MenuInfoId = table.Column<string>(nullable: true),
                    ButtonId = table.Column<string>(nullable: true),
                    SetTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuButton_Rs", x => x.MenuButtonId);
                });

            migrationBuilder.CreateTable(
                name: "MenuInfos",
                columns: table => new
                {
                    MenuInfoId = table.Column<string>(nullable: false),
                    SelfId = table.Column<string>(nullable: true),
                    FatherId = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    MenuName = table.Column<string>(nullable: true),
                    MenuIcon = table.Column<string>(nullable: true),
                    MenyDes = table.Column<string>(nullable: true),
                    SetTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuInfos", x => x.MenuInfoId);
                });

            migrationBuilder.CreateTable(
                name: "RoleInfos",
                columns: table => new
                {
                    RoleId = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Des = table.Column<string>(nullable: true),
                    SetTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfos", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenu_Rs",
                columns: table => new
                {
                    RoleMenuId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: true),
                    MenuInfoId = table.Column<string>(nullable: true),
                    SetTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenu_Rs", x => x.RoleMenuId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Count = table.Column<string>(nullable: true),
                    Pwd = table.Column<string>(nullable: true),
                    SetTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole_Rs",
                columns: table => new
                {
                    UserRoleId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true),
                    SetTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole_Rs", x => x.UserRoleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ButtonInfos");

            migrationBuilder.DropTable(
                name: "MenuButton_Rs");

            migrationBuilder.DropTable(
                name: "MenuInfos");

            migrationBuilder.DropTable(
                name: "RoleInfos");

            migrationBuilder.DropTable(
                name: "RoleMenu_Rs");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "UserRole_Rs");
        }
    }
}
