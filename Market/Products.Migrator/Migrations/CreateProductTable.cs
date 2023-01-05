using Common.Entities;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Products.Data.Migrations
{
    [Migration(1)]
    public class CreateProductTable : Migration
    {
        public override void Up()
        {
            Create.Table(nameof(Products))
                .WithColumn(nameof(Product.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(Product.Name)).AsString().NotNullable()
                .WithColumn(nameof(Product.Price)).AsDouble().NotNullable()
                .WithColumn(nameof(Product.AmountLeft)).AsDouble().NotNullable()
                .WithColumn(nameof(Product.UpdatedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(Product.CreatedAt)).AsDateTime().NotNullable()
                .WithColumn(nameof(Product.Deleted)).AsBoolean().NotNullable();

            Insert.IntoTable(nameof(Products))
                .Row(new { Name = "Name1", Price = 100, AmountLeft = 1, CreatedAt = DateTime.UtcNow, Deleted = false }).WithIdentityInsert()
                .Row(new { Name = "Name2", Price = 200, AmountLeft = 2, CreatedAt = DateTime.UtcNow, Deleted = false }).WithIdentityInsert()
                .Row(new { Name = "Name3", Price = 300, AmountLeft = 3, CreatedAt = DateTime.UtcNow, Deleted = false }).WithIdentityInsert();
        }

        public override void Down()
        {
            Delete.Table(nameof(Products));
        }
    }
}
