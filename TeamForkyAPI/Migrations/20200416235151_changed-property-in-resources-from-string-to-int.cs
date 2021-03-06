﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamForkyAPI.Migrations
{
    public partial class changedpropertyinresourcesfromstringtoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Birthday = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CheckIn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ResourcesType = table.Column<int>(nullable: false),
                    PatientID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resources_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientResources",
                columns: table => new
                {
                    PatientID = table.Column<int>(nullable: false),
                    ResourcesID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientResources", x => new { x.PatientID, x.ResourcesID });
                    table.ForeignKey(
                        name: "FK_PatientResources_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientResources_Resources_ResourcesID",
                        column: x => x.ResourcesID,
                        principalTable: "Resources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "ID", "Birthday", "CheckIn", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "02/16/1991", new DateTime(2020, 4, 16, 16, 51, 50, 688, DateTimeKind.Local).AddTicks(8172), "Teddy", 0 },
                    { 2, "03/23/1986", new DateTime(2020, 4, 16, 16, 51, 50, 692, DateTimeKind.Local).AddTicks(1504), "Joseph", 0 },
                    { 3, "08/29/1992", new DateTime(2020, 4, 16, 16, 51, 50, 692, DateTimeKind.Local).AddTicks(1564), "Matthew", 0 }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "ID", "Description", "Name", "PatientID", "ResourcesType" },
                values: new object[,]
                {
                    { 1, "Specialist in C# surgery", "Dr. Amanda", null, 0 },
                    { 2, "Specialist in education touch up", "Dr. Brook", null, 0 },
                    { 3, "Bacteria sanitizer", "Microwave", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "PatientResources",
                columns: new[] { "PatientID", "ResourcesID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientResources_ResourcesID",
                table: "PatientResources",
                column: "ResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_PatientID",
                table: "Resources",
                column: "PatientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientResources");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
