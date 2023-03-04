/****** Object:  Table [dbo].[OrderDetails]    Script Date: 3/4/2023 7:01:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] NOT NULL,
	[OrderDetailID] [smallint] NOT NULL,
	[ProductNumber] [varchar](50) NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[ProductOrigin] [varchar](50) NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO