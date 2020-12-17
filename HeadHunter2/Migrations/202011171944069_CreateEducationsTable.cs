namespace HeadHunter2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEducationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CvId = c.Int(nullable: false),
                        Level = c.String(),
                        Place = c.String(),
                        Faculty = c.String(),
                        Speciality = c.String(),
                        YearOfCompletion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CvId, cascadeDelete: true)
                .Index(t => t.CvId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Educations", "CvId", "dbo.CVs");
            DropIndex("dbo.Educations", new[] { "CvId" });
            DropTable("dbo.Educations");
        }
    }
}
