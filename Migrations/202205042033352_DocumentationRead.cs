namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentationRead : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentationReads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateRead = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SkillId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentationReads", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DocumentationReads", "SkillId", "dbo.Skills");
            DropIndex("dbo.DocumentationReads", new[] { "SkillId" });
            DropIndex("dbo.DocumentationReads", new[] { "UserId" });
            DropTable("dbo.DocumentationReads");
        }
    }
}
