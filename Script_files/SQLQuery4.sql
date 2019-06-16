USE [CTX]
GO

/****** Object:  Table [dbo].[Order_Products]    Script Date: 6/16/2019 7:04:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order_Products](
	[OrderID] [int] NOT NULL,
	[PID] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[TotalSale] [money] NOT NULL,
 CONSTRAINT [PK_Order_Products] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Order_Products]  WITH CHECK ADD  CONSTRAINT [FK_Order_Products_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Order_Products] CHECK CONSTRAINT [FK_Order_Products_Orders]
GO

ALTER TABLE [dbo].[Order_Products]  WITH CHECK ADD  CONSTRAINT [FK_Order_Products_Products] FOREIGN KEY([PID])
REFERENCES [dbo].[Products] ([PID])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Order_Products] CHECK CONSTRAINT [FK_Order_Products_Products]
GO


