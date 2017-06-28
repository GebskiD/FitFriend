namespace FitFriend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealComponentsUpdatedUserForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "MealComponents_Id", "dbo.MealComponents");
            DropIndex("dbo.Products", new[] { "MealComponents_Id" });
            AddColumn("dbo.MealComponents", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.MealComponents", "UserId");
            AddForeignKey("dbo.MealComponents", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Products", "MealComponents_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MealComponents_Id", c => c.Int());
            DropForeignKey("dbo.MealComponents", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.MealComponents", new[] { "UserId" });
            DropColumn("dbo.MealComponents", "UserId");
            CreateIndex("dbo.Products", "MealComponents_Id");
            AddForeignKey("dbo.Products", "MealComponents_Id", "dbo.MealComponents", "Id");
        }
    }
}
