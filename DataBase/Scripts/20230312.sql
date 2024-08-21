TRUNCATE TABLE LkGovernance
GO
INSERT INTO LkGovernance
VALUES('اللوائح','Regulations'),('السياسيات','Politics'),('الأنظمة','Systems'),('النماذج','Models'),('المحاضر','The Lecturers'),
('اللجان','Committees'),('القوائم المالية','Financial Statements'),('التقارير','Reports'),('الاستبانات','Calls'),
('خطط الجمعية','Assembly Plans')
GO

update GovernanceAttachments
set LkGovernanceNameAr='خطط الجمعية' where LkGovernanceId=10
GO
update GovernanceAttachments
set LkGovernanceId=9 where LkGovernanceId=6
GO
update GovernanceAttachments
set LkGovernanceNameAr='الأنظمة' where LkGovernanceId=3
GO
update GovernanceAttachments
set LkGovernanceNameAr='النماذج' where LkGovernanceId=4
GO
update GovernanceAttachments
set LkGovernanceNameAr='المحاضر' where LkGovernanceId=5
GO
update GovernanceAttachments
set LkGovernanceNameAr='اللجان' where LkGovernanceId=6
GO
update GovernanceAttachments
set LkGovernanceNameAr='القوائم المالية' where LkGovernanceId=7
GO
update GovernanceAttachments
set LkGovernanceNameAr='التقارير' where LkGovernanceId=8
GO
