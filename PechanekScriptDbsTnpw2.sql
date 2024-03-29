USE [master]
GO
/****** Object:  Database [Hokejisti]    Script Date: 15. 4. 2019 18:36:47 ******/
CREATE DATABASE [Hokejisti]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hokejisti', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MILAN\MSSQL\DATA\Hokejisti.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hokejisti_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MILAN\MSSQL\DATA\Hokejisti_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Hokejisti] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hokejisti].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hokejisti] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hokejisti] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hokejisti] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hokejisti] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hokejisti] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hokejisti] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Hokejisti] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hokejisti] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hokejisti] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hokejisti] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hokejisti] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hokejisti] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hokejisti] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hokejisti] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hokejisti] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Hokejisti] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hokejisti] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hokejisti] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hokejisti] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hokejisti] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hokejisti] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Hokejisti] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hokejisti] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Hokejisti] SET  MULTI_USER 
GO
ALTER DATABASE [Hokejisti] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hokejisti] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hokejisti] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hokejisti] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hokejisti] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Hokejisti] SET QUERY_STORE = OFF
GO
USE [Hokejisti]
GO
/****** Object:  Table [dbo].[hokejista]    Script Date: 15. 4. 2019 18:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hokejista](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[jmeno] [varchar](200) NOT NULL,
	[tym] [varchar](50) NOT NULL,
	[datum_narozeni] [int] NOT NULL,
	[popis] [varchar](max) NULL,
	[image_name] [varchar](50) NULL,
	[liga_id] [int] NOT NULL,
	[pocet_bodu] [int] NULL,
	[post_id] [int] NOT NULL,
 CONSTRAINT [PK_hokejista] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hokejista_liga]    Script Date: 15. 4. 2019 18:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hokejista_liga](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev] [varchar](100) NOT NULL,
	[popis] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_hokejista_liga] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hokejista_post]    Script Date: 15. 4. 2019 18:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hokejista_post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev] [varchar](50) NOT NULL,
	[popis] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_hokejista_post] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hokejisti_role]    Script Date: 15. 4. 2019 18:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hokejisti_role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[identificator] [varchar](50) NOT NULL,
	[role_description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_hokejisti_role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hokejisti_user]    Script Date: 15. 4. 2019 18:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hokejisti_user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[surname] [varchar](50) NOT NULL,
	[login] [varchar](20) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_hokejisti_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vstupenka]    Script Date: 15. 4. 2019 18:36:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vstupenka](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[jmeno] [varchar](200) NULL,
	[adresa] [varchar](500) NOT NULL,
	[pocet_vstupenek] [int] NOT NULL,
 CONSTRAINT [PK_vstupenka] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[hokejista]  WITH CHECK ADD  CONSTRAINT [FK_hokejista_hokejista_liga] FOREIGN KEY([liga_id])
REFERENCES [dbo].[hokejista_liga] ([id])
GO
ALTER TABLE [dbo].[hokejista] CHECK CONSTRAINT [FK_hokejista_hokejista_liga]
GO
ALTER TABLE [dbo].[hokejista]  WITH CHECK ADD  CONSTRAINT [FK_hokejista_hokejista_post] FOREIGN KEY([post_id])
REFERENCES [dbo].[hokejista_post] ([id])
GO
ALTER TABLE [dbo].[hokejista] CHECK CONSTRAINT [FK_hokejista_hokejista_post]
GO
USE [master]
GO
ALTER DATABASE [Hokejisti] SET  READ_WRITE 
GO
