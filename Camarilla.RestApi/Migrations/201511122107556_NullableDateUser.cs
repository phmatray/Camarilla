namespace Camarilla.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDateUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "JoinDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "JoinDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
        }
    }
}
