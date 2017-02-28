
CREATE  FUNCTION Truncate_NonASCIICharacters_Stage(@STRING_INPUT varchar(255))
RETURNS varchar(255)
AS
BEGIN

DECLARE @STR_TEMP as varchar(255)
DECLARE @MAX as smallint
DECLARE @ASCII_CODE smallint
DECLARE @INDEX as smallint

SET @STR_TEMP = ''
SET @MAX = len(@STRING_INPUT)
SET @INDEX = 1

WHILE @INDEX <= @MAX 
   BEGIN
	SET @ASCII_CODE = ascii(substring(@STRING_INPUT, @INDEX, 1))


	IF (@ASCII_CODE between 33 and 146)
	Begin
		SET @STR_TEMP = @STR_TEMP + substring(@STRING_INPUT, @INDEX, 1) 
	End

	SET @INDEX = @INDEX + 1
   END -- While

IF LEN(@STR_TEMP) > 0
Begin
  SET @STR_TEMP = LTrim(@STR_TEMP)
  SET @STR_TEMP = RTrim(@STR_TEMP)
End

Return (@STR_TEMP)

END