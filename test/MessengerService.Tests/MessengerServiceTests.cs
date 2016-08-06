using MessengerService.Core;
using MessengerService.Service;
using Xunit;

namespace MessengerService.Tests
{    
    public class MessengerServiceTests
    {
        public bool IsMessageReceived { get; set; }
        
        public MessengerServiceTests()
        {
            IsMessageReceived = false;
        }

        [Fact]
        public void MessengerService_Should_Be_Singleton()
        {
            IMessenger messenger1 = Messenger.Default;
            IMessenger messenger2 = Messenger.Default;

            Assert.Same(messenger1, messenger2);
        }

        [Fact]
        public void Send_Should_Broadcast_Message_To_Subscribers()
        {
            IMessenger messenger = Messenger.Default;            
            var actual = 0;
            var expected = 5;
            
            messenger.Register<int>(message =>
            {
                actual = message;
            });

            messenger.Send(5);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unregister_Should_Not_Receive_Message()
        {
            IMessenger messenger = Messenger.Default;        

            messenger.Register<int>(RegisterMessageReceived);
            messenger.Unregister<int>(RegisterMessageReceived);            
            messenger.Send(5);

            Assert.False(IsMessageReceived);
        }

        void RegisterMessageReceived(int message)
        {
            IsMessageReceived = true;
        }      
    }
}
