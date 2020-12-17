namespace HeadHunter2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringJobExperiencesTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobExperiences", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.JobExperiences", "Position", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobExperiences", "Position", c => c.String());
            AlterColumn("dbo.JobExperiences", "CompanyName", c => c.String());
        }
    }
}
