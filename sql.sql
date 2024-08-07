USE [master]
GO
/****** Object:  Database [PitNik]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE DATABASE [PitNik]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PitNik', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PitNik.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PitNik_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PitNik_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PitNik] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PitNik].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PitNik] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PitNik] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PitNik] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PitNik] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PitNik] SET ARITHABORT OFF 
GO
ALTER DATABASE [PitNik] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PitNik] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PitNik] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PitNik] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PitNik] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PitNik] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PitNik] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PitNik] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PitNik] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PitNik] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PitNik] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PitNik] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PitNik] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PitNik] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PitNik] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PitNik] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PitNik] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PitNik] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PitNik] SET  MULTI_USER 
GO
ALTER DATABASE [PitNik] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PitNik] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PitNik] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PitNik] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PitNik] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PitNik] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PitNik] SET QUERY_STORE = ON
GO
ALTER DATABASE [PitNik] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PitNik]
GO
/****** Object:  Schema [HangFire]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Gender] [int] NOT NULL,
	[Birthday] [datetime2](7) NULL,
	[ImageBackground] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConversationMember]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConversationMember](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConversationId] [int] NOT NULL,
	[IsCreate] [bit] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ConversationMember] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Conversations]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Conversations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Friendships]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friendships](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [nvarchar](450) NOT NULL,
	[ReceiverId] [nvarchar](450) NOT NULL,
	[Status] [int] NOT NULL,
	[RequestedAt] [datetime2](7) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Friendships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMembers]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[JoinedAt] [datetime2](7) NULL,
	[Created] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
	[IsCreate] [bit] NOT NULL,
 CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMessage]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_GroupMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMessageRecipients]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMessageRecipients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupMessageId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[IsRead] [bit] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_GroupMessageRecipients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Created] [datetime2](7) NOT NULL,
	[Background] [nvarchar](max) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImagePost]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImagePost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ImagePost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InforUsers]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InforUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Hobbies] [nvarchar](max) NULL,
	[Education] [nvarchar](max) NULL,
	[AboutMe] [nvarchar](max) NULL,
	[WorkAndExperience] [nvarchar](max) NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_InforUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interactions]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmojiId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Emoji] [int] NOT NULL,
 CONSTRAINT [PK_Interactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageReadStatuses]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageReadStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[IsSeen] [bit] NOT NULL,
	[ReadAt] [datetime2](7) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_MessageReadStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [nvarchar](450) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[ConversationId] [int] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[PostId] [int] NULL,
	[IsSeen] [bit] NOT NULL,
	[SenderId] [nvarchar](450) NULL,
	[ReceiverId] [nvarchar](450) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReplyComments]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReplyComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentId] [int] NOT NULL,
	[CommenterId] [nvarchar](450) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[ResponderId] [nvarchar](450) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReplyComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[ExpireAt] [datetime] NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_HangFire_Counter] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StateId] [bigint] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Queue] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 31/7/2024 10:47:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240615083956_intial', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240616132417_initialization', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240617140719_addImagePost', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240624184421_editFriendShip', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240628124010_editEntityUser', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240629131701_addNotification', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240629131810_editNotification', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240630132737_ediMessage', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240701181831_addProfileUser', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240705135851_addConverstionMember', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240705141108_editMessage', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240706182717_editGroup', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240706183019_editGroupmember', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240717180109_updateAccount', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240724124952_addReplyComment', N'8.0.3')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Name], [Address], [Image], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gender], [Birthday], [ImageBackground]) VALUES (N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'Nguyễn Quang Anh', N'Đặng Lễ-Ân Thi-Hưng Yên', N'04894db7-b48e-40ee-a64a-cc234bbf307c_tải xuống (1).jfif', N'anh@gmail.com', N'ANH@GMAIL.COM', N'anh@gmail.com', N'ANH@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAECxC21nCrUfl0bqQPBrsb+kh82zoh/Fw1X2np00TttIEywRD5tqyoLI7hK39PPO4hw==', N'B25DQFUMNYE37AQMZ7DLQZPKF5N7CN6J', N'4796b354-7eb2-431f-ac02-d141d8187d9a', N'0976767656', 0, 0, NULL, 1, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Name], [Address], [Image], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gender], [Birthday], [ImageBackground]) VALUES (N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Đinh Thành Luân', N'Đặng Lễ-Ân Thi-Hưng Yên', N'2138262c-61b0-4a31-9719-290c0f87fe58_z4564039225215_6dd5f55924f1ebce0605f5fa189c6739.jpg', N'luan2k2hy@gmail.com', N'LUAN2K2HY@GMAIL.COM', N'luan2k2hy@gmail.com', N'LUAN2K2HY@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAECR4LcJ5SaIPY0FwZvAItkRRKFNuWc+FP5kEyjBxulqA4WI7YHBnnqfBNPsdR5v4CQ==', N'DXNXJ5LW2MEYAIQPXY233NANKCZR7F4W', N'51f09652-5910-4688-bb2d-3f3c5317d640', N'0971877014', 0, 0, NULL, 1, 0, 0, CAST(N'2024-07-12T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Name], [Address], [Image], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gender], [Birthday], [ImageBackground]) VALUES (N'5934638f-0911-46b3-aff0-7469bb058fe5', N'Đinh Văn Phúc', N'Đặng Lễ-Ân Thi-Hưng Yên', NULL, N'phuc@gmail.com', N'PHUC@GMAIL.COM', N'taikhoangameonline74@gmail.com', N'TAIKHOANGAMEONLINE74@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEB15rqCom5lpbP7CIQVje3WSHsxnIcreKjOkJM3QOA1PHPkdB3usbJ53vuW9LoZ8Tg==', N'ZMO6ZEJWC7F26462FS24AELPS2JDRTIW', N'b4cce8ee-2319-4b3f-99c7-bc3656efb6d3', N'0989878767', 0, 0, NULL, 1, 0, 0, CAST(N'2003-07-13T17:12:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Name], [Address], [Image], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gender], [Birthday], [ImageBackground]) VALUES (N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', N'Đinh Thị Thanh Tuyền', N'Đặng Lễ, Ân Thi, Hưng Yên', N'7bcfc07f-a837-43e1-9de9-68db9dea5a65_images.jfif', N'luanhy2k2@gmail.com', N'LUANHY2K2@GMAIL.COM', N'luanhy2k2@gmail.com', N'LUANHY2K2@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEOE5UdC3dxxj+Izliiff6MBveAprBOYprFaO6SECucdyZNYSrkAwZV2gP/8UhIvEQA==', N'XGK7HZHL6FCNTEUCZ6NI6DF53JATXB5O', N'db03282e-dd5f-4eb7-820e-2ab089ed58ce', N'0965920038', 0, 0, NULL, 1, 0, 1, CAST(N'2001-09-09T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Name], [Address], [Image], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gender], [Birthday], [ImageBackground]) VALUES (N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Lê Thị Hoà', N'Đặng Lễ, Ân Thi, Hưng Yên', N'd3e0c93a-699d-4e62-a482-25cc5717cd55_album11.jpg', N'hoa@gmail.com', N'HOA@GMAIL.COM', N'hoa@gmail.com', N'HOA@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEFn3+mF5TtMESI+1LUwHZrlNDQA4I2/Lp2N3GsZugrUPjZAZRsEjI02NyH8xlLwuRw==', N'BXN4Q4Q5RF75ITH36A5NVYV5ZNNPERLB', N'046ab10c-7afc-4a29-89eb-2c94f0751ebb', N'0964270667', 0, 0, NULL, 1, 0, 1, CAST(N'1974-06-09T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Name], [Address], [Image], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gender], [Birthday], [ImageBackground]) VALUES (N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'Đinh Văn Anh', N'Đặng Lễ-Ân Thi-Hưng Yên', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2_tải xuống.jfif', N'luan22hy@gmail.com', N'LUAN22HY@GMAIL.COM', N'luan22hy@gmail.com', N'LUAN22HY@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEPIaGYsDhPind3CycdA15z8gjFBrX/0THjuj/RRUtvqpLjL65W17ewifl4yeuCd6UQ==', N'O4JC2ID6C7VBYXHT3DOSXRO7B6PBLUCL', N'3b24dc74-487f-458e-a734-cd27cf230377', N'0964270667', 0, 0, NULL, 1, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL)
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (1, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Hello
', CAST(N'2024-06-24T10:00:15.9392585' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (2, 4, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Chào cậu nhé', CAST(N'2024-06-24T10:24:01.7319709' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (3, 1, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'Chào đằng ấy', CAST(N'2024-06-24T10:26:27.5439276' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (4, 4, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'Chào đằng ấy nhé', CAST(N'2024-06-24T17:28:05.5548790' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (5, 6, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Đẹp đấy', CAST(N'2024-06-24T17:39:41.7295900' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (6, 4, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'Tuyệt', CAST(N'2024-06-26T19:44:52.3254170' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (7, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N':))', CAST(N'2024-06-27T21:09:33.3211724' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (8, 7, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Chào cậu nhé', CAST(N'2024-07-01T20:54:35.5931539' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (9, 6, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Đẹp đấy bạn', CAST(N'2024-07-03T17:35:42.6281589' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (10, 5, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Chào', CAST(N'2024-07-03T17:36:49.4986204' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (11, 5, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'In bạn', CAST(N'2024-07-03T17:37:44.0825899' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (12, 5, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Đẹp đấy bạn ơi', CAST(N'2024-07-03T17:39:49.0424440' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (13, 6, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Chào đằng ấy nhé', CAST(N'2024-07-03T17:43:08.0902666' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (14, 8, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'đẹp ghê', CAST(N'2024-07-11T00:46:45.9911793' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (15, 10, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Mong mọi người giúp đỡ lẫn nhau!', CAST(N'2024-07-17T13:21:24.7345803' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (16, 10, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Cảm ơn chủ post nhiều', CAST(N'2024-07-17T13:38:09.7521107' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (17, 6, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Tuyệt quá', CAST(N'2024-07-17T13:40:27.9021741' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (18, 5, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Đẹp vậy', CAST(N'2024-07-17T13:45:48.0238660' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (19, 13, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Chúc cả nhà vui vẻ:))', CAST(N'2024-07-25T01:45:20.9854424' AS DateTime2))
INSERT [dbo].[Comments] ([Id], [PostId], [UserId], [Content], [Created]) VALUES (20, 13, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Chúc sếp 1 ngày vui vẻ', CAST(N'2024-07-25T02:15:44.0278886' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[ConversationMember] ON 

INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1003, 1003, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1004, 1003, 0, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1005, 1004, 1, N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1006, 1004, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1007, 1005, 1, N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1008, 1005, 0, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1009, 1006, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1010, 1006, 0, N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1011, 1007, 1, N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1012, 1007, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1015, 1009, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1016, 1009, 0, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-16T00:58:16.9523773' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1017, 1010, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ConversationMember] ([Id], [ConversationId], [IsCreate], [UserId], [Created]) VALUES (1018, 1010, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-16T01:06:05.8329643' AS DateTime2))
SET IDENTITY_INSERT [dbo].[ConversationMember] OFF
GO
SET IDENTITY_INSERT [dbo].[Conversations] ON 

INSERT [dbo].[Conversations] ([Id], [Created]) VALUES (1003, CAST(N'2024-07-10T02:13:26.0793364' AS DateTime2))
INSERT [dbo].[Conversations] ([Id], [Created]) VALUES (1004, CAST(N'2024-07-11T21:06:54.3464001' AS DateTime2))
INSERT [dbo].[Conversations] ([Id], [Created]) VALUES (1005, CAST(N'2024-07-11T22:23:13.5739986' AS DateTime2))
INSERT [dbo].[Conversations] ([Id], [Created]) VALUES (1006, CAST(N'2024-07-11T22:50:01.2828143' AS DateTime2))
INSERT [dbo].[Conversations] ([Id], [Created]) VALUES (1007, CAST(N'2024-07-14T02:11:28.9993283' AS DateTime2))
INSERT [dbo].[Conversations] ([Id], [Created]) VALUES (1009, CAST(N'2024-07-16T00:58:17.6982339' AS DateTime2))
INSERT [dbo].[Conversations] ([Id], [Created]) VALUES (1010, CAST(N'2024-07-16T01:06:05.8329661' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Conversations] OFF
GO
SET IDENTITY_INSERT [dbo].[Friendships] ON 

INSERT [dbo].[Friendships] ([Id], [SenderId], [ReceiverId], [Status], [RequestedAt], [Created]) VALUES (13, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', 1, CAST(N'2024-06-27T22:08:05.3994320' AS DateTime2), CAST(N'2024-06-25T02:11:45.0050708' AS DateTime2))
INSERT [dbo].[Friendships] ([Id], [SenderId], [ReceiverId], [Status], [RequestedAt], [Created]) VALUES (1014, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', 1, CAST(N'2024-07-10T02:13:25.9639126' AS DateTime2), CAST(N'2024-07-10T02:13:19.7182349' AS DateTime2))
INSERT [dbo].[Friendships] ([Id], [SenderId], [ReceiverId], [Status], [RequestedAt], [Created]) VALUES (1021, N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', 1, CAST(N'2024-07-11T22:23:13.5445586' AS DateTime2), CAST(N'2024-07-11T22:22:26.3500445' AS DateTime2))
INSERT [dbo].[Friendships] ([Id], [SenderId], [ReceiverId], [Status], [RequestedAt], [Created]) VALUES (1022, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-11T22:50:01.1895647' AS DateTime2), CAST(N'2024-07-11T22:49:55.0899227' AS DateTime2))
INSERT [dbo].[Friendships] ([Id], [SenderId], [ReceiverId], [Status], [RequestedAt], [Created]) VALUES (1023, N'5934638f-0911-46b3-aff0-7469bb058fe5', N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T02:11:28.8930627' AS DateTime2), CAST(N'2024-07-14T02:09:33.7367179' AS DateTime2))
INSERT [dbo].[Friendships] ([Id], [SenderId], [ReceiverId], [Status], [RequestedAt], [Created]) VALUES (1024, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-16T01:05:31.9030553' AS DateTime2), CAST(N'2024-07-16T01:05:26.4552066' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Friendships] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupMembers] ON 

INSERT [dbo].[GroupMembers] ([Id], [GroupId], [UserId], [JoinedAt], [Created], [Status], [IsCreate]) VALUES (4, 3, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-09T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-09T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[GroupMembers] ([Id], [GroupId], [UserId], [JoinedAt], [Created], [Status], [IsCreate]) VALUES (8, 3, N'04894db7-b48e-40ee-a64a-cc234bbf307c', NULL, CAST(N'2024-07-09T05:42:03.1287452' AS DateTime2), 1, 0)
INSERT [dbo].[GroupMembers] ([Id], [GroupId], [UserId], [JoinedAt], [Created], [Status], [IsCreate]) VALUES (9, 3, N'5934638f-0911-46b3-aff0-7469bb058fe5', NULL, CAST(N'2024-07-14T00:48:54.7500713' AS DateTime2), 1, 0)
INSERT [dbo].[GroupMembers] ([Id], [GroupId], [UserId], [JoinedAt], [Created], [Status], [IsCreate]) VALUES (10, 4, N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-14T01:48:54.3511627' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[GroupMembers] ([Id], [GroupId], [UserId], [JoinedAt], [Created], [Status], [IsCreate]) VALUES (11, 4, N'2138262c-61b0-4a31-9719-290c0f87fe58', NULL, CAST(N'2024-07-14T01:51:54.0728592' AS DateTime2), 1, 0)
INSERT [dbo].[GroupMembers] ([Id], [GroupId], [UserId], [JoinedAt], [Created], [Status], [IsCreate]) VALUES (12, 3, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', NULL, CAST(N'2024-07-14T22:11:47.2745241' AS DateTime2), 1, 0)
INSERT [dbo].[GroupMembers] ([Id], [GroupId], [UserId], [JoinedAt], [Created], [Status], [IsCreate]) VALUES (13, 5, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-17T15:30:54.5858241' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[GroupMembers] ([Id], [GroupId], [UserId], [JoinedAt], [Created], [Status], [IsCreate]) VALUES (14, 3, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', NULL, CAST(N'2024-07-17T15:32:13.9762134' AS DateTime2), 1, 0)
SET IDENTITY_INSERT [dbo].[GroupMembers] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 

INSERT [dbo].[Groups] ([Id], [Name], [Description], [Created], [Background]) VALUES (3, N'ASP.NET Core Việt Nam', N'Chia sẻ thảo luận mọi thứ về ASP.NET Core, cùng các công nghệ liên quan', CAST(N'2024-07-08T20:09:18.5239537' AS DateTime2), N'0_35356766_711642512344080_5722752001582825472_n.jpg')
INSERT [dbo].[Groups] ([Id], [Name], [Description], [Created], [Background]) VALUES (4, N'Lập trình hướng đối tượng OOP', N'Nhầm mục đích giúp chúng ta hiểu nhưng vấn đề mà ta thắc mắc trong quá trình học tập.', CAST(N'2024-07-14T01:48:53.5421793' AS DateTime2), N'0_tải xuống (2).jfif')
INSERT [dbo].[Groups] ([Id], [Name], [Description], [Created], [Background]) VALUES (5, N'Hội những người yêu đời', N'Nơi tụ hợp những người sống hết mình với thanh xuân:))', CAST(N'2024-07-17T15:30:53.9356884' AS DateTime2), N'0_tải xuống (4).jfif')
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[ImagePost] ON 

INSERT [dbo].[ImagePost] ([Id], [PostId], [Image], [Created]) VALUES (3, 4, N'album11.jpg', CAST(N'2024-06-21T22:51:22.6165439' AS DateTime2))
INSERT [dbo].[ImagePost] ([Id], [PostId], [Image], [Created]) VALUES (4, 5, N'5_album11.jpg', CAST(N'2024-06-22T21:34:29.2557650' AS DateTime2))
INSERT [dbo].[ImagePost] ([Id], [PostId], [Image], [Created]) VALUES (9, 6, N'6_z4564039225215_6dd5f55924f1ebce0605f5fa189c6739.jpg', CAST(N'2024-06-23T19:16:51.2546437' AS DateTime2))
INSERT [dbo].[ImagePost] ([Id], [PostId], [Image], [Created]) VALUES (10, 8, N'8_IMG_9424.jpg', CAST(N'2024-07-10T20:34:16.3662576' AS DateTime2))
INSERT [dbo].[ImagePost] ([Id], [PostId], [Image], [Created]) VALUES (12, 10, N'10_35356766_711642512344080_5722752001582825472_n.jpg', CAST(N'2024-07-14T22:07:22.0006490' AS DateTime2))
INSERT [dbo].[ImagePost] ([Id], [PostId], [Image], [Created]) VALUES (13, 11, N'11_tải xuống (3).jfif', CAST(N'2024-07-17T14:48:54.5191375' AS DateTime2))
INSERT [dbo].[ImagePost] ([Id], [PostId], [Image], [Created]) VALUES (14, 12, N'12_Capture.PNG', CAST(N'2024-07-17T16:11:42.8768658' AS DateTime2))
INSERT [dbo].[ImagePost] ([Id], [PostId], [Image], [Created]) VALUES (15, 13, N'13_tải xuống (5).jfif', CAST(N'2024-07-17T16:13:56.0137913' AS DateTime2))
SET IDENTITY_INSERT [dbo].[ImagePost] OFF
GO
SET IDENTITY_INSERT [dbo].[InforUsers] ON 

INSERT [dbo].[InforUsers] ([Id], [UserId], [Hobbies], [Education], [AboutMe], [WorkAndExperience], [Created]) VALUES (3, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'Sở thích của tôi là chơi game, xem phim, nghe nhạc,...', N'Tốt nghiệp tại trường đại học SPKT Hưng Yên', N'Xin chào, tôi là Nguyễn Quang Anh, tôi 24 tuổi và làm nhà phát triển web tại Microsoft', N'Web developer', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[InforUsers] ([Id], [UserId], [Hobbies], [Education], [AboutMe], [WorkAndExperience], [Created]) VALUES (4, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Tôi thích đi xe đạp, bơi lội và tập thể dục. Tôi cũng thích đọc tạp chí thiết kế và tìm kiếm trên internet, đồng thời say sưa xem một bộ phim Hollywood hay trong khi ngoài trời đang mưa.', N'Thạc sĩ khoa học máy tính, bằng cấp 16 năm từ Đại học Oxford, London', N'Xin chào, tôi là Đinh Thành Luân, tôi 24 tuổi và làm nhà phát triển web tại Microsoft', N'Hiện đang làm việc trong công ty phát triển web "bàn tay màu" trong 5 năm qua với tư cách là Nhà thiết kế UI/UX cấp cao', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[InforUsers] ([Id], [UserId], [Hobbies], [Education], [AboutMe], [WorkAndExperience], [Created]) VALUES (5, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', NULL, NULL, NULL, NULL, CAST(N'2024-07-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[InforUsers] ([Id], [UserId], [Hobbies], [Education], [AboutMe], [WorkAndExperience], [Created]) VALUES (6, N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', N'', N'', N'', N'', CAST(N'2024-07-11T20:55:46.1796629' AS DateTime2))
INSERT [dbo].[InforUsers] ([Id], [UserId], [Hobbies], [Education], [AboutMe], [WorkAndExperience], [Created]) VALUES (7, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'', N'', N'', N'', CAST(N'2024-07-11T22:07:15.7969058' AS DateTime2))
INSERT [dbo].[InforUsers] ([Id], [UserId], [Hobbies], [Education], [AboutMe], [WorkAndExperience], [Created]) VALUES (17, N'5934638f-0911-46b3-aff0-7469bb058fe5', N'', N'', N'XIn chào, tôi là Đinh Văn Phúc, tôi 19 tuổi và đang độc thân:))', N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[InforUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[Interactions] ON 

INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (32, 3, 5, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-06-26T13:57:24.1005530' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (33, 3, 6, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-06-26T13:57:41.2863647' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (51, 3, 4, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-06-27T09:24:09.3311352' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (52, 3, 4, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-06-27T09:24:14.3963337' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (88, 3, 4, N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-01T13:27:48.0183288' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (89, 3, 5, N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-01T13:27:53.2163457' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (113, 3, 7, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-03T12:39:08.9583485' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (132, 3, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-08T22:17:55.8370664' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (133, 3, 8, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-10T13:37:14.4119464' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (134, 3, 8, N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-13T17:01:06.1087317' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (136, 3, 10, N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-14T15:18:20.5887045' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (140, 3, 6, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-16T11:53:49.9618983' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (154, 3, 8, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-17T06:20:28.2380583' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (157, 3, 10, N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-17T06:33:46.5620383' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (158, 3, 5, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-17T06:45:33.7870653' AS DateTime2), 0)
INSERT [dbo].[Interactions] ([Id], [EmojiId], [PostId], [UserId], [Created], [Emoji]) VALUES (160, 3, 10, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-17T07:58:41.8565246' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Interactions] OFF
GO
SET IDENTITY_INSERT [dbo].[MessageReadStatuses] ON 

INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (92, 1108, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-12T18:01:16.3667854' AS DateTime2), CAST(N'2024-07-12T18:01:16.3586484' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (93, 1108, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-12T18:01:53.6131336' AS DateTime2), CAST(N'2024-07-12T18:01:53.6131330' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (94, 1110, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-12T18:05:34.8665589' AS DateTime2), CAST(N'2024-07-12T18:05:34.8665583' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (95, 1111, N'5934638f-0911-46b3-aff0-7469bb058fe5', 1, CAST(N'2024-07-14T20:29:33.1394670' AS DateTime2), CAST(N'2024-07-14T20:29:33.1393400' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (96, 1112, N'5934638f-0911-46b3-aff0-7469bb058fe5', 1, CAST(N'2024-07-14T13:29:47.6168768' AS DateTime2), CAST(N'2024-07-14T13:29:47.6168771' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (97, 1112, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T20:29:55.4159122' AS DateTime2), CAST(N'2024-07-14T20:29:55.4159114' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (98, 1113, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T13:30:53.4292843' AS DateTime2), CAST(N'2024-07-14T13:30:53.4292844' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (99, 1110, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T20:37:14.5738973' AS DateTime2), CAST(N'2024-07-14T20:37:14.5738410' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (100, 1107, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T20:37:28.2554618' AS DateTime2), CAST(N'2024-07-14T20:37:28.2554613' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (101, 1121, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T13:51:37.8195922' AS DateTime2), CAST(N'2024-07-14T13:51:37.8196525' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (102, 1121, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T21:39:34.0897357' AS DateTime2), CAST(N'2024-07-14T21:39:34.0893717' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (103, 1122, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T14:39:39.3204873' AS DateTime2), CAST(N'2024-07-14T14:39:39.3204874' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (104, 1107, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', 1, CAST(N'2024-07-14T21:45:44.9539378' AS DateTime2), CAST(N'2024-07-14T21:45:44.9539370' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (105, 1122, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T22:07:30.9057534' AS DateTime2), CAST(N'2024-07-14T22:07:30.9057527' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (106, 1123, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T19:44:27.4088202' AS DateTime2), CAST(N'2024-07-14T19:44:27.4088757' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (107, 1123, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-15T02:44:45.2415413' AS DateTime2), CAST(N'2024-07-15T02:44:45.2415405' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (108, 1124, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T19:45:51.7449373' AS DateTime2), CAST(N'2024-07-14T19:45:51.7449374' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (109, 1125, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T19:46:15.3279238' AS DateTime2), CAST(N'2024-07-14T19:46:15.3279239' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (110, 1125, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-15T02:46:59.6953170' AS DateTime2), CAST(N'2024-07-15T02:46:59.6953164' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (111, 1126, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T19:47:06.0664874' AS DateTime2), CAST(N'2024-07-14T19:47:06.0664875' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (112, 1126, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-15T02:47:25.7875130' AS DateTime2), CAST(N'2024-07-15T02:47:25.7875125' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (113, 1127, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T19:47:51.7693747' AS DateTime2), CAST(N'2024-07-14T19:47:51.7693748' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (114, 1128, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T19:48:03.4984630' AS DateTime2), CAST(N'2024-07-14T19:48:03.4984630' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (115, 1128, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-15T02:50:34.6035218' AS DateTime2), CAST(N'2024-07-15T02:50:34.6035215' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (116, 1129, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T19:50:45.8825146' AS DateTime2), CAST(N'2024-07-14T19:50:45.8825147' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (117, 1130, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T19:51:42.0989055' AS DateTime2), CAST(N'2024-07-14T19:51:42.0989056' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (118, 1131, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T20:06:28.6461990' AS DateTime2), CAST(N'2024-07-14T20:06:28.6462500' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (119, 1132, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T20:07:31.4615271' AS DateTime2), CAST(N'2024-07-14T20:07:31.4615272' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (120, 1133, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T20:08:54.9175406' AS DateTime2), CAST(N'2024-07-14T20:08:54.9175407' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (121, 1134, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-14T20:09:16.1929451' AS DateTime2), CAST(N'2024-07-14T20:09:16.1929452' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (122, 1134, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-15T03:09:41.5695640' AS DateTime2), CAST(N'2024-07-15T03:09:41.5695633' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (123, 1135, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T20:10:09.7317227' AS DateTime2), CAST(N'2024-07-14T20:10:09.7317228' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (124, 1136, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-14T20:11:56.6178436' AS DateTime2), CAST(N'2024-07-14T20:11:56.6178438' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (125, 1136, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-15T03:12:04.5819301' AS DateTime2), CAST(N'2024-07-15T03:12:04.5819295' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (126, 1137, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-15T16:56:20.9061348' AS DateTime2), CAST(N'2024-07-15T16:56:20.9061854' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (127, 1138, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-15T16:56:51.9411123' AS DateTime2), CAST(N'2024-07-15T16:56:51.9411124' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (128, 1142, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-15T17:58:38.1760076' AS DateTime2), CAST(N'2024-07-15T17:58:38.1760674' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (129, 1138, N'04894db7-b48e-40ee-a64a-cc234bbf307c', 1, CAST(N'2024-07-16T01:01:45.7942628' AS DateTime2), CAST(N'2024-07-16T01:01:45.7942620' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (130, 1109, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', 1, CAST(N'2024-07-16T01:05:12.0822357' AS DateTime2), CAST(N'2024-07-16T01:05:12.0822351' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (131, 1143, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', 1, CAST(N'2024-07-15T18:06:25.9063887' AS DateTime2), CAST(N'2024-07-15T18:06:25.9063888' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (132, 1143, N'2138262c-61b0-4a31-9719-290c0f87fe58', 1, CAST(N'2024-07-16T01:06:30.2960351' AS DateTime2), CAST(N'2024-07-16T01:06:30.2960345' AS DateTime2))
INSERT [dbo].[MessageReadStatuses] ([Id], [MessageId], [UserId], [IsSeen], [ReadAt], [Created]) VALUES (133, 1113, N'5934638f-0911-46b3-aff0-7469bb058fe5', 1, CAST(N'2024-07-16T05:39:27.5568333' AS DateTime2), CAST(N'2024-07-16T05:39:27.5567231' AS DateTime2))
SET IDENTITY_INSERT [dbo].[MessageReadStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1107, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'Chào cậu, từ giờ chúng ta sẽ là bạn bè, mong được giúp đỡ!', 1003, CAST(N'2024-07-10T02:13:26.0798444' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1108, N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', N'Chào cậu, từ giờ chúng ta sẽ là bạn bè, mong được giúp đỡ!', 1004, CAST(N'2024-07-11T21:06:54.3471723' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1109, N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', N'Chào cậu, từ giờ chúng ta sẽ là bạn bè, mong được giúp đỡ!', 1005, CAST(N'2024-07-11T22:23:13.5740019' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1110, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Chào cậu, từ giờ chúng ta sẽ là bạn bè, mong được giúp đỡ!', 1006, CAST(N'2024-07-11T22:50:01.2839136' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1111, N'5934638f-0911-46b3-aff0-7469bb058fe5', N'Chào cậu, từ giờ chúng ta sẽ là bạn bè, mong được giúp đỡ!', 1007, CAST(N'2024-07-14T02:11:29.0002152' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1112, N'5934638f-0911-46b3-aff0-7469bb058fe5', N'chào cậu', 1007, CAST(N'2024-07-14T20:29:47.5862931' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1113, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'dạo này thế nào', 1007, CAST(N'2024-07-14T20:30:53.4259295' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1121, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'chào cậu', 1006, CAST(N'2024-07-14T20:51:37.7944442' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1122, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'uk', 1006, CAST(N'2024-07-14T21:39:39.2961090' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1123, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'dạo này thế nào', 1006, CAST(N'2024-07-15T02:44:27.3195855' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1124, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'ổn cậu ạ', 1006, CAST(N'2024-07-15T02:45:51.7407800' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1125, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'còn cậu', 1006, CAST(N'2024-07-15T02:46:15.3218081' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1126, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'mình cũng vậy', 1006, CAST(N'2024-07-15T02:47:06.0653335' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1127, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'tẹo đi chơi không', 1006, CAST(N'2024-07-15T02:47:51.7681175' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1128, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'ok luôn', 1006, CAST(N'2024-07-15T02:48:03.4972913' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1129, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'ok', 1006, CAST(N'2024-07-15T02:50:45.8813376' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1130, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'chào cậu', 1004, CAST(N'2024-07-15T02:51:42.0977346' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1131, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'hello', 1004, CAST(N'2024-07-15T03:06:28.4788807' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1132, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'hello', 1003, CAST(N'2024-07-15T03:07:31.4592214' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1133, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'😃😃', 1004, CAST(N'2024-07-15T03:08:54.9147741' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1134, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'👍', 1006, CAST(N'2024-07-15T03:09:16.1914677' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1135, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N':))', 1003, CAST(N'2024-07-15T03:10:09.7306690' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1136, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'ok', 1006, CAST(N'2024-07-15T03:11:56.6167675' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1137, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'😧', 1006, CAST(N'2024-07-15T23:56:20.7461012' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1138, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'👍', 1006, CAST(N'2024-07-15T23:56:51.9373466' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1142, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'chào cậu', 1009, CAST(N'2024-07-16T00:58:38.1420541' AS DateTime2))
INSERT [dbo].[Messages] ([Id], [SenderId], [Content], [ConversationId], [Created]) VALUES (1143, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'chào', 1010, CAST(N'2024-07-16T01:06:25.9018169' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 

INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (28, N'Đã tương tác với bài viết của bạn', 1, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-03T20:39:32.2499668' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (29, N'Đã tương tác với bài viết của bạn', 1, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-04T17:48:15.8261110' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (30, N'Đã tương tác với bài viết của bạn', 1, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-09T05:17:56.0300578' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (31, N'Nguyễn Quang Anh xin tham gia vào nhóm ASP.NET Core Việt Nam', NULL, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-09T05:42:07.7962261' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (32, N'Đinh Thành Luân đã chấp thuận lời mời tham gia của bạn', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-09T19:15:36.3928835' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (33, N'Đinh Văn Anh đã chấp thuận lời mời kết bạn của bạn', NULL, 1, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-10T02:13:25.9786095' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (34, N'Vừa đăng 1 bài viết', NULL, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-10T20:34:16.4094742' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (35, N'Đã tương tác với bài viết của bạn', 8, 1, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-10T20:37:14.5927652' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (36, N'Đinh Thành Luân đã chấp thuận lời mời kết bạn của bạn', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', CAST(N'2024-07-11T21:06:49.7273545' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (37, N'Lê Thị Hoà đã chấp thuận lời mời kết bạn của bạn', NULL, 0, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', CAST(N'2024-07-11T22:23:13.5523627' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (38, N'Nguyễn Quang Anh đã chấp thuận lời mời kết bạn của bạn', NULL, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-11T22:50:01.2050897' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (39, N'Đã tương tác với bài viết của bạn', 8, 1, N'5934638f-0911-46b3-aff0-7469bb058fe5', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-14T00:01:06.1962179' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (40, N'Đinh Văn Phúc xin tham gia vào nhóm ASP.NET Core Việt Nam', NULL, 1, N'5934638f-0911-46b3-aff0-7469bb058fe5', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-14T00:48:54.8122382' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (41, N'Đinh Thành Luân đã chấp thuận lời mời tham gia của bạn', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-14T00:52:24.8408008' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (42, N'Đinh Thành Luân xin tham gia vào nhóm Lập trình hướng đối tượng OOP', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-14T01:51:54.1054671' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (43, N'Đinh Văn Phúc đã chấp thuận lời mời tham gia của bạn', NULL, 1, N'5934638f-0911-46b3-aff0-7469bb058fe5', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-14T01:52:46.5106817' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (44, N'Đinh Thành Luân đã chấp thuận lời mời kết bạn của bạn', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-14T02:11:28.9091954' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (45, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-14T19:56:21.6153846' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (46, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-14T19:56:21.4434416' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (47, N'Vừa đăng 1 bài viết', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-14T19:56:21.6105057' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (48, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-14T22:07:22.0188393' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (49, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-14T22:07:22.1486628' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (50, N'Vừa đăng 1 bài viết', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-14T22:07:22.1428267' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (51, N'Đinh Văn Anh xin tham gia vào nhóm ASP.NET Core Việt Nam', NULL, 1, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-14T22:11:47.3081804' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (52, N'Đinh Thành Luân đã chấp thuận lời mời tham gia của bạn', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-14T22:17:48.2342345' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (53, N'Đã tương tác với bài viết của bạn', 10, 1, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-14T22:18:20.5927613' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (54, N'Đinh Thành Luân đã chấp thuận lời mời kết bạn của bạn', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-16T01:05:31.9238723' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (55, N'Đã tương tác với bài viết của bạn', 10, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-16T18:29:39.0686303' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (56, N'Đã tương tác với bài viết của bạn', 8, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-16T18:31:00.3079996' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (57, N'Đã tương tác với bài viết của bạn', 10, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-16T18:53:43.1506447' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (58, N'Đã tương tác với bài viết của bạn', 6, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-16T18:53:49.9675360' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (59, N'Đã tương tác với bài viết của bạn', 8, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-16T18:54:15.0133072' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (60, N'Đã tương tác với bài viết của bạn', 10, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-16T18:55:03.4781890' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (61, N'Đã tương tác với bài viết của bạn', 8, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-16T19:01:07.6385406' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (62, N'Đã tương tác với bài viết của bạn', 8, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-16T19:01:36.6399115' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (63, N'Đã tương tác với bài viết của bạn', 8, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-16T19:32:50.3534117' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (64, N'Đã tương tác với bài viết của bạn', 8, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-17T13:20:28.2988389' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (65, N'Đã tương tác với bài viết của bạn', 10, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-17T13:33:34.3775395' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (66, N'Đã tương tác với bài viết của bạn', 5, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-17T13:45:33.9491229' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (67, N'Vừa đăng 1 bài viết', NULL, 0, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'7bcfc07f-a837-43e1-9de9-68db9dea5a65', CAST(N'2024-07-17T14:48:54.5477030' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (68, N'Vừa đăng 1 bài viết', NULL, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-17T14:48:54.6787101' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (69, N'Đã tương tác với bài viết của bạn', 10, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-17T14:57:13.0069054' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (70, N'Đã tương tác với bài viết của bạn', 10, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-17T14:58:41.8606382' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (71, N'Lê Thị Hoà xin tham gia vào nhóm ASP.NET Core Việt Nam', NULL, 1, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-17T15:32:13.9970433' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (72, N'Đinh Thành Luân đã chấp thuận lời mời tham gia của bạn', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-17T15:33:24.1115256' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (73, N'Vừa đăng 1 bài viết', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-17T16:11:43.0868777' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (74, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-17T16:11:43.0752037' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (75, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-17T16:11:43.0807075' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (76, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-17T16:11:42.9052243' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (77, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-17T16:13:56.0278614' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (78, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'04894db7-b48e-40ee-a64a-cc234bbf307c', CAST(N'2024-07-17T16:13:56.0625242' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (79, N'Vừa đăng 1 bài viết', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-17T16:13:56.0820865' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (80, N'Vừa đăng 1 bài viết', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'5934638f-0911-46b3-aff0-7469bb058fe5', CAST(N'2024-07-17T16:13:56.0791567' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (81, N'Đã tương tác với bài viết của bạn', 11, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-23T06:39:00.4389754' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (82, N'Đã tương tác với bài viết của bạn', 11, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-23T06:39:51.4977931' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (83, N'Đã tương tác với bài viết của bạn', 11, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-23T06:40:09.5591961' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (84, N'Đã tương tác với bài viết của bạn', 13, 1, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-23T06:40:22.8630121' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (85, N'Đã tương tác với bài viết của bạn', 11, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-23T06:41:45.2359402' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (86, N'Đã tương tác với bài viết của bạn', 11, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-23T06:42:24.6241430' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (87, N'Đã phản hồi bình luận của bạn', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-25T22:37:32.3209876' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (88, N'Đã phản hồi bình luận của bạn', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-25T22:37:40.1185287' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (89, N'Đã phản hồi bình luận của bạn', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-25T22:37:43.2731837' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (90, N'Đã phản hồi bình luận của bạn', NULL, 0, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-25T22:44:38.6891906' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (91, N'Đã phản hồi bình luận của bạn', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-25T22:45:36.7804992' AS DateTime2))
INSERT [dbo].[Notifications] ([Id], [Content], [PostId], [IsSeen], [SenderId], [ReceiverId], [Created]) VALUES (92, N'Đã phản hồi bình luận của bạn', NULL, 1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-25T22:45:41.1281664' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (1, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Xin chào', CAST(N'2024-06-17T23:10:12.9738475' AS DateTime2), NULL)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (4, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'Xin chào mọi người, mình là Luân, mong mọi người giúp đỡ', CAST(N'2024-06-21T22:51:14.5829105' AS DateTime2), NULL)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (5, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Xin chào mọi người', CAST(N'2024-06-22T21:34:29.0645827' AS DateTime2), NULL)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (6, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Nay mình thay ảnh đại diện mới :))', CAST(N'2024-06-23T19:16:51.2249640' AS DateTime2), NULL)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (7, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'Xin chào cả nhà', CAST(N'2024-06-30T17:35:38.7752586' AS DateTime2), NULL)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (8, N'04894db7-b48e-40ee-a64a-cc234bbf307c', N'Ảnh tốt nghiệp đại học của mình:))', CAST(N'2024-07-10T20:34:16.1250920' AS DateTime2), NULL)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (10, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Xin chào tất cả mọi người, mình tạo group này mong anh em có những thắc mắc gì trong học tập có thể trao đổi với nhau', CAST(N'2024-07-14T22:07:21.9636617' AS DateTime2), 3)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (11, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Thời tiết hôm nay thật đẹp', CAST(N'2024-07-17T14:48:54.3251634' AS DateTime2), NULL)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (12, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Đoạn code này của mình có bị sai không ạ', CAST(N'2024-07-17T16:11:42.6853818' AS DateTime2), 4)
INSERT [dbo].[Posts] ([Id], [UserId], [Content], [Created], [GroupId]) VALUES (13, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Chào các thành viên mới của nhóm nhé', CAST(N'2024-07-17T16:13:56.0088373' AS DateTime2), 3)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[ReplyComments] ON 

INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (1, 16, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Không có gì', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (4, 19, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Chào sếp:))', N'd3e0c93a-699d-4e62-a482-25cc5717cd55', CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (8, 19, N'2138262c-61b0-4a31-9719-290c0f87fe58', N'Sếp dạo này thế nào', N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', CAST(N'2024-07-25T02:06:00.0000000' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (9, 20, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Cảm ơn', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (10, 14, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'Đẹp đấy cậu', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-25T22:37:32.1725158' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (11, 14, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'Đẹp đấy cậu', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-25T22:37:40.1164936' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (12, 14, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'Đẹp đấy cậu', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-25T22:37:43.2721264' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (13, 19, N'd728e210-630d-4191-8b0a-bec9ea3aaeb2', N'ổn nhé', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-25T22:44:38.6858996' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (14, 20, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Chúc cậu cũng 1 ngày mới vui vẻ', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-25T22:45:36.7775269' AS DateTime2))
INSERT [dbo].[ReplyComments] ([Id], [CommentId], [CommenterId], [Content], [ResponderId], [Created]) VALUES (15, 19, N'd3e0c93a-699d-4e62-a482-25cc5717cd55', N'Chào', N'2138262c-61b0-4a31-9719-290c0f87fe58', CAST(N'2024-07-25T22:45:41.1250112' AS DateTime2))
SET IDENTITY_INSERT [dbo].[ReplyComments] OFF
GO
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded', 17, NULL)
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2024-07-10', 1, CAST(N'2024-08-10T13:34:17.207' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2024-07-14', 6, CAST(N'2024-08-14T15:07:22.383' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2024-07-17', 10, CAST(N'2024-08-17T09:13:56.120' AS DateTime))
GO
INSERT [HangFire].[Schema] ([Version]) VALUES (9)
GO
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'admin:14716:ec22d2d7-477c-4f9f-a531-ea2b55d661fd', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2024-07-27T13:57:49.3127002Z"}', CAST(N'2024-07-27T15:01:51.440' AS DateTime))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_PostId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_PostId] ON [dbo].[Comments]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Comments_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ConversationMember_ConversationId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_ConversationMember_ConversationId] ON [dbo].[ConversationMember]
(
	[ConversationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ConversationMember_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_ConversationMember_UserId] ON [dbo].[ConversationMember]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Friendships_ReceiverId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Friendships_ReceiverId] ON [dbo].[Friendships]
(
	[ReceiverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Friendships_SenderId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Friendships_SenderId] ON [dbo].[Friendships]
(
	[SenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GroupMembers_GroupId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_GroupMembers_GroupId] ON [dbo].[GroupMembers]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_GroupMembers_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_GroupMembers_UserId] ON [dbo].[GroupMembers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GroupMessage_GroupId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_GroupMessage_GroupId] ON [dbo].[GroupMessage]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_GroupMessage_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_GroupMessage_UserId] ON [dbo].[GroupMessage]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GroupMessageRecipients_GroupMessageId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_GroupMessageRecipients_GroupMessageId] ON [dbo].[GroupMessageRecipients]
(
	[GroupMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_GroupMessageRecipients_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_GroupMessageRecipients_UserId] ON [dbo].[GroupMessageRecipients]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ImagePost_PostId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_ImagePost_PostId] ON [dbo].[ImagePost]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_InforUsers_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_InforUsers_UserId] ON [dbo].[InforUsers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Interactions_PostId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Interactions_PostId] ON [dbo].[Interactions]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Interactions_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Interactions_UserId] ON [dbo].[Interactions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageReadStatuses_MessageId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_MessageReadStatuses_MessageId] ON [dbo].[MessageReadStatuses]
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_MessageReadStatuses_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_MessageReadStatuses_UserId] ON [dbo].[MessageReadStatuses]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Messages_ConversationId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Messages_ConversationId] ON [dbo].[Messages]
(
	[ConversationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Messages_SenderId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Messages_SenderId] ON [dbo].[Messages]
(
	[SenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notifications_PostId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_PostId] ON [dbo].[Notifications]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Notifications_ReceiverId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_ReceiverId] ON [dbo].[Notifications]
(
	[ReceiverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Notifications_SenderId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_SenderId] ON [dbo].[Notifications]
(
	[SenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Posts_GroupId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Posts_GroupId] ON [dbo].[Posts]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Posts_UserId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Posts_UserId] ON [dbo].[Posts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReplyComments_CommenterId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReplyComments_CommenterId] ON [dbo].[ReplyComments]
(
	[CommenterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReplyComments_CommentId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReplyComments_CommentId] ON [dbo].[ReplyComments]
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReplyComments_ResponderId]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReplyComments_ResponderId] ON [dbo].[ReplyComments]
(
	[ResponderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_AggregatedCounter_ExpireAt]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_AggregatedCounter_ExpireAt] ON [HangFire].[AggregatedCounter]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Hash_ExpireAt]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Hash_ExpireAt] ON [HangFire].[Hash]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Job_ExpireAt]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_ExpireAt] ON [HangFire].[Job]
(
	[ExpireAt] ASC
)
INCLUDE([StateName]) 
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Job_StateName]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_StateName] ON [HangFire].[Job]
(
	[StateName] ASC
)
WHERE ([StateName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_List_ExpireAt]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_List_ExpireAt] ON [HangFire].[List]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Server_LastHeartbeat]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Server_LastHeartbeat] ON [HangFire].[Server]
(
	[LastHeartbeat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Set_ExpireAt]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_ExpireAt] ON [HangFire].[Set]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Set_Score]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_Score] ON [HangFire].[Set]
(
	[Key] ASC,
	[Score] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_State_CreatedAt]    Script Date: 31/7/2024 10:47:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_State_CreatedAt] ON [HangFire].[State]
(
	[CreatedAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ((0)) FOR [Gender]
GO
ALTER TABLE [dbo].[GroupMembers] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[GroupMembers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsCreate]
GO
ALTER TABLE [dbo].[Interactions] ADD  DEFAULT ((0)) FOR [Emoji]
GO
ALTER TABLE [dbo].[Messages] ADD  DEFAULT ((0)) FOR [ConversationId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Posts_PostId]
GO
ALTER TABLE [dbo].[ConversationMember]  WITH CHECK ADD  CONSTRAINT [FK_ConversationMember_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConversationMember] CHECK CONSTRAINT [FK_ConversationMember_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ConversationMember]  WITH CHECK ADD  CONSTRAINT [FK_ConversationMember_Conversations_ConversationId] FOREIGN KEY([ConversationId])
REFERENCES [dbo].[Conversations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConversationMember] CHECK CONSTRAINT [FK_ConversationMember_Conversations_ConversationId]
GO
ALTER TABLE [dbo].[Friendships]  WITH CHECK ADD  CONSTRAINT [FK_Friendships_AspNetUsers_ReceiverId] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Friendships] CHECK CONSTRAINT [FK_Friendships_AspNetUsers_ReceiverId]
GO
ALTER TABLE [dbo].[Friendships]  WITH CHECK ADD  CONSTRAINT [FK_Friendships_AspNetUsers_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Friendships] CHECK CONSTRAINT [FK_Friendships_AspNetUsers_SenderId]
GO
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Groups_GroupId]
GO
ALTER TABLE [dbo].[GroupMessage]  WITH CHECK ADD  CONSTRAINT [FK_GroupMessage_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupMessage] CHECK CONSTRAINT [FK_GroupMessage_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[GroupMessage]  WITH CHECK ADD  CONSTRAINT [FK_GroupMessage_Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupMessage] CHECK CONSTRAINT [FK_GroupMessage_Groups_GroupId]
GO
ALTER TABLE [dbo].[GroupMessageRecipients]  WITH CHECK ADD  CONSTRAINT [FK_GroupMessageRecipients_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[GroupMessageRecipients] CHECK CONSTRAINT [FK_GroupMessageRecipients_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[GroupMessageRecipients]  WITH CHECK ADD  CONSTRAINT [FK_GroupMessageRecipients_GroupMessage_GroupMessageId] FOREIGN KEY([GroupMessageId])
REFERENCES [dbo].[GroupMessage] ([Id])
GO
ALTER TABLE [dbo].[GroupMessageRecipients] CHECK CONSTRAINT [FK_GroupMessageRecipients_GroupMessage_GroupMessageId]
GO
ALTER TABLE [dbo].[ImagePost]  WITH CHECK ADD  CONSTRAINT [FK_ImagePost_Posts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImagePost] CHECK CONSTRAINT [FK_ImagePost_Posts_PostId]
GO
ALTER TABLE [dbo].[InforUsers]  WITH CHECK ADD  CONSTRAINT [FK_InforUsers_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InforUsers] CHECK CONSTRAINT [FK_InforUsers_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Interactions]  WITH CHECK ADD  CONSTRAINT [FK_Interactions_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Interactions] CHECK CONSTRAINT [FK_Interactions_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Interactions]  WITH CHECK ADD  CONSTRAINT [FK_Interactions_Posts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Interactions] CHECK CONSTRAINT [FK_Interactions_Posts_PostId]
GO
ALTER TABLE [dbo].[MessageReadStatuses]  WITH CHECK ADD  CONSTRAINT [FK_MessageReadStatuses_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageReadStatuses] CHECK CONSTRAINT [FK_MessageReadStatuses_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[MessageReadStatuses]  WITH CHECK ADD  CONSTRAINT [FK_MessageReadStatuses_Messages_MessageId] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageReadStatuses] CHECK CONSTRAINT [FK_MessageReadStatuses_Messages_MessageId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_AspNetUsers_SenderId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Conversations_ConversationId] FOREIGN KEY([ConversationId])
REFERENCES [dbo].[Conversations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Conversations_ConversationId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_AspNetUsers_ReceiverId] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_AspNetUsers_ReceiverId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_AspNetUsers_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_AspNetUsers_SenderId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Posts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Posts_PostId]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Groups_GroupId]
GO
ALTER TABLE [dbo].[ReplyComments]  WITH CHECK ADD  CONSTRAINT [FK_ReplyComments_AspNetUsers_CommenterId] FOREIGN KEY([CommenterId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ReplyComments] CHECK CONSTRAINT [FK_ReplyComments_AspNetUsers_CommenterId]
GO
ALTER TABLE [dbo].[ReplyComments]  WITH CHECK ADD  CONSTRAINT [FK_ReplyComments_AspNetUsers_ResponderId] FOREIGN KEY([ResponderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ReplyComments] CHECK CONSTRAINT [FK_ReplyComments_AspNetUsers_ResponderId]
GO
ALTER TABLE [dbo].[ReplyComments]  WITH CHECK ADD  CONSTRAINT [FK_ReplyComments_Comments_CommentId] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comments] ([Id])
GO
ALTER TABLE [dbo].[ReplyComments] CHECK CONSTRAINT [FK_ReplyComments_Comments_CommentId]
GO
ALTER TABLE [HangFire].[JobParameter]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
ALTER TABLE [HangFire].[State]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
USE [master]
GO
ALTER DATABASE [PitNik] SET  READ_WRITE 
GO
