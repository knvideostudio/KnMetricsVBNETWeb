CREATE PROCEDURE sp_post_msx_operation
  @operation              VARCHAR(64),
  @object_type            VARCHAR(64)      = 'JOB',
  @job_id                 UNIQUEIDENTIFIER = NULL, -- NOTE: 0x00 means 'ALL'
  @specific_target_server NVARCHAR(30)     = NULL,
  @value                  INT              = NULL  -- For polling interval value
AS

BEGIN
  DECLARE @operation_code            INT
  DECLARE @specific_target_server_id INT
  DECLARE @instructions_posted       INT
  DECLARE @job_id_as_char            VARCHAR(36)
  DECLARE @msx_time_zone_adjustment  INT
  DECLARE @local_machine_name        NVARCHAR(30)
  DECLARE @retval                    INT

  SET NOCOUNT ON

  -- Remove any leading/trailing spaces from parameters
  SELECT @operation              = LTRIM(RTRIM(@operation))
  SELECT @object_type            = LTRIM(RTRIM(@object_type))
  SELECT @specific_target_server = LTRIM(RTRIM(@specific_target_server))

  -- Turn [nullable] empty string parameters into NULLs
  IF (@specific_target_server = N'') SELECT @specific_target_server = NULL

  -- Only a sysadmin can do this, but fail silently for a non-sysadmin
  IF (ISNULL(IS_SRVROLEMEMBER(N'sysadmin'), 0) <> 1)
    RETURN(0) -- Success (or more accurately a no-op)

  -- Check operation
  SELECT @operation = UPPER(@operation)
  SELECT @operation_code = CASE @operation
                             WHEN 'INSERT'    THEN 1
                             WHEN 'UPDATE'    THEN 2
                             WHEN 'DELETE'    THEN 3
                             WHEN 'START'     THEN 4
                             WHEN 'STOP'      THEN 5
                             WHEN 'RE-ENLIST' THEN 6
                             WHEN 'DEFECT'    THEN 7
                             WHEN 'SYNC-TIME' THEN 8
                             WHEN 'SET-POLL'  THEN 9
                             ELSE 0
                           END
  IF (@operation_code = 0)
  BEGIN
    RAISERROR(14266, -1, -1, '@operation_code', 'INSERT, UPDATE, DELETE, START, STOP, RE-ENLIST, DEFECT, SYNC-TIME, SET-POLL')
    RETURN(1) -- Failure
  END

  -- Check object type (in 7.0 only 'JOB' or 'SERVER' are valid)
  IF ((@object_type <> 'JOB') AND (@object_type <> 'SERVER'))
  BEGIN
    RAISERROR(14266, -1, -1, '@object_type', 'JOB, SERVER')
    RETURN(1) -- Failure
  END

  -- Check that for a object type of JOB a job_id has been supplied
  IF ((@object_type = 'JOB') AND (@job_id IS NULL))
  BEGIN
    RAISERROR(14233, -1, -1)
    RETURN(1) -- Failure
  END

  -- Check polling interval value
  IF (@operation_code = 9) AND ((ISNULL(@value, 0) < 10) OR (ISNULL(@value, 0) > 28800))
  BEGIN
    RAISERROR(14266, -1, -1, '@value', '10..28800')
    RETURN(1) -- Failure
  END

  -- Check specific target server
  IF (@specific_target_server IS NOT NULL)
  BEGIN
    -- Check if the local server is being targeted
    IF (UPPER(@specific_target_server) = UPPER(CONVERT(NVARCHAR(30), SERVERPROPERTY('ServerName'))))
    BEGIN
      RETURN(0)
    END
    ELSE
    BEGIN
      SELECT @specific_target_server_id = server_id
      FROM msdb.dbo.systargetservers
      WHERE (server_name = @specific_target_server)
      IF (@specific_target_server_id IS NULL)
      BEGIN
        RAISERROR(14262, -1, -1, '@specific_target_server', @specific_target_server)
        RETURN(1) -- Failure
      END
    END
  END

  -- Check that this server is an MSX server
  IF ((SELECT COUNT(*)
       FROM msdb.dbo.systargetservers) = 0)
  BEGIN
    RETURN(0)
  END

  -- Get local machine name
  EXECUTE @retval = master.dbo.xp_getnetname @local_machine_name OUTPUT
  IF (@retval <> 0) OR (@local_machine_name IS NULL)
  BEGIN
    RAISERROR(14225, -1, -1)
    RETURN(1)
  END

  CREATE TABLE #target_servers (server_name sysname COLLATE database_default NOT NULL)

  -- Optimization: Simply update the date-posted if the operation has already been posted (but
  -- not yet downloaded) and the target server is not currently polling...
  IF ((@object_type = 'JOB')    AND (ISNULL(@job_id, 0x00) <> CONVERT(UNIQUEIDENTIFIER, 0x00)) AND (@operation_code IN (1, 2, 3, 4, 5))) OR -- Any JOB operation
     ((@object_type = 'SERVER') AND (@operation_code IN (6, 7))) -- RE-ENLIST or DEFECT operations
  BEGIN
    -- Populate the list of target servers to post to
    IF (@specific_target_server IS NOT NULL)
      INSERT INTO #target_servers VALUES (@specific_target_server)
    ELSE
    BEGIN
      IF (@object_type = 'SERVER')
        INSERT INTO #target_servers
        SELECT server_name
        FROM msdb.dbo.systargetservers

      IF (@object_type = 'JOB')
        INSERT INTO #target_servers
        SELECT sts.server_name
        FROM msdb.dbo.sysjobs_view     sjv,
             msdb.dbo.sysjobservers    sjs,
             msdb.dbo.systargetservers sts
        WHERE (sjv.job_id = @job_id)
          AND (sjv.job_id = sjs.job_id)
          AND (sjs.server_id = sts.server_id)
          AND (sjs.server_id <> 0)
    END
  END

  -- Job-specific processing...
  IF (@object_type = 'JOB')
  BEGIN
    -- Validate the job (if supplied)
    IF (@job_id <> CONVERT(UNIQUEIDENTIFIER, 0x00))
    BEGIN
      SELECT @job_id_as_char = CONVERT(VARCHAR(36), @job_id)

      -- Check if the job exists
      IF (NOT EXISTS (SELECT *
                      FROM msdb.dbo.sysjobs_view
                      WHERE (job_id = @job_id)))
      BEGIN
        RAISERROR(14262, -1, -1, '@job_id', @job_id_as_char)
        RETURN(1) -- Failure
      END

      -- If this is a local job then there's nothing for us to do
      IF (EXISTS (SELECT *
                  FROM msdb.dbo.sysjobservers
                  WHERE (job_id = @job_id)
                    AND (server_id = 0))) -- 0 means local server
      OR (NOT EXISTS (SELECT *
                      FROM msdb.dbo.sysjobservers
                      WHERE (job_id = @job_id)))
      BEGIN
        RETURN(0)
      END
    END

    -- Generate the sysdownloadlist row(s)...
    IF (@operation_code = 1) OR  -- Insert
       (@operation_code = 2) OR  -- Update
       (@operation_code = 3) OR  -- Delete
       (@operation_code = 4) OR  -- Start
       (@operation_code = 5)     -- Stop
    BEGIN
      IF (@job_id = CONVERT(UNIQUEIDENTIFIER, 0x00)) -- IE. 'ALL'
      BEGIN
        -- All jobs

        -- Handle DELETE as a special case (rather than posting 1 instruction per job we just
        -- post a single instruction that means 'delete all jobs from the MSX')
        IF (@operation_code = 3)
        BEGIN
          INSERT INTO msdb.dbo.sysdownloadlist
                (source_server,
                 operation_code,
                 object_type,
                 object_id,
                 target_server)
          SELECT @local_machine_name,
                 @operation_code,
                 1,                -- 1 means 'JOB'
                 CONVERT(UNIQUEIDENTIFIER, 0x00),
                 sts.server_name
          FROM systargetservers sts
          WHERE ((@specific_target_server_id IS NULL) OR (sts.server_id = @specific_target_server_id))
            AND ((SELECT COUNT(*)
                  FROM msdb.dbo.sysjobservers
                  WHERE (server_id = sts.server_id)) > 0)
          SELECT @instructions_posted = @@rowcount
        END
        ELSE
        BEGIN
          INSERT INTO msdb.dbo.sysdownloadlist
                (source_server,
                 operation_code,
                 object_type,
                 object_id,
                 target_server)
          SELECT @local_machine_name,
                 @operation_code,
                 1,                -- 1 means 'JOB'
                 sjv.job_id,
                 sts.server_name
          FROM sysjobs_view     sjv,
               sysjobservers    sjs,
               systargetservers sts
          WHERE (sjv.job_id = sjs.job_id)
            AND (sjs.server_id = sts.server_id)
            AND (sjs.server_id <> 0) -- We want to exclude local jobs
            AND ((@specific_target_server_id IS NULL) OR (sjs.server_id = @specific_target_server_id))
          SELECT @instructions_posted = @@rowcount
        END
      END
      ELSE
      BEGIN
        -- Specific job (ie. @job_id is not 0x00)
        INSERT INTO msdb.dbo.sysdownloadlist
              (source_server,
               operation_code,
               object_type,
               object_id,
               target_server,
               deleted_object_name)
        SELECT @local_machine_name,
               @operation_code,
               1,                -- 1 means 'JOB'
               sjv.job_id,
               sts.server_name,
               CASE @operation_code WHEN 3 -- Delete
                                      THEN sjv.name
                                      ELSE NULL
                                    END
        FROM sysjobs_view     sjv,
             sysjobservers    sjs,
             systargetservers sts
        WHERE (sjv.job_id = @job_id)
          AND (sjv.job_id = sjs.job_id)
          AND (sjs.server_id = sts.server_id)
          AND (sjs.server_id <> 0) -- We want to exclude local jobs
          AND ((@specific_target_server_id IS NULL) OR (sjs.server_id = @specific_target_server_id))
        SELECT @instructions_posted = @@rowcount
      END
    END
    ELSE
    BEGIN
      RAISERROR(14266, -1, -1, '@operation_code', 'INSERT, UPDATE, DELETE, START, STOP')
      RETURN(1) -- Failure
    END
  END

  -- Server-specific processing...
  IF (@object_type = 'SERVER')
  BEGIN
    -- Generate the sysdownloadlist row(s)...
    IF (@operation_code = 6) OR  -- ReEnlist
       (@operation_code = 7) OR  -- Defect
       (@operation_code = 8) OR  -- Synchronize time (with MSX)
       (@operation_code = 9)     -- Set MSX polling interval (in seconds)
    BEGIN
      IF (@operation_code = 8)
      BEGIN
        EXECUTE master.dbo.xp_regread N'HKEY_LOCAL_MACHINE',
                                      N'SYSTEM\CurrentControlSet\Control\TimeZoneInformation',
                                      N'Bias',
                                      @msx_time_zone_adjustment OUTPUT,
                                      N'no_output'
        SELECT @msx_time_zone_adjustment = -ISNULL(@msx_time_zone_adjustment, 0)
      END

      INSERT INTO msdb.dbo.sysdownloadlist
            (source_server,
             operation_code,
             object_type,
             object_id,
             target_server)
      SELECT @local_machine_name,
             @operation_code,
             2,                  -- 2 means 'SERVER'
             CASE @operation_code
               WHEN 8 THEN CONVERT(UNIQUEIDENTIFIER, CONVERT(BINARY(16), -(@msx_time_zone_adjustment - sts.time_zone_adjustment)))
               WHEN 9 THEN CONVERT(UNIQUEIDENTIFIER, CONVERT(BINARY(16), @value))
               ELSE CONVERT(UNIQUEIDENTIFIER, 0x00)
             END,
             sts.server_name
      FROM systargetservers sts
      WHERE ((@specific_target_server_id IS NULL) OR (sts.server_id = @specific_target_server_id))
      SELECT @instructions_posted = @@rowcount
    END
    ELSE
    BEGIN
      RAISERROR(14266, -1, -1, '@operation_code', 'RE-ENLIST, DEFECT, SYNC-TIME, SET-POLL')
      RETURN(1) -- Failure
    END
  END


  -- Report number of rows inserted
  IF (@object_type = 'JOB') AND
     (@job_id = CONVERT(UNIQUEIDENTIFIER, 0x00)) AND
     (@instructions_posted = 0) AND
     (@specific_target_server_id IS NOT NULL)
    RAISERROR(14231, 0, 1, '@specific_target_server', @specific_target_server)
  ELSE
    RAISERROR(14230, 0, 1, @instructions_posted, @operation)

  -- Delete any [downloaded] instructions that are over the registry-defined limit
  IF (@specific_target_server IS NOT NULL)
    EXECUTE msdb.dbo.sp_downloaded_row_limiter @specific_target_server

  RETURN(0) -- 0 means success
END

GO
