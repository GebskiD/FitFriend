namespace FitFriend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Carbs = c.Int(nullable: false),
                        Fats = c.Int(nullable: false),
                        Proteins = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
