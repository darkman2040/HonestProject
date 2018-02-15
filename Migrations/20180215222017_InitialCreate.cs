﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HonestProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PublicIdentifier = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HoursPerDay = table.Column<int>(nullable: false),
                    IncludeWeekends = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PublicIdentifier = table.Column<Guid>(nullable: false),
                    UniqueSiteId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    PublicIdentifier = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<int>(nullable: true),
                    SiteID = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Site_SiteID",
                        column: x => x.SiteID,
                        principalTable: "Site",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PublicIdentifier = table.Column<Guid>(nullable: false),
                    SiteID = table.Column<int>(nullable: false),
                    TeamLeaderID = table.Column<int>(nullable: true),
                    TeamManagerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Team_Site_SiteID",
                        column: x => x.SiteID,
                        principalTable: "Site",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_User_TeamLeaderID",
                        column: x => x.TeamLeaderID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_User_TeamManagerID",
                        column: x => x.TeamManagerID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeEntry",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hours = table.Column<int>(nullable: false),
                    Minutes = table.Column<int>(nullable: false),
                    TaskDescription = table.Column<string>(maxLength: 50, nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeEntry_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_SiteID",
                table: "Team",
                column: "SiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamLeaderID",
                table: "Team",
                column: "TeamLeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamManagerID",
                table: "Team",
                column: "TeamManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_UserID",
                table: "TimeEntry",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_SiteID",
                table: "User",
                column: "SiteID");

            migrationBuilder.CreateIndex(
                name: "IX_User_TeamID",
                table: "User",
                column: "TeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Team_TeamID",
                table: "User",
                column: "TeamID",
                principalTable: "Team",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(@"INSERT INTO Role (Description, Name, PublicIdentifier) 
                VALUES('Has access to all aspects and features of Honest Project. Has specific privilges for maintaing the site.', 'Site Administrator', NEWID()),
                ('Manages several teams and has access to team data. Able to reconfigure teams.', 'Manager', NEWID()),
                ('Manages an individual team. Can see team members data. Can create projects for a team', 'Team Leader', NEWID()),
                ('Individual team member. Can see team project data and own time sheets.', 'Team Member', NEWID())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Site_SiteID",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Site_SiteID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_User_TeamLeaderID",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_User_TeamManagerID",
                table: "Team");

            migrationBuilder.DropTable(
                name: "TimeEntry");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
