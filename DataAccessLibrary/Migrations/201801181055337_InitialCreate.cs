namespace DataAccessLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accountbalance",
                c => new
                    {
                        year = c.Int(nullable: false),
                        month = c.Int(nullable: false),
                        rnd = c.Double(),
                        canteen = c.Double(),
                        ceocar = c.Double(),
                        marketing = c.Double(),
                        parking = c.Double(),
                        uid = c.Int(),
                    })
                .PrimaryKey(t => new { t.year, t.month });
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        uid = c.Int(nullable: false, identity: true),
                        username = c.String(maxLength: 200, unicode: false),
                        password = c.String(maxLength: 200, unicode: false),
                        userType = c.Int(),
                        fname = c.String(maxLength: 200, unicode: false),
                        lname = c.String(maxLength: 200, unicode: false),
                        salt = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.uid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.user");
            DropTable("dbo.accountbalance");
        }
    }
}
