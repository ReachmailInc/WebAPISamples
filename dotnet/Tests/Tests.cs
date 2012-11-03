using System;
using System.Collections.Generic;
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

        [Test]
        public void should_interact_with_list_api()
        {
            // Post
            var postList = _reachmail.Contacts.Lists.Post(new ListProperties
            {
                Name = listName,
                Fields = new List<string> { "Zip" },
                Type = ListProperties.TypeOptions.Recipient
            });

            // Get
            var getList = _reachmail.Contacts.Lists.ByListId.Get(postList.Id.Value);
            getList.Id.ShouldEqual(postList.Id.Value);
            getList.Name.ShouldEqual(listName);
            getList.Type.ShouldEqual(ReachmailApi.Contacts.Lists.ByListId.Get.Response.List.TypeOptions.Recipient);

            // Put
            _reachmail.Contacts.Lists.ByListId.Put(postList.Id.Value,
                new ReachmailApi.Contacts.Lists.ByListId.Put.Request.ListProperties { Name = "New" + listName });

            // Get many
            var queryLists = _reachmail.Contacts.Lists.Query.Post(new ListFilter { NewerThan = DateTime.Now.AddMinutes(20) });
            var queryList = queryLists.FirstOrDefault(x => x.Id == postList.Id);
            queryList.ShouldNotBeNull();
            getList.Id.ShouldEqual(postList.Id.Value);
            getList.Name.ShouldEqual("New" + listName);
            getList.Type.ShouldEqual(ReachmailApi.Contacts.Lists.ByListId.Get.Response.List.TypeOptions.Recipient);

            // Delete
            _reachmail.Contacts.Lists.ByListId.Delete(postList.Id.Value);
            _reachmail.Contacts.Lists.Query.Post(new ListFilter { NewerThan = DateTime.Now.AddMinutes(20) })
                .Any(x => x.Id == postList.Id).ShouldBeFalse();
        }

        [Test]
        public void should_interact_with_data_api()
        {
            
        }
    }
}
