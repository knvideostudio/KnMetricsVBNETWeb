

SELECT temp.PracticeID AS PracticeID,
	HA_Practice_DirectMail_ZipCode.IncomePerHousehold,
	PP.[Name] as PetName, 
	PP.SexId as PetSexID,
	PP.PatientId as patientid,
	PC.FirstName as ClientFirstName, 
	PC.LastName as ClientLastName, 
	PC.[Address1] as ClientAddress1, 
	PC.[Address2] as ClientAddress2, 
	PC.City as ClientCity, 
	PC.State as ClientState, 
	PC.Zip as ClientZip, 
	PC.[Phone1] as ClientPhone1, 
	PC.[Phone2] as ClientPhone2
into #TEMPB
FROM 	#TEMPA as temp 
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
	PC.Email
Order by  temp.PracticeID, HA_Practice_DirectMail_ZipCode.IncomePerHousehold DESC
