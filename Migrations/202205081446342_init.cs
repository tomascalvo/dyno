namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserProjects",
                c => new
                {
                    ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    ProjectId = c.Int(nullable: false),
                    CanEdit = c.Boolean(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.ApplicationUserId, t.ProjectId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ProjectId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Avatar = c.String(),
                    GitHubUsername = c.String(),
                    ColorPreference = c.Int(),
                    CurrentRoleId = c.Int(),
                    DesiredRoleId = c.Int(),
                    SignupDate = c.DateTime(nullable: false),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.Certifications",
                c => new
                {
                    RecipientId = c.String(nullable: false, maxLength: 128),
                    CertificationTypeId = c.Int(nullable: false),
                    Url = c.String(),
                    Image = c.String(),
                    DateAdded = c.DateTime(nullable: false),
                    DateAwarded = c.DateTime(nullable: false),
                    ProjectId = c.Int(),
                })
                .PrimaryKey(t => new { t.RecipientId, t.CertificationTypeId })
                .ForeignKey("dbo.CertificationTypes", t => t.CertificationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId, cascadeDelete: true)
                .Index(t => t.RecipientId)
                .Index(t => t.CertificationTypeId)
                .Index(t => t.ProjectId);

            CreateTable(
                "dbo.CertificationTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 50),
                    Description = c.String(),
                    Url = c.String(),
                    IsAward = c.Boolean(nullable: false),
                    IsApproved = c.Boolean(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    DateApproved = c.DateTime(nullable: false),
                    AddedById = c.String(maxLength: 128),
                    PlatformId = c.Int(nullable: false),
                    CourseId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Platforms", t => t.PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .Index(t => t.AddedById)
                .Index(t => t.PlatformId)
                .Index(t => t.CourseId);

            CreateTable(
                "dbo.Courses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                    Image = c.String(),
                    Url = c.String(),
                    IsApproved = c.Boolean(nullable: false),
                    IsTutorial = c.Boolean(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    AddedById = c.String(maxLength: 128),
                    PlatformId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Platforms", t => t.PlatformId)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .Index(t => t.AddedById)
                .Index(t => t.PlatformId);

            CreateTable(
                "dbo.CourseCompletions",
                c => new
                {
                    CourseId = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                    DateAdded = c.DateTime(nullable: false),
                    DateCompleted = c.DateTime(nullable: false),
                    Rating = c.Int(),
                    Comment = c.String(),
                })
                .PrimaryKey(t => new { t.CourseId, t.UserId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Projects",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 50),
                    Description = c.String(),
                    Icon = c.String(),
                    RepositoryUrl = c.String(),
                    DeploymentUrl = c.String(),
                    DateAdded = c.DateTime(nullable: false),
                    AddedById = c.String(maxLength: 128),
                    ContractBidId = c.Int(),
                    EmploymentId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .ForeignKey("dbo.Employments", t => t.EmploymentId)
                .Index(t => t.AddedById)
                .Index(t => t.EmploymentId);

            CreateTable(
                "dbo.ContractBids",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Description = c.String(),
                    ListingId = c.Int(nullable: false),
                    ContractorId = c.Int(nullable: false),
                    ProjectId = c.Int(nullable: false),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    DateDue = c.DateTime(),
                    DateDeclined = c.DateTime(),
                    Contractor_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Contractor_Id)
                .ForeignKey("dbo.ContractListings", t => t.ListingId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.ListingId)
                .Index(t => t.Contractor_Id);

            CreateTable(
                "dbo.Contracts",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Description = c.String(),
                    ContractBidId = c.Int(nullable: false),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    DateArchived = c.DateTime(),
                    DateDue = c.DateTime(),
                    DateDelivered = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractBids", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.ContractListings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ContractId = c.Int(nullable: false),
                    Url = c.String(),
                    Description = c.String(),
                    Specifications = c.String(),
                    ClientId = c.Int(nullable: false),
                    PlatformId = c.Int(nullable: false),
                    AddedById = c.String(maxLength: 128),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    DateArchived = c.DateTime(),
                    DateDue = c.DateTime(),
                    Client_Id = c.Int(),
                    Client_Id1 = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id1)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Platforms", t => t.PlatformId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.PlatformId)
                .Index(t => t.AddedById)
                .Index(t => t.Client_Id)
                .Index(t => t.Client_Id1);

            CreateTable(
                "dbo.Clients",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(maxLength: 50),
                    Details = c.String(),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    LastName = c.String(nullable: false, maxLength: 50),
                    CurrentCompanyId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CurrentCompanyId)
                .Index(t => t.CurrentCompanyId);

            CreateTable(
                "dbo.Companies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 50),
                    Description = c.String(),
                    Logo = c.String(),
                    WebsiteUrl = c.String(),
                    IsStaffingCompany = c.Boolean(nullable: false),
                    OrganizationLookupId = c.String(),
                    Country = c.String(),
                    StateProvince = c.String(),
                    City = c.String(),
                    DateFounded = c.DateTime(),
                    DateAdded = c.DateTime(nullable: false),
                    Client_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);

            CreateTable(
                "dbo.EmploymentListings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 50),
                    Url = c.String(),
                    FullText = c.String(),
                    WorkLocation = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    DateArchived = c.DateTime(),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    CreatorId = c.String(maxLength: 128),
                    ClientCompanyId = c.Int(),
                    StaffingCompanyId = c.Int(),
                    RecruiterId = c.Int(),
                    PlatformId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.ClientCompanyId)
                .ForeignKey("dbo.Recruiters", t => t.RecruiterId)
                .ForeignKey("dbo.Platforms", t => t.PlatformId)
                .ForeignKey("dbo.Companies", t => t.StaffingCompanyId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .Index(t => t.CreatorId)
                .Index(t => t.ClientCompanyId)
                .Index(t => t.StaffingCompanyId)
                .Index(t => t.RecruiterId)
                .Index(t => t.PlatformId);

            CreateTable(
                "dbo.EmploymentApplications",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CoverLetter = c.String(),
                    DateApplied = c.DateTime(nullable: false),
                    Comment = c.String(),
                    Rating = c.Int(),
                    ApplicantId = c.String(nullable: false, maxLength: 128),
                    EmploymentListingId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.EmploymentListingId);

            CreateTable(
                "dbo.EmploymentListingAccesses",
                c => new
                {
                    ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    EmploymentListingId = c.Int(nullable: false),
                    DateCreated = c.DateTime(nullable: false),
                    CanEdit = c.Boolean(nullable: false),
                    CanArchive = c.Boolean(nullable: false),
                    CanDelete = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => new { t.ApplicationUserId, t.EmploymentListingId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.EmploymentListingId);

            CreateTable(
                "dbo.EmploymentListingSkills",
                c => new
                {
                    EmploymentListingId = c.Int(nullable: false),
                    SkillId = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.EmploymentListingId, t.SkillId })
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.EmploymentListingId)
                .Index(t => t.SkillId);

            CreateTable(
                "dbo.Skills",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Icon = c.String(),
                    Title = c.String(nullable: false, maxLength: 50),
                    Description = c.String(),
                    Developer = c.String(maxLength: 250),
                    RepositoryUrl = c.String(),
                    DocumentationUrl = c.String(),
                    ReleaseDate = c.DateTime(),
                    DateAdded = c.DateTime(),
                    ContractListing_Id = c.Int(),
                    CertificationType_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractListings", t => t.ContractListing_Id)
                .ForeignKey("dbo.CertificationTypes", t => t.CertificationType_Id)
                .Index(t => t.ContractListing_Id)
                .Index(t => t.CertificationType_Id);

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

            CreateTable(
                "dbo.EmploymentSkills",
                c => new
                {
                    EmploymentId = c.Int(nullable: false),
                    SkillId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.EmploymentId, t.SkillId })
                .ForeignKey("dbo.Employments", t => t.EmploymentId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.EmploymentId)
                .Index(t => t.SkillId);

            CreateTable(
                "dbo.Employments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    InitialPay = c.Decimal(storeType: "money"),
                    FinalPay = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    StartDate = c.DateTime(),
                    QuitDate = c.DateTime(),
                    Country = c.String(),
                    StateProvince = c.String(),
                    City = c.String(),
                    EmployeeId = c.String(nullable: false, maxLength: 128),
                    EmployerId = c.Int(nullable: false),
                    OfferId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.EmployerId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentOffers", t => t.OfferId)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.EmployerId)
                .Index(t => t.OfferId);

            CreateTable(
                "dbo.EmploymentProjects",
                c => new
                {
                    EmploymentId = c.Int(nullable: false),
                    ProjectId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.EmploymentId, t.ProjectId })
                .ForeignKey("dbo.Employments", t => t.EmploymentId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.EmploymentId)
                .Index(t => t.ProjectId);

            CreateTable(
                "dbo.EmploymentOffers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FullText = c.String(),
                    Url = c.String(),
                    Terms = c.String(),
                    Benefits = c.String(),
                    Rating = c.Int(),
                    DateOffered = c.DateTime(),
                    DateAdded = c.DateTime(nullable: false),
                    StartDate = c.DateTime(),
                    Expiration = c.DateTime(),
                    Accepted = c.DateTime(),
                    Declined = c.DateTime(),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    RecipientId = c.String(nullable: false, maxLength: 128),
                    EmploymentListingId = c.Int(nullable: false),
                    RecruiterId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recruiters", t => t.RecruiterId)
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId, cascadeDelete: true)
                .Index(t => t.RecipientId)
                .Index(t => t.EmploymentListingId)
                .Index(t => t.RecruiterId);

            CreateTable(
                "dbo.Recruiters",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(maxLength: 50),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    LastName = c.String(nullable: false, maxLength: 50),
                    StaffingCompanyId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.StaffingCompanyId)
                .Index(t => t.StaffingCompanyId);

            CreateTable(
                "dbo.RecruiterClients",
                c => new
                {
                    RecruiterId = c.Int(nullable: false),
                    CompanyId = c.Int(nullable: false),
                    DateCreated = c.DateTime(nullable: false),
                    CreatorId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => new { t.RecruiterId, t.CompanyId })
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Recruiters", t => t.RecruiterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .Index(t => t.RecruiterId)
                .Index(t => t.CompanyId)
                .Index(t => t.CreatorId);

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
                "dbo.SkillHierarchies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Description = c.String(),
                    DateAdded = c.DateTime(nullable: false),
                    IsApproved = c.Boolean(nullable: false),
                    PrincipalId = c.Int(nullable: false),
                    CreatorId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skills", t => t.PrincipalId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .Index(t => t.PrincipalId)
                .Index(t => t.CreatorId);

            CreateTable(
                "dbo.SkillHierarchyPrerequisites",
                c => new
                {
                    SkillHierarchyId = c.Int(nullable: false),
                    PrerequisiteId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.SkillHierarchyId, t.PrerequisiteId })
                .ForeignKey("dbo.Skills", t => t.PrerequisiteId, cascadeDelete: true)
                .ForeignKey("dbo.SkillHierarchies", t => t.SkillHierarchyId, cascadeDelete: true)
                .Index(t => t.SkillHierarchyId)
                .Index(t => t.PrerequisiteId);

            CreateTable(
                "dbo.ProjectSkills",
                c => new
                {
                    ProjectId = c.Int(nullable: false),
                    SkillId = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.ProjectId, t.SkillId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.SkillId);

            CreateTable(
                "dbo.Platforms",
                c => new
                {
                    Id = c.Int(nullable: false),
                    IsApproved = c.Boolean(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    AddedById = c.String(maxLength: 128),
                    CompanyId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .Index(t => t.Id)
                .Index(t => t.AddedById);

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

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

            CreateTable(
                "dbo.ProjectCourseCompletions",
                c => new
                {
                    Project_Id = c.Int(nullable: false),
                    CourseCompletion_CourseId = c.Int(nullable: false),
                    CourseCompletion_UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.Project_Id, t.CourseCompletion_CourseId, t.CourseCompletion_UserId })
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.CourseCompletions", t => new { t.CourseCompletion_CourseId, t.CourseCompletion_UserId }, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => new { t.CourseCompletion_CourseId, t.CourseCompletion_UserId });

            CreateTable(
                "dbo.CourseSkills",
                c => new
                {
                    CourseRefId = c.Int(nullable: false),
                    SkillRefId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.CourseRefId, t.SkillRefId })
                .ForeignKey("dbo.Courses", t => t.CourseRefId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillRefId, cascadeDelete: true)
                .Index(t => t.CourseRefId)
                .Index(t => t.SkillRefId);

            // SEED DATA
            Sql(@"
                SET IDENTITY_INSERT [dbo].[Companies] ON 
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId], [IsStaffingCompany]) VALUES (3, N'Dice', N'Tech Recruiter', NULL, N'https://www.dice.com/hiring/post-jobs?utm_source=google&utm_medium=cpc&utm_campaign=Search%20-%20-DSA%20Campaign&gclid=CjwKCAjwjZmTBhB4EiwAynRmDzv7wPgIoAyfleS3khLrWNkuu--sGo9h1Xezrq4jevkmanJYAf6gJxoCRlYQAvD_BwE', NULL, NULL, NULL, NULL, CAST(N'2022-04-25T07:39:00.000' AS DateTime), NULL, 1)
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId], [IsStaffingCompany]) VALUES (5, N'Skuid', N'Software Company', NULL, N'https://www.skuid.com/', NULL, NULL, N'Chattanooga', NULL, CAST(N'2022-04-25T07:40:00.000' AS DateTime), NULL, 0)
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId], [IsStaffingCompany]) VALUES (7, N'CodeScience', N'Software Company', NULL, N'https://www.codescience.com/?utm_source=google&utm_medium=gmb&utm_campaign=traffic&utm_content=website&utm_term=link', NULL, NULL, N'Chattanooga', NULL, CAST(N'2022-04-25T07:41:00.000' AS DateTime), NULL, 0)
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId], [IsStaffingCompany]) VALUES (8, N'Allied OneSource', NULL, NULL, N'AlliedOneSource.com', NULL, NULL, NULL, NULL, CAST(N'2022-05-03T01:48:47.480' AS DateTime), NULL, 1)
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId], [IsStaffingCompany]) VALUES (9, N'Insight Global', NULL, NULL, N'https://insightglobal.com/', NULL, NULL, NULL, NULL, CAST(N'2022-05-04T01:55:53.477' AS DateTime), NULL, 1)
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId], [IsStaffingCompany]) VALUES (10, N'Cognizant', N'Cognizant is an American multinational information technology services and consulting company. It is headquartered in Teaneck, New Jersey, United States. Cognizant is part of the NASDAQ-100 and trades under CTSH. It was founded as an in-house technology unit of Dun & Bradstreet in 1994, and started serving external clients in 1996.', NULL, N'https://www.cognizant.com/', NULL, NULL, NULL, NULL, CAST(N'2022-05-04T11:08:19.210' AS DateTime), NULL, 0)
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId], [IsStaffingCompany]) VALUES (11, N'The Vincit Group', N'The Vincit Group is a vertically-integrated organization of member companies that provides a single-source solution for chemical services, contract management, and automation and process engineering solutions throughout the U.S., Canada, and South America.', NULL, N'http://www.vincitgroup.com/#header-video', NULL, NULL, NULL, NULL, CAST(N'2022-05-05T21:58:26.093' AS DateTime), NULL, 0)
                GO
                SET IDENTITY_INSERT [dbo].[Companies] OFF
                GO
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Avatar], [GitHubUsername], [SignupDate], [ColorPreference], [CurrentRoleId], [DesiredRoleId]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', N'tomasrcalvo@gmail.com', 0, N'ADS8V61ZGUpdjfEUFR5kkclc02pqN6amIriak/5axo3nzvM7iX+hLGly9+LjLyxM7Q==', N'2fe4ee84-37ac-41dc-b06d-154b8b64ed93', NULL, 0, 0, NULL, 1, 0, N'tomasrcalvo@gmail.com', NULL, NULL, CAST(N'1900-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL)
                GO
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Avatar], [GitHubUsername], [SignupDate], [ColorPreference], [CurrentRoleId], [DesiredRoleId]) VALUES (N'ab16137b-6bff-4d62-9c3e-acbe1a4730d8', N'calvoirvine@sbcglobal.net', 0, N'ACBpNreaPdm6IbGa4IhbvlYdFOGIHffttXiEC/ItWcerDxvzEbI2ViYtYZaYdoYm7Q==', N'3621ceb7-503c-49c0-8b1c-e71332816388', NULL, 0, 0, NULL, 1, 0, N'calvoirvine@sbcglobal.net', NULL, NULL, CAST(N'2022-04-30T15:19:23.123' AS DateTime), NULL, NULL, NULL)
                GO
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Avatar], [GitHubUsername], [SignupDate], [ColorPreference], [CurrentRoleId], [DesiredRoleId]) VALUES (N'b18365bd-d9f9-43f3-a0fb-3095031f6dfc', N'dominic@dyno.com', 0, N'ADOQblsg84otW9DQlHPDRKl2poswgulgcf5LodZGlpIfs/XIC/BSi6wPUqC/r7x6QQ==', N'ec286855-ea84-4549-9f66-faf56582a6ff', NULL, 0, 0, NULL, 1, 0, N'dominic@dyno.com', NULL, NULL, CAST(N'1900-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL)
                GO
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Avatar], [GitHubUsername], [SignupDate], [ColorPreference], [CurrentRoleId], [DesiredRoleId]) VALUES (N'ceac373c-3600-4c8b-9184-64081e6fcbad', N'guest@vidly.com', 0, N'ADCeCs5RMNTBWmIzW4S3X/4SBSufIAThRRHHZmMoCc1Jk7/sStailSPTGcdxzABIKA==', N'7f82477e-6329-45f1-b8a6-d173c0a448a6', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com', NULL, NULL, CAST(N'1900-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL)
                GO
                SET IDENTITY_INSERT [dbo].[EmploymentListings] ON 
                GO
                INSERT [dbo].[EmploymentListings] ([Id], [Title], [PayQuantity], [Currency], [PayFrequency], [WorkLocation], [ClientCompanyId], [FullText], [Url], [DateAdded], [StaffingCompanyId], [CreatorId], [DateArchived], [RecruiterId]) VALUES (2, N'Software Developer', 50000.0000, 0, 0, 1, 11, N'JOB SUMMARY:
                The Vincit Group is seeking an experienced Developer with a track record of authoring, high-quality software, and enjoys working in a team oriented environment. The position will provide support for back office HR/Payroll/ERP systems, develop bug fixes and enhancements for existing software, develop new solutions, and perform development administrative duties. Qualified candidates will work with the latest tools and technologies including C#, ASP.NET Core, IIS, Visual Studio, Azure Cloud Services, MS SQL Server, SharePoint, React, Blazor, SSIS, and commercial off the shelf software systems. The ideal candidate will work as part of a talented and collaborative team functioning in an agile methodology. This position requires a candidate that is self-disciplined, high performing, comfortable working with end users, motivated and wants to make a difference.

                EDUCATION:
                Required: Bachelor’s Degree in Computer Science, Engineering, Information Technology, Management or equivalent combination of experience, knowledge, skills and abilities.
                Preferred: Microsoft certifications preferred.

                EXPERIENCE:
                Required: 2 years of relevant experience
                Preferred: 5+ years of relevant experience

                POSITION REQUIREMENT(S): Specialized Skills/License/Certification
                Required:
                Minimum 2 years of experience providing support for mission critical back office systems.
                Minimum 2 years of experience with SQL Server.
                Minimum 2 years of experience working with Visual Studio.
                Experience providing Tier 2 / Tier 3 support for back office systems.
                Excellent troubleshooting skills.
                Experience with multi-tier database-driven systems.
                Experience with systems development lifecycle, scrum, agile, etc.
                Experience with SSRS, Crystal and/or other report writing tools.
                Experience with SQL and T-SQL.
                Ability to work in a fast paced, team oriented development environment.
                Skilled in the use of Microsoft Office applications
                Excellent communication skills and be able to communicate technical ideas effectively.
                Capable of building sustainable relationships with business stakeholders.
                Creativity and ingenuity while defining sound and practical solutions.
                Desire to take the initiative, moving projects/ideas forward with clarity.
                Adept negotiation skills in high-pressure situations.

                Bonus:
                Microsoft Certification (MCSE, MCSD, MCITP, MCPD, MCDBA).
                Experience with CRM, Payroll and/or HR systems.
                Experience with Sage HRMS, MAS 500, and/or X3
                Experience and knowledge of multi-tier and cross-platform architectures.
                Experience using formal source control tools to maintain code base.
                Experience with .net framework and Object Oriented Programming.
                Experience with Object Oriented Programming.
                Experience with open source programming languages.
                Experience working with SharePoint along with a desire and ability to quickly develop proficiency.
                Experience administering SharePoint 2010 and/or 2013
                Experience developing applications for SharePoint.
                Exposure to data modeling techniques.
                Experience with Microsoft frameworks and knowledge of technical design patterns/anti-patterns.


                ESSENTIAL JOB FUNCTIONS
                Work with IT members and business users to develop, configure and maintain Business Applications.
                Administer security for various applications such as BI systems, HRM and ERP systems.
                Responsible for database administration tasks.
                Create and maintain business reports and help explain contents and use of reports to end users.
                Logical and Physical design of OLTP databases.
                Implement and support application and database issues as they arise
                Responsible for application and web development
                Collaborate with the IT team to complete projects
                Completes special projects upon requests, and other duties as assigned.', N'https://www.linkedin.com/jobs/view/2752772411', CAST(N'2022-05-05T22:13:21.767' AS DateTime), 9, N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', NULL, NULL)
                GO
                SET IDENTITY_INSERT [dbo].[EmploymentListings] OFF
                GO
                INSERT [dbo].[EmploymentListingAccesses] ([ApplicationUserId], [EmploymentListingId], [DateCreated], [CanEdit], [CanArchive], [CanDelete]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 2, CAST(N'2022-05-05T22:13:21.860' AS DateTime), 1, 1, 1)
                GO
                SET IDENTITY_INSERT [dbo].[Skills] ON 
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (1, N'https://camo.githubusercontent.com/8d56e87edf99e89bfc457cd62462e0b7aae19e6b197b1df5c542d474d8d76f81/68747470733a2f2f646576656c6f7065722e6665646f726170726f6a6563742e6f72672f7374617469632f6c6f676f2f6373686172702e706e67', N'C#', N'C# is a general-purpose, multi-paradigm programming language. C# encompasses static typing, strong typing, lexically scoped, imperative, declarative, functional, generic, object-oriented (class-based), and component-oriented programming disciplines', N'Mads Torgersen, Microsoft', CAST(N'2000-12-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.177' AS DateTime), N'', N'https://docs.microsoft.com/en-us/dotnet/csharp/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (2, N'https://camo.githubusercontent.com/58045a79a69afea4cab1cea6def6d911fba3956cf5fd683addf41c032aa64088/68747470733a2f2f636c6475702e636f6d2f78465646784f696f41552e737667', N'Mocha', N'Mocha is a feature-rich asynchronous JavaScript test framework running on Node.js and in the browser. Mocha tests run serially, allowing for flexible and accurate reporting, while mapping uncaught exceptions to the correct test cases. Hosted on GitHub.', N'Mocha', CAST(N'2011-11-22T00:00:00.000' AS DateTime), CAST(N'2022-02-08T00:00:00.000' AS DateTime), N'https://github.com/mochajs/mocha', N'https://mochajs.org')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (3, N'https://www.docker.com/wp-content/uploads/2022/03/vertical-logo-monochromatic.png', N'Docker', N'Docker is an open source containerization platform. It enables developers to package applications into containers—standardized executable components combining application source code with the operating system (OS) libraries and dependencies required to run that code in any environment.', N'Solomon Hykes', CAST(N'2013-03-20T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.000' AS DateTime), N'github.com/moby/moby', N'https://docs.docker.com/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (4, N'https://logodix.com/logo/1796970.png', N'.NET', N'The .NET Framework is a software framework developed by Microsoft that runs primarily on Microsoft Windows. It includes a large class library called Framework Class Library and provides language interoperability across several programming languages.', NULL, CAST(N'2002-01-05T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.000' AS DateTime), NULL, N'https://docs.microsoft.com/en-us/dotnet/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (5, N'https://logodix.com/logo/1796970.png', N'Entity Framework', N'Entity Framework is an open-source ORM framework for .NET applications supported by Microsoft. It enables developers to work with data using objects of domain specific classes without focusing on the underlying database tables and columns where this data is stored. With the Entity Framework, developers can work at a higher level of abstraction when they deal with data, and can create and maintain data-oriented applications with less code compared with traditional applications.', N'Microsoft', CAST(N'2008-08-11T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.000' AS DateTime), N'https://github.com/dotnet/ef6', N'https://docs.microsoft.com/en-us/ef/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (6, N'https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/Visual_Studio_Icon_2019.svg/512px-Visual_Studio_Icon_2019.svg.png?20210214224138', N'Microsoft Visual Studio', N'Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft. It is used to develop computer programs, as well as websites, web apps, web services and mobile apps.', N'Microsoft', CAST(N'1997-03-19T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://docs.microsoft.com/en-us/visualstudio/windows/?view=vs-2022')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (7, N'~/Content/Icons/skill-icons/sql-logo.png', N'SQL', N'Microsoft SQL Server is a relational database management system developed by Microsoft. As a database server, it is a software product with the primary function of storing and retrieving data as requested by other software applications—which may run either on the same computer or on another computer across a network.', N'Microsoft', CAST(N'1989-04-24T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (8, N'https://git-scm.com/images/logos/downloads/Git-Icon-1788C.png', N'Git', N'Git is a free and open source distributed version control system designed to handle everything from small to very large projects with speed and efficiency.', N'Junio Hamano and others', CAST(N'2005-04-07T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://git-scm.com/doc')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (9, N'~/Content/Icons/skill-icons/bootstrap-logo.png', N'Bootstrap', N'Bootstrap is a free and open-source CSS framework directed at responsive, mobile-first front-end web development. It contains CSS- and JavaScript-based design templates for typography, forms, buttons, navigation, and other interface components.', N'Mark Otto, Jacob Thornton, Boostrap Core Team', CAST(N'2011-08-19T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), N'https://github.com/twbs/bootstrap', N'https://getbootstrap.com/docs/4.1/getting-started/introduction/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (10, N'https://www.wired.com/images_blogs/business/2011/08/HTML5_Logo_512.png', N'HTML5', N'HTML is the standard markup language for documents designed to be displayed in a web browser.', N'Tim Berners-Lee, WHATWG', CAST(N'1993-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Web/HTML')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (11, N'https://cdn.iconscout.com/icon/free/png-256/css-37-226088.png', N'CSS3', N'CSS3 is the latest version of the CSS specification. CSS is a stylesheet language used for describing the presentation of a document written in a markup language such as HTML.', N'W3C', CAST(N'1996-12-17T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Web/CSS')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (12, N'https://upload.wikimedia.org/wikipedia/commons/thumb/9/98/WordPress_blue_logo.svg/1024px-WordPress_blue_logo.svg.png', N'Wordpress', N'WordPress is a free and open-source content management system written in PHP and paired with a MySQL or MariaDB database. Features include a plugin architecture and a template system, referred to within WordPress as Themes.', N'WordPress Foundation', CAST(N'2003-05-27T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developer.wordpress.com/docs/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (13, NULL, N'Digital Marketing', N'Digital marketing is the component of marketing that uses internet and online based digital technologies such as desktop computers, mobile phones and other digital media and platforms to promote products and services.', NULL, NULL, CAST(N'2022-04-27T14:56:43.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (14, N'~/Content/Icons/skill-icons/react-icon.png', N'React', N'React.js is an open-source JavaScript library that is used for building user interfaces specifically for single-page applications.', N'Jordan Walke at Facebook', CAST(N'2013-05-29T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), N'https://github.com/facebook/react', N'https://reactjs.org/docs/getting-started.html')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (15, N'~/Content/Icons/skill-icons/axios-logo.svg', N'Axios', N'Axios.js is a promise-based HTTP Client for node.js and the browser. On the server-side it uses the native node.js http module, while on the client (browser) it uses XMLHttpRequests.', N'Matt Zabriskie', CAST(N'2014-09-18T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), N'https://github.com/axios/axios', N'https://axios-http.com/docs/intro')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (16, N'~/Content/Icons/skill-icons/commercejs-logo.png', N'Commerce.js', N'Commerce.js is a headless eCommerce platform built specifically for develoeprs and designers to build custom eCommerce solutions.', NULL, CAST(N'2016-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://commercejs.com/docs/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (17, N'~/Content/Icons/skill-icons/google-logo.png', N'Google OAuth', N'OAuth is an open standard for access delegation, commonly used as a way for Internet users to grant websites or applications access to their information on other websites without giving them the passwords.', N'Google', NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developers.google.com/identity/protocols/oauth2')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (18, N'~/Content/Icons/skill-icons/pwa-logo.png', N'PWAs (Progressive Web Apps)', N'Progressive Web Apps use services workers and manifests to give users an experience on par with native apps.', N'Google', CAST(N'2015-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Web/Progressive_web_apps')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (19, N'~/Content/Icons/skill-icons/google-lighthouse-logo.png', N'Google Lighthouse', N'Google Lighthouse audits progressive web applications for performance, accessibility, and search engine optimization.', N'Google', CAST(N'2018-05-08T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developers.google.com/web/tools/lighthouse')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (20, N'~/Content/Icons/skill-icons/socketio-logo.png', N'SocketIO', N'Socket.IO is a JavaScript library that enables realtime, bi-directional communication between web clients and servers.', N'Guillermo Rauch', CAST(N'2010-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://socket.io/docs/v4/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (21, N'~/Content/Icons/skill-icons/themeui-logo.png', N'Theme UI', N'Theme UI is a library for creating themeable user interfaces based on constraint-based design principles. Build custom component libraries, design systems, web applications, Gatsby themes, and more with a flexible API for best-in-class developer ergonomics.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (22, N'~/Content/Icons/skill-icons/materialui-logo.png', N'Material-UI', N'Material UI is an open-source, front-end framework for React components.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (23, N'~/Content/Icons/skill-icons/node-logo.png', N'Node.js', N'Node.js is a JavaScript runtime built on Chrome''s V8 JavaScript engine. Node.js uses an event-driven, non-blocking I/O model that makes it lightweight and efficient.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (24, N'~/Content/Icons/skill-icons/mongodb-logo.png', N'MongoDB', N'MongoDB is a document-oriented database which stores data in JSON-like documents with dynamic schema.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (25, N'~/Content/Icons/skill-icons/vite-logo.svg', N'Vite', N'Vite is a front end build tool and dev server.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (26, N'~/Content/Icons/skill-icons/threejs-logo.png', N'Three.js', N'Three.js is a cross-browser JavaScript library and application programming interface (API) used to create and display animated 3D computer graphics in a web browser using WebGL.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (27, N'~/Content/Icons/skill-icons/rest_api.png', N'REST API', N'A REST API (also known as RESTful API) is an application programming interface (API or web API) that conforms to the constraints of REST architectural style and allows for interaction with RESTful web services. REST stands for representational state transfer.', N'Roy Fielding', NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (28, N'~/Content/Icons/skill-icons/chartjs-logo.svg', N'Chart.js', N'Chart.js is an open-source JavaScript library for data visualization.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (29, N'~/Content/Icons/skill-icons/heroku.png', N'Heroku', N'Heroku is a platform as a service (PaaS) that enables developers to build, run, and operate applications entirely in the cloud.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (30, N'~/Content/Icons/skill-icons/netlify.svg', N'Netlify', N'Netlify is a frontend web hosting platform.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (31, N'~/Content/Icons/skill-icons/hostinger.svg', N'Hostinger', N'Hostinger is shared web hosting.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (32, N'~/Content/Icons/skill-icons/azure.png', N'Azure', N'Azure is a public cloud computing platform—with solutions including Infrastructure as a Service (IaaS), Platform as a Service (PaaS), and Software as a Service (SaaS) that can be used for services such as analytics, virtual computing, storage, and networking.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (33, N'https://symbols.getvecta.com/stencil_88/75_microsoft-net-icon.468c070a03.png', N'ASP.NET', N'The main difference between . NET and ASP.NET is that . NET is a software framework that allows developing, running and executing applications while ASP.NET is a web framework which is a part of . NET that allows building dynamic web applications.', NULL, NULL, CAST(N'2022-04-26T17:11:33.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (34, N'https://cdn.iconscout.com/icon/free/png-256/microsoft-forms-2081996-1756309.png', N'Windows Forms', NULL, NULL, NULL, CAST(N'2022-04-26T17:11:56.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (35, N'~/Content/Icons/skill-icons/sql-logo.png', N'SQL Server', N'SQL is a query language. It is used to write queries to retrieve or manipulate the relational database data. On the other hand, SQL Server is proprietary software or an RDBMS tool that executes the SQL statements.', NULL, NULL, CAST(N'2022-04-26T17:33:08.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (36, N'https://miro.medium.com/max/300/0*goJuBKoyL-zZX4RB.png', N'OOP (Object-Oriented Programming)', NULL, NULL, NULL, CAST(N'2022-04-26T17:33:53.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (37, N'https://icon-library.com/images/web-service-icon/web-service-icon-9.jpg', N'Web Services', NULL, NULL, NULL, CAST(N'2022-04-26T17:34:11.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (38, NULL, N'Service-Oriented Architecture', NULL, NULL, NULL, NULL, NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (39, N'https://symbols.getvecta.com/stencil_88/75_microsoft-net-icon.468c070a03.png', N'WCF (Windows Communication Foundation)', N'Windows Communication Foundation (WCF) is a framework for building service-oriented applications. Using WCF, you can send data as asynchronous messages from one service endpoint to another. A service endpoint can be part of a continuously available service hosted by IIS, or it can be a service hosted in an application.', NULL, NULL, CAST(N'2022-04-26T17:35:33.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (40, NULL, N'Concurrent Development Source Control', N'Git', NULL, NULL, CAST(N'2022-04-26T17:36:15.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (41, NULL, N'Continuous Integration', N'Jenkins or Bamboo', NULL, NULL, CAST(N'2022-04-26T17:36:36.660' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (42, N'https://www.jenkins.io/images/logos/jenkins/jenkins.png', N'Jenkins', N'Continuous Integration', NULL, NULL, CAST(N'2022-04-26T17:36:43.000' AS DateTime), NULL, N'https://www.jenkins.io/doc/book/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (43, N'https://wac-cdn.atlassian.com/dam/jcr:3565304e-e789-486b-a722-be19f067f7c7/Bamboo-blue.svg?cdnVersion=324', N'Bamboo', N'Continuous Integration', NULL, NULL, CAST(N'2022-04-26T17:36:53.000' AS DateTime), NULL, N'https://confluence.atlassian.com/bamboo/bamboo-documentation-289276551.html')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (44, N'https://static.thenounproject.com/png/253212-200.png', N'Agile', N'Agile Methodologies', NULL, NULL, CAST(N'2022-04-26T18:56:54.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (45, N'https://e6.pngbyte.com/pngpicture/115994/png-cycle-icon-png-Software-Development-Life-Cycle-Icon_thumbnail.png', N'Software Development Life Cycle', N'In systems engineering, information systems and software engineering, the systems development life cycle, also referred to as the application development life-cycle, is a process for planning, creating, testing, and deploying an information system.', NULL, NULL, CAST(N'2022-04-26T18:58:14.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (46, N'https://cdn-icons-png.flaticon.com/512/2103/2103478.png', N'SaaS', N'Software as a Service', NULL, NULL, CAST(N'2022-04-26T18:59:15.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (47, N'http://emmet.io/i/logo-large.png', N'Emmet', N'Emmet is a popular plugin for code editors that vastly improves the HTML and CSS workflow involved with creating web pages. Instead of having to manually type opening and closing tags for HTML elements, format nested elements, or add in CSS classes and IDs, Emmet shortcuts will autogenerate everything for you.', N'emmetio', CAST(N'2008-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-29T19:20:58.300' AS DateTime), N'github.com/emmetio/emmet', N'https://docs.emmet.io/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (49, N'https://www.pikpng.com/pngl/m/440-4404136_jquery-jquery-logo-transparent-background-clipart.png', N'jQuery', N'jQuery is a JavaScript library designed to simplify HTML DOM tree traversal and manipulation, as well as event handling, CSS animation, and Ajax. It is free, open-source software using the permissive MIT License. As of May 2019, jQuery is used by 73% of the 10 million most popular websites.', NULL, NULL, CAST(N'2022-04-29T21:27:05.000' AS DateTime), NULL, N'https://jquery.com/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (50, NULL, N'RegEx', N'Regular Expressions', NULL, NULL, CAST(N'2022-04-30T14:38:12.873' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (51, N'https://miro.medium.com/max/800/1*GwBZgjItyjEwaaZn-lxTTA.png', N'PWA', N'A progressive web application is a type of application software delivered through the web, built using common web technologies including HTML, CSS, JavaScript, and WebAssembly.', NULL, NULL, CAST(N'2022-05-02T17:59:44.657' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Web/Progressive_web_apps')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (52, N'https://www.wired.com/images_blogs/business/2011/08/HTML5_Logo_512.png', N'Semantic HTML', NULL, NULL, NULL, CAST(N'2022-05-04T11:29:28.063' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Glossary/Semantics')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (53, NULL, N'MERN Stack', NULL, NULL, NULL, CAST(N'2022-05-04T11:43:13.857' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (54, NULL, N'Mongoose', NULL, NULL, NULL, CAST(N'2022-05-04T11:43:53.577' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (55, NULL, N'JSON', N'JavaScript Object Notation', NULL, NULL, CAST(N'2022-05-04T11:44:16.247' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (56, NULL, N'IDE', N'Integrated Development Environment', NULL, NULL, CAST(N'2022-05-04T11:44:35.227' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (57, NULL, N'VS Code', NULL, NULL, NULL, CAST(N'2022-05-04T11:45:01.510' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (58, NULL, N'Design Patterns', NULL, NULL, NULL, CAST(N'2022-05-04T11:45:17.527' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (59, NULL, N'MVC', NULL, NULL, NULL, CAST(N'2022-05-04T11:45:23.110' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (60, NULL, N'Express.js ', NULL, NULL, NULL, CAST(N'2022-05-04T11:55:37.380' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (61, NULL, N'Data Structures', N'Array, Graph, Binary Tree, Dictionary, Stack', NULL, NULL, CAST(N'2022-05-04T11:56:33.637' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (62, NULL, N'OAuth', NULL, NULL, NULL, CAST(N'2022-05-04T11:58:36.820' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (63, NULL, N'Facebook OAuth', NULL, NULL, NULL, CAST(N'2022-05-04T11:58:55.697' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (64, NULL, N'Authentication', NULL, NULL, NULL, CAST(N'2022-05-04T11:59:04.147' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (65, NULL, N'Session Authentication ', NULL, NULL, NULL, CAST(N'2022-05-04T12:08:17.700' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (66, NULL, N'Token Authentication', NULL, NULL, NULL, CAST(N'2022-05-04T12:08:31.300' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (67, NULL, N'Debugging', NULL, NULL, NULL, CAST(N'2022-05-04T12:08:45.647' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (68, NULL, N'Glimpse', NULL, NULL, NULL, CAST(N'2022-05-04T12:08:51.380' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (69, NULL, N'Performance Optimization', NULL, NULL, NULL, CAST(N'2022-05-04T12:18:02.307' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (70, NULL, N'Caching', NULL, NULL, NULL, CAST(N'2022-05-04T12:18:16.620' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (71, NULL, N'Asynchronous Programming', NULL, NULL, NULL, CAST(N'2022-05-04T12:18:25.920' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (72, NULL, N'Identity Framework', NULL, NULL, NULL, CAST(N'2022-05-04T12:19:22.687' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (73, NULL, N'Postman', NULL, NULL, NULL, CAST(N'2022-05-04T12:19:36.353' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (74, NULL, N'Stripe', NULL, NULL, NULL, CAST(N'2022-05-04T12:20:19.760' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (75, NULL, N'Redux', N'State Management', NULL, NULL, CAST(N'2022-05-04T12:21:43.447' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (76, NULL, N'React Router', N'Library', NULL, NULL, CAST(N'2022-05-04T12:22:08.740' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (77, NULL, N'React Hook Form', NULL, NULL, NULL, CAST(N'2022-05-04T12:22:15.010' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (78, NULL, N'Mobile-First Design', NULL, NULL, NULL, CAST(N'2022-05-04T12:22:33.270' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (79, NULL, N'Responsive Design', NULL, NULL, NULL, CAST(N'2022-05-04T12:22:39.950' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (80, NULL, N'Adobe Photoshop', NULL, NULL, NULL, CAST(N'2022-05-04T12:22:56.353' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (81, NULL, N'GIMP', NULL, NULL, NULL, CAST(N'2022-05-04T12:23:00.933' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (82, NULL, N'Inkscape', NULL, NULL, NULL, CAST(N'2022-05-04T12:23:09.437' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (83, NULL, N'GoDaddy', NULL, NULL, NULL, CAST(N'2022-05-04T12:23:23.883' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (84, NULL, N'SPA', N'Single-Page Application', NULL, NULL, CAST(N'2022-05-04T12:23:42.873' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (85, NULL, N'Figma', NULL, NULL, NULL, CAST(N'2022-05-04T12:23:50.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (86, NULL, N'Web Standards', NULL, NULL, NULL, CAST(N'2022-05-04T12:24:40.867' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (87, NULL, N'Sass', NULL, NULL, NULL, CAST(N'2022-05-05T11:09:22.933' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (88, NULL, N'Svelte', NULL, NULL, NULL, CAST(N'2022-05-05T11:09:32.577' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (89, NULL, N'Angular', NULL, NULL, NULL, CAST(N'2022-05-05T11:09:38.457' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (90, NULL, N'Vue.js', NULL, NULL, NULL, CAST(N'2022-05-05T11:10:33.603' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (91, NULL, N'Yarn', NULL, NULL, NULL, CAST(N'2022-05-05T11:19:24.163' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (92, NULL, N'NPM', N'Node Package Manager', NULL, NULL, CAST(N'2022-05-05T11:19:38.027' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (93, NULL, N'SharePoint', NULL, NULL, NULL, CAST(N'2022-05-05T11:22:27.683' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (94, NULL, N'ERP', N'Enterprise Resource Planning. It''s software that manages a company''s financials, supply chain, operations, commerce, reporting, manufacturing, and human resource activities. ', NULL, NULL, CAST(N'2022-05-05T11:30:33.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (95, NULL, N'Blazor', N'Blazor lets you build interactive web UIs using C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries.  Blazor is a feature of ASP.NET, the popular web development framework that extends the .NET developer platform with tools and libraries for building web apps.', NULL, NULL, CAST(N'2022-05-05T11:35:20.580' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (96, NULL, N'SSIS', N'SQL Server Integration Services is a component of the Microsoft SQL Server database software that can be used to perform a broad range of data migration tasks. SSIS is a platform for data integration and workflow applications. It features a data warehousing tool used for data extraction, transformation, and loading.', NULL, NULL, CAST(N'2022-05-05T11:36:22.827' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (97, NULL, N'AutoMapper', N'A convention-based object-object mapper.  AutoMapper uses a fluent configuration API to define an object-object mapping strategy. AutoMapper uses a convention-based matching algorithm to match up source to destination values. AutoMapper is geared towards model projection scenarios to flatten complex object models to DTOs and other simple objects, whose design is better suited for serialization, communication, messaging, or simply an anti-corruption layer between the domain and application layer.', NULL, NULL, CAST(N'2022-05-05T12:15:52.410' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (98, NULL, N'XML', N'XML (Extensible Markup Language) is a markup language similar to HTML, but without predefined tags to use. Instead, you define your own tags designed specifically for your needs. This is a powerful way to store data in a format that can be stored, searched, and shared.', NULL, NULL, CAST(N'2022-05-05T12:27:34.907' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (99, NULL, N'DTO', N'A data transfer object (DTO) is an object that carries data between processes. You can use this technique to facilitate communication between two systems (like an API and your server) without potentially exposing sensitive information. DTOs are commonsense solutions for people with programming backgrounds.', NULL, NULL, CAST(N'2022-05-05T12:31:55.483' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (100, NULL, N'GraphQL', N'GraphQL is a query language for APIs and a runtime for fulfilling those queries with your existing data. GraphQL provides a complete and understandable description of the data in your API, gives clients the power to ask for exactly what they need and nothing more, makes it easier to evolve APIs over time, and enables powerful developer tools.', NULL, NULL, CAST(N'2022-05-05T12:54:19.320' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (101, NULL, N'SSRS', N'SQL Server Reporting Services is a server-based report generating software system from Microsoft. It is part of a suite of Microsoft SQL Server services, including SSAS and SSIS. Administered via a Web interface, it can be used to prepare and deliver a variety of interactive and printed reports.', NULL, NULL, CAST(N'2022-05-05T13:39:41.080' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (102, NULL, N'Crystal Reports', N'Crystal Reports is a popular Windows-based report writer solution that allows a developer to create reports and dashboards from a variety of data sources with a minimum of code to write. Crystal Reports is owned and developed by SAP.', NULL, NULL, CAST(N'2022-05-05T13:40:40.693' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (103, NULL, N'T-SQL', N'T-SQL or Transact SQL is the query language specific to the Microsoft SQL Server product. It can help perform operations like retrieving the data from a single row, inserting new rows, and retrieving multiple rows. It is a procedural language that is used by the SQL Server.', NULL, NULL, CAST(N'2022-05-05T13:42:06.117' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (104, NULL, N'CRM', N'Customer relationship management is a process in which a business or other organization administers its interactions with customers, typically using data analysis to study large amounts of information. ', NULL, NULL, CAST(N'2022-05-05T13:45:52.613' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (105, NULL, N'Sage HRMS', N'Sage HRMS is an on-premise, human resources solution for small and midsize businesses. It allows users to manage the entire employee lifecycle. Primary features include employee record management, payroll processing and management, recruiting, onboarding, time and attendance management, talent management, risk mitigation and compliance management. Other modules include workforce management, learning management, decision support and employee benefits management.', NULL, NULL, CAST(N'2022-05-05T13:47:44.370' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (106, NULL, N'Sage X3', NULL, NULL, NULL, CAST(N'2022-05-05T19:21:08.137' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (107, NULL, N'Sage 500 ERP', N'Formerly MAS 500', NULL, NULL, CAST(N'2022-05-05T19:22:02.383' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (108, NULL, N'Multitier Architecture', N'In software engineering, multitier architecture is a client–server architecture in which presentation, application processing and data management functions are physically separated. The most widespread use of multitier architecture is the three-tier architecture.', NULL, NULL, CAST(N'2022-05-05T19:24:30.760' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (109, NULL, N'Cross-Platform Architecture', N'For software to be considered cross-platform, it must be function on more than one computer architecture or OS. Developing such software can be a time-consuming task because different OSs have different application programming interfaces (API). For example, Linux uses a different API from Windows.', NULL, NULL, CAST(N'2022-05-05T19:30:46.183' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (110, NULL, N'Open-Source Programming Language', N'An open-source language refers to a programming language that falls within the parameters of open-source protocol. This basically means that the language is not proprietary, and with certain provisions (depending on the open source license), can be modified or built upon in a manner that is open to the public. This eventually led to the foundations of the open-source movement. Out of that, open-source programming languages evolved. The rules for those languages include the following:  Source codes must be open and accessible. Derived works must also be open source. The languages must be freely distributed. The integrity of the source code must be maintained. Licenses must not restrict other software. There can be no discrimination against fields of endeavor.', NULL, NULL, CAST(N'2022-05-05T19:32:32.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (111, NULL, N'Data Modeling', N'How to Model Data Identify entity types. Identify attributes. Apply naming conventions. Identify relationships. Apply data model patterns. Assign keys. Normalize to reduce data redundancy. Denormalize to improve performance.', NULL, NULL, CAST(N'2022-05-05T19:43:14.500' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (112, NULL, N'Anti-Patterns', N'An anti-pattern is a common response to a recurring problem that is usually ineffective and risks being highly counterproductive.” Note the reference to “a common response.” Anti-patterns are not occasional mistakes, they are common ones, and are nearly always followed with good intentions.', NULL, NULL, CAST(N'2022-05-05T21:32:09.483' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (113, NULL, N'Business Intelligence (BI)', N'Business intelligence comprises the strategies and technologies used by enterprises for the data analysis and management of business information.', NULL, NULL, CAST(N'2022-05-05T21:34:28.663' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (114, NULL, N'OLTP', N'OLTP or Online Transaction Processing is a type of data processing that consists of executing a number of transactions occurring concurrently—online banking, shopping, order entry, or sending text messages, for example.', NULL, NULL, CAST(N'2022-05-05T21:35:47.523' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (115, NULL, N'Tauri ', N'Tauri is a framework for building tiny, blazingly fast binaries for all major desktop platforms. Developers can integrate any front-end framework that compiles to HTML, JS and CSS for building their user interface. See cross-platform architecture. ', NULL, NULL, CAST(N'2022-05-07T01:35:49.000' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (116, NULL, N'Electron', N'Electron is a free and open-source software framework developed and maintained by GitHub. It allows for the development of desktop GUI applications using web technologies: it combines the Chromium rendering engine and the Node.js runtime. It was originally built for Atom.', NULL, NULL, CAST(N'2022-05-07T01:37:15.377' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (117, NULL, N'Rust', N'Rust is a multi-paradigm, general-purpose programming language designed for performance and safety, especially safe concurrency. It is syntactically similar to C++, but can guarantee memory safety by using a borrow checker to validate references.', NULL, NULL, CAST(N'2022-05-07T01:38:06.787' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (118, NULL, N'Chromium', N'Chromium is a free and open-source web browser project, principally developed and maintained by Google. This codebase provides the vast majority of code for the Google Chrome browser, which is proprietary software and has some additional features. The Chromium codebase is widely used.', NULL, NULL, CAST(N'2022-05-07T01:39:52.340' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (119, NULL, N'WebAssembly', N'WebAssembly is a new type of code that can be run in modern web browsers — it is a low-level assembly-like language with a compact binary format that runs with near-native performance and provides languages such as C/C++, C# and Rust with a compilation target so that they can run on the web.', NULL, NULL, CAST(N'2022-05-07T15:49:12.993' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (120, NULL, N'Containers', N'Containers are executable units of software in which application code is packaged, along with its libraries and dependencies, in common ways so that it can be run anywhere, whether it be on desktop, traditional IT, or the cloud.  To do this, containers take advantage of a form of operating system (OS) virtualization in which features of the OS (in the case of the Linux kernel, namely the namespaces and cgroups primitives) are leveraged to both isolate processes and control the amount of CPU, memory, and disk that those processes have access to.  Containers are small, fast, and portable because unlike a virtual machine, containers do not need include a guest OS in every instance and can, instead, simply leverage the features and resources of the host OS.  Containers first appeared decades ago with versions like FreeBSD Jails and AIX Workload Partitions, but most modern developers remember 2013 as the start of the modern container era with the introduction of Docker.', NULL, NULL, CAST(N'2022-05-08T11:22:37.497' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (121, NULL, N'Virtual Machines', NULL, NULL, NULL, CAST(N'2022-05-08T11:59:08.383' AS DateTime), NULL, NULL)
                GO
                SET IDENTITY_INSERT [dbo].[Skills] OFF
                GO
                SET IDENTITY_INSERT [dbo].[Projects] ON 
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (1, N'Strength', N'A MERN stack web app for tracking fitness and training together remotely.', N'~/Content/Icons/project-icons/strength-logo-3-lightmode.svg', N'https://github.com/tomascalvo/otrera-server', N'https://otrera.netlify.app', CAST(N'2022-04-27T14:56:42.947' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (2, N'Dyno', N'An ASP.NET web app for matching web developers with relevant jobs and learning resources.', N'~/Content/Icons/project-icons/dyno-logo-lightmode.png', N'https://github.com/tomascalvo/devpath', N'https://dyno.azurewebsites.net/', CAST(N'2022-04-27T14:56:42.967' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (3, N'Consensa', N'An ASP.NET web app for ranked-choice voting that uses QR codes, websockets, encryption, and a distributed database to help users quickly reach consensus.', N'https://www.seekpng.com/png/detail/28-280338_vote-icon-png-download-icono-elecciones-png.png', N'', N'', CAST(N'2022-04-27T14:56:42.970' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (4, N'Indulgence', N'An ASP.NET web app that allows users to purchase beautifully illustrated illuminated certificates stored in a distributed database that absolves them of their microaggressions.', N'https://cdn-icons-png.flaticon.com/512/2397/2397091.png', N'', N'', CAST(N'2022-04-27T14:56:42.973' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (5, N'Bamberga Minerals', N'A website that displays interactive 3D graphics using the Three.js library.', N'~/Content/Icons/project-icons/bamberga-logo-darkmode.svg', N'https://github.com/tomascalvo/threejs-site', N'https://bamberga.netlify.app/', CAST(N'2022-04-27T14:56:42.977' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (6, N'Weather Beach', N'A PWA that uses React and OpenWeather API to display weather information by city.', N'~/Content/Icons/project-icons/island.svg', N'https://github.com/tomascalvo/weather-pwa', N'https://weatherbeach.netlify.app/', CAST(N'2022-04-27T14:56:42.977' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (7, N'Pandemic Tracker', N'A React COVID-19 tracker that consumes data from a Johns Hopkins University API.', N'~/Content/Icons/project-icons/graph.png', N'https://github.com/tomascalvo/covid-tracker', N'https://v-tracker.netlify.app/', CAST(N'2022-04-27T14:56:42.980' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (8, N'IO Chat', N'A real-time chat app using React and Socket.IO', N'~/Content/Icons/project-icons/chat.png', N'https://github.com/tomascalvo/react-chat-app', N'https://io-chat.netlify.app/', CAST(N'2022-04-27T14:56:42.980' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (9, N'Cumberland Honey Shop', N'A React e-commerce app using Commerce.JS.', N'~/Content/Icons/project-icons/honey.svg', N'https://github.com/tomascalvo/E-Commerce_WebApp', N'https://cumberlandhoney.netlify.app/', CAST(N'2022-04-27T14:56:42.983' AS DateTime))
                GO
                SET IDENTITY_INSERT [dbo].[Projects] OFF
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 8, CAST(N'2022-04-27T14:56:43.373' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 10, CAST(N'2022-04-27T14:56:43.377' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 11, CAST(N'2022-04-27T14:56:43.380' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 14, CAST(N'2022-04-27T14:56:43.383' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 15, CAST(N'2022-04-27T14:56:43.387' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 17, CAST(N'2022-04-27T14:56:43.390' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 20, CAST(N'2022-04-27T14:56:43.393' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 22, CAST(N'2022-04-27T14:56:43.397' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 23, CAST(N'2022-04-27T14:56:43.397' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 24, CAST(N'2022-04-27T14:56:43.400' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 27, CAST(N'2022-04-27T14:56:43.403' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 28, CAST(N'2022-04-27T14:56:43.407' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 29, CAST(N'2022-04-27T14:56:43.410' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 30, CAST(N'2022-04-27T14:56:43.413' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 1, CAST(N'2022-04-27T14:56:43.413' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 2, CAST(N'2022-04-27T14:56:43.417' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 4, CAST(N'2022-04-27T14:56:43.420' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 5, CAST(N'2022-04-27T14:56:43.423' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 6, CAST(N'2022-04-27T14:56:43.427' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 7, CAST(N'2022-04-27T14:56:43.427' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 8, CAST(N'2022-04-27T14:56:43.430' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 9, CAST(N'2022-04-27T14:56:43.433' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 10, CAST(N'2022-04-27T14:56:43.437' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 11, CAST(N'2022-04-27T14:56:43.440' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 15, CAST(N'2022-04-27T14:56:43.443' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 17, CAST(N'2022-04-27T14:56:43.447' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 27, CAST(N'2022-04-27T14:56:43.450' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 32, CAST(N'2022-04-27T14:56:43.453' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 8, CAST(N'2022-04-27T14:56:43.457' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 10, CAST(N'2022-04-27T14:56:43.460' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 11, CAST(N'2022-04-27T14:56:43.460' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 25, CAST(N'2022-04-27T14:56:43.463' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 26, CAST(N'2022-04-27T14:56:43.467' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 30, CAST(N'2022-04-27T14:56:43.470' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 8, CAST(N'2022-04-27T14:56:43.473' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 10, CAST(N'2022-04-27T14:56:43.477' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 11, CAST(N'2022-04-27T14:56:43.480' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 14, CAST(N'2022-04-27T14:56:43.480' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 15, CAST(N'2022-04-27T14:56:43.483' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 18, CAST(N'2022-04-27T14:56:43.487' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 27, CAST(N'2022-04-27T14:56:43.490' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 30, CAST(N'2022-04-27T14:56:43.493' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 8, CAST(N'2022-04-27T14:56:43.497' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 10, CAST(N'2022-04-27T14:56:43.500' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 11, CAST(N'2022-04-27T14:56:43.503' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 27, CAST(N'2022-04-27T14:56:43.507' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 28, CAST(N'2022-04-27T14:56:43.510' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 30, CAST(N'2022-04-27T14:56:43.513' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 8, CAST(N'2022-04-27T14:56:43.517' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 10, CAST(N'2022-04-27T14:56:43.517' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 11, CAST(N'2022-04-27T14:56:43.523' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 20, CAST(N'2022-04-27T14:56:43.527' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 29, CAST(N'2022-04-27T14:56:43.527' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 30, CAST(N'2022-04-27T14:56:43.533' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 8, CAST(N'2022-04-27T14:56:43.537' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 10, CAST(N'2022-04-27T14:56:43.537' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 11, CAST(N'2022-04-27T14:56:43.543' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 16, CAST(N'2022-04-27T14:56:43.547' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 22, CAST(N'2022-04-27T14:56:43.547' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 27, CAST(N'2022-04-27T14:56:43.550' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 30, CAST(N'2022-04-27T14:56:43.557' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 1, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 2, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 3, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 4, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 5, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 6, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 7, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 8, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[ApplicationUserProjects] ([ApplicationUserId], [ProjectId], [CanEdit], [DateAdded]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 9, 1, CAST(N'2022-04-27T16:37:00.000' AS DateTime))
                GO
                INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b7d929a7-6d73-4323-b36a-89860290eb62', N'CanManageAll')
                GO
                INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a16f8309-c8ec-4c5a-979d-fcf922d9f534', N'CanManageSkills')
                GO
                INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b18365bd-d9f9-43f3-a0fb-3095031f6dfc', N'a16f8309-c8ec-4c5a-979d-fcf922d9f534')
                GO
                INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', N'b7d929a7-6d73-4323-b36a-89860290eb62')
                GO
            ");
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ApplicationUserProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ApplicationUserProjects", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SkillHierarchies", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecruiterClients", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Platforms", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Goals", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employments", "EmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentOffers", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentListings", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentApplications", "ApplicantId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DocumentationReads", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseCompletions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CertificationTypes", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certifications", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Skills", "CertificationType_Id", "dbo.CertificationTypes");
            DropForeignKey("dbo.CertificationTypes", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseSkills", "SkillRefId", "dbo.Skills");
            DropForeignKey("dbo.CourseSkills", "CourseRefId", "dbo.Courses");
            DropForeignKey("dbo.CourseCompletions", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Projects", "EmploymentId", "dbo.Employments");
            DropForeignKey("dbo.ProjectCourseCompletions", new[] { "CourseCompletion_CourseId", "CourseCompletion_UserId" }, "dbo.CourseCompletions");
            DropForeignKey("dbo.ProjectCourseCompletions", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ContractBids", "Id", "dbo.Projects");
            DropForeignKey("dbo.Skills", "ContractListing_Id", "dbo.ContractListings");
            DropForeignKey("dbo.ContractListings", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.ContractListings", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Companies", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Clients", "CurrentCompanyId", "dbo.Companies");
            DropForeignKey("dbo.Recruiters", "StaffingCompanyId", "dbo.Companies");
            DropForeignKey("dbo.Platforms", "Id", "dbo.Companies");
            DropForeignKey("dbo.EmploymentListings", "StaffingCompanyId", "dbo.Companies");
            DropForeignKey("dbo.EmploymentListings", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.Courses", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.CertificationTypes", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.EmploymentOffers", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentListingSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.ProjectSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.ProjectSkills", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.SkillHierarchies", "PrincipalId", "dbo.Skills");
            DropForeignKey("dbo.SkillHierarchyPrerequisites", "SkillHierarchyId", "dbo.SkillHierarchies");
            DropForeignKey("dbo.SkillHierarchyPrerequisites", "PrerequisiteId", "dbo.Skills");
            DropForeignKey("dbo.GoalSkills", "SkillRefId", "dbo.Skills");
            DropForeignKey("dbo.GoalSkills", "GoalRefId", "dbo.Goals");
            DropForeignKey("dbo.EmploymentSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.EmploymentSkills", "EmploymentId", "dbo.Employments");
            DropForeignKey("dbo.Employments", "OfferId", "dbo.EmploymentOffers");
            DropForeignKey("dbo.RecruiterClients", "RecruiterId", "dbo.Recruiters");
            DropForeignKey("dbo.RecruiterClients", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.EmploymentOffers", "RecruiterId", "dbo.Recruiters");
            DropForeignKey("dbo.EmploymentListings", "RecruiterId", "dbo.Recruiters");
            DropForeignKey("dbo.EmploymentProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EmploymentProjects", "EmploymentId", "dbo.Employments");
            DropForeignKey("dbo.Employments", "EmployerId", "dbo.Companies");
            DropForeignKey("dbo.DocumentationReads", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.EmploymentListingSkills", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentListingAccesses", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentListingAccesses", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentApplications", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentListings", "ClientCompanyId", "dbo.Companies");
            DropForeignKey("dbo.ContractListings", "Client_Id1", "dbo.Clients");
            DropForeignKey("dbo.ContractListings", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ContractBids", "ListingId", "dbo.ContractListings");
            DropForeignKey("dbo.ContractListings", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContractBids", "Contractor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "Id", "dbo.ContractBids");
            DropForeignKey("dbo.Certifications", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certifications", "CertificationTypeId", "dbo.CertificationTypes");
            DropIndex("dbo.CourseSkills", new[] { "SkillRefId" });
            DropIndex("dbo.CourseSkills", new[] { "CourseRefId" });
            DropIndex("dbo.ProjectCourseCompletions", new[] { "CourseCompletion_CourseId", "CourseCompletion_UserId" });
            DropIndex("dbo.ProjectCourseCompletions", new[] { "Project_Id" });
            DropIndex("dbo.GoalSkills", new[] { "SkillRefId" });
            DropIndex("dbo.GoalSkills", new[] { "GoalRefId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Platforms", new[] { "AddedById" });
            DropIndex("dbo.Platforms", new[] { "Id" });
            DropIndex("dbo.ProjectSkills", new[] { "SkillId" });
            DropIndex("dbo.ProjectSkills", new[] { "ProjectId" });
            DropIndex("dbo.SkillHierarchyPrerequisites", new[] { "PrerequisiteId" });
            DropIndex("dbo.SkillHierarchyPrerequisites", new[] { "SkillHierarchyId" });
            DropIndex("dbo.SkillHierarchies", new[] { "CreatorId" });
            DropIndex("dbo.SkillHierarchies", new[] { "PrincipalId" });
            DropIndex("dbo.Goals", new[] { "UserId" });
            DropIndex("dbo.RecruiterClients", new[] { "CreatorId" });
            DropIndex("dbo.RecruiterClients", new[] { "CompanyId" });
            DropIndex("dbo.RecruiterClients", new[] { "RecruiterId" });
            DropIndex("dbo.Recruiters", new[] { "StaffingCompanyId" });
            DropIndex("dbo.EmploymentOffers", new[] { "RecruiterId" });
            DropIndex("dbo.EmploymentOffers", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentOffers", new[] { "RecipientId" });
            DropIndex("dbo.EmploymentProjects", new[] { "ProjectId" });
            DropIndex("dbo.EmploymentProjects", new[] { "EmploymentId" });
            DropIndex("dbo.Employments", new[] { "OfferId" });
            DropIndex("dbo.Employments", new[] { "EmployerId" });
            DropIndex("dbo.Employments", new[] { "EmployeeId" });
            DropIndex("dbo.EmploymentSkills", new[] { "SkillId" });
            DropIndex("dbo.EmploymentSkills", new[] { "EmploymentId" });
            DropIndex("dbo.DocumentationReads", new[] { "SkillId" });
            DropIndex("dbo.DocumentationReads", new[] { "UserId" });
            DropIndex("dbo.Skills", new[] { "CertificationType_Id" });
            DropIndex("dbo.Skills", new[] { "ContractListing_Id" });
            DropIndex("dbo.EmploymentListingSkills", new[] { "SkillId" });
            DropIndex("dbo.EmploymentListingSkills", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentListingAccesses", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentListingAccesses", new[] { "ApplicationUserId" });
            DropIndex("dbo.EmploymentApplications", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentApplications", new[] { "ApplicantId" });
            DropIndex("dbo.EmploymentListings", new[] { "PlatformId" });
            DropIndex("dbo.EmploymentListings", new[] { "RecruiterId" });
            DropIndex("dbo.EmploymentListings", new[] { "StaffingCompanyId" });
            DropIndex("dbo.EmploymentListings", new[] { "ClientCompanyId" });
            DropIndex("dbo.EmploymentListings", new[] { "CreatorId" });
            DropIndex("dbo.Companies", new[] { "Client_Id" });
            DropIndex("dbo.Clients", new[] { "CurrentCompanyId" });
            DropIndex("dbo.ContractListings", new[] { "Client_Id1" });
            DropIndex("dbo.ContractListings", new[] { "Client_Id" });
            DropIndex("dbo.ContractListings", new[] { "AddedById" });
            DropIndex("dbo.ContractListings", new[] { "PlatformId" });
            DropIndex("dbo.ContractListings", new[] { "ClientId" });
            DropIndex("dbo.Contracts", new[] { "Id" });
            DropIndex("dbo.ContractBids", new[] { "Contractor_Id" });
            DropIndex("dbo.ContractBids", new[] { "ListingId" });
            DropIndex("dbo.ContractBids", new[] { "Id" });
            DropIndex("dbo.Projects", new[] { "EmploymentId" });
            DropIndex("dbo.Projects", new[] { "AddedById" });
            DropIndex("dbo.CourseCompletions", new[] { "UserId" });
            DropIndex("dbo.CourseCompletions", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "PlatformId" });
            DropIndex("dbo.Courses", new[] { "AddedById" });
            DropIndex("dbo.CertificationTypes", new[] { "CourseId" });
            DropIndex("dbo.CertificationTypes", new[] { "PlatformId" });
            DropIndex("dbo.CertificationTypes", new[] { "AddedById" });
            DropIndex("dbo.Certifications", new[] { "ProjectId" });
            DropIndex("dbo.Certifications", new[] { "CertificationTypeId" });
            DropIndex("dbo.Certifications", new[] { "RecipientId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ApplicationUserProjects", new[] { "ProjectId" });
            DropIndex("dbo.ApplicationUserProjects", new[] { "ApplicationUserId" });
            DropTable("dbo.CourseSkills");
            DropTable("dbo.ProjectCourseCompletions");
            DropTable("dbo.GoalSkills");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Platforms");
            DropTable("dbo.ProjectSkills");
            DropTable("dbo.SkillHierarchyPrerequisites");
            DropTable("dbo.SkillHierarchies");
            DropTable("dbo.Goals");
            DropTable("dbo.RecruiterClients");
            DropTable("dbo.Recruiters");
            DropTable("dbo.EmploymentOffers");
            DropTable("dbo.EmploymentProjects");
            DropTable("dbo.Employments");
            DropTable("dbo.EmploymentSkills");
            DropTable("dbo.DocumentationReads");
            DropTable("dbo.Skills");
            DropTable("dbo.EmploymentListingSkills");
            DropTable("dbo.EmploymentListingAccesses");
            DropTable("dbo.EmploymentApplications");
            DropTable("dbo.EmploymentListings");
            DropTable("dbo.Companies");
            DropTable("dbo.Clients");
            DropTable("dbo.ContractListings");
            DropTable("dbo.Contracts");
            DropTable("dbo.ContractBids");
            DropTable("dbo.Projects");
            DropTable("dbo.CourseCompletions");
            DropTable("dbo.Courses");
            DropTable("dbo.CertificationTypes");
            DropTable("dbo.Certifications");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ApplicationUserProjects");
        }
    }
}
