using DB_API_TEST.Event;
using DB_API_TEST.Event.ArgsModels;

namespace DB_API_TEST.Serives.EMListeners
{
    public class LogingListener : IListener
    {
        public void InitListener()
        {
            // subscribe on the required event
            EventManager.Subscribe("GetAllCustomersAPIHit", "GetAllCustomersAPIHitLogFunc", (args) =>
            {
                Console.WriteLine($"[{DateTime.Now.ToString("MM/dd/yyyy h:mm")}] [INFO] GetAllCustomers API Endpoint Hit");
            });

            EventManager.Subscribe("UserLoggedIn", "UserLoggedInLogFunc", (args) =>
            {
                UserLoggedArgs userLoggedArgs = (UserLoggedArgs) args;
                Console.WriteLine($"[{DateTime.Now.ToString("MM/dd/yyyy h:mm")}] [INFO] User ({userLoggedArgs.UserName}) Logged in");
            });

            EventManager.Subscribe("UserAdded", "UserAddedLogFunc", (args) =>
            {
                UserAddedArgs userAddedArgs = (UserAddedArgs) args;
                Console.WriteLine($"[{DateTime.Now.ToString("MM/dd/yyyy h:mm")}] [INFO] User ({userAddedArgs.FirstName} {userAddedArgs.LastName}) Added to Database");
            });            
        }
    }
}