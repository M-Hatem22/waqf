CREATE TABLE LkGovernance(
LkGovernanceId INT PRIMARY KEY IDENTITY(1,1),
LkGovernanceNameAr NVARCHAR(200),
LkGovernanceNameEn NVARCHAR(200) 
);
GO

INSERT INTO LkGovernance
VALUES('اللوائح','Regulations'),('السياسيات','Politics'),('الخطة الاستراتيجية والتشغيلية','Strategic and Operational Plan'),
('محاضر الجمعية العمومية','Lectures of the General Assembly'),('محاضر اجتماعات مجلس الادارة','Lectures of the Board of Directors'),('الاستبانات','Calls'),
('شواهد الموقع الإلكتروني','Evidence of the website'),('المراسلات الرسمية','Official Correspondence'),
('قوائم اعضاء مجلس الادارة والجمعية العمومية واللجان والموظفين','Lists of Members of The Board of Directors, The General Assembly The Committees and Employees'),
('أخرى','Others')
GO

CREATE TABLE GovernanceAttachments(
GovernanceAttachmentsId BIGINT PRIMARY KEY IDENTITY(1,1),
LkGovernanceId INT,
LkGovernanceNameAr NVARCHAR(200),
GovernanceAttachmentsNameAr NVARCHAR(200),
GovernanceAttachmentsNameEn NVARCHAR(200),
AttachmentFile IMAGE  ,
AttachmentContent NVARCHAR(500),
AttachmentName NVARCHAR(500),
UploadedDate DATETIME,
Deleted DATETIME
);
GO