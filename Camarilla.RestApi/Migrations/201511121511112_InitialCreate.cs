namespace Camarilla.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Background = c.String(),
                        Generation = c.Int(nullable: false),
                        ExperienceActual = c.Int(nullable: false),
                        ExperienceRemaining = c.Int(nullable: false),
                        Nights = c.Int(nullable: false),
                        Willingness = c.Int(nullable: false),
                        Humanity = c.Int(nullable: false),
                        Clan_Id = c.Int(),
                        Mail_Id = c.Int(),
                        Race_Id = c.Int(),
                        Sire_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clans", t => t.Clan_Id)
                .ForeignKey("dbo.Mails", t => t.Mail_Id)
                .ForeignKey("dbo.Races", t => t.Race_Id)
                .ForeignKey("dbo.Personas", t => t.Sire_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Clan_Id)
                .Index(t => t.Mail_Id)
                .Index(t => t.Race_Id)
                .Index(t => t.Sire_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Clans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ClanCategory = c.Int(nullable: false),
                        ClanKind = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonaDisciplines",
                c => new
                    {
                        PersonaId = c.Int(nullable: false),
                        DisciplineId = c.Int(nullable: false),
                        Demonized = c.Boolean(nullable: false),
                        Level = c.Int(nullable: false),
                        Clan_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.PersonaId, t.DisciplineId })
                .ForeignKey("dbo.Clans", t => t.Clan_Id)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.Personas", t => t.PersonaId, cascadeDelete: true)
                .Index(t => t.PersonaId)
                .Index(t => t.DisciplineId)
                .Index(t => t.Clan_Id);
            
            CreateTable(
                "dbo.PersonaMails",
                c => new
                    {
                        PersonaId = c.Int(nullable: false),
                        MailId = c.Int(nullable: false),
                        Read = c.DateTime(),
                        Deleted = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.PersonaId, t.MailId })
                .ForeignKey("dbo.Mails", t => t.MailId, cascadeDelete: true)
                .ForeignKey("dbo.Personas", t => t.PersonaId, cascadeDelete: true)
                .Index(t => t.PersonaId)
                .Index(t => t.MailId);
            
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Message = c.String(),
                        Sent = c.DateTime(nullable: false),
                        From_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.From_Id)
                .Index(t => t.From_Id);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Experience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DisciplineClans",
                c => new
                    {
                        Discipline_Id = c.Int(nullable: false),
                        Clan_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Discipline_Id, t.Clan_Id })
                .ForeignKey("dbo.Disciplines", t => t.Discipline_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clans", t => t.Clan_Id, cascadeDelete: true)
                .Index(t => t.Discipline_Id)
                .Index(t => t.Clan_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Personas", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Personas", "Sire_Id", "dbo.Personas");
            DropForeignKey("dbo.Personas", "Race_Id", "dbo.Races");
            DropForeignKey("dbo.PersonaMails", "PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Personas", "Mail_Id", "dbo.Mails");
            DropForeignKey("dbo.Mails", "From_Id", "dbo.Personas");
            DropForeignKey("dbo.PersonaMails", "MailId", "dbo.Mails");
            DropForeignKey("dbo.Personas", "Clan_Id", "dbo.Clans");
            DropForeignKey("dbo.PersonaDisciplines", "PersonaId", "dbo.Personas");
            DropForeignKey("dbo.PersonaDisciplines", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.PersonaDisciplines", "Clan_Id", "dbo.Clans");
            DropForeignKey("dbo.DisciplineClans", "Clan_Id", "dbo.Clans");
            DropForeignKey("dbo.DisciplineClans", "Discipline_Id", "dbo.Disciplines");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.DisciplineClans", new[] { "Clan_Id" });
            DropIndex("dbo.DisciplineClans", new[] { "Discipline_Id" });
            DropIndex("dbo.Mails", new[] { "From_Id" });
            DropIndex("dbo.PersonaMails", new[] { "MailId" });
            DropIndex("dbo.PersonaMails", new[] { "PersonaId" });
            DropIndex("dbo.PersonaDisciplines", new[] { "Clan_Id" });
            DropIndex("dbo.PersonaDisciplines", new[] { "DisciplineId" });
            DropIndex("dbo.PersonaDisciplines", new[] { "PersonaId" });
            DropIndex("dbo.Personas", new[] { "User_Id" });
            DropIndex("dbo.Personas", new[] { "Sire_Id" });
            DropIndex("dbo.Personas", new[] { "Race_Id" });
            DropIndex("dbo.Personas", new[] { "Mail_Id" });
            DropIndex("dbo.Personas", new[] { "Clan_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.DisciplineClans");
            DropTable("dbo.Races");
            DropTable("dbo.Mails");
            DropTable("dbo.PersonaMails");
            DropTable("dbo.PersonaDisciplines");
            DropTable("dbo.Disciplines");
            DropTable("dbo.Clans");
            DropTable("dbo.Personas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
