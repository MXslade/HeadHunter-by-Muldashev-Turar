namespace HeadHunter2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringJobExperiencesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobExperiences", "EndDate", c => c.DateTime());
            DropColumn("dbo.JobExperiences", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobExperiences", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.JobExperiences", "EndDate");
        }
    }
}
