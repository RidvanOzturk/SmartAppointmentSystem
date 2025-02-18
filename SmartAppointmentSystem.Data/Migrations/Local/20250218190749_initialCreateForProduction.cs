    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    #nullable disable

    namespace SmartAppointmentSystem.Data.Migrations.Local
    {
        /// <inheritdoc />
        public partial class initialCreateForProduction : Migration
        {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.AlterColumn<Guid>(
                    name: "ProcessId",
                    table: "TimeSlots",
                    type: "uuid",
                    nullable: true,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier",
                    oldNullable: true);

                migrationBuilder.AlterColumn<Guid>(
                    name: "DoctorId",
                    table: "TimeSlots",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<TimeSpan>(
                    name: "AvailableTo",
                    table: "TimeSlots",
                    type: "interval",
                    nullable: false,
                    oldClrType: typeof(TimeSpan),
                    oldType: "time");

                migrationBuilder.AlterColumn<TimeSpan>(
                    name: "AvailableFrom",
                    table: "TimeSlots",
                    type: "interval",
                    nullable: false,
                    oldClrType: typeof(TimeSpan),
                    oldType: "time");

                migrationBuilder.AlterColumn<int>(
                    name: "AvailableDay",
                    table: "TimeSlots",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "AppointmentFrequency",
                    table: "TimeSlots",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "TimeSlots",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<int>(
                    name: "Score",
                    table: "Ratings",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<Guid>(
                    name: "PatientId",
                    table: "Ratings",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<Guid>(
                    name: "DoctorId",
                    table: "Ratings",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<DateTime>(
                    name: "CreatedAt",
                    table: "Ratings",
                    type: "timestamp with time zone",
                    nullable: false,
                    oldClrType: typeof(DateTime),
                    oldType: "datetime2");

                migrationBuilder.AlterColumn<string>(
                    name: "Comment",
                    table: "Ratings",
                    type: "character varying(500)",
                    maxLength: 500,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(500)",
                    oldMaxLength: 500);

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Ratings",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<string>(
                    name: "Name",
                    table: "Processes",
                    type: "character varying(150)",
                    maxLength: 150,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(150)",
                    oldMaxLength: 150);

                migrationBuilder.AlterColumn<int>(
                    name: "Duration",
                    table: "Processes",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<Guid>(
                    name: "DoctorId",
                    table: "Processes",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Processes",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<string>(
                    name: "PasswordHash",
                    table: "Patients",
                    type: "text",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(max)");

                migrationBuilder.AlterColumn<string>(
                    name: "Name",
                    table: "Patients",
                    type: "character varying(100)",
                    maxLength: 100,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(100)",
                    oldMaxLength: 100);

                migrationBuilder.AlterColumn<string>(
                    name: "Email",
                    table: "Patients",
                    type: "character varying(200)",
                    maxLength: 200,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(200)",
                    oldMaxLength: 200);

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Patients",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<string>(
                    name: "PasswordHash",
                    table: "Doctors",
                    type: "text",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(max)");

                migrationBuilder.AlterColumn<string>(
                    name: "Name",
                    table: "Doctors",
                    type: "character varying(100)",
                    maxLength: 100,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(100)",
                    oldMaxLength: 100);

                migrationBuilder.AlterColumn<string>(
                    name: "Image",
                    table: "Doctors",
                    type: "character varying(200)",
                    maxLength: 200,
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(200)",
                    oldMaxLength: 200,
                    oldNullable: true);

                migrationBuilder.AlterColumn<string>(
                    name: "Email",
                    table: "Doctors",
                    type: "character varying(200)",
                    maxLength: 200,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(200)",
                    oldMaxLength: 200);

                migrationBuilder.AlterColumn<string>(
                    name: "Description",
                    table: "Doctors",
                    type: "character varying(500)",
                    maxLength: 500,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(500)",
                    oldMaxLength: 500);

                migrationBuilder.AlterColumn<int>(
                    name: "BranchId",
                    table: "Doctors",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Doctors",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<DateTime>(
                    name: "UpdatedAt",
                    table: "Branches",
                    type: "timestamp with time zone",
                    nullable: false,
                    oldClrType: typeof(DateTime),
                    oldType: "datetime2");

                migrationBuilder.AlterColumn<string>(
                    name: "Title",
                    table: "Branches",
                    type: "character varying(100)",
                    maxLength: 100,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(100)",
                    oldMaxLength: 100);

                migrationBuilder.AlterColumn<string>(
                    name: "Description",
                    table: "Branches",
                    type: "character varying(500)",
                    maxLength: 500,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(500)",
                    oldMaxLength: 500);

                migrationBuilder.AlterColumn<DateTime>(
                    name: "CreatedAt",
                    table: "Branches",
                    type: "timestamp with time zone",
                    nullable: false,
                    oldClrType: typeof(DateTime),
                    oldType: "datetime2");

                migrationBuilder.AlterColumn<int>(
                    name: "Id",
                    table: "Branches",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                migrationBuilder.AlterColumn<DateTime>(
                    name: "Time",
                    table: "Appointments",
                    type: "timestamp with time zone",
                    nullable: false,
                    oldClrType: typeof(DateTime),
                    oldType: "datetime2");

                migrationBuilder.AlterColumn<string>(
                    name: "Status",
                    table: "Appointments",
                    type: "character varying(50)",
                    maxLength: 50,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(50)",
                    oldMaxLength: 50);

                migrationBuilder.AlterColumn<Guid>(
                    name: "PatientId",
                    table: "Appointments",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<string>(
                    name: "Notes",
                    table: "Appointments",
                    type: "character varying(500)",
                    maxLength: 500,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(500)",
                    oldMaxLength: 500);

                migrationBuilder.AlterColumn<Guid>(
                    name: "DoctorId",
                    table: "Appointments",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Appointments",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier");
            }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.AlterColumn<Guid>(
                    name: "ProcessId",
                    table: "TimeSlots",
                    type: "uniqueidentifier",
                    nullable: true,
                    oldClrType: typeof(Guid),
                    oldType: "uuid",
                    oldNullable: true);

                migrationBuilder.AlterColumn<Guid>(
                    name: "DoctorId",
                    table: "TimeSlots",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<TimeSpan>(
                    name: "AvailableTo",
                    table: "TimeSlots",
                    type: "time",
                    nullable: false,
                    oldClrType: typeof(TimeSpan),
                    oldType: "interval");

                migrationBuilder.AlterColumn<TimeSpan>(
                    name: "AvailableFrom",
                    table: "TimeSlots",
                    type: "time",
                    nullable: false,
                    oldClrType: typeof(TimeSpan),
                    oldType: "interval");

                migrationBuilder.AlterColumn<int>(
                    name: "AvailableDay",
                    table: "TimeSlots",
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer");

                migrationBuilder.AlterColumn<int>(
                    name: "AppointmentFrequency",
                    table: "TimeSlots",
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "TimeSlots",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<int>(
                    name: "Score",
                    table: "Ratings",
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer");

                migrationBuilder.AlterColumn<Guid>(
                    name: "PatientId",
                    table: "Ratings",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<Guid>(
                    name: "DoctorId",
                    table: "Ratings",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<DateTime>(
                    name: "CreatedAt",
                    table: "Ratings",
                    type: "datetime2",
                    nullable: false,
                    oldClrType: typeof(DateTime),
                    oldType: "timestamp with time zone");

                migrationBuilder.AlterColumn<string>(
                    name: "Comment",
                    table: "Ratings",
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(500)",
                    oldMaxLength: 500);

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Ratings",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<string>(
                    name: "Name",
                    table: "Processes",
                    type: "nvarchar(150)",
                    maxLength: 150,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(150)",
                    oldMaxLength: 150);

                migrationBuilder.AlterColumn<int>(
                    name: "Duration",
                    table: "Processes",
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer");

                migrationBuilder.AlterColumn<Guid>(
                    name: "DoctorId",
                    table: "Processes",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Processes",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<string>(
                    name: "PasswordHash",
                    table: "Patients",
                    type: "nvarchar(max)",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "text");

                migrationBuilder.AlterColumn<string>(
                    name: "Name",
                    table: "Patients",
                    type: "nvarchar(100)",
                    maxLength: 100,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(100)",
                    oldMaxLength: 100);

                migrationBuilder.AlterColumn<string>(
                    name: "Email",
                    table: "Patients",
                    type: "nvarchar(200)",
                    maxLength: 200,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(200)",
                    oldMaxLength: 200);

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Patients",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<string>(
                    name: "PasswordHash",
                    table: "Doctors",
                    type: "nvarchar(max)",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "text");

                migrationBuilder.AlterColumn<string>(
                    name: "Name",
                    table: "Doctors",
                    type: "nvarchar(100)",
                    maxLength: 100,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(100)",
                    oldMaxLength: 100);

                migrationBuilder.AlterColumn<string>(
                    name: "Image",
                    table: "Doctors",
                    type: "nvarchar(200)",
                    maxLength: 200,
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "character varying(200)",
                    oldMaxLength: 200,
                    oldNullable: true);

                migrationBuilder.AlterColumn<string>(
                    name: "Email",
                    table: "Doctors",
                    type: "nvarchar(200)",
                    maxLength: 200,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(200)",
                    oldMaxLength: 200);

                migrationBuilder.AlterColumn<string>(
                    name: "Description",
                    table: "Doctors",
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(500)",
                    oldMaxLength: 500);

                migrationBuilder.AlterColumn<int>(
                    name: "BranchId",
                    table: "Doctors",
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Doctors",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<DateTime>(
                    name: "UpdatedAt",
                    table: "Branches",
                    type: "datetime2",
                    nullable: false,
                    oldClrType: typeof(DateTime),
                    oldType: "timestamp with time zone");

                migrationBuilder.AlterColumn<string>(
                    name: "Title",
                    table: "Branches",
                    type: "nvarchar(100)",
                    maxLength: 100,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(100)",
                    oldMaxLength: 100);

                migrationBuilder.AlterColumn<string>(
                    name: "Description",
                    table: "Branches",
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(500)",
                    oldMaxLength: 500);

                migrationBuilder.AlterColumn<DateTime>(
                    name: "CreatedAt",
                    table: "Branches",
                    type: "datetime2",
                    nullable: false,
                    oldClrType: typeof(DateTime),
                    oldType: "timestamp with time zone");

                migrationBuilder.AlterColumn<int>(
                    name: "Id",
                    table: "Branches",
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer")
                    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                migrationBuilder.AlterColumn<DateTime>(
                    name: "Time",
                    table: "Appointments",
                    type: "datetime2",
                    nullable: false,
                    oldClrType: typeof(DateTime),
                    oldType: "timestamp with time zone");

                migrationBuilder.AlterColumn<string>(
                    name: "Status",
                    table: "Appointments",
                    type: "nvarchar(50)",
                    maxLength: 50,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(50)",
                    oldMaxLength: 50);

                migrationBuilder.AlterColumn<Guid>(
                    name: "PatientId",
                    table: "Appointments",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<string>(
                    name: "Notes",
                    table: "Appointments",
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(500)",
                    oldMaxLength: 500);

                migrationBuilder.AlterColumn<Guid>(
                    name: "DoctorId",
                    table: "Appointments",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "Appointments",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uuid");
            }
        }
    }
