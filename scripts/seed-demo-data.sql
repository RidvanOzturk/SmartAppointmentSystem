-- SmartAppointmentSystem demo dataset v1. PostgreSQL / Supabase SQL Editor.
-- Requires the existing application schema. Does not run migrations or change RLS.
-- All accounts are fictional. Password: DemoRandevu!26 (BCrypt, work factor 11).
-- Re-running adds missing fixtures; it never updates/deletes existing records.
BEGIN;
SET LOCAL client_encoding = 'UTF8';
SET LOCAL lock_timeout = '10s';
SET LOCAL statement_timeout = '120s';
SET LOCAL TIME ZONE 'UTC';

-- Serialize with application writes, including bookings, for this short seed transaction.
LOCK TABLE public."Branches", public."Doctors", public."Patients",
    public."TimeSlots", public."Appointments", public."Ratings"
    IN SHARE ROW EXCLUSIVE MODE;

DO $seed$
DECLARE
    -- The optional date override is intended for reproducible tests only.
    today date := COALESCE(NULLIF(current_setting('smartappointment.seed_today', true), '')::date,
                           (CURRENT_TIMESTAMP AT TIME ZONE 'Europe/Istanbul')::date);
    password_hash text := '$2a$11$8giN0kmHcRvx5ltj7gGaqe0w9wLDg06//du0.VE7X3NxcX0aJP2Ku';
    branch_titles text[] := ARRAY['Kardiyoloji', 'Dermatoloji', 'Ortopedi', 'Göz Hastalıkları',
        'Kulak Burun Boğaz', 'Dahiliye', 'Nöroloji', 'Çocuk Sağlığı ve Hastalıkları'];
    branch_descriptions text[] := ARRAY['Kalp ve damar sağlığı polikliniği.', 'Cilt sağlığı polikliniği.',
        'Kas ve iskelet sağlığı polikliniği.', 'Göz sağlığı ve görme kontrolleri.',
        'Kulak, burun ve boğaz polikliniği.', 'İç hastalıkları polikliniği.',
        'Sinir sistemi polikliniği.', 'Çocuk sağlığı ve gelişim takibi polikliniği.'];
    doctor_names text[] := ARRAY['Ayşe Yılmaz', 'Emre Demir', 'Selin Kaya', 'Deniz Aydın',
        'Elif Şahin', 'Mert Koç', 'Zeynep Arslan', 'Burak Çelik', 'Derya Yıldız',
        'Ece Aksoy', 'Onur Güneş', 'İrem Eren', 'Can Öztürk', 'Aslı Polat', 'Kerem Acar',
        'Seda Tekin', 'Oğuz Yalçın', 'Melis Erdem', 'Cem Kılıç', 'Buse Sezer',
        'Arda Toprak', 'Nil Karaca', 'Umut Şen', 'İpek Kurt'];
    patient_first_names text[] := ARRAY['Ada', 'Ali', 'Eylül', 'Efe', 'Nehir', 'Deniz',
        'Defne', 'Mete', 'Yağmur', 'Kaan', 'Duru', 'Bora'];
    patient_last_names text[] := ARRAY['Yalın', 'Işık', 'Bulut', 'Çınar'];
    comments text[] := ARRAY['Randevu saatinde başladık, açıklamalar anlaşılırdı.',
        'Kontrol süreci düzenliydi, sorularıma yanıt aldım.',
        'İletişim iyiydi, kısa bir bekleme oldu.', 'Muayene ve takip bilgilendirmesinden memnun kaldım.'];
    branch_ids integer[] := ARRAY[]::integer[];
    branch_id integer;
    branch_count integer;
    doctor_id uuid;
    patient_id uuid;
    slot_id uuid;
    appointment_id uuid;
    rating_id uuid;
    email text;
    b integer;
    d integer;
    p integer;
    weekday integer;
    day_offset integer;
    visit integer;
    shift integer;
    from_time interval;
    to_time interval;
    visit_date date;
    visit_local timestamp;
    visit_utc timestamptz;
    last_completed timestamptz;
    existing_slot record;
    existing_appointment record;
    inserted_branches integer := 0;
    inserted_doctors integer := 0;
    inserted_patients integer := 0;
    inserted_slots integer := 0;
    inserted_appointments integer := 0;
    inserted_ratings integer := 0;
    skipped_conflicts integer := 0;
BEGIN
    FOR b IN 1..8 LOOP
        SELECT count(*), min("Id") INTO branch_count, branch_id
        FROM public."Branches" WHERE "Title" = branch_titles[b];
        IF branch_count > 1 THEN
            RAISE EXCEPTION 'Multiple branches named %. Resolve the ambiguity before seeding.', branch_titles[b];
        END IF;
        IF branch_id IS NULL THEN
            INSERT INTO public."Branches" ("Title", "Description", "CreatedAt", "UpdatedAt")
            VALUES (branch_titles[b], branch_descriptions[b], CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
            RETURNING "Id" INTO branch_id;
            inserted_branches := inserted_branches + 1;
        END IF;
        branch_ids := array_append(branch_ids, branch_id);
    END LOOP;

    FOR p IN 1..48 LOOP
        patient_id := md5('smartappointment/demo/v1/patient/' || p)::uuid;
        email := 'demo.patient' || lpad(p::text, 2, '0') || '@example.test';
        IF EXISTS (SELECT 1 FROM public."Patients"
                   WHERE ("Id" = patient_id AND "Email" <> email)
                      OR (lower("Email") = email AND "Id" <> patient_id)) THEN
            RAISE EXCEPTION 'Demo patient identity collision: %', email;
        END IF;
        IF NOT EXISTS (SELECT 1 FROM public."Patients" WHERE "Id" = patient_id) THEN
            INSERT INTO public."Patients" ("Id", "Name", "Email", "PasswordHash")
            VALUES (patient_id, patient_first_names[((p - 1) % 12) + 1] || ' ' ||
                patient_last_names[((p - 1) / 12) + 1], email, password_hash);
            inserted_patients := inserted_patients + 1;
        END IF;
    END LOOP;

    FOR d IN 1..24 LOOP
        b := ((d - 1) / 3) + 1;
        shift := (d - 1) % 3;
        doctor_id := md5('smartappointment/demo/v1/doctor/' || d)::uuid;
        email := 'demo.doctor' || lpad(d::text, 2, '0') || '@example.test';
        IF EXISTS (SELECT 1 FROM public."Doctors"
                   WHERE ("Id" = doctor_id AND ("Email" <> email OR "BranchId" IS DISTINCT FROM branch_ids[b]))
                      OR (lower("Email") = email AND "Id" <> doctor_id)) THEN
            RAISE EXCEPTION 'Demo doctor identity/branch collision: %', email;
        END IF;
        IF NOT EXISTS (SELECT 1 FROM public."Doctors" WHERE "Id" = doctor_id) THEN
            INSERT INTO public."Doctors"
                ("Id", "Name", "Email", "PasswordHash", "Description", "Image", "BranchId", "CreatedAt")
            VALUES (doctor_id, doctor_names[d], email, password_hash,
                '[DEMO:v1] Kurgusal ' || branch_titles[b] || ' uzmanı. ' ||
                CASE shift WHEN 0 THEN 'Hafta içi sabah polikliniği.'
                           WHEN 1 THEN 'Her gün öğleden sonra polikliniği.'
                           ELSE 'Pazartesi-cumartesi öğleden sonra ve akşam polikliniği.' END,
                NULL, branch_ids[b], CURRENT_TIMESTAMP - interval '90 days');
            inserted_doctors := inserted_doctors + 1;
        END IF;

        from_time := CASE WHEN shift = 0 THEN interval '9 hours' ELSE interval '13 hours' END;
        to_time := CASE shift WHEN 0 THEN interval '12 hours'
                             WHEN 1 THEN interval '18 hours' ELSE interval '19 hours' END;
        FOR weekday IN 0..6 LOOP
            -- .NET DayOfWeek: Sunday=0. One continuous shift per doctor/day:
            -- the current availability service reads FirstOrDefault, not multiple shifts.
            IF (shift = 0 AND weekday IN (0, 6)) OR (shift = 2 AND weekday = 0) THEN
                CONTINUE;
            END IF;
            slot_id := md5('smartappointment/demo/v1/slot/' || d || '/' || weekday)::uuid;
            IF EXISTS (SELECT 1 FROM public."TimeSlots"
                       WHERE "DoctorId" = doctor_id AND "AvailableDay" = weekday AND "Id" <> slot_id) THEN
                RAISE EXCEPTION 'Doctor % has an additional schedule for weekday %.', doctor_id, weekday;
            END IF;
            SELECT * INTO existing_slot FROM public."TimeSlots" WHERE "Id" = slot_id;
            IF FOUND THEN
                IF existing_slot."DoctorId" <> doctor_id OR existing_slot."AvailableDay" <> weekday
                   OR existing_slot."AvailableFrom" <> from_time OR existing_slot."AvailableTo" <> to_time
                   OR existing_slot."AppointmentFrequency" <> 30 THEN
                    RAISE EXCEPTION 'Demo schedule % was modified. No data has been overwritten.', slot_id;
                END IF;
            ELSE
                IF EXISTS (SELECT 1 FROM public."TimeSlots" WHERE "DoctorId" = doctor_id AND "AvailableDay" = weekday) THEN
                    RAISE EXCEPTION 'Doctor % already has another schedule for weekday %.', doctor_id, weekday;
                END IF;
                INSERT INTO public."TimeSlots"
                    ("Id", "DoctorId", "AvailableFrom", "AvailableTo", "AvailableDay", "AppointmentFrequency")
                VALUES (slot_id, doctor_id, from_time, to_time, weekday, 30);
                inserted_slots := inserted_slots + 1;
            END IF;
        END LOOP;

        -- Past seven days + next fourteen days. No partially elapsed appointments today.
        -- Each doctor has two dedicated patients: no patient collisions across doctors.
        FOR day_offset IN -7..14 LOOP
            IF day_offset = 0 THEN CONTINUE; END IF;
            visit_date := today + day_offset;
            weekday := extract(dow FROM visit_date)::integer;
            IF (shift = 0 AND weekday IN (0, 6)) OR (shift = 2 AND weekday = 0) THEN CONTINUE; END IF;
            FOR visit IN 0..1 LOOP
                patient_id := md5('smartappointment/demo/v1/patient/' || (2 * d - 1 + visit))::uuid;
                visit_local := visit_date::timestamp + from_time + visit * interval '1 hour';
                visit_utc := visit_local AT TIME ZONE 'Europe/Istanbul';
                appointment_id := md5('smartappointment/demo/v1/appointment/' || d || '/' ||
                    to_char(visit_local, 'YYYY-MM-DD/HH24:MI'))::uuid;
                SELECT * INTO existing_appointment FROM public."Appointments" WHERE "Id" = appointment_id;
                IF FOUND THEN
                    -- Preserve changes to existing appointments, including their status/time.
                    IF existing_appointment."DoctorId" <> doctor_id OR existing_appointment."PatientId" <> patient_id THEN
                        RAISE EXCEPTION 'Demo appointment identity collision: %', appointment_id;
                    END IF;
                    CONTINUE;
                END IF;
                -- Conservative conflict handling: even cancelled rows reserve the interval here,
                -- since the current model has free-text status, not a shared status enum.
                IF EXISTS (SELECT 1 FROM public."Appointments" a
                           WHERE (a."DoctorId" = doctor_id OR a."PatientId" = patient_id)
                             AND a."Time" > visit_utc - interval '30 minutes'
                             AND a."Time" < visit_utc + interval '30 minutes') THEN
                    skipped_conflicts := skipped_conflicts + 1;
                    CONTINUE;
                END IF;
                INSERT INTO public."Appointments" ("Id", "DoctorId", "PatientId", "Time", "Status", "Notes")
                VALUES (appointment_id, doctor_id, patient_id, visit_utc,
                    CASE WHEN day_offset < 0 THEN 'Completed' ELSE 'Scheduled' END,
                    '[DEMO:v1] ' || branch_titles[b] || ' - ' ||
                    CASE WHEN visit = 0 THEN 'rutin kontrol' ELSE 'ilk görüşme' END ||
                    '. Kurgusal test kaydı; gerçek hasta bilgisi içermez.');
                inserted_appointments := inserted_appointments + 1;
            END LOOP;
        END LOOP;

        FOR visit IN 0..1 LOOP
            patient_id := md5('smartappointment/demo/v1/patient/' || (2 * d - 1 + visit))::uuid;
            rating_id := md5('smartappointment/demo/v1/rating/' || d || '/' || visit)::uuid;
            SELECT max("Time") INTO last_completed FROM public."Appointments"
            WHERE "DoctorId" = doctor_id AND "PatientId" = patient_id
              AND "Status" = 'Completed' AND "Time" < (today::timestamp AT TIME ZONE 'Europe/Istanbul');
            IF last_completed IS NOT NULL AND NOT EXISTS (
                SELECT 1 FROM public."Ratings" WHERE "DoctorId" = doctor_id AND "PatientId" = patient_id) THEN
                INSERT INTO public."Ratings" ("Id", "DoctorId", "PatientId", "Score", "Comment", "CreatedAt")
                VALUES (rating_id, doctor_id, patient_id, 3 + ((d + visit) % 3),
                    '[DEMO:v1] ' || comments[((d + visit) % 4) + 1], last_completed + interval '1 hour');
                inserted_ratings := inserted_ratings + 1;
            END IF;
        END LOOP;
    END LOOP;

    RAISE NOTICE 'Demo seed date (Europe/Istanbul): %. Added: % branches, % doctors, % patients, % schedules, % appointments, % ratings. Skipped occupied times: %.',
        today, inserted_branches, inserted_doctors, inserted_patients, inserted_slots,
        inserted_appointments, inserted_ratings, skipped_conflicts;
END
$seed$;

-- Example: every branch's seven-day afternoon doctor; tomorrow 14:30 is left free
-- by this seed. A pre-existing user booking can of course occupy that interval.
SELECT b."Title" AS "Branch", d."Name" AS "Doctor", d."Id" AS "DoctorId", d."Email",
       'demo.patient01@example.test' AS "DemoPatientLogin",
       to_char((((CURRENT_TIMESTAMP AT TIME ZONE 'Europe/Istanbul')::date + 1 + time '14:30')
           AT TIME ZONE 'Europe/Istanbul') AT TIME ZONE 'UTC', 'YYYY-MM-DD"T"HH24:MI:SS"Z"') AS "ExampleTimeUtc"
FROM public."Doctors" d JOIN public."Branches" b ON b."Id" = d."BranchId"
WHERE d."Id" IN (SELECT md5('smartappointment/demo/v1/doctor/' || n)::uuid FROM generate_series(2, 23, 3) n)
ORDER BY b."Title";

COMMIT;
