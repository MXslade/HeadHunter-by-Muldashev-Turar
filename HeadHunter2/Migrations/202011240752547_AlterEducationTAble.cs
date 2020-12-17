namespace HeadHunter2.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AlterEducationTAble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Educations", "YearOfCompletionTmp", c => c.Int(nullable: false));
            Sql("Update dbo.Educations SET YearOfCompletionTmp = Convert(int, YearOfCompletion)");
            DropColumn("dbo.Educations", "YearOfCompletion");
            RenameColumn("dbo.Educations", "YearOfCompletionTmp", "YearOfCompletion");
        }

        public override void Down()
        {
            AlterColumn("dbo.Educations", "YearOfCompletion", c => c.DateTime(nullable: false));
        }
    }
}
