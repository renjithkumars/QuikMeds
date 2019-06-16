USE [CTX]
GO

/****** Object:  Table [dbo].[ShoppingCartData]    Script Date: 6/16/2019 7:05:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShoppingCartData](
	[TempOrderID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[PName] [varchar](50) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ShoppingCartData] PRIMARY KEY CLUSTERED 
(
	[TempOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


