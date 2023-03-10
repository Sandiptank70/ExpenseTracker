USE [master]
GO
/****** Object:  Database [ExpanceTracker]    Script Date: 1/11/2023 10:32:16 AM ******/
CREATE DATABASE [ExpanceTracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ExpanceTracker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ExpanceTracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ExpanceTracker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ExpanceTracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ExpanceTracker] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExpanceTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExpanceTracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ExpanceTracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ExpanceTracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ExpanceTracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ExpanceTracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [ExpanceTracker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ExpanceTracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ExpanceTracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ExpanceTracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ExpanceTracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ExpanceTracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ExpanceTracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ExpanceTracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ExpanceTracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ExpanceTracker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ExpanceTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ExpanceTracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ExpanceTracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ExpanceTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ExpanceTracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ExpanceTracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ExpanceTracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ExpanceTracker] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ExpanceTracker] SET  MULTI_USER 
GO
ALTER DATABASE [ExpanceTracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ExpanceTracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ExpanceTracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ExpanceTracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ExpanceTracker] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ExpanceTracker] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ExpanceTracker] SET QUERY_STORE = OFF
GO
USE [ExpanceTracker]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/11/2023 10:32:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Userid] [int] NULL,
	[CatName] [nchar](10) NOT NULL,
	[CatAmount] [int] NOT NULL,
	[CatAvalibleAmt] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expance]    Script Date: 1/11/2023 10:32:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [int] NOT NULL,
	[ExpName] [nchar](10) NOT NULL,
	[ExpAmt] [int] NOT NULL,
	[ExpDescription] [nvarchar](50) NOT NULL,
	[ExpDate] [date] NOT NULL,
 CONSTRAINT [PK_Expance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Limit]    Script Date: 1/11/2023 10:32:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Limit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExpLimit] [int] NOT NULL,
	[AvalibleAmt] [int] NULL,
 CONSTRAINT [PK_Limit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 1/11/2023 10:32:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](10) NOT NULL,
	[Password] [nchar](10) NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_UserInfo] FOREIGN KEY([Userid])
REFERENCES [dbo].[UserInfo] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_UserInfo]
GO
ALTER TABLE [dbo].[Expance]  WITH CHECK ADD  CONSTRAINT [FK_Expance_Category] FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Expance] CHECK CONSTRAINT [FK_Expance_Category]
GO
ALTER TABLE [dbo].[Limit]  WITH CHECK ADD  CONSTRAINT [FK_Limit_UserInfo] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserInfo] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Limit] CHECK CONSTRAINT [FK_Limit_UserInfo]
GO
/****** Object:  StoredProcedure [dbo].[displayCategory]    Script Date: 1/11/2023 10:32:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[displayCategory]
as 
begin
select e.Category,e.ExpAmt,e.ExpDate,e.ExpDescription,e.ExpName,e.Id,c.CatName from Expance e inner join Category c on e.Category=c.Id
end
GO
/****** Object:  StoredProcedure [dbo].[FilterRecord]    Script Date: 1/11/2023 10:32:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[FilterRecord] (
    @id int
    
) as
begin
    SELECT e.Category,e.ExpAmt,e.ExpDate,e.ExpDescription,e.ExpName,e.Id,c.CatName    FROM Expance e inner JOIN
         Category c
         On e.Category = c.Id
		 where e.Category=@id
		 end
GO
USE [master]
GO
ALTER DATABASE [ExpanceTracker] SET  READ_WRITE 
GO
