namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateArchivedToEmploymentListing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentListings", "DateArchived", c => c.DateTime());
            DropColumn("dbo.EmploymentListings", "DatePublished");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmploymentListings", "DatePublished", c => c.DateTime());
            DropColumn("dbo.EmploymentListings", "DateArchived");
        }
    }
}
