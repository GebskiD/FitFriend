namespace FitFriend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnRename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "Id", newName: "ProductId");
            AddColumn("dbo.MealComponents", "Quantity", c => c.Single(nullable: false));
            AddColumn("dbo.MealComponents", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.MealComponents", "ProductId");
            AddForeignKey("dbo.MealComponents", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MealComponents", "ProductId", "dbo.Products");
            DropIndex("dbo.MealComponents", new[] { "ProductId" });
            DropColumn("dbo.MealComponents", "ProductId");
            DropColumn("dbo.MealComponents", "Quantity");
            RenameColumn(table: "dbo.Products", name: "ProductId", newName: "Id");
        }
    }
}
