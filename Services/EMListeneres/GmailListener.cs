using DB_API_TEST.Event;
using DB_API_TEST.Event.ArgsModels;

namespace DB_API_TEST.Serives.EMListeners
{
    public class GmailListener : IListener
    {
        public void InitListener()
        {
            // subscribe on the required event
            EventManager.Subscribe("UserAdded", "UserAddedGmailFunc", (args) =>
            {
                UserAddedArgs userAddedArgs = (UserAddedArgs) args;
                Console.WriteLine($"Sending a Gmail Welcome Message to ({userAddedArgs.FirstName} {userAddedArgs.LastName})"); 
            });
        }
    }
}