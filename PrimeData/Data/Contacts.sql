SET NOCOUNT ON
 
MERGE INTO [Contact] AS Target
USING (
		VALUES  
		  ('81c4763c-b225-4756-903a-750064167813', '26e31dde-4bcb-47d4-be80-958676c5cafd', 'Theresa', 'Reyes', 'treyes0@goo.gl', 'Dr')
		,('cc772bf2-40bd-4b25-9e3a-0e80b1a63383', '26e31dde-4bcb-47d4-be80-958676c5cafd', 'Pamela', 'Wagner', 'pwagner1@ed.gov', 'Honorable')
		,('6e7ca25f-d438-4076-b2bf-180fbffe809e', '26e31dde-4bcb-47d4-be80-958676c5cafd', 'Steve', 'Tucker', 'stucker2@tuttocitta.it', 'Mrs')
		,('94669c7c-02f3-41a7-a8af-e6a3cee307bc', '7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf', 'Sean', 'Baker', 'sbaker3@noaa.gov', 'Honorable')
		,('58c1eb1e-1513-4f19-97f3-d8571f97115f', '7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf', 'Melissa', 'Tucker', 'mtucker4@yale.edu', 'Mrs')
		,('e3ee2f2b-3ace-4fd4-8ca7-7d6960f7a9fb', '7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf', 'Shirley', 'Graham', 'sgraham5@bbc.co.uk', 'Honorable')
		,('2ae69661-72c6-4e33-a6ec-1ca93152fa80', '5875412f-e8b8-493e-bd58-5df35083342c', 'Ashley', 'Hunt', 'ahunt6@narod.ru', 'Rev')
		,('c37f6cf4-bde1-4692-b4f5-b60826040209', '5875412f-e8b8-493e-bd58-5df35083342c', 'Walter', 'Riley', 'wriley7@dagondesign.com', 'Rev')
		,('80a7852a-9e10-4148-b38e-5c7abee7415b', '5875412f-e8b8-493e-bd58-5df35083342c', 'Jessica', 'Dunn', 'jdunn8@ycombinator.com', 'Rev')
		,('e8b71916-1a56-4c46-baeb-f5cec36a8344', '5875412f-e8b8-493e-bd58-5df35083342c', 'Shirley', 'Grant', 'sgrant9@liveinternet.ru', 'Honorable')
		,('30e027c4-2102-4ec9-a208-b9c439348d8a', 'cef70a7a-3349-4368-85ed-66b8c274fad1', 'Norma', 'Wilson', 'nwilsona@zimbio.com', 'Rev')
		,('be35f4de-780c-4a21-bb0a-f33a869f7d37', 'cef70a7a-3349-4368-85ed-66b8c274fad1', 'Helen', 'Robertson', 'hrobertsonb@vistaprint.com', 'Mrs')
		,('9789c200-6952-479b-914d-71e3cc21f6cc', '71d8e924-7c58-4424-9e1b-b14eefa76abc', 'Edward', 'Russell', 'erussellc@about.com', 'Rev')
		,('4fc6ce9c-9da1-46ae-bf25-c962717bee41', '71d8e924-7c58-4424-9e1b-b14eefa76abc', 'Janet', 'Long', 'jlongd@t.co', 'Mrs')
		,('947f9aec-099a-4c02-aa94-90ef141103c4', '71d8e924-7c58-4424-9e1b-b14eefa76abc', 'Betty', 'Payne', 'bpaynee@mapquest.com', 'Mr')
		,('0b0e277f-00ff-47f2-bcb7-13a6dc506709', '71d8e924-7c58-4424-9e1b-b14eefa76abc', 'Martha', 'Greene', 'mgreenef@harvard.edu', 'Ms')
		,('8f504ff8-8fb7-4bd5-96fd-652094649427', '71d8e924-7c58-4424-9e1b-b14eefa76abc', 'Anthony', 'Campbell', 'acampbellg@intel.com', 'Honorable')
		,('67e5f91c-35da-4664-8b9d-3cfcc3a85dd4', '71d8e924-7c58-4424-9e1b-b14eefa76abc', 'Jean', 'Hunter', 'jhunterh@yolasite.com', 'Rev')
		,('669e3d31-4c69-42db-af1f-c114ffab4627', '71d8e924-7c58-4424-9e1b-b14eefa76abc', 'Joyce', 'Gonzalez', 'jgonzalezi@cpanel.net', 'Dr')
		,('3666a490-9a9e-4bb2-a470-91584a036358', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Randy', 'Rice', 'rricej@tumblr.com', 'Ms')
		,('b784b2e8-cc0c-4912-86cf-1d1d6ff46ce8', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Martin', 'Henderson', 'mhendersonk@clickbank.net', 'Mr')
		,('899a3836-5e7c-4487-b731-38762f73bc93', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Paul', 'Foster', 'pfosterl@ebay.com', 'Ms')
		,('e55326e2-7a01-4462-9821-a6202926e902', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Margaret', 'Harper', 'mharperm@cnn.com', 'Mr')
		,('b0226782-70c0-49a2-94ae-dff730fc0f24', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Joshua', 'Medina', 'jmedinan@disqus.com', 'Dr')
		,('8beedd0e-5c45-406e-a7e2-5ff489089c74', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Howard', 'Lopez', 'hlopezo@wsj.com', 'Dr')
		,('11d8625a-e82f-4fc8-876c-f36ff497da1f', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Kenneth', 'White', 'kwhitep@nps.gov', 'Dr')
		,('b2aef82d-a08e-483c-99fa-3dc91df4293d', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Jeremy', 'Lewis', 'jlewisq@cbslocal.com', 'Dr')
		,('5ab798e2-d2d8-4ff9-a663-98edb0a54b30', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Jane', 'Bennett', 'jbennettr@dmoz.org', 'Rev')
		,('214694c0-3fe1-4cd6-889b-6ea011950b73', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Robin', 'Richards', 'rrichardss@upenn.edu', 'Ms')
		,('1a257809-9614-46ea-8aae-156c370d3f5d', '2b3b4d72-1c15-40e0-a05a-012b724950c3', 'Frances', 'Thompson', 'fthompsont@pen.io', 'Ms')
		,('51a590b3-4de3-4aba-828c-50afd8870e78', '2550f510-e5c9-45a4-90a0-c286e4bcd948', 'Michael', 'Myers', 'mmyersu@wired.com', 'Honorable')
		,('ba8b679b-390f-45d9-9829-1fddfc176d20', '2550f510-e5c9-45a4-90a0-c286e4bcd948', 'Stephen', 'Morris', 'smorrisv@arizona.edu', 'Rev')
		,('d9eef536-6c16-4fb8-a556-676717aaef4e', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Kathryn', 'Martinez', 'kmartinezw@weebly.com', 'Mr')
		,('1844fd9c-f08d-4c7b-95cb-cf58291c1fdc', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Walter', 'Ryan', 'wryanx@flickr.com', 'Mr')
		,('f1417949-e751-4f1f-a5b5-091d8205a793', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Susan', 'Perez', 'sperezy@nyu.edu', 'Mr')
		,('19872a6b-0ba3-4701-acd4-0082c24b748a', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Louis', 'Mcdonald', 'lmcdonaldz@dedecms.com', 'Mr')
		,('967e9b2f-b3ac-43e9-a429-7bc5ace2caf7', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Patricia', 'Price', 'pprice10@mysql.com', 'Ms')
		,('744280d2-c961-40f5-b120-5548d5aecbab', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Robert', 'Bishop', 'rbishop11@cbc.ca', 'Dr')
		,('4f11d302-ffc5-4358-adc2-ee6649147c5e', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Irene', 'Gomez', 'igomez12@stumbleupon.com', 'Mrs')
		,('6cc8386d-233d-4374-bda8-da3578fe8aca', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Marie', 'Hughes', 'mhughes13@hp.com', 'Honorable')
		,('2ec78dec-888f-4a6b-a736-69b339addab1', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Betty', 'Woods', 'bwoods14@europa.eu', 'Rev')
		,('b3754dc6-a956-4699-815d-d2b61a0aeec0', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Jack', 'Hernandez', 'jhernandez15@scientificamerican.com', 'Honorable')
		,('58d2142a-4a9c-4fd0-9521-3e5597469bf5', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Daniel', 'Robertson', 'drobertson16@statcounter.com', 'Rev')
		,('ba3ebd47-6dc1-49d7-87c7-5acb6ec45e1b', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Billy', 'Snyder', 'bsnyder17@bloomberg.com', 'Ms')
		,('b56141bd-2a2c-47da-943e-d7ebd8429c22', '874c0bc3-6d9b-4dfa-b42c-8403fe1b281d', 'Melissa', 'Palmer', 'mpalmer18@odnoklassniki.ru', 'Mrs')
		,('5726e485-04f2-4b93-a9cc-198ab78d233c', '16c6e264-0091-45f6-b9fd-02716d8d62dd', 'Donna', 'Johnston', 'djohnston19@archive.org', 'Mrs')
		,('51267270-d4da-412d-823d-673d1fb4562c', '16c6e264-0091-45f6-b9fd-02716d8d62dd', 'Christine', 'Henry', 'chenry1a@instagram.com', 'Rev')
		,('513e688c-6b19-42ad-a885-6462ffcc8dcc', '16c6e264-0091-45f6-b9fd-02716d8d62dd', 'Frances', 'Moore', 'fmoore1b@vistaprint.com', 'Rev')
		,('ca3b9112-ff9b-4a94-97ab-24f3b302aecf', '16c6e264-0091-45f6-b9fd-02716d8d62dd', 'Rachel', 'Spencer', 'rspencer1c@comsenz.com', 'Ms')
		,('c68b7a10-5f44-4661-92b3-d11cfcca87c1', '16c6e264-0091-45f6-b9fd-02716d8d62dd', 'Brian', 'Bryant', 'bbryant1d@qq.com', 'Ms')
		,('5369f72d-e7d5-48f9-81fd-6007c3332dd3', '16c6e264-0091-45f6-b9fd-02716d8d62dd', 'Harold', 'Fisher', 'hfisher1e@pagesperso-orange.fr', 'Ms')
		,('3f92501c-194b-4423-b780-64ecb4a11e2c', '16c6e264-0091-45f6-b9fd-02716d8d62dd', 'Harry', 'Gray', 'hgray1f@buzzfeed.com', 'Mr')
		,('9d0f4fba-d3a8-40e8-9662-f09f6ad7d341', '0d1a6711-e9eb-418e-adda-47a62a7900c9', 'Kenneth', 'Olson', 'kolson1g@gizmodo.com', 'Mrs')

) AS Source ([Id],[UserId],[Title],[Forename],[Surname],[Email])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[UserId] <> Source.[UserId] OR Target.[Title] <> Source.[Title] OR Target.[Forename] <> Source.[Forename] OR Target.[Surname] <> Source.[Surname] OR Target.[Email] <> Source.[Email]) THEN
 UPDATE SET [UserId] = Source.[UserId], [Title] = Source.[Title], [Forename] = Source.[Forename], [Surname] = Source.[Surname], [Email] = Source.[Email]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([Id],[UserId],[Title],[Forename],[Surname],[Email])
 VALUES(Source.[Id],Source.[UserId],Source.[Title],Source.[Forename],Source.[Surname],Source.[Email])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int, @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Contact]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Contact] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET NOCOUNT OFF
GO
