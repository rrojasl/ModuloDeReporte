USE [steelgo-sam3]
GO
/****** Object:  StoredProcedure [dbo].[Sam3_ObtenerFormatoIncidencias]    Script Date: 05/08/2016 01:24:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sam3_ObtenerIncidencia]
	@incidenciaID int
AS
BEGIN

	select 
	i.IncidenciaID as [numRFI], -- Me parece que este es el Id de la Incidencia
	i.[Version] as [numRFIRevNo], -- Puede ser la version
	(
		select Count(Rel_Incidencia_DocumentoID)
		from Sam3_Rel_Incidencia_Documento id
		where id.IncidenciaID = i.IncidenciaID
	) as [numNoOfAttachment],  -- Numero de documentos relacionados con la incidencia
	CONVERT(VARCHAR(10), i.FechaCreacion, 101) + ' ' 
       + LTRIM(RIGHT(CONVERT(CHAR(20), i.FechaCreacion, 22), 11)) as datDate,
	(
		select u.Nombre + ' ' + u.ApellidoPaterno
		from Sam3_Usuario u
		where u.UsuarioID = i.UsuarioID
	) as [txtAskedBy], -- Nombre del usuario que registro la incidencia
	(
		select u.Nombre + ' ' + u.ApellidoPaterno
		from Sam3_Usuario u
		where u.UsuarioID =i.UsuarioIDRespuesta
	) as [ResponseBy],  -- Nombre del usuario que responde a la incidencia
	CONVERT(VARCHAR(10), i.FechaRespuesta, 101)as [ResponseDate],
	0 as [TransNo], -- este realmente no se de donde sale
	(
		select u.Nombre + ' ' + u.ApellidoPaterno
		from Sam3_Usuario u
		where u.UsuarioID = i.UsuarioResuelveID
	) as [ActionBy], -- usuario que resuelve la incidencia,
	CONVERT(VARCHAR(10), i.FechaSolucion, 101) + ' ' 
       + LTRIM(RIGHT(CONVERT(CHAR(20), i.FechaSolucion, 22), 11)) as [ActionDate],
	(
		select case 
			when i.Estatus = 'Cerrado' then 'Yes'
			when i.Estatus <> 'Cerrado' then 'No' 
		end
	) as [ynClosed], -- si la incidencia esta cerrada o no
	i.Titulo as [Reference], -- este podria ser el titulo ??
	i.Descripcion as [mmQuestion], -- descripcion,
	i.Respuesta [mmResponse] -- la respuesta a la incidencia 
from Sam3_Incidencia i
where  i.IncidenciaID = @incidenciaID

END
