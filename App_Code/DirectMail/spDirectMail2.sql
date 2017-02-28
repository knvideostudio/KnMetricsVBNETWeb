-- *****************************************************************************************************
-- * Direct Mail
-- * Version 2.00
-- * Date May 11, 2007
-- *****************************************************************************************************

CREATE PROCEDURE spDirectMail2
AS

DECLARE @CURRENT_DATE as DateTime
SET @CURRENT_DATE = getdate()


-- Part 1 get Counters
select dms.practiceid,
            Canine = (Select  COUNT(*)
        FROM dbo.HA_Practice_DirectMail_Status  INNER JOIN
	dbo.HA_Practice_Patient ON dbo.HA_Practice_DirectMail_Status.PracticeID = dbo.HA_Practice_Patient.PracticeID 
	INNER JOIN
        dbo.HA_Practice_Species ON dbo.HA_Practice_Patient.PracticeId = dbo.HA_Practice_Species.PracticeID 
	AND dbo.HA_Practice_Patient.speciesid = dbo.HA_Practice_Species.PrSpeciesID 
	INNER JOIN
	dbo.HA_Practice_Client ON dbo.HA_Practice_Patient.PracticeId = dbo.HA_Practice_Client.PracticeId 
	AND dbo.HA_Practice_Patient.clientid = dbo.HA_Practice_Client.Clientid
        WHERE dbo.HA_Practice_Patient.PracticeID = dms.PracticeID
	AND (dbo.HA_Practice_Species.VMSpeciesName = 'Canine')
        	AND (dbo.HA_Practice_DirectMail_Status.Active = '1')
	AND (dbo.HA_Practice_Client.Active = '1')
	AND (dbo.HA_Practice_Client.Address1 is not NULL)
	AND (dbo.HA_Practice_Patient.StatusID = 1)
        	AND (DATEDIFF([month], dbo.HA_Practice_Patient.birthdate, GETDATE()) < 25)),

        Feline = (Select  COUNT(*)
        FROM dbo.HA_Practice_DirectMail_Status INNER JOIN
        dbo.HA_Practice_Patient  ON dbo.HA_Practice_DirectMail_Status.PracticeID = dbo.HA_Practice_Patient.PracticeID 
	INNER JOIN
	dbo.HA_Practice_Species  ON dbo.HA_Practice_Patient.PracticeId = dbo.HA_Practice_Species.PracticeID 
	AND dbo.HA_Practice_Patient.speciesid = dbo.HA_Practice_Species.PrSpeciesID 
	INNER JOIN
        dbo.HA_Practice_Client  ON dbo.HA_Practice_Patient.PracticeId = dbo.HA_Practice_Client.PracticeId 
	AND dbo.HA_Practice_Patient.clientid = dbo.HA_Practice_Client.Clientid
        WHERE dbo.HA_Practice_Patient.PracticeID = dms.PracticeID
       	AND (dbo.HA_Practice_Species.VMSpeciesName = 'Feline')
	AND (dbo.HA_Practice_DirectMail_Status.Active = '1')
	AND (dbo.HA_Practice_Client.Address1 is not NULL)
	AND (dbo.HA_Practice_Client.Active = '1')
	AND (dbo.HA_Practice_Patient.StatusID = 1)
	AND (DATEDIFF([month], dbo.HA_Practice_Patient.birthdate, GETDATE()) < 25)),
        Species = '      '
into #TotalDogCat
From dbo.HA_Practice_DirectMail_Status dms
Order By dms.PracticeID

-- delete records with zero Pets
delete from #TotalDogCat  WHERE Canine = 0 AND Feline = 0

-- get the 70% and update the Species's field
-- prevent devided by ZERO
Update #TotalDogCat Set Species = 'Feline'
Where PracticeID in (Select PracticeID From #TotalDogCat Where  convert(decimal,Feline) / CASE convert(decimal,Canine) + convert(decimal,Feline)  WHEN 0 then 1 else convert(decimal,Canine) + convert(decimal,Feline) end  > .7)

Update #TotalDogCat Set Species = 'Canine' Where Species = '      '

-- Create a tracking 
-- Add current data
Insert into dbo.HA_Practice_DirectMail_Totals (PracticeID, TotalCanine, TotalFeline, SpeciesName)
Select 	PracticeID, Canine, Feline, Species FROM #TotalDogCat
-- End Part 1
-- time 18 - 25 seconds
-- ****************************************************************************************************

-- Part 2
-- Drop table #PracticeClient
-- Move Client records into temp table
Select 	PC.PracticeID,
	PC.FirstName, 
	PC.LastName, 
	PC.[Address1], 
	PC.[Address2], 
	PC.City, 
	PC.State, 
	PC.[Phone1], 
	PC.[Phone2],
	LEFT(PC.Zip, 5) AS Zip,
	PC.ClientId,
	PC.Active,
	PC.Email
into #PracticeClient
FROM HA_Practice_Client PC
Inner join #TotalDogCat A
ON PC.PracticeID = A.PracticeID
WHERE 	(Len(PC.Address1) > 2)
	AND (Len(PC.City) > 1)
	AND (Len(PC.State) > 0)
	AND (Len(PC.Zip) > 1)
	AND (PC.Active = '1')
-- End Client

-- Delete the returned Mail
Delete FROM #PracticeClient where Address1 like '%Returned Mail%'

-- drop table #PracticePatient
-- Move the Practice Patients to temp table
Select 	PP.PracticeID as PracticeID,
	PP.[Name], 
	PP.SexId,
	PP.PatientId,
	PP.SpeciesID,
	PP.ClientID,
	PP.BirthDate,
	PP.StatusID,
	PP.MicrochipID
into #PracticePatient
FROM HA_Practice_Patient PP
Inner join #TotalDogCat A
ON PP.PracticeID = A.PracticeID
WHERE  	DATEDIFF(month, PP.BirthDate, GETDATE()) < 25
	AND (PP.BirthDate < getdate())
	AND (PP.StatusID = 1)
 	AND (PP.MicrochipID IS NULL OR PP.MicrochipID = '')

-- End Patient 

-- Actual Records
SELECT temp.PracticeID AS PracticeID,
	HA_Practice_DirectMail_ZipCode.IncomePerHousehold,
	PP.[Name], 
	PP.SexId,
	PP.PatientId,
	PP.ClientId as PPClientId,
	PP.BirthDate,
	PP.StatusId,
	PC.ClientId as PCClientId,
	PC.Active,
	PC.FirstName, 
	PC.LastName, 
	PC.[Address1], 
	PC.[Address2], 
	PC.City, 
	PC.State, 
	PC.Zip, 
	PC.[Phone1], 
	PC.[Phone2],
	PC.Email,
	PP.MicrochipID,
	'0' as TopStatusId
into #DirectMail
FROM 	#TotalDogCat as temp 
	INNER JOIN
	#PracticePatient PP ON temp.PracticeID = PP.PracticeID 
	INNER JOIN
	HA_Practice_Species  ON 
	PP.PracticeId = HA_Practice_Species.PracticeID 
	AND PP.speciesid = HA_Practice_Species.PrSpeciesID
	INNER JOIN
	#PracticeClient PC  ON 
	PP.PracticeId = PC.PracticeId
	AND PP.clientid = PC.Clientid
	Inner Join
	HA_Practice_DirectMail_ZipCode  ON 
	PC.Zip = HA_Practice_DirectMail_ZipCode.ZipCode
WHERE 	(HA_Practice_Species.VMSpeciesName= temp.Species)
	-- AND (DATEDIFF(month, PP.BirthDate, GETDATE()) < 25)
	-- AND (PC.Address1 is not NULL)
	-- AND (PC.Active = '1')
	-- AND (PP.StatusID = 1)
GROUP BY temp.PracticeID,
	HA_Practice_DirectMail_ZipCode.IncomePerHousehold,
	PP.[Name], 
	PP.SexId,
	PP.Patientid,
	PC.FirstName, 
	PC.LastName, 
	PC.[Address1], 
	PC.[Address2], 
	PC.City, 
	PC.State, 
	PC.Zip, 
	PC.[Phone1], 
	PC.[Phone2],
	PC.Email,
	PP.ClientId,
	PC.ClientId,
	PP.BirthDate,
	PP.StatusId,
	PC.Active,
	PP.MicrochipID
Order by  temp.PracticeID, 
HA_Practice_DirectMail_ZipCode.IncomePerHousehold DESC
-- end Actual

-- insert Totals
Insert into dbo.HA_Practice_DirectMail_TotalOf (PracticeID, Total, batchId)
Select PracticeID, Count(*) as Total, 1
FROM #DirectMail
Group By PracticeID
Having Count(1) >= 0
-- End insert Totals


-- make a list of total more then 450
Select PracticeID, Count(*) as Total
into #DirectMailTotal
FROM #DirectMail
Group By PracticeID
Having Count(1) >= 450
-- End Total

-- Prepare a new table for TOP 500
Select top 1 * 
into #DirectMail500
From #DirectMail

Delete FROM #DirectMail500
-- End Prepare

-- Declare a Cursor for TOP 500
DECLARE @PRACTICE_ID as int
DECLARE @TOTAL_BY_PRACTICE as int
DECLARE @TMP_STR as nvarchar(2000)

DECLARE DirectMail_Cursor CURSOR FOR 
	SELECT PracticeID, Total FROM #DirectMailTotal 
	order By PracticeID

	OPEN DirectMail_Cursor
	FETCH NEXT FROM DirectMail_Cursor INTO @PRACTICE_ID, @TOTAL_BY_PRACTICE
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @TMP_STR = N'insert into #DirectMail500' + char(10)
		SET @TMP_STR = @TMP_STR + N'Select top 500 * from #DirectMail where PracticeID = '+ Convert(varchar(10),@PRACTICE_ID)  +' order by IncomePerHouseHold desc'
		
		-- print @TMP_STR
		Execute sp_executesql @TMP_STR
		
		-- execute sp_executesql 
		-- N'select * from pubs.dbo.employee where job_lvl = @level',
		-- N'@level tinyint',
		-- @level = 35

	FETCH NEXT FROM DirectMail_Cursor INTO @PRACTICE_ID, @TOTAL_BY_PRACTICE
	END

	CLOSE DirectMail_Cursor
	DEALLOCATE DirectMail_Cursor
-- End cursor

insert into dbo.HA_Practice_DirectMail_History 
(	PracticeID,
	IncomePerHousehold,
	[Name], 
	SexId,
	PatientId,
	PPClientId,
	BirthDate,
	StatusId,
	PCClientId,
	Active,
	FirstName, 
	LastName, 
	[Address1], 
	[Address2], 
	City, 
	State, 
	Zip, 
	[Phone1], 
	[Phone2],
	PC.Email,
	MicrochipID,
	Processed
)
SELECT	PracticeID,
	IncomePerHousehold,
	dbo.ProperCase([Name]), 
	SexId,
	dbo.ProperCase(PatientId),
	PPClientId,
	BirthDate,
	StatusId,
	PCClientId,
	Active,
	dbo.ProperCase(FirstName), 
	dbo.ProperCase(LastName), 
	dbo.ProperCase([Address1]), 
	dbo.ProperCase([Address2]), 
	dbo.ProperCase(City), 
	State, 
	Zip, 
	[Phone1], 
	[Phone2],
	Email,
	MicrochipID,
	TopStatusId
FROM #DirectMail500
-- end add in history

-- Update ProcessDate
UPDATE dbo.HA_Practice_DirectMail_Status 
SET ProcessedDate = @CURRENT_DATE
FROM dbo.HA_Practice_DirectMail_Status DS, #DirectMailTotal temp
WHERE DS.PracticeID = temp.PracticeID
-- End Update Process Date

-- Update Status Table address info 
UPDATE dbo.HA_Practice_DirectMail_Status 
SET Account_NM = MP.Account_NM,
Line_1_ADDR = MP.Line_1_ADDR,
Line_2_ADDR = MP.Line_2_ADDR,
Line_3_ADDR = MP.Line_3_ADDR,
City_NM = MP.City_NM,
State_CD = MP.State_CD,
Zip_CD = MP.Zip_CD,
Phone_NBR = MP.Phone_NBR
FROM dbo.HA_Practice_DirectMail_Status DM, dbo.HA_Master_Clinic_Profile MP
WHERE DM.PracticeID = MP.PracticeID
AND DM.ProcessedDate is not NULL
-- End Last Step

