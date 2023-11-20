using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class CristianJWT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refreshtoken_user_IdUserFk",
                table: "refreshtoken");

            migrationBuilder.DropForeignKey(
                name: "FK_userrol_rol_IdRolFk",
                table: "userrol");

            migrationBuilder.DropForeignKey(
                name: "FK_userrol_user_IdUserFk",
                table: "userrol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userrol",
                table: "userrol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshtoken",
                table: "refreshtoken");

            migrationBuilder.RenameTable(
                name: "userrol",
                newName: "userRol");

            migrationBuilder.RenameTable(
                name: "refreshtoken",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "IdRolFk",
                table: "userRol",
                newName: "RolId");

            migrationBuilder.RenameColumn(
                name: "IdUserFk",
                table: "userRol",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_userrol_IdRolFk",
                table: "userRol",
                newName: "IX_userRol_RolId");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "user",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "rol",
                newName: "rolName");

            migrationBuilder.RenameColumn(
                name: "IdUserFk",
                table: "RefreshToken",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_refreshtoken_IdUserFk",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "user",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(225)",
                oldMaxLength: 225)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRol",
                table: "userRol",
                columns: new[] { "UsuarioId", "RolId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_user_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRol_rol_RolId",
                table: "userRol",
                column: "RolId",
                principalTable: "rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRol_user_UsuarioId",
                table: "userRol",
                column: "UsuarioId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_user_UserId",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_userRol_rol_RolId",
                table: "userRol");

            migrationBuilder.DropForeignKey(
                name: "FK_userRol_user_UsuarioId",
                table: "userRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRol",
                table: "userRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "userRol",
                newName: "userrol");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "refreshtoken");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "userrol",
                newName: "IdRolFk");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "userrol",
                newName: "IdUserFk");

            migrationBuilder.RenameIndex(
                name: "IX_userRol_RolId",
                table: "userrol",
                newName: "IX_userrol_IdRolFk");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "user",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "user",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "user",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "rolName",
                table: "rol",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "refreshtoken",
                newName: "IdUserFk");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "refreshtoken",
                newName: "IX_refreshtoken_IdUserFk");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Username",
                keyValue: null,
                column: "Username",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "user",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "user",
                type: "varchar(225)",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userrol",
                table: "userrol",
                columns: new[] { "IdUserFk", "IdRolFk" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshtoken",
                table: "refreshtoken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshtoken_user_IdUserFk",
                table: "refreshtoken",
                column: "IdUserFk",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userrol_rol_IdRolFk",
                table: "userrol",
                column: "IdRolFk",
                principalTable: "rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userrol_user_IdUserFk",
                table: "userrol",
                column: "IdUserFk",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
