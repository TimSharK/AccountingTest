INSERT INTO dbo.Items
VALUES (1, 'Sony VAIO (SVF1521D1RB)(HD)', 20990)

DECLARE @ItemId1 BIGINT = SCOPE_IDENTITY()

INSERT INTO dbo.Items
VALUES(2, 'Sony VAIO (SVF1532P1RB)(HD)', 27990)

DECLARE @ItemId2 BIGINT = SCOPE_IDENTITY()

INSERT INTO dbo.Parameters
VALUES ('IsCool', 0, 1)

DECLARE @ParameterId1 BIGINT = SCOPE_IDENTITY()

INSERT INTO dbo.ParameterValues
VALUES (NEWID(), '0', @ItemId1, @ParameterId1),
	   (NEWID(), '1', @ItemId2, @ParameterId1)

INSERT INTO dbo.Parameters
VALUES ('IsNotebook', 0, 1)

DECLARE @ParameterId2 BIGINT = SCOPE_IDENTITY()

INSERT INTO dbo.ParameterValues
VALUES (NEWID(), '1', @ItemId1, @ParameterId2),
	   (NEWID(), '1', @ItemId2, @ParameterId2)

INSERT INTO dbo.Parameters
VALUES ('Date', 1, 0)

DECLARE @ParameterId3 BIGINT = SCOPE_IDENTITY()

INSERT INTO dbo.ParameterValues
VALUES (NEWID(), '21.06.2014', @ItemId1, @ParameterId3)