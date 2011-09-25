using System;
using System.Collections.Generic;
using NHibernate;

namespace NH.Data
{
    public static class SessionDataContext
    {
        [ThreadStatic]
        private static Dictionary<Type, ISession> _sessions;

        private static Dictionary<Type, ISession> Sessions
        {
            get
            {
                if( null == _sessions)
                    _sessions = new Dictionary<Type, ISession>();

                return _sessions;
            }
        }

        public static ISession Get<T>() where T : SessionConfiguration
        {
            ISession session = null;
            Sessions.TryGetValue(typeof (T), out session);
            return session;
        }

        public static void Set<T>(ISession session)
        {
            Sessions[typeof(T)] = session;
        }
    }
}