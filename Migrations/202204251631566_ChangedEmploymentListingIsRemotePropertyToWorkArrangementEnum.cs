namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedEmploymentListingIsRemotePropertyToWorkArrangementEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentListings", "WorkLocation", c => c.Int());
            DropColumn("dbo.EmploymentListings", "IsRemote");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmploymentListings", "IsRemote", c => c.Boolean());
            DropColumn("dbo.EmploymentListings", "WorkLocation");
        }
    }
}
