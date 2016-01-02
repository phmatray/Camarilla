namespace Camarilla.RestApi.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForeignKeys : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Personas", name: "Clan_Id", newName: "ClanId");
            RenameColumn(table: "dbo.Personas", name: "Race_Id", newName: "RaceId");
            RenameColumn(table: "dbo.Personas", name: "Sire_Id", newName: "SireId");
            RenameColumn(table: "dbo.Personas", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Mails", name: "From_Id", newName: "FromId");
            RenameIndex(table: "dbo.Personas", name: "IX_Clan_Id", newName: "IX_ClanId");
            RenameIndex(table: "dbo.Personas", name: "IX_Race_Id", newName: "IX_RaceId");
            RenameIndex(table: "dbo.Personas", name: "IX_Sire_Id", newName: "IX_SireId");
            RenameIndex(table: "dbo.Personas", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Mails", name: "IX_From_Id", newName: "IX_FromId");
            AddColumn("dbo.Mails", "Persona_Id", c => c.Int());
            CreateIndex("dbo.Mails", "Persona_Id");
            AddForeignKey("dbo.Mails", "Persona_Id", "dbo.Personas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mails", "Persona_Id", "dbo.Personas");
            DropIndex("dbo.Mails", new[] { "Persona_Id" });
            DropColumn("dbo.Mails", "Persona_Id");
            RenameIndex(table: "dbo.Mails", name: "IX_FromId", newName: "IX_From_Id");
            RenameIndex(table: "dbo.Personas", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Personas", name: "IX_SireId", newName: "IX_Sire_Id");
            RenameIndex(table: "dbo.Personas", name: "IX_RaceId", newName: "IX_Race_Id");
            RenameIndex(table: "dbo.Personas", name: "IX_ClanId", newName: "IX_Clan_Id");
            RenameColumn(table: "dbo.Mails", name: "FromId", newName: "From_Id");
            RenameColumn(table: "dbo.Personas", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Personas", name: "SireId", newName: "Sire_Id");
            RenameColumn(table: "dbo.Personas", name: "RaceId", newName: "Race_Id");
            RenameColumn(table: "dbo.Personas", name: "ClanId", newName: "Clan_Id");
        }
    }
}
