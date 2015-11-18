namespace Camarilla.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalGender : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
        }
    }
}
