IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'sp_GetAccountingReport')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE sp_GetAccountingReport
GO

IF TYPE_ID(N'ParameterIds') IS NOT NULL
DROP TYPE ParameterIds
GO

CREATE TYPE ParameterIds AS Table (
        ParameterId BIGINT
);
GO

CREATE PROCEDURE dbo.sp_GetAccountingReport( @ParameterIds AS ParameterIds READONLY )
AS
BEGIN
	DECLARE @Query nvarchar(max) = 'SELECT DISTINCT dbo.Items.Id, Number, Name, Price'
	DECLARE @SubQuery nvarchar(1000) = ''
	
	DECLARE @ParameterId int

	DECLARE MY_CURSOR CURSOR 
	  LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR 
	SELECT DISTINCT ParameterId 
	FROM @ParameterIds
	
	OPEN MY_CURSOR
	FETCH NEXT FROM MY_CURSOR INTO @ParameterId
	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE @StringParameterId NVARCHAR(max) = CAST(@ParameterId AS NVARCHAR(max))
		SET @Query += ', i' + @StringParameterId
		SET @SubQuery += 'LEFT OUTER JOIN (SELECT ItemId, Value as ''i' + @StringParameterId + '''
						  FROM dbo.ParameterValues WHERE ParameterId = ' + @StringParameterId + ')
							AS Pv' + @StringParameterId + ' ON Pv' + @StringParameterId + '.ItemId = dbo.Items.Id '
		FETCH NEXT FROM MY_CURSOR INTO @ParameterId
	END
	CLOSE MY_CURSOR
	DEALLOCATE MY_CURSOR

	SET @Query += ' FROM dbo.Items ' + @SubQuery
	PRINT @Query
	EXEC(@Query)
END
GO