SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND type in (N'U'))

BEGIN
CREATE TABLE [dbo].[Inventory] (
	Id [int] IDENTITY(1,1) NOT NULL,
	[Name] varchar(50) NOT NULL,
	[Description] varchar(max)  NULL,
	PricePerUnit int  NULL,
	Quantity int null,
	TotalPrice int null,
	CreatedDate DateTime DEFAULT GETDATE(),
	LastModifiedDate DateTime DEFAULT GETDATE(),
	CONSTRAINT [PK_Inventory_ID] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END