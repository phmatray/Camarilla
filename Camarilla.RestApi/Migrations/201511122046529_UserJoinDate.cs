namespace Camarilla.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserJoinDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "JoinDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "JoinDate");
        }
    }
}
