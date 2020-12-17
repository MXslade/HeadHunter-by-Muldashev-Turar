namespace HeadHunter2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringEducationsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Educations", "Level", c => c.String(nullable: false));
            AlterColumn("dbo.Educations", "Place", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Educations", "Place", c => c.String());
            AlterColumn("dbo.Educations", "Level", c => c.String());
        }
    }
}
