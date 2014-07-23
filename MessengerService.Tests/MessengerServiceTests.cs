using MessengerService.Core;
using MessengerService.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessengerService.Tests
{
    [TestClass]
    public class MessengerServiceTests
    {
        public bool IsMessageReceived { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            IsMessageReceived = false;
        }

        [TestMethod]
        public void SendShouldBroadcastMessageToSubscribers()
        {
            IMessenger messenger = Messenger.Default;            
            var actual = 0;
            var expected = 5;

            //Arrange
            messenger.Register<int>(message =>
            {
                actual = message;
            });

            //Act
            messenger.Send<int>(5);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnregisterShouldNotReceiveMessage()
        {
            IMessenger messenger = Messenger.Default;                        

            //Arrange
            messenger.Register<int>(RegisterMessageReceived);

            messenger.Unregister<int>(RegisterMessageReceived);

            //Act
            messenger.Send<int>(5);

            //Assert
            Assert.IsFalse(IsMessageReceived);

        }

        void RegisterMessageReceived(int message)
        {
            IsMessageReceived = true;
        }      

    }
}
