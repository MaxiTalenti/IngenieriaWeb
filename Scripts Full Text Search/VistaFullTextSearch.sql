USE [dba43a70469a324b91b493a7430186b6b8]
GO

/****** Object:  View [dbo].[VistaFullTextSearch]    Script Date: 03/06/2017 12:32:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[VistaFullTextSearch] WITH SCHEMABINDING  AS
Select Et.Descripcion, Et.Direccion, Et.Id, CE.Descripcion as Categoria, Et.NombreEvento, U.Usuario
From dbo.Events Et Inner join
dbo.CategoriesEvents CE on CE.IdCategoria = Et.IdCategoria inner JOIN
dbo.Users U on Et.IdUser = U.Id

Where Et.Estado = 1 and U.Estado = 1




GO


--INDICE DE LA VISTA

USE [dba43a70469a324b91b493a7430186b6b8]
GO

SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO

/****** Object:  Index [UCI_DataType]    Script Date: 03/06/2017 12:33:08 ******/
CREATE UNIQUE CLUSTERED INDEX [UCI_DataType] ON [dbo].[VistaFullTextSearch]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

