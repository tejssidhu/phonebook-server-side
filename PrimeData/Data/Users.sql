SET NOCOUNT ON
 
MERGE INTO [User] AS Target
USING (
		VALUES  
		  ('7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf', 'User123')
		,('5875412f-e8b8-493e-bd58-5df35083342c', 'User456')
		,('26e31dde-4bcb-47d4-be80-958676c5cafd', 'User789')
		,('cef70a7a-3349-4368-85ed-66b8c274fad1', 'mjenkins0')
		,('71d8e924-7c58-4424-9e1b-b14eefa76abc', 'amartin1')
		,('a051d1ca-a3c5-45d4-be60-5bc5256ce83e', 'vallen2')
		,('2b3b4d72-1c15-40e0-a05a-012b724950c3', 'mblack3')
		,('2550f510-e5c9-45a4-90a0-c286e4bcd948', 'schapman4')
		,('874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'gdiaz5')
		,('16c6e264-0091-45f6-b9fd-02716d8d62dd', 'cwheeler6')
		,('0d1a6711-e9eb-418e-adda-47a62a7900c9', 'bparker7')

) AS Source ([Id],[Username])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Username] <> Source.[Username]) THEN
 UPDATE SET [Username] = Source.[Username]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([Id],[Username])
 VALUES(Source.[Id],Source.[Username])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int, @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [User]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[User] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET NOCOUNT OFF
GO
