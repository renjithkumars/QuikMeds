USE [CTX]
GO

/****** Object:  Table [dbo].[Customers]    Script Date: 6/16/2019 7:04:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers](
	[CID] [int] IDENTITY(1,1) NOT NULL,
	[FName] [varchar](50) NOT NULL,
	[LName] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Address1] [varchar](50) NOT NULL,
	[Address2] [varchar](50) NULL,
	[Suburb] [varchar](50) NOT NULL,
	[Postcode] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Ctype] [varchar](50) NOT NULL,
	[CardNo] [varchar](50) NOT NULL,
	[ExpDate] [datetime] NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


