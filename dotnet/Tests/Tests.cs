using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ReachmailApi;
using ReachmailApi.Mailings.Filtered.Post.Request;
using ReachmailApi.Mailings.Post.Request;
using ReachmailApi.Lists.Post.Request;
using ReachmailApi.Lists.Filtered.Post.Request;
using ReachmailApi.Reports.Mailings.Detail.Post.Request;
using ReachmailApi.Lists.Recipients.ByListId.Post.Request;
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
            _reachmail = Reachmail.Create(_accountKey, _username, _password, allowSelfSignedCerts: true, timeout: 1200);
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
        public void should_interact_with_mailing_api()
        {
            var mailingName = "TestMailing-" + Guid.NewGuid().ToString("N");

            // Post
            var postMailing = _reachmail.Mailings.Post(new MailingProperties
            {
                Name = mailingName,
                MailingFormat = MailingProperties.MailingFormatOptions.TextAndHtml
            });

            // Get
            var getMailing = _reachmail.Mailings.ByMailingId.Get(postMailing.Id.Value);
            getMailing.Id.ShouldEqual(postMailing.Id.Value);
            getMailing.Name.ShouldEqual(mailingName);
            getMailing.MailingFormat.Value.ShouldEqual(ReachmailApi.Mailings.ByMailingId.Get.Response.Mailing.MailingFormatOptions.TextAndHtml);

            // Get many
            var queryLists = _reachmail.Mailings.Filtered.Post(new MailingFilter { NewerThan = DateTime.Now.AddDays(-1) });
            var queryList = queryLists.FirstOrDefault(x => x.Id == postMailing.Id.Value);
            queryList.ShouldNotBeNull();
            queryList.Id.ShouldEqual(postMailing.Id.Value);
            queryList.Name.ShouldEqual(mailingName);
            queryList.MailingFormat.ShouldEqual(ReachmailApi.Mailings.Filtered.Post.Response.Mailing.MailingFormatOptions.TextAndHtml);

            // Put
            _reachmail.Mailings.ByMailingId.Put(postMailing.Id.Value,
                new ReachmailApi.Mailings.ByMailingId.Put.Request.MailingProperties { Name = "New" + mailingName });
            getMailing = _reachmail.Mailings.ByMailingId.Get(postMailing.Id.Value);
            getMailing.Id.ShouldEqual(postMailing.Id.Value);
            getMailing.Name.ShouldEqual("New" + mailingName);
            getMailing.MailingFormat.ShouldEqual(ReachmailApi.Mailings.ByMailingId.Get.Response.Mailing.MailingFormatOptions.TextAndHtml);

            // Delete
            _reachmail.Mailings.ByMailingId.Delete(postMailing.Id.Value);
            _reachmail.Mailings.Filtered.Post(new MailingFilter { NewerThan = DateTime.Now.AddMinutes(-10) })
                .Any(x => x.Id == postMailing.Id).ShouldBeFalse();
        }

        [Test]
        public void should_interact_with_list_api()
        {
            var listName = "TestList-" + Guid.NewGuid().ToString("N");

            // Post
            var postList = _reachmail.Lists.Post(new ListProperties
            {
                Name = listName,
                Fields = new List<string> { "Zip" },
                Type = ListProperties.TypeOptions.Recipient
            });

            // Get
            var getList = _reachmail.Lists.ByListId.Get(postList.Id.Value);
            getList.Id.ShouldEqual(postList.Id.Value);
            getList.Name.ShouldEqual(listName);
            getList.Type.ShouldEqual(ReachmailApi.Lists.ByListId.Get.Response.List.TypeOptions.Recipient);

            // Get many
            var queryLists = _reachmail.Lists.Filtered.Post(new ListFilter { NewerThan = DateTime.Now.AddDays(-1) });
            var queryList = queryLists.FirstOrDefault(x => x.Id == postList.Id.Value);
            queryList.ShouldNotBeNull();
            queryList.Id.ShouldEqual(postList.Id.Value);
            queryList.Name.ShouldEqual(listName);
            queryList.Type.ShouldEqual(ReachmailApi.Lists.Filtered.Post.Response.List.TypeOptions.Recipient);

            // Put
            _reachmail.Lists.ByListId.Put(postList.Id.Value,
                new ReachmailApi.Lists.ByListId.Put.Request.ListProperties { Name = "New" + listName });
            getList = _reachmail.Lists.ByListId.Get(postList.Id.Value);
            getList.Id.ShouldEqual(postList.Id.Value);
            getList.Name.ShouldEqual("New" + listName);
            getList.Type.ShouldEqual(ReachmailApi.Lists.ByListId.Get.Response.List.TypeOptions.Recipient);

            // Add recipient
            _reachmail.Lists.Recipients.ByListId.Post(postList.Id.Value, new RecipientProperties { Email = "test@test.com" });

            // Delete recipients
            _reachmail.Lists.Recipients.Filtered.Delete.ByListId.Post(postList.Id.Value, new ReachmailApi.Lists.Recipients.Filtered.Delete.ByListId.Post.Request.RecipientFilter());

            // Delete
            _reachmail.Lists.ByListId.Delete(postList.Id.Value);
			_reachmail.Lists.Filtered.Post(new ListFilter { NewerThan = DateTime.Now.AddMinutes(-10) })
                .Any(x => x.Id == postList.Id).ShouldBeFalse();
        }

        [Test]
        public void should_interact_with_data_api()
        {
            var data = _reachmail.Data.Post(new MemoryStream(Encoding.ASCII.GetBytes("oh hai")));
            new StreamReader(_reachmail.Data.ByDataId.Get(data.Id.Value)).ReadToEnd().ShouldEqual("oh hai");
        }

        [Test]
        public void should_interact_with_reports()
        {
            _reachmail.Reports.Mailings.Detail.Post( new MailingReportFilter {
                    ScheduledDeliveryOnOrAfter = DateTime.Now.AddDays(-1)
                }).ShouldNotBeNull();

            _reachmail.Reports.Mailings.Trackedlinks.Summary.ByMailingId.Get(
                 Guid.Empty).ShouldBeEmpty();
        }
    }
}
