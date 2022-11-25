using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmial(string email, bool expectedResult)
        {
            var accountController = new AccountController();
            var result = accountController.ValidateEmail(email);
            Assert.AreEqual(expectedResult, result);
        }

        [
            Test,
            TestCase("Asztapaszta", false),
            TestCase("ASZTAPASZTA2", false),
            TestCase("asztapaszta2", false),
            TestCase("Sztpsz2", false),
            TestCase("Asztapaszta2", true)
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            var accountController = new AccountController();
            var result = accountController.ValidatePassword(password);
            Assert.AreEqual(expectedResult, result);
        }

        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("kkk@mesemaci.com", "kukucska23L"),
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            var accountController = new AccountController();
            var result = accountController.Register(email, password);
            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(email, result.Password);
            Assert.AreNotEqual(Guid.Empty, result.ID);
        }

        [
            Test,
            TestCase("kiskutya@gomolyfelso", "246AGZlll"),
            TestCase("heshtag@macskaszor.hu", "nem"),
            TestCase("szarazteszta@marhabor-com", "Abrakadabra83"),
            TestCase("hagyjbekenmostmar.hu", "nemfoglakH2"),
            TestCase("@kecskebeka.com", "Tancitanci00")
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            var accountController = new AccountController();

            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }
        }
    }
}
