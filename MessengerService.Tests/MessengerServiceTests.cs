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
        public void MessengerService_Should_Be_Singleton()
        {
            IMessenger messenger1 = Messenger.Default;
            IMessenger messenger2 = Messenger.Default;

            Assert.AreSame(messenger1, messenger2);
        }

        [TestMethod]
        public void Send_Should_Broadcast_Message_To_Subscribers()
        {
            IMessenger messenger = Messenger.Default;            
            var actual = 0;
            var expected = 5;
            
            messenger.Register<int>(message =>
            {
                actual = message;
            });

            messenger.Send<int>(5);
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unregister_Should_Not_Receive_Message()
        {
            IMessenger messenger = Messenger.Default;        

            messenger.Register<int>(RegisterMessageReceived);
            messenger.Unregister<int>(RegisterMessageReceived);            
            messenger.Send<int>(5);

            Assert.IsFalse(IsMessageReceived);
        }

        void RegisterMessageReceived(int message)
        {
            IsMessageReceived = true;
        }      
    }
}
