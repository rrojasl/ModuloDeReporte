
/****** Object:  StoredProcedure [dbo].[Sam3_ObtenerFormatoIncidencias]    Script Date: 10/12/2015 12:03:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sam3_ObtenerFormatoIncidencias]
	@FolioAvisoLlegadaID int
AS
BEGIN
	

	Select  1 as [numRFI], 
			0 as [numRFIRevNo], 
			1 as [numNoOfAttachment],
			FORMAT(convert(datetime,GETDATE()),'MM/dd/yy hh:mm:ss tt') as datDate,
			'michael.mainvielle' as [txtAskedBy],
			'Mitchel Richardson' as [ResponseBy],
			 FORMAT(convert(datetime,GETDATE()),'MM/dd/yy') as [ResponseDate],
			 0 as [TransNo],
			'michael.minvielle' as [ActionBy],
			 FORMAT(convert(datetime,GETDATE()),'MM/dd/yy hh:mm:ss tt') as [ActionDate],
			 1 as [ynClosed],
			 'Confirm Nominal Wall Thickness for 26 Pipe' as [Reference],
			 'Please review the attached Material pre-Buy for line item 24' as [mmQuestion],
			 'Confirmed, the wall thickness shown for the 26 nps is nominal wall thickness' as [mmResponse]
	union all
			Select  1 as [numRFI], 
			0 as [numRFIRevNo], 
			1 as [numNoOfAttachment],
			FORMAT(convert(datetime,GETDATE()),'MM/dd/yy hh:mm:ss tt') as datDate,
			'michael.mainvielle' as [txtAskedBy],
			'Mitchel Richardson' as [ResponseBy],
			 FORMAT(convert(datetime,GETDATE()),'MM/dd/yy') as [ResponseDate],
			 0 as [TransNo],
			'michael.minvielle' as [ActionBy],
			 FORMAT(convert(datetime,GETDATE()),'MM/dd/yy hh:mm:ss tt') as [ActionDate],
			 1 as [ynClosed],
			 'Confirm Nominal Wall Thickness for 26 Pipe' as [Reference],
			 'Please review the attached Material pre-Buy for line item 24' as [mmQuestion],
			 'Confirmed, the wall thickness shown for the 26 nps is nominal wall thickness' as [mmResponse]

END
