namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoalEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        DateAchieved = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GoalSkills",
                c => new
                    {
                        GoalRefId = c.Int(nullable: false),
                        SkillRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GoalRefId, t.SkillRefId })
                .ForeignKey("dbo.Goals", t => t.GoalRefId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillRefId, cascadeDelete: true)
                .Index(t => t.GoalRefId)
                .Index(t => t.SkillRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goals", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GoalSkills", "SkillRefId", "dbo.Skills");
            DropForeignKey("dbo.GoalSkills", "GoalRefId", "dbo.Goals");
            DropIndex("dbo.GoalSkills", new[] { "SkillRefId" });
            DropIndex("dbo.GoalSkills", new[] { "GoalRefId" });
            DropIndex("dbo.Goals", new[] { "UserId" });
            DropTable("dbo.GoalSkills");
            DropTable("dbo.Goals");
        }
    }
}
