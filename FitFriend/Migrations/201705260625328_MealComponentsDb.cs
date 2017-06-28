namespace FitFriend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealComponentsDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MealComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        MealNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "MealComponents_Id", c => c.Int());
            CreateIndex("dbo.Products", "MealComponents_Id");
            AddForeignKey("dbo.Products", "MealComponents_Id", "dbo.MealComponents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "MealComponents_Id", "dbo.MealComponents");
            DropIndex("dbo.Products", new[] { "MealComponents_Id" });
            DropColumn("dbo.Products", "MealComponents_Id");
            DropTable("dbo.MealComponents");
        }
    }
}
