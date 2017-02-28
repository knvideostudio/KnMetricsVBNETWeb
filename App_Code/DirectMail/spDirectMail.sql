-- ********************************************************************************************
-- * Direct Mail Report
-- * Created on Feb 27, 2007
-- * Last Update on May 07, 2007
-- * Version 4.0
-- ********************************************************************************************
CREATE    PROCEDURE spDirectMail
AS

DECLARE @CURRENT_DATE as varchar(11)
DECLARE @CURRENT_TIME as varchar(8)

SET nocount ON

-- drop table #TEMPA
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
into #TEMPA
From dbo.HA_Practice_DirectMail_Status dms
Order By dms.PracticeID

-- delete records with zero Pets
delete from #TEMPA  WHERE Canine = 0 AND Feline = 0

-- get the 70% and update the Species's field
-- prevent devided by ZERO
	Update #TEMPA
	Set Species = 'Feline'
	Where PracticeID in (Select PracticeID From #TEMPA
	      Where  convert(decimal,Feline) / CASE convert(decimal,Canine) + convert(decimal,Feline)  WHEN 0 then 1 else convert(decimal,Canine) + convert(decimal,Feline) end  > .7)
	
	Update #TEMPA
	Set Species = 'Canine'
	Where Species = '      '

-- Delete data
-- delete from #TEMPA  WHERE Species = 'Canine' AND Canine < 450
-- delete from #TEMPA  WHERE Species = 'Feline' AND Feline < 450

-- Add current data
Insert into dbo.HA_Practice_DirectMail_Totals (PracticeID, TotalCanine,	TotalFeline, SpeciesName)
Select PracticeID, Canine, Feline, Species FROM #TEMPA

-- SELECT * FROM #TEMPA
-- get the actual data all records per PRACTICE
-- link the ZIP CODES table too

SELECT temp.PracticeID AS PracticeID,
	HA_Practice_DirectMail_ZipCode.IncomePerHousehold,
	HA_Practice_Patient.[Name] as PetName, 
	HA_Practice_Patient.SexId as PetSexID,
	HA_Practice_Patient.PatientId as patientid,
	HA_Practice_Client.FirstName as ClientFirstName, 
	HA_Practice_Client.LastName as ClientLastName, 
	HA_Practice_Client.[Address1] as ClientAddress1, 
	HA_Practice_Client.[Address2] as ClientAddress2, 
	HA_Practice_Client.City as ClientCity, 
	HA_Practice_Client.State as ClientState, 
	HA_Practice_Client.Zip as ClientZip, 
	HA_Practice_Client.[Phone1] as ClientPhone1, 
	HA_Practice_Client.[Phone2] as ClientPhone2
into #TEMPB
FROM 	#TEMPA as temp 
	INNER JOIN
	HA_Practice_Patient ON temp.PracticeID = HA_Practice_Patient.PracticeID 
	INNER JOIN
	HA_Practice_Species  ON 
	HA_Practice_Patient.PracticeId = HA_Practice_Species.PracticeID 
	AND HA_Practice_Patient.speciesid = HA_Practice_Species.PrSpeciesID
	INNER JOIN
	HA_Practice_Client  ON 
	HA_Practice_Patient.PracticeId = HA_Practice_Client.PracticeId
	AND HA_Practice_Patient.clientid = HA_Practice_Client.Clientid
	Inner Join
	HA_Practice_DirectMail_ZipCode  ON 
	LEFT(HA_Practice_Client.Zip, 5) = HA_Practice_DirectMail_ZipCode.ZipCode
WHERE 	(HA_Practice_Species.VMSpeciesName= temp.Species)
	AND (DATEDIFF(month, HA_Practice_Patient.BirthDate, GETDATE()) < 25)
	AND (HA_Practice_Client.Address1 is not NULL)
	AND (HA_Practice_Client.Active = '1')
	AND (HA_Practice_Patient.StatusID = 1)
GROUP BY temp.PracticeID,
	HA_Practice_DirectMail_ZipCode.IncomePerHousehold,
	HA_Practice_Patient.[Name], 
	HA_Practice_Patient.SexId,
	HA_Practice_Patient.Patientid,
	HA_Practice_Client.FirstName, 
	HA_Practice_Client.LastName, 
	HA_Practice_Client.[Address1], 
	HA_Practice_Client.[Address2], 
	HA_Practice_Client.City, 
	HA_Practice_Client.State, 
	HA_Practice_Client.Zip, 
	HA_Practice_Client.[Phone1], 
	HA_Practice_Client.[Phone2],
	HA_Practice_Client.Email
Order by  temp.PracticeID, HA_Practice_DirectMail_ZipCode.IncomePerHousehold DESC

SET @CURRENT_DATE = Convert(varchar, getdate(), 101)
SET @CURRENT_TIME = DATENAME(hour, getdate()) + ':' + DATENAME(minute, getdate()) + ':00'

-- Final get all record but top 500 per practice
insert into dbo.HA_Practice_DirectMail_History
(
	PracticeID, 
	MedianIncome, 
	PetName,
	PetSexID,
	ClientFirstName,
	ClientLastName,
	[ClientAddress1],
	[ClientAddress2],
	ClientCity,
	ClientState,
	ClientZip,
	[ClientPhone1],
	[ClientPhone2],
	CreatedDate
)
Select P.PracticeId,
	P.IncomePerHousehold,
	P.PetName, 
	P.PetSexID, 
	P.ClientFirstName, 
	P.ClientLastName, 
	P.[ClientAddress1], 
	P.[ClientAddress2], 
	P.ClientCity, 
	P.ClientState, 
	P.ClientZip, 
	P.[ClientPhone1], 
	P.[ClientPhone2],
	@CURRENT_DATE + ' ' + @CURRENT_TIME
FROM #TEMPB as P
WHERE P.PatientID IN (SELECT TOP 500 PatientID FROM #TEMPB WHERE PracticeId = P.PracticeId)
Group BY P.PracticeId,
	P.IncomePerHousehold,
	P.PetName, 
	P.PetSexID, 
	P.ClientFirstName, 
	P.ClientLastName, 
	P.[ClientAddress1], 
	P.[ClientAddress2], 
	P.ClientCity, 
	P.ClientState, 
	P.ClientZip, 
	P.[ClientPhone1], 
	P.[ClientPhone2]
HAVING P.PracticeId IN (Select PracticeID FROM dbo.HA_Practice_DirectMail_Status where Active = '1')
ORDER By P.PracticeId, P.IncomePerHousehold DESC

-- Insert The 500
Insert into dbo.[HA_Practice_DirectMail_500] (PracticeID, Total, BatchId)
SELECT PracticeID, Count(PracticeID) as pr, 1
FROM dbo.HA_Practice_DirectMail_History
--where BatchId = 1
group by PracticeID
having Count(PracticeID) >= 450

-- UPDATE Status Table
UPDATE dbo.HA_Practice_DirectMail_Status
    SET ProcessedDate = @CURRENT_DATE + ' ' + @CURRENT_TIME
    FROM dbo.HA_Practice_DirectMail_Status DS, dbo.[HA_Practice_DirectMail_500] temp
    WHERE DS.PracticeID = temp.PracticeID

-- Update multiple records on the end
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

SET nocount OFF

Return
GO