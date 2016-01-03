namespace Camarilla.RestApi.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                        ClanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonaId, t.DisciplineId })
                .ForeignKey("dbo.Clans", t => t.ClanId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.Personas", t => t.PersonaId, cascadeDelete: true)
                .Index(t => t.PersonaId)
                .Index(t => t.DisciplineId)
                .Index(t => t.ClanId);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pseudo = c.String(),
                        Name = c.String(),
                        PersonaGender = c.Int(),
                        BirthDate = c.DateTime(),
                        BirthPlace = c.String(),
                        Background = c.String(),
                        Generation = c.Int(nullable: false),
                        ExperienceActual = c.Int(nullable: false),
                        ExperienceRemaining = c.Int(nullable: false),
                        Nights = c.Int(nullable: false),
                        Willingness = c.Int(nullable: false),
                        Humanity = c.Int(nullable: false),
                        PictureUrl = c.String(),
                        ClanId = c.Int(),
                        RaceId = c.Int(),
                        SireId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clans", t => t.ClanId)
                .ForeignKey("dbo.Races", t => t.RaceId)
                .ForeignKey("dbo.Personas", t => t.SireId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ClanId)
                .Index(t => t.RaceId)
                .Index(t => t.SireId)
                .Index(t => t.UserId);
            
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
                        FromPseudo = c.String(),
                        ToPseudos = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTime(),
                        JoinDate = c.DateTime(),
                        Gender = c.Int(),
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PersonaDisciplines", "PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Personas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Personas", "SireId", "dbo.Personas");
            DropForeignKey("dbo.Personas", "RaceId", "dbo.Races");
            DropForeignKey("dbo.PersonaMails", "PersonaId", "dbo.Personas");
            DropForeignKey("dbo.PersonaMails", "MailId", "dbo.Mails");
            DropForeignKey("dbo.Personas", "ClanId", "dbo.Clans");
            DropForeignKey("dbo.PersonaDisciplines", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.PersonaDisciplines", "ClanId", "dbo.Clans");
            DropForeignKey("dbo.DisciplineClans", "Clan_Id", "dbo.Clans");
            DropForeignKey("dbo.DisciplineClans", "Discipline_Id", "dbo.Disciplines");
            DropIndex("dbo.DisciplineClans", new[] { "Clan_Id" });
            DropIndex("dbo.DisciplineClans", new[] { "Discipline_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PersonaMails", new[] { "MailId" });
            DropIndex("dbo.PersonaMails", new[] { "PersonaId" });
            DropIndex("dbo.Personas", new[] { "UserId" });
            DropIndex("dbo.Personas", new[] { "SireId" });
            DropIndex("dbo.Personas", new[] { "RaceId" });
            DropIndex("dbo.Personas", new[] { "ClanId" });
            DropIndex("dbo.PersonaDisciplines", new[] { "ClanId" });
            DropIndex("dbo.PersonaDisciplines", new[] { "DisciplineId" });
            DropIndex("dbo.PersonaDisciplines", new[] { "PersonaId" });
            DropTable("dbo.DisciplineClans");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Races");
            DropTable("dbo.Mails");
            DropTable("dbo.PersonaMails");
            DropTable("dbo.Personas");
            DropTable("dbo.PersonaDisciplines");
            DropTable("dbo.Disciplines");
            DropTable("dbo.Clans");
        }
    }
}
