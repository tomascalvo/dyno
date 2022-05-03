namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRatingAndUrlPropertiesToEmploymentOfferEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentOffers", "Url", c => c.String());
            AddColumn("dbo.EmploymentOffers", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmploymentOffers", "Rating");
            DropColumn("dbo.EmploymentOffers", "Url");
        }
    }
}
