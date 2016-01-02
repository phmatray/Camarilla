namespace Camarilla.RestApi.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMailbox : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mailboxes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.PersonaMails", "Mailbox_Id", c => c.Int());
            CreateIndex("dbo.PersonaMails", "Mailbox_Id");
            AddForeignKey("dbo.PersonaMails", "Mailbox_Id", "dbo.Mailboxes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonaMails", "Mailbox_Id", "dbo.Mailboxes");
            DropForeignKey("dbo.Mailboxes", "Id", "dbo.Personas");
            DropIndex("dbo.PersonaMails", new[] { "Mailbox_Id" });
            DropIndex("dbo.Mailboxes", new[] { "Id" });
            DropColumn("dbo.PersonaMails", "Mailbox_Id");
            DropTable("dbo.Mailboxes");
        }
    }
}
