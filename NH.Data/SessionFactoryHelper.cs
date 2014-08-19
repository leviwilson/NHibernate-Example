using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;

namespace NH.Data
{
    public class SessionFactoryContext : IDisposable
    {
        private static readonly Dictionary<Type, ISessionFactory> SessionFactories = new Dictionary<Type, ISessionFactory>();
        private static readonly object SyncObject = new object();

        public ISessionFactory Get<T>() where T : SessionConfiguration, new()
        {
            return Get<T>(x => { });
        }

        public ISessionFactory Get<T>(Action<Configuration> configurationAction) where T : SessionConfiguration, new()
        {
            lock (SyncObject)
            {
                var configurationType = typeof(T);
                if (SessionFactories.ContainsKey(configurationType))
                    return SessionFactories[configurationType];

                var sessionFactory = SessionFactoryFor<T>(configurationAction);

                SessionFactories.Add(configurationType, sessionFactory);
                return sessionFactory;
            }
        }

        public void Alias<T, TAlias>() where TAlias : SessionConfiguration, new() where T : SessionConfiguration, new()
        {
            Set<T>(Get<TAlias>());
        }

        public void Dispose()
        {
            lock (SyncObject)
            {
                foreach (var sessionFactory in SessionFactories)
                    sessionFactory.Value.Dispose();
                SessionFactories.Clear();
            }
        }

        private static ISessionFactory SessionFactoryFor<T>(Action<Configuration> configurationAction) where T : SessionConfiguration, new()
        {
            var sessionConfiguration = new T();
            return Fluently.Configure(sessionConfiguration.ToConfiguration())
                           .Mappings(sessionConfiguration.ConfigureMappings)
                           .ExposeConfiguration(configurationAction)
                           .BuildSessionFactory();
        }

        private static void Set<T>(ISessionFactory sessionFactory) where T : SessionConfiguration, new()
        {
            lock (SyncObject)
            {
                var configurationType = typeof(T);
                if (SessionFactories.ContainsKey(configurationType))
                {
                    SessionFactories[configurationType].Dispose();
                    SessionFactories[configurationType] = sessionFactory;
                }
                else
                {
                    SessionFactories.Add(configurationType, sessionFactory);
                }
            }
        }
    }
}
