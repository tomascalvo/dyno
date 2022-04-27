namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', N'tomasrcalvo@gmail.com', 0, N'ADS8V61ZGUpdjfEUFR5kkclc02pqN6amIriak/5axo3nzvM7iX+hLGly9+LjLyxM7Q==', N'2fe4ee84-37ac-41dc-b06d-154b8b64ed93', NULL, 0, 0, NULL, 1, 0, N'tomasrcalvo@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b18365bd-d9f9-43f3-a0fb-3095031f6dfc', N'dominic@dyno.com', 0, N'ADOQblsg84otW9DQlHPDRKl2poswgulgcf5LodZGlpIfs/XIC/BSi6wPUqC/r7x6QQ==', N'ec286855-ea84-4549-9f66-faf56582a6ff', NULL, 0, 0, NULL, 1, 0, N'dominic@dyno.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ceac373c-3600-4c8b-9184-64081e6fcbad', N'guest@vidly.com', 0, N'ADCeCs5RMNTBWmIzW4S3X/4SBSufIAThRRHHZmMoCc1Jk7/sStailSPTGcdxzABIKA==', N'7f82477e-6329-45f1-b8a6-d173c0a448a6', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a16f8309-c8ec-4c5a-979d-fcf922d9f534', N'CanManageSkills')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b18365bd-d9f9-43f3-a0fb-3095031f6dfc', N'a16f8309-c8ec-4c5a-979d-fcf922d9f534')

");
        }

        public override void Down()
        {
        }
    }
}
