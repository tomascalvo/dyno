namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyTitleRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "Title", c => c.String(maxLength: 50));
        }
    }
}
