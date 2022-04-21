namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredProjectSkillRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectSkills", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectSkills", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectSkills", "DateAdded");
            DropColumn("dbo.ProjectSkills", "Id");
        }
    }
}
