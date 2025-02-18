CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE TABLE "Branches" (
        "Id" int NOT NULL,
        "Title" nvarchar(100) NOT NULL,
        "Description" nvarchar(500) NOT NULL,
        "CreatedAt" datetime2 NOT NULL,
        "UpdatedAt" datetime2 NOT NULL,
        CONSTRAINT "PK_Branches" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE TABLE "Patients" (
        "Id" uniqueidentifier NOT NULL,
        "Name" nvarchar(100) NOT NULL,
        "Email" nvarchar(200) NOT NULL,
        "PasswordHash" nvarchar(max) NOT NULL,
        CONSTRAINT "PK_Patients" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE TABLE "Doctors" (
        "Id" uniqueidentifier NOT NULL,
        "Name" nvarchar(100) NOT NULL,
        "Email" nvarchar(200) NOT NULL,
        "PasswordHash" nvarchar(max) NOT NULL,
        "Description" nvarchar(500) NOT NULL,
        "Image" nvarchar(200),
        "BranchId" int NOT NULL,
        CONSTRAINT "PK_Doctors" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Doctors_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE TABLE "Appointments" (
        "Id" uniqueidentifier NOT NULL,
        "DoctorId" uniqueidentifier NOT NULL,
        "PatientId" uniqueidentifier NOT NULL,
        "Time" datetime2 NOT NULL,
        "Status" nvarchar(50) NOT NULL,
        "Notes" nvarchar(500) NOT NULL,
        CONSTRAINT "PK_Appointments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Appointments_Doctors_DoctorId" FOREIGN KEY ("DoctorId") REFERENCES "Doctors" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_Appointments_Patients_PatientId" FOREIGN KEY ("PatientId") REFERENCES "Patients" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE TABLE "Processes" (
        "Id" uniqueidentifier NOT NULL,
        "Name" nvarchar(150) NOT NULL,
        "Duration" int NOT NULL,
        "DoctorId" uniqueidentifier NOT NULL,
        CONSTRAINT "PK_Processes" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Processes_Doctors_DoctorId" FOREIGN KEY ("DoctorId") REFERENCES "Doctors" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE TABLE "Ratings" (
        "Id" uniqueidentifier NOT NULL,
        "DoctorId" uniqueidentifier NOT NULL,
        "PatientId" uniqueidentifier NOT NULL,
        "Score" int NOT NULL,
        "Comment" nvarchar(500) NOT NULL,
        "CreatedAt" datetime2 NOT NULL,
        CONSTRAINT "PK_Ratings" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Ratings_Doctors_DoctorId" FOREIGN KEY ("DoctorId") REFERENCES "Doctors" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_Ratings_Patients_PatientId" FOREIGN KEY ("PatientId") REFERENCES "Patients" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE TABLE "TimeSlots" (
        "Id" uniqueidentifier NOT NULL,
        "DoctorId" uniqueidentifier NOT NULL,
        "AvailableFrom" time NOT NULL,
        "AvailableTo" time NOT NULL,
        "AvailableDay" int NOT NULL,
        "AppointmentFrequency" int NOT NULL,
        "ProcessId" uniqueidentifier,
        CONSTRAINT "PK_TimeSlots" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_TimeSlots_Doctors_DoctorId" FOREIGN KEY ("DoctorId") REFERENCES "Doctors" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_TimeSlots_Processes_ProcessId" FOREIGN KEY ("ProcessId") REFERENCES "Processes" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE INDEX "IX_Appointments_DoctorId" ON "Appointments" ("DoctorId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE INDEX "IX_Appointments_PatientId" ON "Appointments" ("PatientId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE INDEX "IX_Doctors_BranchId" ON "Doctors" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE INDEX "IX_Processes_DoctorId" ON "Processes" ("DoctorId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE INDEX "IX_Ratings_DoctorId" ON "Ratings" ("DoctorId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE INDEX "IX_Ratings_PatientId" ON "Ratings" ("PatientId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE INDEX "IX_TimeSlots_DoctorId" ON "TimeSlots" ("DoctorId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    CREATE INDEX "IX_TimeSlots_ProcessId" ON "TimeSlots" ("ProcessId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218185849_InitialCreateForLocalhost') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250218185849_InitialCreateForLocalhost', '9.0.2');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "TimeSlots" ALTER COLUMN "ProcessId" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "TimeSlots" ALTER COLUMN "DoctorId" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "TimeSlots" ALTER COLUMN "AvailableTo" TYPE interval;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "TimeSlots" ALTER COLUMN "AvailableFrom" TYPE interval;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "TimeSlots" ALTER COLUMN "AvailableDay" TYPE integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "TimeSlots" ALTER COLUMN "AppointmentFrequency" TYPE integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "TimeSlots" ALTER COLUMN "Id" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Ratings" ALTER COLUMN "Score" TYPE integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Ratings" ALTER COLUMN "PatientId" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Ratings" ALTER COLUMN "DoctorId" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Ratings" ALTER COLUMN "CreatedAt" TYPE timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Ratings" ALTER COLUMN "Comment" TYPE character varying(500);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Ratings" ALTER COLUMN "Id" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Processes" ALTER COLUMN "Name" TYPE character varying(150);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Processes" ALTER COLUMN "Duration" TYPE integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Processes" ALTER COLUMN "DoctorId" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Processes" ALTER COLUMN "Id" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Patients" ALTER COLUMN "PasswordHash" TYPE text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Patients" ALTER COLUMN "Name" TYPE character varying(100);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Patients" ALTER COLUMN "Email" TYPE character varying(200);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Patients" ALTER COLUMN "Id" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Doctors" ALTER COLUMN "PasswordHash" TYPE text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Doctors" ALTER COLUMN "Name" TYPE character varying(100);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Doctors" ALTER COLUMN "Image" TYPE character varying(200);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Doctors" ALTER COLUMN "Email" TYPE character varying(200);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Doctors" ALTER COLUMN "Description" TYPE character varying(500);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Doctors" ALTER COLUMN "BranchId" TYPE integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Doctors" ALTER COLUMN "Id" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Branches" ALTER COLUMN "UpdatedAt" TYPE timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Branches" ALTER COLUMN "Title" TYPE character varying(100);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Branches" ALTER COLUMN "Description" TYPE character varying(500);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Branches" ALTER COLUMN "CreatedAt" TYPE timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Branches" ALTER COLUMN "Id" TYPE integer;
    ALTER TABLE "Branches" ALTER COLUMN "Id" DROP DEFAULT;
    ALTER TABLE "Branches" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Appointments" ALTER COLUMN "Time" TYPE timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Appointments" ALTER COLUMN "Status" TYPE character varying(50);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Appointments" ALTER COLUMN "PatientId" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Appointments" ALTER COLUMN "Notes" TYPE character varying(500);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Appointments" ALTER COLUMN "DoctorId" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    ALTER TABLE "Appointments" ALTER COLUMN "Id" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250218190749_initialCreateForProduction') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250218190749_initialCreateForProduction', '9.0.2');
    END IF;
END $EF$;
COMMIT;

