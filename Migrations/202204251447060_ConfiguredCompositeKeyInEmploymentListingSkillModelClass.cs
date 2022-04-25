namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfiguredCompositeKeyInEmploymentListingSkillModelClass : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EmploymentListingSkills", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmploymentListingSkills", "Id", c => c.Int(nullable: false));
        }
    }
}
