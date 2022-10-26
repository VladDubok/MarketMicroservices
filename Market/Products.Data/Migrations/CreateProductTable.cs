using FluentMigrator;

namespace Products.Data.Migrations
{
    [Migration(1)]
    public class CreateProductTable : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Price").AsDouble().NotNullable()
                .WithColumn("AmountLeft").AsDouble().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().Nullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}
