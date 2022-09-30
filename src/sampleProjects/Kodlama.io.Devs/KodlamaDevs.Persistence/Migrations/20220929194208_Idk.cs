using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KodlamaDevs.Persistence.Migrations
{
    public partial class Idk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOperationClaims_OperationClaims_OperationClaimsId",
                table: "UserOperationClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOperationClaims_Users_UsersId",
                table: "UserOperationClaims");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "UserOperationClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "OperationClaimsId",
                table: "UserOperationClaims",
                newName: "OperationClaimId");

            migrationBuilder.RenameIndex(
                name: "IX_UserOperationClaims_UsersId",
                table: "UserOperationClaims",
                newName: "IX_UserOperationClaims_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId",
                principalTable: "OperationClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperationClaims_Users_UserId",
                table: "UserOperationClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                table: "UserOperationClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOperationClaims_Users_UserId",
                table: "UserOperationClaims");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserOperationClaims",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "OperationClaimId",
                table: "UserOperationClaims",
                newName: "OperationClaimsId");

            migrationBuilder.RenameIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                newName: "IX_UserOperationClaims_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperationClaims_OperationClaims_OperationClaimsId",
                table: "UserOperationClaims",
                column: "OperationClaimsId",
                principalTable: "OperationClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperationClaims_Users_UsersId",
                table: "UserOperationClaims",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
