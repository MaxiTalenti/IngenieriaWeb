USE [dba43a70469a324b91b493a7430186b6b8]
GO

/****** Object:  StoredProcedure [dbo].[sp_BusquedaFullText]    Script Date: 03/06/2017 12:34:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE Procedure [dbo].[sp_BusquedaFullText] 
@Busqueda nvarchar(100)
as

Select aux.NombreEvento, aux.Descripcion, aux.Direccion, aux.Id, aux.Categoria, aux.Usuario, aux.EncontradoEn, U.Id as IdUser, C.IdCategoria
from(

		Select NombreEvento, Descripcion, Direccion, Id, Categoria, Usuario, 'Evento' as EncontradoEn
		From VistaFullTextSearch 
		WHERE FREETEXT((NombreEvento, Descripcion, Direccion), @Busqueda)

		UNION

		Select NombreEvento, Descripcion, Direccion, Id, Categoria, Usuario, 'Categoria' as EncontradoEn
		From VistaFullTextSearch 
		WHERE FREETEXT(Categoria,@Busqueda)

		UNION

		Select NombreEvento, Descripcion, Direccion, Id, Categoria, Usuario, 'Usuario' as EncontradoEn
		From VistaFullTextSearch 
		WHERE FREETEXT(Usuario,@Busqueda)) as aux
	Inner join CategoriesEvents C on C.Descripcion = aux.Categoria 
	inner join Users U on aux.Usuario = U.Usuario





GO


