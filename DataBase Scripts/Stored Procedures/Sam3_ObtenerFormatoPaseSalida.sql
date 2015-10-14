USE [steelgo-sam3]
GO
/****** Object:  StoredProcedure [dbo].[Sam3_ObtenerFormatoPaseSalida]    Script Date: 10/7/2015 2:59:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sam3_ObtenerFormatoPaseSalida]
	@FolioAvisoLlegadaID int	
AS
BEGIN
	SET NOCOUNT ON;

	declare @i int, 
			@strPlacas varchar(max);

	set @i=1;
	set @strPlacas='';


	create table #Placas(
	idx int identity(1,1),
	Placas varchar(20)
	)

	insert into #Placas
	select Placas from Sam3_Rel_FolioAvisoLlegada_Vehiculo a
	inner join Sam3_Vehiculo b on a.VehiculoID=b.VehiculoID
	inner join Sam3_TipoVehiculo c on b.TipoVehiculoID=c.TipoVehiculoID
	where a.Activo=1 and b.Activo=1 and c.Activo=1
	and FolioAvisoLlegadaID=@FolioAvisoLlegadaID

	declare @FechaInicioDescarga varchar(12),@FechaFinDescarga varchar(12),
			@cantidadPackingFirmado int,  @cantidadPlanas int

	select @FechaInicioDescarga=isnull(FORMAT(convert(datetime,FechainicioDescarga),'hh:mm:ss tt'),''),
		   @FechaFinDescarga=isnull(FORMAT(convert(datetime,FechaFinDescarga),'hh:mm:ss tt'),'')
	from Sam3_FolioAvisoEntrada where FolioAvisoLlegadaID=@FolioAvisoLlegadaID


	set @cantidadPlanas=(select COUNT(*) from #Placas)

	create table #Plana(
		FolioAvisoLlegadaID int,
		Plana varchar(max),
		ValorPlana varchar(max)
	)

	while(@i<= @cantidadPlanas)
	begin
		declare @Plana varchar(max), @Placas varchar(max)
		set @Plana='Plana '+convert(varchar(max),@i);

		select @Placas=Placas from #Placas where idx=@i

		insert into #Plana values(@FolioAvisoLlegadaID, @Plana,@Placas )
		set @i+=1;
	end

	declare @string varchar(max)
	SET @string='';

	select  convert(varchar(10),getdate(),103) as Fecha,
			@FechaInicioDescarga  as FechaInicioDescarga,
			@FechaFinDescarga as FechaFinDescarga,
			 FORMAT(convert(datetime,FechaRecepcion),'hh:mm:ss tt') as FechaLlegada,
			(select Nombre from Sam3_Chofer where ChoferID= a.ChoferID) as [NombreOperador],
			1 as IncidenciaFirmada,
			15001 as NumeroIncidencia, 
			p.Nombre as Proyecto,
			(select Placas from Sam3_Vehiculo where VehiculoID= a.VehiculoID) as Tracto, 
			fae.OrdenCompra as Pedimento, 
			fae.Factura as Factura, 
			'N/A'  as Remision,
			convert(varchar(max),@cantidadPlanas)  as [NumeroPlanas],
			Plana,
			ValorPlana
	from Sam3_FolioAvisoLlegada a 
	inner join Sam3_Rel_FolioAvisoLlegada_Proyecto relp 
	on a.FolioAvisoLlegadaID=relp.FolioAvisoLlegadaID 
	inner join Sam3_Proyecto  p 
	on  relp.ProyectoID=p.ProyectoID 
	inner join Sam3_FolioAvisoEntrada  fae 
	on  fae.FolioAvisoLlegadaID=a.FolioAvisoLlegadaID 
	left join #Plana pl
	on pl.FolioAvisoLlegadaID=a.FolioAvisoLlegadaID
	where a.FolioAvisoLlegadaID=@FolioAvisoLlegadaID




END