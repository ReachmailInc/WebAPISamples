using System;
using NUnit.Framework;
using ReachmailApi;
using Should;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly string _accountKey = Environment.GetEnvironmentVariable("RM_TEST_ACCTKEY");
        private readonly string _username = Environment.GetEnvironmentVariable("RM_TEST_USERNAME");
        private readonly string _password = Environment.GetEnvironmentVariable("RM_TEST_PASSWORD");

        private Reachmail _reachmail;

        [SetUp]
        public void Setup()
        {
            _reachmail = Reachmail.Create(_accountKey, _username, _password);
        }

        [Test]
        public void should_get_current_user()
        {
            var currentUser = _reachmail.Administration.Users.Current.Get();
            currentUser.AccountId.ShouldNotEqual(Guid.Empty);
            currentUser.AccountKey.ShouldEqual(_accountKey);
            currentUser.Username.ShouldEqual(_username);
        }
    }
}
