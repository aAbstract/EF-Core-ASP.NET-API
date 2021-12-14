using DB_API_TEST.Event;
using DB_API_TEST.Serives.EMListeners;

namespace DB_API_TEST.Serives
{
    public static class EMBootstrap
    {
        private static List<IListener> _EMListeners = new List<IListener>()
        {
            new LogingListener(),
            new GmailListener()
        };

        public static void SetupEMListeners()
        {
            foreach (IListener listener in EMBootstrap._EMListeners)
                listener.InitListener();       
        }
    }
}