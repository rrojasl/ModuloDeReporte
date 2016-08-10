USE [steelgo-sam3]
GO
/****** Object:  StoredProcedure [dbo].[Sam3_RPT_OrdenRecepcion]    Script Date: 10/08/2016 01:27:44 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Julian Hernandez
-- Create date: 01-03-2016
-- Description:	Obtener datos para reporte de Orden de recepcion
-- =============================================
ALTER PROCEDURE [dbo].[Sam3_RPT_OrdenRecepcion]
	-- Add the parameters for the stored procedure here
	@ordenRecepcionID int, @usuarioID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @relfcID int, @relbID int, @fcID int, @tempID int

--set @orID = 7188

declare @temp table(
	OrdenRecepcionID int,
	RelFCID int,
	RELBID int
)

declare @result table(
	NumeroUnico varchar(max),
	OrdenRecepcion varchar(max),
	Cliente varchar(max),
	Proyecto varchar(max),
	AvisoEntrada varchar(max),
	PackingList varchar(max),
	ItemCode varchar(max),
	Descripcion varchar(max),
	Diametro1 varchar(max),
	Diametro2 varchar(max),
	Espesor varchar(max),
	TipoMaterial varchar(max),
	Cantidad varchar(max),
	Colada varchar(max),
	Responsable varchar(max)
)

set @tempID = (
	select OrdenRecepcionID from Sam3_OrdenRecepcion where Folio = @ordenRecepcionID
)

set @ordenRecepcionID = @tempID

insert into @temp (OrdenRecepcionID, RelFCID, RELBID)
	select OrdenRecepcionID, Rel_FolioCuantificacion_ItemCode_ID, Rel_Bulto_ItemCode_ID
	from Sam3_Rel_NumeroUnico_RelFC_RelB
	where OrdenRecepcionID = @ordenRecepcionID

	insert into @result
	Select
		(
			select 
				nu.Prefijo +'-' + 
				REPLACE(
					STR(
						nu.Consecutivo,
						CASE 
							WHEN LEN(nu.Consecutivo)> pc.DigitosNumeroUnico THEN LEN(nu.Consecutivo) 
							ELSE pc.DigitosNumeroUnico 
							END
						), SPACE(1), '0')
			from Sam3_NumeroUnico nu inner join Sam3_ProyectoConfiguracion pc
			on nu.ProyectoID = pc.ProyectoID
			where nu.NumeroUnicoID = relnu.NumeroUnicoID
		), 
		ord.Folio,
		cli.Nombre,
		(
			select Nombre
			from Sam3_Proyecto
			where ProyectoID = (
				select ProyectoID
				from Sam3_NumeroUnico
				where NumeroUnicoID = relnu.NumeroUnicoID
			)
		),
		fa.FolioAvisoLlegadaID,
		convert(varchar, fa.FolioAvisoLlegadaID) + '-' + convert(varchar,fc.Consecutivo),
		it.Codigo,
		it.DescripcionEspanol,
		convert(numeric(9,3),d1.Valor),
		convert(numeric(9,3),d2.Valor),
		convert(numeric(9,3),(
			select EspesorMM 
			from Sam3_CatalogoCedulas cc 
			inner join Sam3_ItemCodeSteelgo ics on cc.CatalogoCedulasID = ics.CedulaID
			inner join Sam3_Rel_ItemCodeSteelgo_Diametro ricsd on ics.ItemCodeSteelgoID = ricsd.ItemCodeSteelgoID
			inner join Sam3_Rel_ItemCode_ItemCodeSteelgo ritic on ricsd.Rel_ItemCodeSteelgo_Diametro_ID = ritic.Rel_ItemCodeSteelgo_Diametro_ID
			inner join Sam3_Rel_ItemCode_Diametro rid on ritic.Rel_ItemCode_Diametro_ID = rid.Rel_ItemCode_Diametro_ID
			inner join Sam3_ItemCode it2 on rid.ItemCodeID = it2.ItemCodeID
			inner join Sam3_NumeroUnico nu2 on it2.ItemCodeID = nu2.ItemCodeID
			where nu2.NumeroUnicoID = relnu.NumeroUnicoID
		)), --Se toma el valor de Espesor MM desde la relacion con Item code steelgo
		tpm.Nombre,
		'1',
		c.NumeroColada,
		(
			select Nombre
			from Sam3_Usuario
			where UsuarioID = @usuarioID
		)
	from Sam3_OrdenRecepcion ord
	inner join Sam3_Rel_NumeroUnico_RelFC_RelB relnu 
		on ord.OrdenRecepcionID = relnu.OrdenRecepcionID
	inner join Sam3_Rel_FolioCuantificacion_ItemCode relfc
		on relnu.Rel_FolioCuantificacion_ItemCode_ID = relfc.Rel_FolioCuantificacion_ItemCode_ID
	inner join Sam3_FolioCuantificacion fc 
		on relfc.FolioCuantificacionID = fc.FolioCuantificacionID
	inner join Sam3_FolioAvisoEntrada fe 
		on fc.FolioAvisoEntradaID = fe.FolioAvisoEntradaID
	inner join Sam3_FolioAvisoLlegada fa 
		on fe.FolioAvisoLlegadaID = fa.FolioAvisoLlegadaID
	inner join Sam3_Rel_ItemCode_Diametro relitd 
		on relfc.Rel_ItemCode_Diametro_ID = relitd.Rel_ItemCode_Diametro_ID
	inner join Sam3_ItemCode it
		on relitd.ItemCodeID = it.ItemCodeID
	inner join Sam3_Diametro d1 
		on relitd.Diametro1ID = d1.DiametroID
	inner join Sam3_Diametro d2
		on relitd.Diametro2ID = d2.DiametroID
	inner join Sam3_TipoMaterial tpm 
		on it.TipoMaterialID = tpm.TipoMaterialID
	inner join Sam3_Colada c
		on relfc.ColadaID = c.ColadaID
	inner join Sam3_Cliente cli 
	 on fe.ClienteID = cli.ClienteID
	where relfc.Rel_FolioCuantificacion_ItemCode_ID in (
		select RelFCID from @temp where RelFCID is not null
		and ord.OrdenRecepcionID = @ordenRecepcionID
	)
	and ord.OrdenRecepcionID = @ordenRecepcionID
---------------------------------------------------------------------------------------------------	
	insert into @result
	Select
		(
			select 
				nu.Prefijo +'-' + 
				REPLACE(
					STR(
						nu.Consecutivo,
						CASE 
							WHEN LEN(nu.Consecutivo)> pc.DigitosNumeroUnico THEN LEN(nu.Consecutivo) 
							ELSE pc.DigitosNumeroUnico 
							END
						), SPACE(1), '0')
			from Sam3_NumeroUnico nu inner join Sam3_ProyectoConfiguracion pc
			on nu.ProyectoID = pc.ProyectoID
			where nu.NumeroUnicoID = relnu.NumeroUnicoID
		), 
		ord.Folio,
		cli.Nombre,
		(
			select Nombre
			from Sam3_Proyecto
			where ProyectoID = (
				select ProyectoID
				from Sam3_NumeroUnico
				where NumeroUnicoID = relnu.NumeroUnicoID
			)
		),
		fa.FolioAvisoLlegadaID,
		convert(varchar, fa.FolioAvisoLlegadaID) + '-' + convert(varchar,fc.Consecutivo),
		it.Codigo,
		it.DescripcionEspanol,
		convert(numeric(9,3),d1.Valor),
		convert(numeric(9,3),d2.Valor),
		convert(numeric(9,3),(
			select EspesorMM 
			from Sam3_CatalogoCedulas cc 
			inner join Sam3_ItemCodeSteelgo ics on cc.CatalogoCedulasID = ics.CedulaID
			inner join Sam3_Rel_ItemCodeSteelgo_Diametro ricsd on ics.ItemCodeSteelgoID = ricsd.ItemCodeSteelgoID
			inner join Sam3_Rel_ItemCode_ItemCodeSteelgo ritic on ricsd.Rel_ItemCodeSteelgo_Diametro_ID = ritic.Rel_ItemCodeSteelgo_Diametro_ID
			inner join Sam3_Rel_ItemCode_Diametro rid on ritic.Rel_ItemCode_Diametro_ID = rid.Rel_ItemCode_Diametro_ID
			inner join Sam3_ItemCode it2 on rid.ItemCodeID = it2.ItemCodeID
			inner join Sam3_NumeroUnico nu2 on it2.ItemCodeID = nu2.ItemCodeID
			where nu2.NumeroUnicoID = relnu.NumeroUnicoID
		)), 
		tpm.Nombre,
		'1',
		c.NumeroColada,
		(
			select Nombre
			from Sam3_Usuario
			where UsuarioID = @usuarioID
		)
	from Sam3_OrdenRecepcion ord
	inner join Sam3_Rel_NumeroUnico_RelFC_RelB relnu 
		on ord.OrdenRecepcionID = relnu.OrdenRecepcionID
	inner join Sam3_Rel_Bulto_ItemCode relb
		on relnu.Rel_Bulto_ItemCode_ID = relb.Rel_Bulto_ItemCode_ID
	inner join Sam3_Bulto b
		on relb.BultoID = b.BultoID
	inner join Sam3_FolioCuantificacion fc 
		on b.FolioCuantificacionID = fc.FolioCuantificacionID
	inner join Sam3_FolioAvisoEntrada fe 
		on fc.FolioAvisoEntradaID = fe.FolioAvisoEntradaID
	inner join Sam3_FolioAvisoLlegada fa 
		on fe.FolioAvisoLlegadaID = fa.FolioAvisoLlegadaID
	inner join Sam3_Rel_ItemCode_Diametro relitd 
		on relb.Rel_ItemCode_Diametro_ID = relitd.Rel_ItemCode_Diametro_ID
	inner join Sam3_ItemCode it
		on relitd.ItemCodeID = it.ItemCodeID
	inner join Sam3_Diametro d1 
		on relitd.Diametro1ID = d1.DiametroID
	inner join Sam3_Diametro d2
		on relitd.Diametro2ID = d2.DiametroID
	inner join Sam3_TipoMaterial tpm 
		on it.TipoMaterialID = tpm.TipoMaterialID
	inner join Sam3_Colada c
		on relb.ColadaID = c.ColadaID
	inner join Sam3_Cliente cli 
		on fe.ClienteID = cli.ClienteID
	where relb.Rel_Bulto_ItemCode_ID in (
		select RELBID from @temp where RELBID is not null
		and ord.OrdenRecepcionID = @ordenRecepcionID
	)
	and ord.OrdenRecepcionID = @ordenRecepcionID

select * from @result
END

