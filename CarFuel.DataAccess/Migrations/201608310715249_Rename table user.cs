namespace CarFuel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renametableuser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "tblUser");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tblUser", newName: "Users");
        }
    }
}
