namespace CarFuel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCar",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblFillUp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IS_FULL = c.Boolean(nullable: false),
                        Liters = c.Double(nullable: false),
                        Odometer = c.Int(nullable: false),
                        NextFillUp_Id = c.Int(),
                        Car_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblFillUp", t => t.NextFillUp_Id)
                .ForeignKey("dbo.tblCar", t => t.Car_Id)
                .Index(t => t.NextFillUp_Id)
                .Index(t => t.Car_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFillUp", "Car_Id", "dbo.tblCar");
            DropForeignKey("dbo.tblFillUp", "NextFillUp_Id", "dbo.tblFillUp");
            DropIndex("dbo.tblFillUp", new[] { "Car_Id" });
            DropIndex("dbo.tblFillUp", new[] { "NextFillUp_Id" });
            DropTable("dbo.tblFillUp");
            DropTable("dbo.tblCar");
        }
    }
}
