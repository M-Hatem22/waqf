CREATE TABLE [dbo].[Projects](
	[ProjectId] [BIGINT] IDENTITY(1,1) NOT NULL primary key,
	[ProjectTitleAr] [nvarchar](200) NULL,
	[ProjectTitleEn] [nvarchar](200) NULL,
	[ContentAr] [nvarchar](max) NULL,
	[ContentEn] [nvarchar](max) NULL
	)
