USE [dba43a70469a324b91b493a7430186b6b8]
GO

/****** Object:  StoredProcedure [dbo].[sp_BusquedaFullText]    Script Date: 04/06/2017 4:11:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







ALTER Procedure [dbo].[sp_BusquedaFullText] 
@Busqueda nvarchar(100)
as

Select aux.NombreEvento, aux.Descripcion, aux.Direccion, aux.Id, aux.Categoria, aux.Usuario, aux.EncontradoEn, U.Id as IdUser, 
C.IdCategoria, aux.Comentario
from(

		Select NombreEvento, Descripcion, Direccion, Id, Categoria, Usuario, 'Evento' as EncontradoEn, '' as Comentario
		From VistaFullTextSearch 
		WHERE FREETEXT((NombreEvento, Descripcion, Direccion), @Busqueda)

		UNION

		Select '', '', '', Id, '', Usuario, 'Usuario' as EncontradoEn, '' as Comentario
		From VistaFullTextSearchUsuarios 
		WHERE FREETEXT((Usuario), @Busqueda)

		UNION

		Select NombreEvento, Descripcion, Direccion, Id, Categoria, Usuario, 'Comentario' as EncontradoEn, Comentario as Comentario
		From VistaFullTextSearchComentarios
		WHERE FREETEXT(Comentario, @Busqueda)

		) as aux
	left join CategoriesEvents C on C.Descripcion = aux.Categoria 
	left join Users U on aux.Usuario = U.Usuario







GO


