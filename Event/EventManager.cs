namespace DB_API_TEST.Event
{
    public delegate void EMHandler(object args);

    public static class EventManager
    {
        private static Dictionary<string, Dictionary<string, EMHandler>> _subscribers = new Dictionary<string, Dictionary<string, EMHandler>>();

        public static void Subscribe(string eventName, string funcId, EMHandler func)
        {
            if (!_subscribers.Keys.Contains(eventName))
                EventManager._subscribers[eventName] = new Dictionary<string, EMHandler>();
            EventManager._subscribers[eventName][funcId] = func;
        }

        public static void UnSubscribe(string eventName, string funcId)
        {
            EventManager._subscribers[eventName].Remove(funcId);
            if (EventManager._subscribers[eventName].Count == 0)
                EventManager._subscribers.Remove(eventName);
        }

        public static void PostEvent(string eventName, object args)
        {
            if (!EventManager._subscribers.Keys.Contains(eventName))
                return;
            foreach (string funcId in EventManager._subscribers[eventName].Keys)
                EventManager._subscribers[eventName][funcId](args);
        }
    }
}