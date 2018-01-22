namespace Dentist.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reworkall : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DentistStudios",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hour = c.DateTime(nullable: false),
                        DentistId = c.String(),
                        PatientId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Years", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "StudioId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "StudioId");
            AddForeignKey("dbo.AspNetUsers", "StudioId", "dbo.DentistStudios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "StudioId", "dbo.DentistStudios");
            DropIndex("dbo.AspNetUsers", new[] { "StudioId" });
            DropColumn("dbo.AspNetUsers", "StudioId");
            DropColumn("dbo.AspNetUsers", "Years");
            DropTable("dbo.Hours");
            DropTable("dbo.DentistStudios");
        }
    }
}
