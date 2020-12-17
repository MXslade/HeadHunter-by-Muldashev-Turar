namespace HeadHunter2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserProfessionalFieldsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfessionalFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CvId = c.Int(nullable: false),
                        ProfessionalFieldId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CvId, cascadeDelete: true)
                .ForeignKey("dbo.ProfessionalFields", t => t.ProfessionalFieldId, cascadeDelete: true)
                .Index(t => t.CvId)
                .Index(t => t.ProfessionalFieldId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfessionalFields", "ProfessionalFieldId", "dbo.ProfessionalFields");
            DropForeignKey("dbo.UserProfessionalFields", "CvId", "dbo.CVs");
            DropIndex("dbo.UserProfessionalFields", new[] { "ProfessionalFieldId" });
            DropIndex("dbo.UserProfessionalFields", new[] { "CvId" });
            DropTable("dbo.UserProfessionalFields");
        }
    }
}
