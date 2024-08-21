--------------------------- Mohamed Hamed Start 04-08-2021 -------------------------------

CREATE TABLE ContactUs(
 ID INT PRIMARY key IDENTITY (1,1),
 PhoneNumber nvarchar(20) null,
 MobileNumber nvarchar(20) null,
 Email nvarchar(100) NULL,
 TitleAr nvarchar(200) NULL,
 TitleEn nvarchar(200) NULL,
 GoogleURL nvarchar(200) NULL,
 FacebookUrl nvarchar(100) null,
 GooglePlus  nvarchar(100) null,
 InstagramUrl nvarchar(100) null,
 YoutubeUrl nvarchar(100) null,
 LinkedinUrl nvarchar(100) null,
 TwitterUrl nvarchar(100) null,
 Latitude float NULL, 
 Longitude float NULL, 
)
GO


CREATE TABLE LKMenuCatContent(
 ID INT PRIMARY key IDENTITY (1,1),
 [TitleAr] [nvarchar](200) NULL,
[TitleEn] [nvarchar](200) NULL,
[ContentAr] [nvarchar](max) NULL,
[ContentEn] [nvarchar](max) NULL,
MenuCatId int Null
)
GO


CREATE TABLE [dbo].[Projects](
	[ProjectId] [BIGINT] IDENTITY(1,1) NOT NULL primary key,
	[ProjectTitleAr] [nvarchar](200) NULL,
	[ProjectTitleEn] [nvarchar](200) NULL,
	[ContentAr] [nvarchar](max) NULL,
	[ContentEn] [nvarchar](max) NULL
)
GO

CREATE TABLE [dbo].[Sliders](
	[SliderId] [int] IDENTITY(1,1) NOT NULL primary key,
	[SliderTitleAr] [nvarchar](200) NULL,
	[SliderTitleEn] [nvarchar](200) NULL,
	[ContentAr] [nvarchar](max) NULL,
	[ContentEn] [nvarchar](max) NULL,
	[MainSlider] bit not null default 1
)
GO

--------------------------- Mohamed Hamed Start 04-08-2021 -------------------------------