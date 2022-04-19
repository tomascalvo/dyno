namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedSkillProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "Developer", c => c.String(maxLength: 250));
            AddColumn("dbo.Skills", "RepositoryUrl", c => c.String());
            AddColumn("dbo.Skills", "DocumentationUrl", c => c.String());
            DropColumn("dbo.Skills", "Repository");
            DropColumn("dbo.Skills", "Documentation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Documentation", c => c.String());
            AddColumn("dbo.Skills", "Repository", c => c.String());
            DropColumn("dbo.Skills", "DocumentationUrl");
            DropColumn("dbo.Skills", "RepositoryUrl");
            DropColumn("dbo.Skills", "Developer");
        }
    }
}
