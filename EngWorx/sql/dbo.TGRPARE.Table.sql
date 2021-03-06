USE [ENGWORXDB]
GO
/****** Object:  Table [dbo].[TGRPARE]    Script Date: 19/12/2014 11:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TGRPARE](
	[CODGRPARE] [nvarchar](3) NOT NULL,
	[DESGRPARE] [nvarchar](100) NULL,
	[CODUSRMOD] [nvarchar](8) NULL,
	[FLGMODTYP] [nchar](1) NULL,
	[TMSLSTMOD] [datetime] NULL,
 CONSTRAINT [PK_TGRPARE] PRIMARY KEY CLUSTERED 
(
	[CODGRPARE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[TGRPARE] ([CODGRPARE], [DESGRPARE], [CODUSRMOD], [FLGMODTYP], [TMSLSTMOD]) VALUES (N'000', N'Dummy', NULL, NULL, NULL)
