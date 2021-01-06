namespace HeadHunter2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewTypeToCV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CVs", "ViewType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CVs", "ViewType");
        }
    }
}
