namespace HeadHunter2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateJobExperiencesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CvId = c.Int(nullable: false),
                        CompanyName = c.String(),
                        BeginDate = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CvId, cascadeDelete: true)
                .Index(t => t.CvId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobExperiences", "CvId", "dbo.CVs");
            DropIndex("dbo.JobExperiences", new[] { "CvId" });
            DropTable("dbo.JobExperiences");
        }
    }
}
