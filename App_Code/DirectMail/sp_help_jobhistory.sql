CREATE PROCEDURE sp_help_jobhistory
  @job_id               UNIQUEIDENTIFIER = NULL,
  @job_name             sysname          = NULL,
  @step_id              INT              = NULL,
  @sql_message_id       INT              = NULL,
  @sql_severity         INT              = NULL,
  @start_run_date       INT              = NULL,     -- YYYYMMDD
  @end_run_date         INT              = NULL,     -- YYYYMMDD
  @start_run_time       INT              = NULL,     -- HHMMSS
  @end_run_time         INT              = NULL,     -- HHMMSS
  @minimum_run_duration INT              = NULL,     -- HHMMSS
  @run_status           INT              = NULL,     -- SQLAGENT_EXEC_X code
  @minimum_retries      INT              = NULL,
  @oldest_first         INT              = 0,        -- Or 1
  @server               NVARCHAR(30)     = NULL,
  @mode                 VARCHAR(7)       = 'SUMMARY' -- Or 'FULL' or 'SEM'
AS
BEGIN
  DECLARE @retval   INT
  DECLARE @order_by INT  -- Must be INT since it can be -1

  SET NOCOUNT ON

  -- Remove any leading/trailing spaces from parameters
  SELECT @server   = LTRIM(RTRIM(@server))
  SELECT @mode     = LTRIM(RTRIM(@mode))

  -- Turn [nullable] empty string parameters into NULLs
  IF (@server = N'')   SELECT @server = NULL

  -- Check job id/name (if supplied)
  IF ((@job_id IS NOT NULL) OR (@job_name IS NOT NULL))
  BEGIN
    EXECUTE @retval = sp_verify_job_identifiers '@job_name',
'@job_id',
                                                 @job_name OUTPUT,
                                                 @job_id   OUTPUT
    IF (@retval <> 0)
      RETURN(1) -- Failure
  END

  -- Check @start_run_date
  IF (@start_run_date IS NOT NULL)
  BEGIN
    EXECUTE @retval = sp_verify_job_date @start_run_date, '@start_run_date'
    IF (@retval <> 0)
      RETURN(1) -- Failure
  END

  -- Check @end_run_date
  IF (@end_run_date IS NOT NULL)
  BEGIN
    EXECUTE @retval = sp_verify_job_date @end_run_date, '@end_run_date'
    IF (@retval <> 0)
      RETURN(1) -- Failure
  END

  -- Check @start_run_time
  EXECUTE @retval = sp_verify_job_time @start_run_time, '@start_run_time'
  IF (@retval <> 0)
    RETURN(1) -- Failure

  -- Check @end_run_time
  EXECUTE @retval = sp_verify_job_time @end_run_time, '@end_run_time'
  IF (@retval <> 0)
    RETURN(1) -- Failure

  -- Check @run_status
  IF ((@run_status < 0) OR (@run_status > 5))
  BEGIN
    RAISERROR(13266, -1, -1, '@run_status', '0..5')
    RETURN(1) -- Failure
  END

  -- Check mode
  SELECT @mode = UPPER(@mode)
  IF (@mode NOT IN ('SUMMARY', 'FULL', 'SEM'))
  BEGIN
    RAISERROR(14266, -1, -1, '@mode', 'SUMMARY, FULL, SEM')
    RETURN(1) -- Failure
  END

  SELECT @order_by = -1
  IF (@oldest_first = 1)
    SELECT @order_by = 1

  -- Return history information filtered by the supplied parameters.
  -- NOTE: SQLDMO relies on the 'FULL' format; ** DO NOT CHANGE IT **
  IF (@mode = 'FULL')
  BEGIN
    SELECT sjh.instance_id, -- This is included just for ordering purposes
           sj.job_id,
           job_name = sj.name,
           sjh.step_id,
           sjh.step_name,
           sjh.sql_message_id,
           sjh.sql_severity,
           sjh.message,
           sjh.run_status,
           sjh.run_date,
           sjh.run_time,
           sjh.run_duration,
           operator_emailed = so1.name,
           operator_netsent = so2.name,
           operator_paged = so3.name,
           sjh.retries_attempted,
           sjh.server
    FROM msdb.dbo.sysjobhistory                sjh
         LEFT OUTER JOIN msdb.dbo.sysoperators so1  ON (sjh.operator_id_emailed = so1.id)
         LEFT OUTER JOIN msdb.dbo.sysoperators so2  ON (sjh.operator_id_netsent = so2.id)
         LEFT OUTER JOIN msdb.dbo.sysoperators so3  ON (sjh.operator_id_paged = so3.id),
         msdb.dbo.sysjobs_view                 sj
    WHERE (sj.job_id = sjh.job_id)
      AND ((@job_id               IS NULL) OR (@job_id = sjh.job_id))
      AND ((@step_id              IS NULL) OR (@step_id = sjh.step_id))
      AND ((@sql_message_id       IS NULL) OR (@sql_message_id = sjh.sql_message_id))
      AND ((@sql_severity         IS NULL) OR (@sql_severity = sjh.sql_severity))
      AND ((@start_run_date       IS NULL) OR (sjh.run_date >= @start_run_date))
      AND ((@end_run_date         IS NULL) OR (sjh.run_date <= @end_run_date))
      AND ((@start_run_time       IS NULL) OR (sjh.run_time >= @start_run_time))
      AND ((@end_run_time         IS NULL) OR (sjh.run_time <= @end_run_time))
      AND ((@minimum_run_duration IS NULL) OR (sjh.run_duration >= @minimum_run_duration))
      AND ((@run_status           IS NULL) OR (@run_status = sjh.run_status))
      AND ((@minimum_retries      IS NULL) OR (sjh.retries_attempted >= @minimum_retries))
      AND ((@server               IS NULL) OR (sjh.server = @server))
    ORDER BY (sjh.instance_id * @order_by)
  END
  ELSE
  IF (@mode = 'SUMMARY')
  BEGIN
    -- Summary format: same WHERE clause just a different SELECT list
    SELECT sj.job_id,
           job_name = sj.name,
           sjh.run_status,
           sjh.run_date,
           sjh.run_time,
           sjh.run_duration,
           operator_emailed = substring(so1.name, 1, 20),
           operator_netsent = substring(so2.name, 1, 20),
           operator_paged = substring(so3.name, 1, 20),
           sjh.retries_attempted,
           sjh.server
    FROM msdb.dbo.sysjobhistory                sjh
         LEFT OUTER JOIN msdb.dbo.sysoperators so1  ON (sjh.operator_id_emailed = so1.id)
         LEFT OUTER JOIN msdb.dbo.sysoperators so2  ON (sjh.operator_id_netsent = so2.id)
         LEFT OUTER JOIN msdb.dbo.sysoperators so3  ON (sjh.operator_id_paged = so3.id),
         msdb.dbo.sysjobs_view                 sj
    WHERE (sj.job_id = sjh.job_id)
      AND ((@job_id               IS NULL) OR (@job_id = sjh.job_id))
      AND ((@step_id              IS NULL) OR (@step_id = sjh.step_id))
      AND ((@sql_message_id       IS NULL) OR (@sql_message_id = sjh.sql_message_id))
      AND ((@sql_severity         IS NULL) OR (@sql_severity = sjh.sql_severity))
      AND ((@start_run_date       IS NULL) OR (sjh.run_date >= @start_run_date))
      AND ((@end_run_date         IS NULL) OR (sjh.run_date <= @end_run_date))
      AND ((@start_run_time       IS NULL) OR (sjh.run_time >= @start_run_time))
      AND ((@end_run_time         IS NULL) OR (sjh.run_time <= @end_run_time))
      AND ((@minimum_run_duration IS NULL) OR (sjh.run_duration >= @minimum_run_duration))
      AND ((@run_status           IS NULL) OR (@run_status = sjh.run_status))
      AND ((@minimum_retries      IS NULL) OR (sjh.retries_attempted >= @minimum_retries))
      AND ((@server               IS NULL) OR (sjh.server = @server))
    ORDER BY (sjh.instance_id * @order_by)
  END
  ELSE
  IF (@mode = 'SEM')
  BEGIN
    -- SQL Enterprise Manager format
    SELECT sjh.step_id,
           sjh.step_name,
           sjh.message,
           sjh.run_status,
           sjh.run_date,
           sjh.run_time,
           sjh.run_duration,
           operator_emailed = so1.name,
           operator_netsent = so2.name,
           operator_paged = so3.name
    FROM msdb.dbo.sysjobhistory                sjh
         LEFT OUTER JOIN msdb.dbo.sysoperators so1  ON (sjh.operator_id_emailed = so1.id)
         LEFT OUTER JOIN msdb.dbo.sysoperators so2  ON (sjh.operator_id_netsent = so2.id)
         LEFT OUTER JOIN msdb.dbo.sysoperators so3  ON (sjh.operator_id_paged = so3.id),
         msdb.dbo.sysjobs_view                 sj
    WHERE (sj.job_id = sjh.job_id)
      AND (@job_id = sjh.job_id)
    ORDER BY (sjh.instance_id * @order_by)
  END

  RETURN(0) -- Success
END

GO
