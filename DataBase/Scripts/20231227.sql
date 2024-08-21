UPDATE LkGovernance
SET LkGovernanceNameAr='اللوائح والسياسيات',LkGovernanceNameEn='Regulations And Politics' where LkGovernanceId=1
GO
UPDATE LkGovernance
SET LkGovernanceNameAr='اجتماعات الجمعية العمومية',LkGovernanceNameEn='General Assembly Meetings' where LkGovernanceId=2
GO

Update GovernanceAttachments
SET LkGovernanceNameAr='اللوائح والسياسيات' where LkGovernanceId=1
GO

Update GovernanceAttachments
SET LkGovernanceNameAr='اللوائح والسياسيات' ,LkGovernanceId=1 where LkGovernanceId=2
GO