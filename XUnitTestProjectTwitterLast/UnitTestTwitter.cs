using _6.Twitter.Interfaces;
using _6.Twitter.Models;
using Moq;
using System;
using Xunit;

namespace XUnitTestProjectTwitterLast
{
    public class UnitTestTwitter
    {
        [Fact]
        public void SendTweetToServerShouldSendTheMessageToItsServer()
        {
            //
            // Create mocks:
            //
            var writerMock = new Mock<IWriter>();
            var tweetRepoMock = new Mock<ITweetRepository>();

            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));
            tweetRepoMock.Setup(x => x.SaveTweet(It.IsAny<string>()));
            //
            // Create instance of class I'm testing:
            //
            var classUnderTest = new MicrowaveOven(writerMock.Object, tweetRepoMock.Object);

            //
            // Run some logic to test:
            //
            classUnderTest.SendTweetToServer(It.IsAny<string>());

            //
            // Check the results:
            //

            /*
            * Add verifications 
            *
            */
            tweetRepoMock.Verify(x => x.SaveTweet(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public void WriteTweetShouldCallItsWriterWithTheTweetsMessage()
        {
            //
            // Create mocks:
            //
            var writerMock = new Mock<IWriter>();
            var tweetRepoMock = new Mock<ITweetRepository>();

            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));
            tweetRepoMock.Setup(x => x.SaveTweet(It.IsAny<string>()));
            //
            // Create instance of class I'm testing:
            //
            var classUnderTest = new MicrowaveOven(writerMock.Object, tweetRepoMock.Object);

            //
            // Run some logic to test:
            //
            classUnderTest.WriteTweet(It.IsAny<string>());

            //
            // Check the results:
            //

            /*
            * Add verifications 
            *
            */
            writerMock.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);

        }
        [Fact]
        public void ReceiveMessageShouldInvokeItsClientToWriteTheMessage()
        {
            //
            // Create mocks:
            //
            var clientMock = new Mock<IClient>();
            //
            // Create instance of class I'm testing:
            //
            var classUnderTest = new Tweet(clientMock.Object);

            //
            // Run some logic to test:
            //
            classUnderTest.ReceiveMessage(It.IsAny<string>());

            //
            // Check the results:
            //

            /*
            * Add verifications 
            *
            */
            clientMock.Verify(x => x.WriteTweet(It.IsAny<string>()), Times.Once);

        }
        [Fact]
        public void ReceiveMessageShouldInvokeItsClientToSendTheMessageToTheServer()
        {
            //
            // Create mocks:
            //
            var clientMock = new Mock<IClient>();
            clientMock.Setup(x => x.SendTweetToServer(It.IsAny<string>())).Verifiable();
            //
            // Create instance of class I'm testing:
            //
            var classUnderTest = new Tweet(clientMock.Object);

            //
            // Run some logic to test:
            //
            classUnderTest.ReceiveMessage(It.IsAny<string>());

            //
            // Check the results:
            //

            /*
            * Add verifications 
            *
            */
            clientMock.Verify(x => x.SendTweetToServer(It.IsAny<string>()), Times.Once);

        }
    }
}
