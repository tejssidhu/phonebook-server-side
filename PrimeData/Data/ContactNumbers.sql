SET NOCOUNT ON
 
MERGE INTO [ContactNumber] AS Target
USING (
		VALUES  
		  ('9a005b3e-d9ec-4e08-aefa-589ab5e00bfa', '81c4763c-b225-4756-903a-750064167813', 'Mobile', '391714697203')
		,('368b5e82-a019-4a4f-8f66-4bb670e6b769', '81c4763c-b225-4756-903a-750064167813', 'Home', '297724563901')
		,('22b4f6e9-27c2-4636-b431-a37bdbc1b325', 'cc772bf2-40bd-4b25-9e3a-0e80b1a63383', 'Home', '864785278888')
		,('f5f162c8-33af-4ad2-b00d-1f618250401b', 'cc772bf2-40bd-4b25-9e3a-0e80b1a63383', 'Work', '44167868359')
		,('fda6127e-702b-4f32-9ec5-7f449deabf11', '6e7ca25f-d438-4076-b2bf-180fbffe809e', 'Home', '576800489823')
		,('9d656825-9033-4be4-adef-a0dd9ebeef70', '6e7ca25f-d438-4076-b2bf-180fbffe809e', 'Work', '985888887522')
		,('64aa5304-556f-4606-a118-03b43f46294b', '6e7ca25f-d438-4076-b2bf-180fbffe809e', 'Mobile', '188936967961')
		,('c94df269-db84-45a7-9b2a-47fffdbc3399', '94669c7c-02f3-41a7-a8af-e6a3cee307bc', 'Home', '427370908494')
		,('b74b006e-f985-47a6-932a-051e3437e31f', '94669c7c-02f3-41a7-a8af-e6a3cee307bc', 'Mobile', '165328050436')
		,('6222209a-ce18-4d88-a7a5-27c8bd0ef92f', '58c1eb1e-1513-4f19-97f3-d8571f97115f', 'Mobile', '381792056593')
		,('47f7f4e8-593c-4f6d-b1a5-1725e578ee86', '58c1eb1e-1513-4f19-97f3-d8571f97115f', 'Home', '632196085018')
		,('6ce58c67-b584-460f-8d5b-20cce37a4e90', 'e3ee2f2b-3ace-4fd4-8ca7-7d6960f7a9fb', 'Mobile', '924308316065')
		,('8e9e8fe1-f9bf-482c-b1bd-77c1e472e683', 'e3ee2f2b-3ace-4fd4-8ca7-7d6960f7a9fb', 'Home', '962474029779')
		,('6fcceb1d-f27c-401f-bd95-f3519be6d6d4', 'e3ee2f2b-3ace-4fd4-8ca7-7d6960f7a9fb', 'Work', '412115311830')
		,('d8fc029d-8062-4cc4-ac29-e4339d1b48d3', '2ae69661-72c6-4e33-a6ec-1ca93152fa80', 'Home', '962216940411')
		,('0be9339f-706d-4e34-9938-afc76c7e746f', '2ae69661-72c6-4e33-a6ec-1ca93152fa80', 'Mobile', '641533924552')

) AS Source ([Id],[ContactId],[Description],[TelephoneNumber])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[ContactId] <> Source.[ContactId] OR Target.[Description] <> Source.[Description] OR Target.[TelephoneNumber] <> Source.[TelephoneNumber]) THEN
 UPDATE SET [ContactId] = Source.[ContactId], [Description] = Source.[Description], [TelephoneNumber] = Source.[TelephoneNumber]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([Id],[ContactId],[Description],[TelephoneNumber])
 VALUES(Source.[Id],Source.[ContactId],Source.[Description],Source.[TelephoneNumber])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int, @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ContactNumber]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ContactNumber] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET NOCOUNT OFF
GO
