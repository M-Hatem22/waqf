CREATE TABLE LKMenuCatContent(
 ID INT PRIMARY key IDENTITY (1,1),
 [TitleAr] [nvarchar](200) NULL,
[TitleEn] [nvarchar](200) NULL,
[ContentAr] [nvarchar](max) NULL,
[ContentEn] [nvarchar](max) NULL,
MenuCatId int Null
)