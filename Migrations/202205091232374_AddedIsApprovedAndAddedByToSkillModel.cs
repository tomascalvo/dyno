namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsApprovedAndAddedByToSkillModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Skills", "AddedById", c => c.String(maxLength: 128));
            CreateIndex("dbo.Skills", "AddedById");
            AddForeignKey("dbo.Skills", "AddedById", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "AddedById", "dbo.AspNetUsers");
            DropIndex("dbo.Skills", new[] { "AddedById" });
            DropColumn("dbo.Skills", "AddedById");
            DropColumn("dbo.Skills", "IsApproved");
        }
    }
}
