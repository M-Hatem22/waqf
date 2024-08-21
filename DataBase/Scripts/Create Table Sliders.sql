CREATE TABLE [dbo].[Sliders](
	[SliderId] [int] IDENTITY(1,1) NOT NULL primary key,
	[SliderTitleAr] [nvarchar](200) NULL,
	[SliderTitleEn] [nvarchar](200) NULL,
	[ContentAr] [nvarchar](max) NULL,
	[ContentEn] [nvarchar](max) NULL,
	[MainSlider] bit not null default 1
	)