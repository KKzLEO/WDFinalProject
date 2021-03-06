USE [WebFinalProject]
GO
/****** Object:  StoredProcedure [dbo].[sp_user_register]    Script Date: 2019/06/24/星期一 下午 07:50:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_user_register]
    @Account varchar(50),   
    @Password varchar(50),
	@Email varchar(50),
	@TitleCode varchar(10),
	@UserId varchar(20),
	@EName varchar(20),
	@CName varchar(20),
	@FbId varchar(100),
	@GenderCode varchar(10),
	@Phone varchar(12),
	@Birthday datetime
AS   
	declare @count varchar(10)

	select @count = count(*) from USERS(NOLOCK) WHERE ACCOUNT = @Account 
				
	IF @count > 1
		select 'false'
	ELSE
		INSERT INTO [dbo].[USERS]
                               ([ACCOUNT]
                               ,[PASSWORD]
                               ,[EMAIL]
                               ,[TITLE_CODE]
                               ,[CRE_DATE]
                               ,[CRE_USR]
                               ,[MOD_DATE]
                               ,[MOD_USR]
                               ,[E_NAME]
                               ,[C_NAME]
		                       ,[USER_ID]
		                       ,[PER_SERIL_NO]
							   ,[FB_ID]
							   ,[GENDER_CODE]
							   ,[PHONE]
							   ,[BIRTHDAY])
                         VALUES
                               (
				                    @Account,
				                    @Password,
				                    @Email,
				                    @TitleCode,
				                    GETDATE(),
				                    @UserId,
				                    GETDATE(),
				                    @UserId,
				                    @EName,
				                    @CName,
				                    @UserId,
				                    'WD' + REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(varchar(100), GETDATE(), 21),'-',''),':',''),'.',''),' ',''),
									@FbId,
									@GenderCode,
									@Phone,
									@Birthday
		                       )
    SELECT 'true'
