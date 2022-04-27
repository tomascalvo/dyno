namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCompanyPropertyIsStaffingCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "IsStaffingCompany", c => c.Boolean(nullable: false));

            Sql(@"
                UPDATE Companies
                SET IsStaffingCompany = 0
                WHERE Id BETWEEN 1 AND 3;
            ");
        }

        public override void Down()
        {
            DropColumn("dbo.Companies", "IsStaffingCompany");
        }
    }
}
