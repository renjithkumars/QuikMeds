USE [CTX]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 6/16/2019 7:03:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[PID] [int] IDENTITY(1,1) NOT NULL,
	[PName] [varchar](50) NOT NULL,
	[Brand] [varchar](50) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[UnitsInStock] [int] NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[SID] [int] NOT NULL,
	[ROL] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY([SID])
REFERENCES [dbo].[Suppliers] ([SID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Suppliers]
GO


