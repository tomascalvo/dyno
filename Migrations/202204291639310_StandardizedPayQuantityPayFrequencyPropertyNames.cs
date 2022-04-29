namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandardizedPayQuantityPayFrequencyPropertyNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentOffers", "PayQuantity", c => c.Decimal(storeType: "money"));
            DropColumn("dbo.EmploymentOffers", "PaymentQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmploymentOffers", "PaymentQuantity", c => c.Decimal(storeType: "money"));
            DropColumn("dbo.EmploymentOffers", "PayQuantity");
        }
    }
}
