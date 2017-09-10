using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Interfaces.UnitOfWork;
using Phonebook.Domain.Model;
using Phonebook.TestTools;

namespace Phonebook.WebApi.Tests
{
    public class BaseApiTests
    {
        protected PhonebookTestContext testContext;
        private Mock<IUnitOfWork> _mockUnitofWork;
        protected Mock<IGenericRepository<User>> MockUserRepository;
        protected Mock<IGenericRepository<Contact>> MockContactRepository;
        protected Mock<IContactNumberRepository> MockContactNumberRepository;
        protected Mock<IContactService> MockContactService;
        protected Mock<IUserService> MockUserService;
        protected Mock<IContactNumberService> MockContactNumberService;

        protected Mock<IUnitOfWork> MockUnitOfWork
        {
            get
            {
                return _mockUnitofWork;
            }

            set
            {
                _mockUnitofWork = value;
            }
        }

        [TestInitialize]
        public void SetupTestData()
        {
            testContext = new PhonebookTestContext();

            SetupMocks();
        }

        #region private methods

        public void SetupMocks()
        {
            MockUnitOfWork = new Mock<IUnitOfWork>();

            MockUserRepository = MockProvider.GetMockUserRepository(testContext.Users);
            MockContactRepository = MockProvider.GetMockContactRepository(testContext.Contacts);
            MockContactNumberRepository = MockProvider.GetMockContactNumberRepository(testContext.ContactNumbers);

            MockUnitOfWork.Setup(x => x.ContactNumberRepository).Returns(MockContactNumberRepository.Object);
            MockUnitOfWork.Setup(x => x.ContactRepository).Returns(MockContactRepository.Object);
            MockUnitOfWork.Setup(x => x.UserRepository).Returns(MockUserRepository.Object);

            MockContactService = MockProvider.GetContactService(testContext.Contacts);
            MockUserService = MockProvider.GetUserService(testContext.Users, testContext.Contacts);
            MockContactNumberService = MockProvider.GetContactNumberService(testContext.ContactNumbers);
        }
        #endregion
    }
}
