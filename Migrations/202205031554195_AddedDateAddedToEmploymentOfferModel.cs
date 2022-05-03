namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateAddedToEmploymentOfferModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentOffers", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmploymentOffers", "DateOffered", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmploymentOffers", "DateOffered", c => c.DateTime(nullable: false));
            DropColumn("dbo.EmploymentOffers", "DateAdded");
        }
    }
}
