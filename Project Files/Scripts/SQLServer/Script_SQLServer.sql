
USE DATABASE_NAME;

/****** Object:  Table [dbo].[ImagesStorage]    Script Date: 11/12/2018 01:20:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ImagesStorage](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[ImagePath] [varchar](max) NOT NULL,
	[ImageName] [varchar](100) NOT NULL,
	[ImageFileExtension] [varchar](20) NOT NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_ImagesStorage] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



/****** Object:  Table [dbo].[GeoCompanyImage]    Script Date: 11/12/2018 01:26:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GeoCompanyImage](
	[CompanyImageID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[ImageSizeMB] [int] NOT NULL,
	[ImageExtension] [varchar](20) NOT NULL,
	[ImageUrl] [varchar](max) NOT NULL,
	[Lat] [varchar](30) NULL,
	[Lng] [varchar](30) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_GeoCompanyImage] PRIMARY KEY CLUSTERED 
(
	[CompanyImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

