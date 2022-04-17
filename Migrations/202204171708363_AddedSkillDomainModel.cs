namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSkillDomainModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Icon = c.String(),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(),
                        DateAdded = c.DateTime(),
                        Repository = c.String(),
                        Documentation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Skills");
        }
    }
}
