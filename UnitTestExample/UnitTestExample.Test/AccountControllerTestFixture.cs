using NUnit.Framework;
using System;
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
    }
}
