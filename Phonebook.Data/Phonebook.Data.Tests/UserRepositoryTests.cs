using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.Domain.Model;
using Phonebook.Data.Context;
using Moq;
using System.Data.Entity;

namespace Phonebook.Data.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class UserRepositoryTests
	{
		private PhonebookContext phonebookContext;
		private IQueryable<User> _users;
		private IQueryable<Contact> _contacts;
		private IQueryable<ContactNumber> _contactNumbers;

		#region Test Initialise and Cleanup
		[TestInitialize]
		public void InitialiseTests()
		{
			var user1 = new User { Id = new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf"), Username = "User123" };
			var user2 = new User { Id = new Guid("5875412f-e8b8-493e-bd58-5df35083342c"), Username = "User456" };
			var user3 = new User { Id = new Guid("26e31dde-4bcb-47d4-be80-958676c5cafd"), Username = "User789" };
			var user4 = new User { Id = new Guid("cef70a7a-3349-4368-85ed-66b8c274fad1"), Username = "mjenkins0" };
			var user5 = new User { Id = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Username = "amartin1" };
			var user6 = new User { Id = new Guid("a051d1ca-a3c5-45d4-be60-5bc5256ce83e"), Username = "vallen2" };
			var user7 = new User { Id = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Username = "mblack3" };
			var user8 = new User { Id = new Guid("2550f510-e5c9-45a4-90a0-c286e4bcd948"), Username = "schapman4" };
			var user9 = new User { Id = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Username = "gdiaz5" };
			var user10 = new User { Id = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Username = "cwheeler6" };
			var user11 = new User { Id = new Guid("0d1a6711-e9eb-418e-adda-47a62a7900c9"), Username = "bparker7" };
					  
			var contact1 = new Contact { Id = new Guid("81c4763c-b225-4756-903a-750064167813"), UserId = new Guid("26e31dde-4bcb-47d4-be80-958676c5cafd"), Forename = "Theresa", Surname = "Reyes", Email = "treyes0@goo.gl", Title = "Dr" };
			var contact2 = new Contact { Id = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383"), UserId = new Guid("26e31dde-4bcb-47d4-be80-958676c5cafd"), Forename = "Pamela", Surname = "Wagner", Email = "pwagner1@ed.gov", Title = "Honorable" };
			var contact3 = new Contact { Id = new Guid("6e7ca25f-d438-4076-b2bf-180fbffe809e"), UserId = new Guid("26e31dde-4bcb-47d4-be80-958676c5cafd"), Forename = "Steve", Surname = "Tucker", Email = "stucker2@tuttocitta.it", Title = "Mrs" };
			var contact4 = new Contact { Id = new Guid("94669c7c-02f3-41a7-a8af-e6a3cee307bc"), UserId = new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf"), Forename = "Sean", Surname = "Baker", Email = "sbaker3@noaa.gov", Title = "Honorable" };
			var contact5 = new Contact { Id = new Guid("58c1eb1e-1513-4f19-97f3-d8571f97115f"), UserId = new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf"), Forename = "Melissa", Surname = "Tucker", Email = "mtucker4@yale.edu", Title = "Mrs" };
			var contact6 = new Contact { Id = new Guid("e3ee2f2b-3ace-4fd4-8ca7-7d6960f7a9fb"), UserId = new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf"), Forename = "Shirley", Surname = "Graham", Email = "sgraham5@bbc.co.uk", Title = "Honorable" };
			var contact7 = new Contact { Id = new Guid("2ae69661-72c6-4e33-a6ec-1ca93152fa80"), UserId = new Guid("5875412f-e8b8-493e-bd58-5df35083342c"), Forename = "Ashley", Surname = "Hunt", Email = "ahunt6@narod.ru", Title = "Rev" };
			var contact8 = new Contact { Id = new Guid("c37f6cf4-bde1-4692-b4f5-b60826040209"), UserId = new Guid("5875412f-e8b8-493e-bd58-5df35083342c"), Forename = "Walter", Surname = "Riley", Email = "wriley7@dagondesign.com", Title = "Rev" };
			var contact9 = new Contact { Id = new Guid("80a7852a-9e10-4148-b38e-5c7abee7415b"), UserId = new Guid("5875412f-e8b8-493e-bd58-5df35083342c"), Forename = "Jessica", Surname = "Dunn", Email = "jdunn8@ycombinator.com", Title = "Rev" };
			var contact10 = new Contact { Id = new Guid("e8b71916-1a56-4c46-baeb-f5cec36a8344"), UserId = new Guid("5875412f-e8b8-493e-bd58-5df35083342c"), Forename = "Shirley", Surname = "Grant", Email = "sgrant9@liveinternet.ru", Title = "Honorable" };
			var contact11 = new Contact { Id = new Guid("30e027c4-2102-4ec9-a208-b9c439348d8a"), UserId = new Guid("cef70a7a-3349-4368-85ed-66b8c274fad1"), Forename = "Norma", Surname = "Wilson", Email = "nwilsona@zimbio.com", Title = "Rev" };
			var contact12 = new Contact { Id = new Guid("be35f4de-780c-4a21-bb0a-f33a869f7d37"), UserId = new Guid("cef70a7a-3349-4368-85ed-66b8c274fad1"), Forename = "Helen", Surname = "Robertson", Email = "hrobertsonb@vistaprint.com", Title = "Mrs" };
			var contact13 = new Contact { Id = new Guid("9789c200-6952-479b-914d-71e3cc21f6cc"), UserId = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Forename = "Edward", Surname = "Russell", Email = "erussellc@about.com", Title = "Rev" };
			var contact14 = new Contact { Id = new Guid("4fc6ce9c-9da1-46ae-bf25-c962717bee41"), UserId = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Forename = "Janet", Surname = "Long", Email = "jlongd@t.co", Title = "Mrs" };
			var contact15 = new Contact { Id = new Guid("947f9aec-099a-4c02-aa94-90ef141103c4"), UserId = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Forename = "Betty", Surname = "Payne", Email = "bpaynee@mapquest.com", Title = "Mr" };
			var contact16 = new Contact { Id = new Guid("0b0e277f-00ff-47f2-bcb7-13a6dc506709"), UserId = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Forename = "Martha", Surname = "Greene", Email = "mgreenef@harvard.edu", Title = "Ms" };
			var contact17 = new Contact { Id = new Guid("8f504ff8-8fb7-4bd5-96fd-652094649427"), UserId = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Forename = "Anthony", Surname = "Campbell", Email = "acampbellg@intel.com", Title = "Honorable" };
			var contact18 = new Contact { Id = new Guid("67e5f91c-35da-4664-8b9d-3cfcc3a85dd4"), UserId = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Forename = "Jean", Surname = "Hunter", Email = "jhunterh@yolasite.com", Title = "Rev" };
			var contact19 = new Contact { Id = new Guid("669e3d31-4c69-42db-af1f-c114ffab4627"), UserId = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Forename = "Joyce", Surname = "Gonzalez", Email = "jgonzalezi@cpanel.net", Title = "Dr" };
			var contact20 = new Contact { Id = new Guid("3666a490-9a9e-4bb2-a470-91584a036358"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Randy", Surname = "Rice", Email = "rricej@tumblr.com", Title = "Ms" };
			var contact21 = new Contact { Id = new Guid("b784b2e8-cc0c-4912-86cf-1d1d6ff46ce8"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Martin", Surname = "Henderson", Email = "mhendersonk@clickbank.net", Title = "Mr" };
			var contact22 = new Contact { Id = new Guid("899a3836-5e7c-4487-b731-38762f73bc93"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Paul", Surname = "Foster", Email = "pfosterl@ebay.com", Title = "Ms" };
			var contact23 = new Contact { Id = new Guid("e55326e2-7a01-4462-9821-a6202926e902"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Margaret", Surname = "Harper", Email = "mharperm@cnn.com", Title = "Mr" };
			var contact24 = new Contact { Id = new Guid("b0226782-70c0-49a2-94ae-dff730fc0f24"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Joshua", Surname = "Medina", Email = "jmedinan@disqus.com", Title = "Dr" };
			var contact25 = new Contact { Id = new Guid("8beedd0e-5c45-406e-a7e2-5ff489089c74"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Howard", Surname = "Lopez", Email = "hlopezo@wsj.com", Title = "Dr" };
			var contact26 = new Contact { Id = new Guid("11d8625a-e82f-4fc8-876c-f36ff497da1f"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Kenneth", Surname = "White", Email = "kwhitep@nps.gov", Title = "Dr" };
			var contact27 = new Contact { Id = new Guid("b2aef82d-a08e-483c-99fa-3dc91df4293d"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Jeremy", Surname = "Lewis", Email = "jlewisq@cbslocal.com", Title = "Dr" };
			var contact28 = new Contact { Id = new Guid("5ab798e2-d2d8-4ff9-a663-98edb0a54b30"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Jane", Surname = "Bennett", Email = "jbennettr@dmoz.org", Title = "Rev" };
			var contact29 = new Contact { Id = new Guid("214694c0-3fe1-4cd6-889b-6ea011950b73"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Robin", Surname = "Richards", Email = "rrichardss@upenn.edu", Title = "Ms" };
			var contact30 = new Contact { Id = new Guid("1a257809-9614-46ea-8aae-156c370d3f5d"), UserId = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Forename = "Frances", Surname = "Thompson", Email = "fthompsont@pen.io", Title = "Ms" };
			var contact31 = new Contact { Id = new Guid("51a590b3-4de3-4aba-828c-50afd8870e78"), UserId = new Guid("2550f510-e5c9-45a4-90a0-c286e4bcd948"), Forename = "Michael", Surname = "Myers", Email = "mmyersu@wired.com", Title = "Honorable" };
			var contact32 = new Contact { Id = new Guid("ba8b679b-390f-45d9-9829-1fddfc176d20"), UserId = new Guid("2550f510-e5c9-45a4-90a0-c286e4bcd948"), Forename = "Stephen", Surname = "Morris", Email = "smorrisv@arizona.edu", Title = "Rev" };
			var contact33 = new Contact { Id = new Guid("d9eef536-6c16-4fb8-a556-676717aaef4e"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Kathryn", Surname = "Martinez", Email = "kmartinezw@weebly.com", Title = "Mr" };
			var contact34 = new Contact { Id = new Guid("1844fd9c-f08d-4c7b-95cb-cf58291c1fdc"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Walter", Surname = "Ryan", Email = "wryanx@flickr.com", Title = "Mr" };
			var contact35 = new Contact { Id = new Guid("f1417949-e751-4f1f-a5b5-091d8205a793"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Susan", Surname = "Perez", Email = "sperezy@nyu.edu", Title = "Mr" };
			var contact36 = new Contact { Id = new Guid("19872a6b-0ba3-4701-acd4-0082c24b748a"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Louis", Surname = "Mcdonald", Email = "lmcdonaldz@dedecms.com", Title = "Mr" };
			var contact37 = new Contact { Id = new Guid("967e9b2f-b3ac-43e9-a429-7bc5ace2caf7"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Patricia", Surname = "Price", Email = "pprice10@mysql.com", Title = "Ms" };
			var contact38 = new Contact { Id = new Guid("744280d2-c961-40f5-b120-5548d5aecbab"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Robert", Surname = "Bishop", Email = "rbishop11@cbc.ca", Title = "Dr" };
			var contact39 = new Contact { Id = new Guid("4f11d302-ffc5-4358-adc2-ee6649147c5e"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Irene", Surname = "Gomez", Email = "igomez12@stumbleupon.com", Title = "Mrs" };
			var contact40 = new Contact { Id = new Guid("6cc8386d-233d-4374-bda8-da3578fe8aca"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Marie", Surname = "Hughes", Email = "mhughes13@hp.com", Title = "Honorable" };
			var contact41 = new Contact { Id = new Guid("2ec78dec-888f-4a6b-a736-69b339addab1"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Betty", Surname = "Woods", Email = "bwoods14@europa.eu", Title = "Rev" };
			var contact42 = new Contact { Id = new Guid("b3754dc6-a956-4699-815d-d2b61a0aeec0"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Jack", Surname = "Hernandez", Email = "jhernandez15@scientificamerican.com", Title = "Honorable" };
			var contact43 = new Contact { Id = new Guid("58d2142a-4a9c-4fd0-9521-3e5597469bf5"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Daniel", Surname = "Robertson", Email = "drobertson16@statcounter.com", Title = "Rev" };
			var contact44 = new Contact { Id = new Guid("ba3ebd47-6dc1-49d7-87c7-5acb6ec45e1b"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Billy", Surname = "Snyder", Email = "bsnyder17@bloomberg.com", Title = "Ms" };
			var contact45 = new Contact { Id = new Guid("b56141bd-2a2c-47da-943e-d7ebd8429c22"), UserId = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Forename = "Melissa", Surname = "Palmer", Email = "mpalmer18@odnoklassniki.ru", Title = "Mrs" };
			var contact46 = new Contact { Id = new Guid("5726e485-04f2-4b93-a9cc-198ab78d233c"), UserId = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Forename = "Donna", Surname = "Johnston", Email = "djohnston19@archive.org", Title = "Mrs" };
			var contact47 = new Contact { Id = new Guid("51267270-d4da-412d-823d-673d1fb4562c"), UserId = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Forename = "Christine", Surname = "Henry", Email = "chenry1a@instagram.com", Title = "Rev" };
			var contact48 = new Contact { Id = new Guid("513e688c-6b19-42ad-a885-6462ffcc8dcc"), UserId = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Forename = "Frances", Surname = "Moore", Email = "fmoore1b@vistaprint.com", Title = "Rev" };
			var contact49 = new Contact { Id = new Guid("ca3b9112-ff9b-4a94-97ab-24f3b302aecf"), UserId = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Forename = "Rachel", Surname = "Spencer", Email = "rspencer1c@comsenz.com", Title = "Ms" };
			var contact50 = new Contact { Id = new Guid("c68b7a10-5f44-4661-92b3-d11cfcca87c1"), UserId = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Forename = "Brian", Surname = "Bryant", Email = "bbryant1d@qq.com", Title = "Ms" };
			var contact51 = new Contact { Id = new Guid("5369f72d-e7d5-48f9-81fd-6007c3332dd3"), UserId = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Forename = "Harold", Surname = "Fisher", Email = "hfisher1e@pagesperso-orange.fr", Title = "Ms" };
			var contact52 = new Contact { Id = new Guid("3f92501c-194b-4423-b780-64ecb4a11e2c"), UserId = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Forename = "Harry", Surname = "Gray", Email = "hgray1f@buzzfeed.com", Title = "Mr" };
			var contact53 = new Contact { Id = new Guid("9d0f4fba-d3a8-40e8-9662-f09f6ad7d341"), UserId = new Guid("0d1a6711-e9eb-418e-adda-47a62a7900c9"), Forename = "Kenneth", Surname = "Olson", Email = "kolson1g@gizmodo.com", Title = "Mrs" };

			var contactNumber1 = new ContactNumber { Id = new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa"), ContactId = new Guid("81c4763c-b225-4756-903a-750064167813"), Description = "Mobile", TelephoneNumber = "391714697203" };
			var contactNumber2 = new ContactNumber { Id = new Guid("368b5e82-a019-4a4f-8f66-4bb670e6b769"), ContactId = new Guid("81c4763c-b225-4756-903a-750064167813"), Description = "Home", TelephoneNumber = "297724563901" };
			var contactNumber3 = new ContactNumber { Id = new Guid("22b4f6e9-27c2-4636-b431-a37bdbc1b325"), ContactId = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383"), Description = "Home", TelephoneNumber = "864785278888" };
			var contactNumber4 = new ContactNumber { Id = new Guid("f5f162c8-33af-4ad2-b00d-1f618250401b"), ContactId = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383"), Description = "Work", TelephoneNumber = "44167868359" };
			var contactNumber5 = new ContactNumber { Id = new Guid("fda6127e-702b-4f32-9ec5-7f449deabf11"), ContactId = new Guid("6e7ca25f-d438-4076-b2bf-180fbffe809e"), Description = "Home", TelephoneNumber = "576800489823" };
			var contactNumber6 = new ContactNumber { Id = new Guid("9d656825-9033-4be4-adef-a0dd9ebeef70"), ContactId = new Guid("6e7ca25f-d438-4076-b2bf-180fbffe809e"), Description = "Work", TelephoneNumber = "985888887522" };
			var contactNumber7 = new ContactNumber { Id = new Guid("64aa5304-556f-4606-a118-03b43f46294b"), ContactId = new Guid("6e7ca25f-d438-4076-b2bf-180fbffe809e"), Description = "Mobile", TelephoneNumber = "188936967961" };
			var contactNumber8 = new ContactNumber { Id = new Guid("c94df269-db84-45a7-9b2a-47fffdbc3399"), ContactId = new Guid("94669c7c-02f3-41a7-a8af-e6a3cee307bc"), Description = "Home", TelephoneNumber = "427370908494" };
			var contactNumber9 = new ContactNumber { Id = new Guid("b74b006e-f985-47a6-932a-051e3437e31f"), ContactId = new Guid("94669c7c-02f3-41a7-a8af-e6a3cee307bc"), Description = "Mobile", TelephoneNumber = "165328050436" };
			var contactNumber10 = new ContactNumber { Id = new Guid("6222209a-ce18-4d88-a7a5-27c8bd0ef92f"), ContactId = new Guid("58c1eb1e-1513-4f19-97f3-d8571f97115f"), Description = "Mobile", TelephoneNumber = "381792056593" };
			var contactNumber11 = new ContactNumber { Id = new Guid("47f7f4e8-593c-4f6d-b1a5-1725e578ee86"), ContactId = new Guid("58c1eb1e-1513-4f19-97f3-d8571f97115f"), Description = "Home", TelephoneNumber = "632196085018" };
			var contactNumber12 = new ContactNumber { Id = new Guid("6ce58c67-b584-460f-8d5b-20cce37a4e90"), ContactId = new Guid("e3ee2f2b-3ace-4fd4-8ca7-7d6960f7a9fb"), Description = "Mobile", TelephoneNumber = "924308316065" };
			var contactNumber13 = new ContactNumber { Id = new Guid("8e9e8fe1-f9bf-482c-b1bd-77c1e472e683"), ContactId = new Guid("e3ee2f2b-3ace-4fd4-8ca7-7d6960f7a9fb"), Description = "Home", TelephoneNumber = "962474029779" };
			var contactNumber14 = new ContactNumber { Id = new Guid("6fcceb1d-f27c-401f-bd95-f3519be6d6d4"), ContactId = new Guid("e3ee2f2b-3ace-4fd4-8ca7-7d6960f7a9fb"), Description = "Work", TelephoneNumber = "412115311830" };
			var contactNumber15 = new ContactNumber { Id = new Guid("d8fc029d-8062-4cc4-ac29-e4339d1b48d3"), ContactId = new Guid("2ae69661-72c6-4e33-a6ec-1ca93152fa80"), Description = "Home", TelephoneNumber = "962216940411" };
			var contactNumber16 = new ContactNumber { Id = new Guid("0be9339f-706d-4e34-9938-afc76c7e746f"), ContactId = new Guid("2ae69661-72c6-4e33-a6ec-1ca93152fa80"), Description = "Mobile", TelephoneNumber = "641533924552" };

			var userList = new List<User> { user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, user11 };
			_users = userList.AsQueryable();
			var contactList = new List<Contact> { contact1, contact2, contact3, contact4, contact5, contact6, contact7, contact8, contact9, contact10, contact11, contact12, contact13, contact14, contact15, contact16, contact17, contact18, contact19, contact20, contact21, contact22, contact23, contact24, contact25, contact26, contact27, contact28, contact29, contact31, contact32, contact33, contact34, contact35, contact36, contact37, contact38, contact39, contact40, contact41, contact42, contact43, contact44, contact45, contact46, contact47, contact48, contact49, contact50, contact51, contact52, contact53 };
			_contacts = contactList.AsQueryable();
			var contactNumberList = new List<ContactNumber> { contactNumber1, contactNumber2, contactNumber3, contactNumber4, contactNumber5, contactNumber6, contactNumber7, contactNumber8, contactNumber9, contactNumber10, contactNumber11, contactNumber12, contactNumber13, contactNumber14, contactNumber15, contactNumber16 };
			_contactNumbers = contactNumberList.AsQueryable();

			var contactNumberSet = new Mock<DbSet<ContactNumber>>();
			contactNumberSet.As<IQueryable<ContactNumber>>().Setup(m => m.Provider).Returns(_contactNumbers.Provider);
			contactNumberSet.As<IQueryable<ContactNumber>>().Setup(m => m.Expression).Returns(_contactNumbers.Expression);
			contactNumberSet.As<IQueryable<ContactNumber>>().Setup(m => m.ElementType).Returns(_contactNumbers.ElementType);
			contactNumberSet.As<IQueryable<ContactNumber>>().Setup(m => m.GetEnumerator()).Returns(_contactNumbers.GetEnumerator());
			contactNumberSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(x => (contactNumberList as List<ContactNumber>).FirstOrDefault(y => y.Id == (Guid)x[0]) as ContactNumber);
			contactNumberSet.Setup(x => x.Add(It.IsAny<ContactNumber>())).Callback<ContactNumber>(contactNumberList.Add);

			var contactSet = new Mock<DbSet<Contact>>();
			contactSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(_contacts.Provider);
			contactSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(_contacts.Expression);
			contactSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(_contacts.ElementType);
			contactSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(_contacts.GetEnumerator());
			contactSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(x => (contactList as List<Contact>).FirstOrDefault(y => y.Id == (Guid)x[0]) as Contact);
			contactSet.Setup(x => x.Add(It.IsAny<Contact>())).Callback<Contact>(contactList.Add);

			var userSet = new Mock<DbSet<User>>();
			userSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(_users.Provider);
			userSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(_users.Expression);
			userSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(_users.ElementType);
			userSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(_users.GetEnumerator());
			userSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(x => (userList as List<User>).FirstOrDefault(y => y.Id == (Guid)x[0]) as User);
			userSet.Setup(x => x.Add(It.IsAny<User>())).Callback<User>(userList.Add);

			var pbContext = new Mock<PhonebookContext>();
			pbContext.Setup(m => m.ContactNumbers).Returns(contactNumberSet.Object);
			pbContext.Setup(m => m.Contacts).Returns(contactSet.Object);
			pbContext.Setup(m => m.Users).Returns(userSet.Object);
			pbContext.Setup(x => x.Set<ContactNumber>()).Returns(contactNumberSet.Object);
			pbContext.Setup(x => x.Set<Contact>()).Returns(contactSet.Object);
			pbContext.Setup(x => x.Set<User>()).Returns(userSet.Object);
			
			phonebookContext = pbContext.Object;
		}

        
		#endregion

		[TestMethod]
		public void GetAllOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var userList = new List<User>(_users.OrderBy(c => c.Username));

			//Act
			List<User> users = unitOfWork.UserRepository.GetAll().ToList();

			//Assert
			CollectionAssert.AreEqual(userList, users.OrderBy(c => c.Username).ToList());
		}

		[TestMethod]
		public void GetOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var userList = new List<User>(_users);

			//Act
			User user = unitOfWork.UserRepository.Get(new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf"));

			//Assert
			Assert.AreEqual(userList[0], user);
			unitOfWork.Dispose();
		}

		[TestMethod]
		public void CreateOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);

			var userToCreate = new User
			{
				Username = "Tej.Sidhu"
			};

			//Act
			unitOfWork.UserRepository.Create(userToCreate);
			unitOfWork.SaveChanges();
			User user = unitOfWork.UserRepository.Get(userToCreate.Id);

			//Assert
			Assert.AreEqual(UnProxy(user), userToCreate);
			Assert.IsNotNull(userToCreate.Id);
		}

		[TestMethod]
		public void UpdateOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var userList = new List<User>(_users);

			var userToUpdate = userList[1];

			//Act
			unitOfWork.UserRepository.Update(userToUpdate);
			unitOfWork.SaveChanges();
			User user = unitOfWork.UserRepository.Get(userToUpdate.Id);

			//Assert
			Assert.AreEqual(UnProxy(user), userToUpdate);
		}

		[TestMethod]
		public void DeleteOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var userList = new List<User>(_users);
			var userToDelete = userList[1];

			//Act
			unitOfWork.UserRepository.Delete(userToDelete.Id);
			unitOfWork.SaveChanges();
			User user = unitOfWork.UserRepository.Get(userToDelete.Id);

			//Assert
			Assert.IsNull(user);
		}

		[TestMethod]
		public void DeleteMultipleOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);

			//Act
			unitOfWork.UserRepository.DeleteMany(t => t.Username.Contains("User"));
			unitOfWork.SaveChanges();

			bool anyStillExist = unitOfWork.UserRepository.GetAll(t => t.Username.Contains("User")).Any();
			
			//Assert
			Assert.AreEqual(anyStillExist, false);
		}


		private User UnProxy(dynamic proxiedType)
		{
			User user = new User();

			user.Id = proxiedType.Id;
			user.Username = proxiedType.Username;

			return user;
		}
	}

}
