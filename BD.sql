USE [master]
GO
/****** Object:  Database [dba43a70469a324b91b493a7430186b6b8]    Script Date: 02/05/2017 14:05:30 ******/
CREATE DATABASE [dba43a70469a324b91b493a7430186b6b8] ON  PRIMARY 
( NAME = N'dba43a70469a324b91b493a7430186b6b8', FILENAME = N'D:\mssqldata\dba43a70469a324b91b493a7430186b6b8.mdf' , SIZE = 2304KB , MAXSIZE = 20480KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dba43a70469a324b91b493a7430186b6b8_log', FILENAME = N'E:\mssqllog\dba43a70469a324b91b493a7430186b6b8_log.LDF' , SIZE = 504KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dba43a70469a324b91b493a7430186b6b8].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET ARITHABORT OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET  MULTI_USER 
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET DB_CHAINING OFF 
GO
USE [dba43a70469a324b91b493a7430186b6b8]
GO
/****** Object:  Table [dbo].[CategoriesEvents]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesEvents](
	[IdCategoria] [int] NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Events]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NombreEvento] [nvarchar](200) NOT NULL,
	[lat] [nvarchar](50) NULL,
	[lng] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](500) NOT NULL,
	[FechaInicio] [datetime] NOT NULL,
	[FechaFin] [datetime] NULL,
	[IdUser] [int] NOT NULL,
	[Estado] [int] NOT NULL,
	[IdCategoria] [int] NOT NULL,
	[Destacado] [bit] NOT NULL,
	[Direccion] [nvarchar](200) NULL,
	[RutaImagen] [nvarchar](200) NULL,
	[HoraInicio] [time](7) NULL,
	[HoraFin] [time](7) NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventState]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventState](
	[IdEventState] [int] NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEventState] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](100) NOT NULL,
	[Nombre] [nvarchar](150) NULL,
	[Apellido] [nvarchar](100) NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Userss] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserState]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserState](
	[Estado] [int] NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 02/05/2017 14:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[CategoriesEvents] ([IdCategoria], [Descripcion]) VALUES (1, N'Música')
GO
INSERT [dbo].[CategoriesEvents] ([IdCategoria], [Descripcion]) VALUES (2, N'Fiestas')
GO
INSERT [dbo].[CategoriesEvents] ([IdCategoria], [Descripcion]) VALUES (3, N'Artes')
GO
INSERT [dbo].[CategoriesEvents] ([IdCategoria], [Descripcion]) VALUES (4, N'Gastronomía')
GO
INSERT [dbo].[CategoriesEvents] ([IdCategoria], [Descripcion]) VALUES (5, N'Clases')
GO
INSERT [dbo].[CategoriesEvents] ([IdCategoria], [Descripcion]) VALUES (6, N'Deportes')
GO
SET IDENTITY_INSERT [dbo].[Events] ON 

GO
INSERT [dbo].[Events] ([Id], [NombreEvento], [lat], [lng], [Descripcion], [FechaInicio], [FechaFin], [IdUser], [Estado], [IdCategoria], [Destacado], [Direccion], [RutaImagen], [HoraInicio], [HoraFin]) VALUES (2, N'Evento destacado', N'-30.945', N'-61.562', N'Información de un evento destacado pero sin imagen', CAST(0x0000A75B00000000 AS DateTime), CAST(0x0000A75B00000000 AS DateTime), 9, 1, 1, 1, N'En SanCor', NULL, CAST(0x0700E03495640000 AS Time), CAST(0x070048F9F66C0000 AS Time))
GO
INSERT [dbo].[Events] ([Id], [NombreEvento], [lat], [lng], [Descripcion], [FechaInicio], [FechaFin], [IdUser], [Estado], [IdCategoria], [Destacado], [Direccion], [RutaImagen], [HoraInicio], [HoraFin]) VALUES (4, N'Tesis de Maxi', N'-30.9452', N'-61.56056', N'El Maxi presenta la tesis y vamos todos a agitar', CAST(0x0000A75C00000000 AS DateTime), CAST(0x0000A75C00000000 AS DateTime), 9, 1, 5, 0, N'En la pla', NULL, CAST(0x0700E03495640000 AS Time), CAST(0x070048F9F66C0000 AS Time))
GO
INSERT [dbo].[Events] ([Id], [NombreEvento], [lat], [lng], [Descripcion], [FechaInicio], [FechaFin], [IdUser], [Estado], [IdCategoria], [Destacado], [Direccion], [RutaImagen], [HoraInicio], [HoraFin]) VALUES (5, N'Evento x', N'-30.9452', N'-61.56056', N'Subida de imagen y eliminacion de archivo en la carpeta', CAST(0x0000A75C00000000 AS DateTime), CAST(0x0000A75C00000000 AS DateTime), 9, 1, 1, 0, N'Desde la silla', NULL, CAST(0x0700E03495640000 AS Time), CAST(0x070048F9F66C0000 AS Time))
GO
INSERT [dbo].[Events] ([Id], [NombreEvento], [lat], [lng], [Descripcion], [FechaInicio], [FechaFin], [IdUser], [Estado], [IdCategoria], [Destacado], [Direccion], [RutaImagen], [HoraInicio], [HoraFin]) VALUES (7, N'Primer Evento con Horario', N'-30.86843', N'-61.35504', N'Eventos con horarios a partir de ahora', CAST(0x0000A75D00000000 AS DateTime), CAST(0x0000A75D00000000 AS DateTime), 9, 1, 1, 0, N'En mi casa de Humberto', N'http://res.cloudinary.com/hrr6lj3xm/image/upload/v1492882861/l3zcnrrjwzsdrkryxrky.jpg', CAST(0x0700B893419F0000 AS Time), CAST(0x07002058A3A70000 AS Time))
GO
INSERT [dbo].[Events] ([Id], [NombreEvento], [lat], [lng], [Descripcion], [FechaInicio], [FechaFin], [IdUser], [Estado], [IdCategoria], [Destacado], [Direccion], [RutaImagen], [HoraInicio], [HoraFin]) VALUES (10, N'Evento nuevo', N'-30.9452', N'-61.56056', N'Probando el nuevo evento', CAST(0x0000A75F00000000 AS DateTime), CAST(0x0000A75F00000000 AS DateTime), 9, 1, 1, 0, N'Direccion nueva', N'', CAST(0x0700E03495640000 AS Time), CAST(0x070048F9F66C0000 AS Time))
GO
INSERT [dbo].[Events] ([Id], [NombreEvento], [lat], [lng], [Descripcion], [FechaInicio], [FechaFin], [IdUser], [Estado], [IdCategoria], [Destacado], [Direccion], [RutaImagen], [HoraInicio], [HoraFin]) VALUES (11, N'Evento probando imagen', N'-30.9452', N'-61.56056', N'Pruebita de imagen', CAST(0x0000A75F00000000 AS DateTime), CAST(0x0000A75F00000000 AS DateTime), 9, 1, 1, 0, N'Imagenes 2700', N'', CAST(0x0700E03495640000 AS Time), CAST(0x07007CDB27710000 AS Time))
GO
INSERT [dbo].[Events] ([Id], [NombreEvento], [lat], [lng], [Descripcion], [FechaInicio], [FechaFin], [IdUser], [Estado], [IdCategoria], [Destacado], [Direccion], [RutaImagen], [HoraInicio], [HoraFin]) VALUES (12, N'Prueba evento', N'-30.9452', N'-61.56056', N'Probando este evento', CAST(0x0000A75F00000000 AS DateTime), CAST(0x0000A75F00000000 AS DateTime), 9, 1, 5, 0, N'-', N'http://res.cloudinary.com/hrr6lj3xm/image/upload/v1493068793/k9kwomz2jzniq16ekim4.jpg', CAST(0x0700587660670000 AS Time), CAST(0x07001417C6680000 AS Time))
GO
SET IDENTITY_INSERT [dbo].[Events] OFF
GO
INSERT [dbo].[EventState] ([IdEventState], [Descripcion]) VALUES (1, N'Habilitado')
GO
INSERT [dbo].[EventState] ([IdEventState], [Descripcion]) VALUES (2, N'Bloqueado')
GO
INSERT [dbo].[EventState] ([IdEventState], [Descripcion]) VALUES (3, N'Reportado')
GO
INSERT [dbo].[EventState] ([IdEventState], [Descripcion]) VALUES (4, N'Eliminado')
GO
INSERT [dbo].[EventState] ([IdEventState], [Descripcion]) VALUES (5, N'Inhabilitado')
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([Id], [Usuario], [Nombre], [Apellido], [Email], [Estado]) VALUES (7, N'Admin@GlobalEvents.com', NULL, NULL, N'Admin@GlobalEvents.com', 1)
GO
INSERT [dbo].[Users] ([Id], [Usuario], [Nombre], [Apellido], [Email], [Estado]) VALUES (9, N'ema_colombo@hotmail.com', NULL, NULL, N'ema_colombo@hotmail.com', 1)
GO
INSERT [dbo].[Users] ([Id], [Usuario], [Nombre], [Apellido], [Email], [Estado]) VALUES (10, N'maximiliano.talenti@gmail.com', NULL, NULL, N'maximiliano.talenti@gmail.com', 1)
GO
INSERT [dbo].[Users] ([Id], [Usuario], [Nombre], [Apellido], [Email], [Estado]) VALUES (11, N'Prueba2@Prueba.com', NULL, NULL, N'Prueba2@Prueba.com', 1)
GO
INSERT [dbo].[Users] ([Id], [Usuario], [Nombre], [Apellido], [Email], [Estado]) VALUES (12, N'maximiliano.talenti@icloud.com', NULL, NULL, N'maximiliano.talenti@icloud.com', 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UserState] ([Estado], [Descripcion]) VALUES (1, N'Activo')
GO
INSERT [dbo].[UserState] ([Estado], [Descripcion]) VALUES (2, N'Bloqueado')
GO
INSERT [dbo].[UserState] ([Estado], [Descripcion]) VALUES (3, N'Reportado')
GO
INSERT [dbo].[UserState] ([Estado], [Descripcion]) VALUES (4, N'Eliminado')
GO
INSERT [dbo].[UserState] ([Estado], [Descripcion]) VALUES (5, N'Inactivo')
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (7, CAST(0x0000A75201464BCA AS DateTime), NULL, 1, CAST(0x0000A754008AC286 AS DateTime), 0, N'AFa6SFxnIZjyvJPEF1koW58DvZSjQjqPEdXgE/lpAZ+R4E40BmYz0aLOmTurXqQ/2g==', CAST(0x0000A754007838BB AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (9, CAST(0x0000A7580105123F AS DateTime), NULL, 1, CAST(0x0000A75C015A6362 AS DateTime), 0, N'AHgWDpB6HTIzxOTMW7sKJX+7EN75k3OGyv6xl1sJu59Vo/G06VN9ZHiyvstd/jI6rw==', CAST(0x0000A7580105123F AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (10, CAST(0x0000A75A00591D80 AS DateTime), N'bc9qS8jcHPue6aet6u3VoA2', 1, NULL, 0, N'ABl9uwxIx9XWk/6hvf5hvyWgubEXoLiXxPg49/iefZ/xdeZdG9WsLyrVvB137wyVjw==', CAST(0x0000A75A00591D80 AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (11, CAST(0x0000A75A00595B84 AS DateTime), N'EygCvAp6trFRaexDe43SWA2', 0, NULL, 0, N'APvIAjCKWm/ve/e7dYmHAx5R89wjMHjGXM8BvVzkhiGNEk41hQsIUmKqxWt0ublWdg==', CAST(0x0000A75A00595B84 AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (12, CAST(0x0000A75A005C75FF AS DateTime), N'CQeM3MmxpiyX5AJvoh-_TA2', 1, NULL, 0, N'AA6l+NmlOsNhSQTii8xH4hhIjPeg/q5xZFk0yfzyLVmD7XzCfJDW+Exx9aXhNt7bRw==', CAST(0x0000A75A005C75FF AS DateTime), N'', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[webpages_Roles] ON 

GO
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'Mod')
GO
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (3, N'Usuario')
GO
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (7, 3)
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (9, 3)
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (10, 3)
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (11, 3)
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (12, 3)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__webpages__8A2B616076969D2E]    Script Date: 02/05/2017 14:05:41 ******/
ALTER TABLE [dbo].[webpages_Roles] ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_webpages_UsersInRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [FK_webpages_UsersInRoles_Users]
GO
USE [master]
GO
ALTER DATABASE [dba43a70469a324b91b493a7430186b6b8] SET  READ_WRITE 
GO
