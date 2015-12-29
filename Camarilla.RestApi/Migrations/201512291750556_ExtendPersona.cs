namespace Camarilla.RestApi.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendPersona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personas", "Pseudo", c => c.String());
            AddColumn("dbo.Personas", "PersonaGender", c => c.Int(nullable: true));
            AddColumn("dbo.Personas", "BirthDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.Personas", "BirthPlace", c => c.String());
            AddColumn("dbo.Personas", "PictureUrl", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Personas", "PictureUrl");
            DropColumn("dbo.Personas", "BirthPlace");
            DropColumn("dbo.Personas", "BirthDate");
            DropColumn("dbo.Personas", "PersonaGender");
            DropColumn("dbo.Personas", "Pseudo");
        }
    }
}
