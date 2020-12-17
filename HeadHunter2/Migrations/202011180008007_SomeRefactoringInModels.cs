namespace HeadHunter2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeRefactoringInModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfessionalFieldCVs",
                c => new
                    {
                        ProfessionalField_Id = c.Int(nullable: false),
                        CV_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfessionalField_Id, t.CV_Id })
                .ForeignKey("dbo.ProfessionalFields", t => t.ProfessionalField_Id, cascadeDelete: true)
                .ForeignKey("dbo.CVs", t => t.CV_Id, cascadeDelete: true)
                .Index(t => t.ProfessionalField_Id)
                .Index(t => t.CV_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfessionalFieldCVs", "CV_Id", "dbo.CVs");
            DropForeignKey("dbo.ProfessionalFieldCVs", "ProfessionalField_Id", "dbo.ProfessionalFields");
            DropIndex("dbo.ProfessionalFieldCVs", new[] { "CV_Id" });
            DropIndex("dbo.ProfessionalFieldCVs", new[] { "ProfessionalField_Id" });
            DropTable("dbo.ProfessionalFieldCVs");
        }
    }
}
