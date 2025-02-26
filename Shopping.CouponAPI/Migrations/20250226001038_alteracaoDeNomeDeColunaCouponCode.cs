using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.CouponAPI.Migrations
{
    public partial class alteracaoDeNomeDeColunaCouponCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "couponCode",
                table: "coupon",
                newName: "coupon_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "coupon_code",
                table: "coupon",
                newName: "couponCode");
        }
    }
}
