USE [WebFinalProject]
GO
/****** Object:  StoredProcedure [dbo].[sp_remove_user]    Script Date: 2019/06/24/星期一 下午 07:57:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_remove_user]
    @PerSerilNo varchar(50)
AS
	IF @PerSerilNo = ''
	BEGIN
		SELECT '系統錯誤' AS [Message]
			    ,'fail' AS [Status]
		RETURN
	END


	DECLARE @CourseNumber int 
	SELECT @CourseNumber = Count(*)
	FROM TEACHERS AS T
	WHERE T.PER_SERIL_NO = @PerSerilNo

	IF @CourseNumber > 0
	BEGIN
		SELECT '無法刪除，該老師有課程' AS [Message]
			    ,'fail' AS [Status]
		RETURN
	END

	IF (SELECT COUNT(*) FROM USERS
	WHERE USERS.PER_SERIL_NO = @PerSerilNo AND TITLE_CODE = '0') > 0
	BEGIN
		SELECT '無法刪除系統管理員' AS [Message]
				,'fail' AS [Status]
		RETURN
	END

	DELETE FROM USERS WHERE USERS.PER_SERIL_NO = @PerSerilNo
	IF @@ROWCOUNT = 0
	BEGIN
		SELECT '刪除失敗，無此使用者' AS [Message]
				,'fail' AS [Status]
		RETURN
	END
	ELSE
	BEGIN
		SELECT '刪除成功' AS [Message]
				,'success' AS [Status]
		RETURN
	END
	