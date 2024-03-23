USE [iiteAttendance]
GO
/****** Object:  Table [dbo].[Absence]    Script Date: 22/03/2024 00:01:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Absence](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateheure] [datetime] NULL,
	[matiere_id] [int] NULL,
	[eleve_id] [int] NULL,
 CONSTRAINT [PK_Absence_3213E83F0EFB3412] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Eleve]    Script Date: 22/03/2024 00:01:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eleve](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
	[filiere_id] [int] NULL,
 CONSTRAINT [PK_Eleve_3213E83FAA4020F6] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Filiere]    Script Date: 22/03/2024 00:01:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Filiere](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[professeur_id] [int] NULL,
 CONSTRAINT [PK_Filiere_3213E83F0CF506F3] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matiere]    Script Date: 22/03/2024 00:01:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matiere](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[professeur_id] [int] NULL,
 CONSTRAINT [PK_Matiere_3213E83FE95223AA] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Professeur]    Script Date: 22/03/2024 00:01:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professeur](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
 CONSTRAINT [PK_Professe_3213E83FC888C7E2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Absence]  WITH CHECK ADD  CONSTRAINT [FK_Absenceeleve_i_4316F928] FOREIGN KEY([eleve_id])
REFERENCES [dbo].[Eleve] ([id])
GO
ALTER TABLE [dbo].[Absence] CHECK CONSTRAINT [FK_Absenceeleve_i_4316F928]
GO
ALTER TABLE [dbo].[Absence]  WITH CHECK ADD  CONSTRAINT [FK_Absencematiere_4222D4EF] FOREIGN KEY([matiere_id])
REFERENCES [dbo].[Matiere] ([id])
GO
ALTER TABLE [dbo].[Absence] CHECK CONSTRAINT [FK_Absencematiere_4222D4EF]
GO
ALTER TABLE [dbo].[Eleve]  WITH CHECK ADD  CONSTRAINT [FK_Elevefiliere_i_3F466844] FOREIGN KEY([filiere_id])
REFERENCES [dbo].[Filiere] ([id])
GO
ALTER TABLE [dbo].[Eleve] CHECK CONSTRAINT [FK_Elevefiliere_i_3F466844]
GO
ALTER TABLE [dbo].[Filiere]  WITH CHECK ADD  CONSTRAINT [FK_Filiereprofess_3C69FB99] FOREIGN KEY([professeur_id])
REFERENCES [dbo].[Professeur] ([id])
GO
ALTER TABLE [dbo].[Filiere] CHECK CONSTRAINT [FK_Filiereprofess_3C69FB99]
GO
ALTER TABLE [dbo].[Matiere]  WITH CHECK ADD  CONSTRAINT [FK_Matiereprofess_398D8EEE] FOREIGN KEY([professeur_id])
REFERENCES [dbo].[Professeur] ([id])
GO
ALTER TABLE [dbo].[Matiere] CHECK CONSTRAINT [FK_Matiereprofess_398D8EEE]
GO
USE [master]
GO
ALTER DATABASE [iiteAttendance] SET  READ_WRITE 
GO