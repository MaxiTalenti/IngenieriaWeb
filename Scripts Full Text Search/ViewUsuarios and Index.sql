USE [dba43a70469a324b91b493a7430186b6b8]
GO

/****** Object:  View [dbo].[VistaFullTextSearch]    Script Date: 04/06/2017 2:47:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create VIEW [dbo].[VistaFullTextSearchUsuarios] WITH SCHEMABINDING  AS
Select U.Id, U.Usuario
From dbo.Users U 

Where U.Estado = 1

GO


CREATE UNIQUE CLUSTERED INDEX UCI_Usuarios ON VistaFullTextSearchUsuarios (Id)
GO