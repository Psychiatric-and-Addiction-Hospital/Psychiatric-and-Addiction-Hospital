using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CurrentCompany = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CurrentPosition = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ExpectedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ResumeUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.CheckConstraint("CK_Candidate_FirstName", "LEN(LTRIM(RTRIM([FirstName]))) > 0");
                    table.CheckConstraint("CK_Candidate_LastName", "LEN(LTRIM(RTRIM([LastName]))) > 0");
                    table.CheckConstraint("CK_Candidate_YearsOfExperience", "[YearsOfExperience] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MaxDaysPerYear = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RequiresApproval = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                    table.CheckConstraint("CK_LeaveType_MaxDaysPerYear", "[MaxDaysPerYear] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MaxScore = table.Column<int>(type: "int", nullable: false, defaultValue: 100),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceCriteria", x => x.Id);
                    table.CheckConstraint("CK_PerformanceCriteria_MaxScore", "[MaxScore] > 0");
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakMinutes = table.Column<int>(type: "int", nullable: false, defaultValue: 60),
                    IsNightShift = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ToleranceMinutes = table.Column<int>(type: "int", nullable: false, defaultValue: 15)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.CheckConstraint("CK_Shift_BreakMinutes", "[BreakMinutes] >= 0");
                    table.CheckConstraint("CK_Shift_Name_NotEmpty", "LEN(LTRIM(RTRIM([Name]))) > 0");
                    table.CheckConstraint("CK_Shift_Start_End", "[StartTime] <> [EndTime]");
                    table.CheckConstraint("CK_Shift_ToleranceMinutes", "[ToleranceMinutes] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Tages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationInterviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterviewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false, defaultValue: 60),
                    InterviewType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MeetingLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationInterviews", x => x.Id);
                    table.CheckConstraint("CK_ApplicationInterview_Score", "[Score] >= 0 AND [Score] <= 100");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ApprovedByEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OfferDocumentUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationOffers", x => x.Id);
                    table.CheckConstraint("CK_ApplicationOffer_Dates", "[ExpiryDate] >= [OfferDate]");
                    table.CheckConstraint("CK_ApplicationOffer_OfferedSalary", "[OfferedSalary] >= 0");
                    table.CheckConstraint("CK_ApplicationOffer_ResponseDate", "[ResponseDate] IS NULL OR [ResponseDate] >= [OfferDate]");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProbationEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Terms = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.CheckConstraint("CK_Contract_BaseSalary", "[BaseSalary] >= 0");
                    table.CheckConstraint("CK_Contract_Dates", "[EndDate] IS NULL OR [EndDate] >= [StartDate]");
                    table.CheckConstraint("CK_Contract_Probation", "[ProbationEndDate] IS NULL OR [ProbationEndDate] >= [StartDate]");
                    table.CheckConstraint("CK_Contract_SignedDate", "[SignedDate] IS NULL OR [SignedDate] <= [StartDate]");
                    table.ForeignKey(
                        name: "FK_Contracts_ApplicationOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "ApplicationOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobPostingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Pending"),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CoverLetter = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    ResumeSnapshotUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.CheckConstraint("CK_Application_AppliedDate", "[AppliedDate] <= GETDATE()");
                    table.ForeignKey(
                        name: "FK_Applications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Addres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPosts_BlogCategorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetCodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceType = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UploadedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_AspNetUsers_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    SessionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancellationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supporters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supporters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supporters_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTags",
                columns: table => new
                {
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTags", x => new { x.BlogPostId, x.BlogTagId });
                    table.ForeignKey(
                        name: "FK_BlogPostTags_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTags_Tages_BlogTagId",
                        column: x => x.BlogTagId,
                        principalTable: "Tages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    RelatedSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notifications_Sessions_RelatedSessionId",
                        column: x => x.RelatedSessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressTrackers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressTrackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressTrackers_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProgressTrackers_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionRate = table.Column<int>(type: "int", nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LateMinutes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    EarlyLeaveMinutes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ActualWorkedTime = table.Column<TimeSpan>(type: "time", nullable: false, defaultValue: new TimeSpan(0, 0, 0, 0, 0)),
                    Overtime = table.Column<TimeSpan>(type: "time", nullable: false, defaultValue: new TimeSpan(0, 0, 0, 0, 0)),
                    AttendanceStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.CheckConstraint("CK_Attendance_LateMinutes", "[LateMinutes] >= 0");
                    table.ForeignKey(
                        name: "FK_Attendances_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.CheckConstraint("CK_Department_Name_NotEmpty", "LEN(LTRIM(RTRIM([Name]))) > 0");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    EmployeeCodePrefix = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.CheckConstraint("CK_Position_BasicSalary", "[BasicSalary] >= 0");
                    table.CheckConstraint("CK_Position_EmployeeCodePrefix", "LEN(LTRIM(RTRIM([EmployeeCodePrefix]))) > 0");
                    table.CheckConstraint("CK_Position_Name_NotEmpty", "LEN(LTRIM(RTRIM([Name]))) > 0");
                    table.ForeignKey(
                        name: "FK_Positions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EmergencyContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmploymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.CheckConstraint("CK_Employee_FirstName_NotEmpty", "LEN(LTRIM(RTRIM([FirstName]))) > 0");
                    table.CheckConstraint("CK_Employee_LastName_NotEmpty", "LEN(LTRIM(RTRIM([LastName]))) > 0");
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorProfiles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MinSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vacancies = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    WorkMode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExperienceLevel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Draft"),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HiringManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.Id);
                    table.CheckConstraint("CK_JobPosting_ClosingDate", "[ClosingDate] >= [PublishedDate]");
                    table.CheckConstraint("CK_JobPosting_MaxSalary", "[MaxSalary] >= 0");
                    table.CheckConstraint("CK_JobPosting_MinSalary", "[MinSalary] >= 0");
                    table.CheckConstraint("CK_JobPosting_Salary", "[MaxSalary] >= [MinSalary]");
                    table.CheckConstraint("CK_JobPosting_Title_NotEmpty", "LEN(LTRIM(RTRIM([Title]))) > 0");
                    table.CheckConstraint("CK_JobPosting_Vacancies", "[Vacancies] > 0");
                    table.ForeignKey(
                        name: "FK_JobPostings_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Employees_HiringManagerId",
                        column: x => x.HiringManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApprovedByEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManagerComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.CheckConstraint("CK_LeaveRequest_Dates", "[EndDate] >= [StartDate]");
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_ApprovedByEmployeeId",
                        column: x => x.ApprovedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrollType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.Id);
                    table.CheckConstraint("CK_Payroll_Amount", "[Amount] >= 0");
                    table.ForeignKey(
                        name: "FK_Payrolls_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GeneralComment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    OverallScore = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviews", x => x.Id);
                    table.CheckConstraint("CK_PerformanceReview_OverallScore", "[OverallScore] >= 0 AND [OverallScore] <= 100");
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Employees_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_DoctorProfiles_DoctorProfileId",
                        column: x => x.DoctorProfileId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviewItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformanceReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformanceCriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviewItems", x => x.Id);
                    table.CheckConstraint("CK_PerformanceReviewItem_Score", "[Score] >= 0 AND [Score] <= 100");
                    table.ForeignKey(
                        name: "FK_PerformanceReviewItems_PerformanceCriteria_PerformanceCriteriaId",
                        column: x => x.PerformanceCriteriaId,
                        principalTable: "PerformanceCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceReviewItems_PerformanceReviews_PerformanceReviewId",
                        column: x => x.PerformanceReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferredTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicBookings_DoctorProfiles_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicBookings_DoctorSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "DoctorSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationInterviews_ApplicationId",
                table: "ApplicationInterviews",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationInterviews_InterviewerId",
                table: "ApplicationInterviews",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationInterviews_ScheduledAt",
                table: "ApplicationInterviews",
                column: "ScheduledAt");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOffers_ApplicationId",
                table: "ApplicationOffers",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOffers_ApprovedByEmployeeId",
                table: "ApplicationOffers",
                column: "ApprovedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOffers_Status",
                table: "ApplicationOffers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppliedDate",
                table: "Applications",
                column: "AppliedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CandidateId_JobPostingId",
                table: "Applications",
                columns: new[] { "CandidateId", "JobPostingId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobPostingId",
                table: "Applications",
                column: "JobPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Status",
                table: "Applications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorProfileId",
                table: "AspNetUsers",
                column: "DoctorProfileId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_AttendanceStatus",
                table: "Attendances",
                column: "AttendanceStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId_AttendanceDate",
                table: "Attendances",
                columns: new[] { "EmployeeId", "AttendanceDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ShiftId",
                table: "Attendances",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CategoryId",
                table: "BlogPosts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTags_BlogTagId",
                table: "BlogPostTags",
                column: "BlogTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                table: "Candidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PhoneNumber",
                table: "Candidates",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceiverId",
                table: "ChatMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId_ReceiverId",
                table: "ChatMessages",
                columns: new[] { "SenderId", "ReceiverId" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OfferId",
                table: "Contracts",
                column: "OfferId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StartDate",
                table: "Contracts",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Status",
                table: "Contracts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_EmployeeId",
                table: "DoctorProfiles",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorProfileId",
                table: "DoctorSchedules",
                column: "DoctorProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AppUserId",
                table: "Employees",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeCode",
                table: "Employees",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalId",
                table: "Employees",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftId",
                table: "Employees",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_DepartmentId_PositionId",
                table: "JobPostings",
                columns: new[] { "DepartmentId", "PositionId" });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_HiringManagerId",
                table: "JobPostings",
                column: "HiringManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_PositionId",
                table: "JobPostings",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_PublishedDate",
                table: "JobPostings",
                column: "PublishedDate");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_Status",
                table: "JobPostings",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_Title",
                table: "JobPostings",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ApprovedByEmployeeId",
                table: "LeaveRequests",
                column: "ApprovedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EndDate",
                table: "LeaveRequests",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_StartDate",
                table: "LeaveRequests",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_Status",
                table: "LeaveRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_Name",
                table: "LeaveTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientId",
                table: "Notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RelatedSessionId",
                table: "Notifications",
                column: "RelatedSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetCodes_UserId",
                table: "PasswordResetCodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_UserId",
                table: "PatientProfiles",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_EffectiveDate",
                table: "Payrolls",
                column: "EffectiveDate");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_EmployeeId",
                table: "Payrolls",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_PayrollType",
                table: "Payrolls",
                column: "PayrollType");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_ReferenceNumber",
                table: "Payrolls",
                column: "ReferenceNumber",
                unique: true,
                filter: "[ReferenceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_Status",
                table: "Payrolls",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceCriteria_Name",
                table: "PerformanceCriteria",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviewItems_PerformanceCriteriaId",
                table: "PerformanceReviewItems",
                column: "PerformanceCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviewItems_PerformanceReviewId",
                table: "PerformanceReviewItems",
                column: "PerformanceReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviewItems_PerformanceReviewId_PerformanceCriteriaId",
                table: "PerformanceReviewItems",
                columns: new[] { "PerformanceReviewId", "PerformanceCriteriaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_EmployeeId",
                table: "PerformanceReviews",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_EmployeeId_ReviewDate",
                table: "PerformanceReviews",
                columns: new[] { "EmployeeId", "ReviewDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewDate",
                table: "PerformanceReviews",
                column: "ReviewDate");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewerId",
                table: "PerformanceReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_Status",
                table: "PerformanceReviews",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId_Name",
                table: "Positions",
                columns: new[] { "DepartmentId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressTrackers_PatientId",
                table: "ProgressTrackers",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressTrackers_SessionId",
                table: "ProgressTrackers",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicBookings_DoctorId",
                table: "PublicBookings",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicBookings_ScheduleId",
                table: "PublicBookings",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_DoctorId",
                table: "Reports",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PatientId",
                table: "Reports",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SessionId",
                table: "Reports",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_UploadedById",
                table: "Resources",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DepartmentId",
                table: "Services",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_DoctorId",
                table: "Sessions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_PatientId",
                table: "Sessions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_Name",
                table: "Shifts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supporters_PatientId",
                table: "Supporters",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationInterviews_Applications_ApplicationId",
                table: "ApplicationInterviews",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationInterviews_Employees_InterviewerId",
                table: "ApplicationInterviews",
                column: "InterviewerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationOffers_Applications_ApplicationId",
                table: "ApplicationOffers",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationOffers_Employees_ApprovedByEmployeeId",
                table: "ApplicationOffers",
                column: "ApprovedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_JobPostings_JobPostingId",
                table: "Applications",
                column: "JobPostingId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DoctorProfiles_DoctorProfileId",
                table: "AspNetUsers",
                column: "DoctorProfileId",
                principalTable: "DoctorProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_EmployeeId",
                table: "Attendances",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_ManagerId",
                table: "Departments",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_ManagerId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorProfiles_Employees_EmployeeId",
                table: "DoctorProfiles");

            migrationBuilder.DropTable(
                name: "ApplicationInterviews");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "BlogPostTags");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PasswordResetCodes");

            migrationBuilder.DropTable(
                name: "PatientProfiles");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "PerformanceReviewItems");

            migrationBuilder.DropTable(
                name: "ProgressTrackers");

            migrationBuilder.DropTable(
                name: "PublicBookings");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Supporters");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tages");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "ApplicationOffers");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "PerformanceCriteria");

            migrationBuilder.DropTable(
                name: "PerformanceReviews");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "BlogCategorys");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "DoctorProfiles");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
