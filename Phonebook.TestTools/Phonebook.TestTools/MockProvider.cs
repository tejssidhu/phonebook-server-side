using Moq;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Phonebook.TestTools
{
	[ExcludeFromCodeCoverage]
	public class MockProvider
    {
        public static Mock<IGenericRepository<User>> GetMockUserRepository(List<User> users)
        {
            var mockUserRepository = new Mock<IGenericRepository<User>>();

            mockUserRepository.Setup(x => x.Get(
                            It.IsAny<Guid>()))
                            .Returns(
                            new Func<Guid, User>((arg1) =>
                            {
                                IQueryable<User> query = users.AsQueryable();

                                return query.Where(c => c.Id == arg1).FirstOrDefault();
                            }));

            mockUserRepository.Setup(x => x.GetAll(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                It.IsAny<string>()))
                .Returns(new Func<Expression<Func<User, bool>>,
                    Func<IQueryable<User>, IOrderedQueryable<User>>,
                    string, IEnumerable<User>>((arg1, arg2, arg3) =>
                    {
                        IQueryable<User> query = users.AsQueryable();

                        if (arg1 != null)
                        {
                            query = query.Where(arg1);
                        }

                        if (arg2 != null)
                        {
                            return arg2(query).ToList();
                        }
                        else
                        {
                            return query.ToList();
                        }

                    }));

            return mockUserRepository;
        }

        public static Mock<IGenericRepository<Contact>> GetMockContactRepository(List<Contact> contacts)
        {
            var mockContactRepository = new Mock<IGenericRepository<Contact>>();

            mockContactRepository.Setup(x => x.Get(
                It.IsAny<Guid>()))
                .Returns(
                    new Func<Guid, Contact>((arg1) =>
                    {
                        IQueryable<Contact> query = contacts.AsQueryable();

                        return query.Where(c => c.Id == arg1).FirstOrDefault();
                    }));

            mockContactRepository.Setup(x => x.GetAll(
                It.IsAny<Expression<Func<Contact, bool>>>(),
                It.IsAny<Func<IQueryable<Contact>, IOrderedQueryable<Contact>>>(),
                It.IsAny<string>()))
                .Returns(new Func<Expression<Func<Contact, bool>>,
                    Func<IQueryable<Contact>, IOrderedQueryable<Contact>>,
                    string, IEnumerable<Contact>>((arg1, arg2, arg3) =>
                    {
                        IQueryable<Contact> query = contacts.AsQueryable();

                        if (arg1 != null)
                        {
                            query = query.Where(arg1);
                        }

                        if (arg2 != null)
                        {
                            return arg2(query).ToList();
                        }
                        else
                        {
                            return query.ToList();
                        }

                    }));

            return mockContactRepository;
        }

        public static Mock<IContactNumberRepository> GetMockContactNumberRepository(List<ContactNumber> contactNumbers)
        {
            var mockRepository = new Mock<IContactNumberRepository>();

            mockRepository.Setup(x => x.GetAll(
                It.IsAny<Expression<Func<ContactNumber, bool>>>(),
                It.IsAny<Func<IQueryable<ContactNumber>, IOrderedQueryable<ContactNumber>>>(),
                It.IsAny<string>()))
                .Returns(new Func<Expression<Func<ContactNumber, bool>>,
                    Func<IQueryable<ContactNumber>, IOrderedQueryable<ContactNumber>>,
                    string, IEnumerable<ContactNumber>>((arg1, arg2, arg3) =>
                    {
                        IQueryable<ContactNumber> query = contactNumbers.AsQueryable();

                        if (arg1 != null)
                        {
                            query = query.Where(arg1);
                        }

                        if (arg2 != null)
                        {
                            return arg2(query).ToList();
                        }
                        else
                        {
                            return query.ToList();
                        }

                    }));

            return mockRepository;
        }

        public static Mock<IContactService> GetContactService(List<Contact> contacts)
        {
            var mockService = new Mock<IContactService>();

            mockService.Setup(x => x.GetAll()).Returns(contacts);

            mockService.Setup(x => x.Get(
                It.IsAny<Guid>()))
                .Returns(
                    new Func<Guid, Contact>((arg1) =>
                    {
                        IQueryable<Contact> query = contacts.AsQueryable();

                        return query.Where(c => c.Id == arg1).FirstOrDefault();
                    }));

            mockService.Setup(x => x.Create(
                It.IsAny<Contact>()))
                .Returns((Contact c) => c.Id);

            return mockService;
        }

        public static Mock<IUserService> GetUserService(List<User> users, List<Contact> contacts)
        {
            var mockService = new Mock<IUserService>();

            mockService.Setup(x => x.GetAll()).Returns(users);

            mockService.Setup(x => x.Get(
                It.IsAny<Guid>()))
                .Returns(
                    new Func<Guid, User>((arg1) =>
                    {
                        IQueryable<User> query = users.AsQueryable();

                        return query.Where(c => c.Id == arg1).FirstOrDefault();
                    }));

            mockService.Setup(x => x.Create(
                It.IsAny<User>()))
                .Returns((User e) => e.Id);

            mockService.Setup(x => x.GetContacts(It.IsAny<Guid>()))
                .Returns((Guid userGuid) =>
                {
                    IQueryable<Contact> query = contacts.AsQueryable();

                    if (userGuid != null)
                    {
                        query = query.Where(c => c.UserId == userGuid);
                    }

                    return query.ToList();

                });

            return mockService;
        }

        public static Mock<IContactNumberService> GetContactNumberService(List<ContactNumber> contactNumbers)
        {
            var mockService = new Mock<IContactNumberService>();

            mockService.Setup(x => x.GetAll()).Returns(contactNumbers);

            mockService.Setup(x => x.Get(
                It.IsAny<Guid>()))
                .Returns(
                    new Func<Guid, ContactNumber>((arg1) =>
                    {
                        IQueryable<ContactNumber> query = contactNumbers.AsQueryable();

                        return query.Where(c => c.Id == arg1).FirstOrDefault();
                    }));

            mockService.Setup(x => x.Create(
                It.IsAny<ContactNumber>()))
                .Returns((ContactNumber e) => e.Id);

            return mockService;
        }


    }
}
