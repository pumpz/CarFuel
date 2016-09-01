namespace CarFuel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addusersandcardowner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        DisplayName = c.String(nullable: false),
                        ConsumptionRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.tblCar", "Owner_UserId", c => c.Guid());
            CreateIndex("dbo.tblCar", "Owner_UserId");
            AddForeignKey("dbo.tblCar", "Owner_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCar", "Owner_UserId", "dbo.Users");
            DropIndex("dbo.tblCar", new[] { "Owner_UserId" });
            DropColumn("dbo.tblCar", "Owner_UserId");
            DropTable("dbo.Users");
        }
    }
}
