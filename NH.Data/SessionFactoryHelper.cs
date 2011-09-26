using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;

namespace NH.Data
{
    public class SessionFactoryContext : IDisposable
    {
        [ThreadStatic]
        private static Dictionary<Type, ISessionFactory> _sessionFactories;

        private static Dictionary<Type, ISessionFactory> SessionFactories
        {
            get { return _sessionFactories ?? (_sessionFactories = new Dictionary<Type, ISessionFactory>()); }
        }

        public ISessionFactory Get<T>() where T : SessionConfiguration, new()
        {
            return Get<T>(x => {});
        }

        public ISessionFactory Get<T>(Action<Configuration> configurationAction) where T : SessionConfiguration, new()
        {
            var configurationType = typeof(T);
            if (SessionFactories.ContainsKey(configurationType))
                return SessionFactories[configurationType];

            var sessionConfiguration = new T();
            var sessionFactory = Fluently.Configure(sessionConfiguration.ToConfiguration())
                .Mappings(sessionConfiguration.ConfigureMappings)
                .ExposeConfiguration(configurationAction)
                .BuildSessionFactory();

            SessionFactories.Add(configurationType, sessionFactory);

            return sessionFactory;
        }

        public void Dispose()
        {
            foreach(var sessionFactory in SessionFactories)
                sessionFactory.Value.Dispose();
            SessionFactories.Clear();
        }
    }
}
