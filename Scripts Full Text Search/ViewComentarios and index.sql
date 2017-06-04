USE [dba43a70469a324b91b493a7430186b6b8]
GO

/****** Object:  View [dbo].[VistaFullTextSearch]    Script Date: 04/06/2017 3:32:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VistaFullTextSearchComentarios] WITH SCHEMABINDING  AS
Select Et.Descripcion, Et.Direccion, C.CommentId as Id, '' as Categoria, Et.NombreEvento, U.Usuario, C.Comentario
From dbo.Events Et Inner join
dbo.Users U on Et.IdUser = U.Id inner join
dbo.Comments C on C.EventId = Et.Id
Where Et.Estado = 1 and U.Estado = 1



GO

CREATE UNIQUE CLUSTERED INDEX UCI_Comentarios ON VistaFullTextSearchComentarios(Id)
GO


