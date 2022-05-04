namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class VincitCompanyTitle : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                UPDATE Companies
                SET Title = 'Vincit'
                WHERE Id = 9;
            ");

        }

        public override void Down()
        {
        }
    }
}
