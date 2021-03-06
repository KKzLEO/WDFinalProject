USE [WebFinalProject]
GO
/****** Object:  StoredProcedure [dbo].[sp_create_course]    Script Date: 2019/06/24/星期一 下午 07:57:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_create_course]
    @CourseName varchar(100),   
    @CategoryCode varchar(5),
	@Desc varchar(300),
	@CreateUser varchar(20),
	@Price int,
	@CourseImageName varchar(100),
	@TtlHr int,
	@LearningText varchar(1000),
	@RequirementText varchar(1000),
	@InsText varchar(1000),
	@TargetText varchar(1000),
	@ShortIntroText varchar(1000),
	@TeacherPerSerilNo varchar(30)
AS   
	declare @Id varchar(10)
	INSERT INTO [dbo].[COURSE]
                ([COURSE_NAME]
                ,[CATEGORY_CODE]
                ,[START_DATE]
                ,[END_DATE]
                ,[VER]
                ,[DESCRIPTION]
                ,[CRE_DATE]
                ,[CRE_USR]
                ,[MOD_DATE]
                ,[MOD_USR]
                ,[PRICE]
                ,[COURSE_IMAGE_NAME]
                ,[TTL_HR]
                ,[LEARNING_TEXT]
                ,[REQUIREMENT_TEXT]
                ,[INS_TEXT]
                ,[TARGET_TEXT]
                ,[SHORT_INTRO_TEXT])
            VALUES
                (
		        @CourseName,
		        @CategoryCode,
		        GETDATE(),
		        GETDATE(),
		        '0',
		        @Desc,
		        GETDATE(),
		        @CreateUser,
		        GETDATE(),
		        @CreateUser,
		        @Price,
		        @CourseImageName,
		        @TtlHr,
		        @LearningText,
		        @RequirementText,
		        @InsText,
		        @TargetText,
		        @ShortIntroText)
	SELECT @Id = @@Identity 
	INSERT INTO [dbo].[TEACHERS]
			   ([COURSE_SERIL_NO]
			   ,[PER_SERIL_NO])
		 VALUES
			   (
					@Id,
					@TeacherPerSerilNo
			   )


	SELECT D.CATEGORY_CODE AS CategoryCode
	        ,D.CATEGORY_NAME AS CategoryName
	        ,B.COURSE_SERIL_NO AS CourseSerilNo
	        ,A.COURSE_NAME AS CourseName
	        ,A.START_DATE AS StartDate
	        ,A.END_DATE AS EndDate
	        ,A.DESCRIPTION AS Description
	        ,B.PER_SERIL_NO AS TeacherPerSerilNo
	        ,C.ACCOUNT AS TeacherAccount
	        ,C.EMAIL AS TeacherEmail
	        ,C.E_NAME AS TeacherEName
	        ,C.C_NAME AS TeacherCName
            ,A.PRICE AS Price
            ,A.COURSE_IMAGE_NAME AS CourseImageName
            ,A.CRE_DATE AS CreDate
            ,A.CRE_USR AS CreUsr
            ,A.MOD_DATE AS ModDate
            ,A.MOD_USR AS ModUsr
            ,A.TTL_HR AS TtlHr
            ,A.LEARNING_TEXT AS LearningText
            ,A.REQUIREMENT_TEXT AS RequirementText
            ,A.INS_TEXT AS InsText
            ,A.TARGET_TEXT AS TargetText
            ,A.SHORT_INTRO_TEXT AS ShortIntroText
    FROM COURSE AS A JOIN TEACHERS AS B ON A.COURSE_SERIL_NO = B.COURSE_SERIL_NO
    JOIN USERS AS C ON B.PER_SERIL_NO = C.PER_SERIL_NO
    JOIN COURSE_CATEGORY_CODE AS D ON A.CATEGORY_CODE = D.CATEGORY_CODE
    WHERE 1=1
