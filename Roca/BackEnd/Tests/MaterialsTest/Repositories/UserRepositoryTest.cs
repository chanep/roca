using System;
using System.Linq;
using System.Transactions;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Repositories
{
    public class UserDto
    {
        public int Id { get; set; }
        public string ShortUserName { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Mail { get; set; }
        public string Roles { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public string SpecialtyAbbreviaton { get; set; }

    }

    [TestClass]
    public class UserRepositoryTest : BaseTest
    {


        [TestMethod]
        public void AddUserTest()
        {
            var user = new User()
            {
                Id = Math.Abs(Guid.NewGuid().GetHashCode()),
                LongUserName = @"ODEBRECHT\mAurelio",
                Name = "Mongo",
                LastName = "Aurelio",
                Initials = "MAU",
                Mail = "maurelio@odebrecht.com",
                Roles = "Read, Admin",
                Specialties = CreateRocaUow().Specialties.GetAll().ToList()

            };

            using (var tx = new TransactionScope())
            {

               
                

                using (var uow = CreateRocaUow())
                {
                    //Console.WriteLine("tx dist: " + Transaction.Current.TransactionInformation.DistributedIdentifier);

                    var repo = uow.Users;

                    //Console.WriteLine("tx dist: " + Transaction.Current.TransactionInformation.DistributedIdentifier);

                    var user2 = repo.Add(user);

                    Console.WriteLine("Id: " + user2.Id);

                    //Console.WriteLine("tx dist: " + Transaction.Current.TransactionInformation.DistributedIdentifier);

                    uow.Commit();
                }

                //Console.WriteLine("tx dist: " + Transaction.Current.TransactionInformation.DistributedIdentifier);

                //var repo3 = new RocaUow().Users;
                //var user3 = repo3.GetByLongUserName(user.LongUserName);
                //Assert.IsTrue(user3.Id>0, "No se genero el Id");
                //Console.WriteLine(user);

                //tx.Complete();
            }

            var repo4 = CreateRocaUow().Users;
            var user4 = repo4.GetAll().SingleOrDefault(u => u.LongUserName == user.LongUserName);
            Assert.IsNull(user4, "se creo el usuario sin llamar al complete del txscope");
        }
    }
}
