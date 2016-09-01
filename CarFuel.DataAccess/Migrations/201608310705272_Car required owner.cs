namespace CarFuel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Carrequiredowner : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblCar", "Owner_UserId", "dbo.Users");
            DropIndex("dbo.tblCar", new[] { "Owner_UserId" });

            //Update nullable.
            var zeroId = new Guid();
            Sql($"Update dbo.tblCar SET Owner_UserId='{zeroId}' where Owner_UserId IS NULL");

            AlterColumn("dbo.tblCar", "Owner_UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.tblCar", "Owner_UserId");
            AddForeignKey("dbo.tblCar", "Owner_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCar", "Owner_UserId", "dbo.Users");
            DropIndex("dbo.tblCar", new[] { "Owner_UserId" });
            AlterColumn("dbo.tblCar", "Owner_UserId", c => c.Guid());
            CreateIndex("dbo.tblCar", "Owner_UserId");
            AddForeignKey("dbo.tblCar", "Owner_UserId", "dbo.Users", "UserId");
        }
    }
}
