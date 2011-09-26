using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using Environment = NHibernate.Cfg.Environment;

namespace NH.Data
{
    public class SessionFactoryContext
    {
        [ThreadStatic]
        private static Dictionary<Type, ISessionFactory> _sessionFactories;

        private static Dictionary<Type, ISessionFactory> SessionFactories
        {
            get { return _sessionFactories ?? (_sessionFactories = new Dictionary<Type, ISessionFactory>()); }
        }

        public ISessionFactory Get<T>() where T : SessionConfiguration, new()
        {
            var configurationType = typeof(T);
            if (SessionFactories.ContainsKey(configurationType))
                return SessionFactories[configurationType];

            var configSettings = new T();

            var config = new Configuration();
            config.SetProperty(Environment.Dialect, configSettings.Dialect);
            config.SetProperty(Environment.ConnectionDriver, configSettings.ConnectionDriver);
            config.SetProperty(Environment.ConnectionString, configSettings.ConnectionString);

            var sessionFactory = Fluently.Configure(config)
                .Mappings(configSettings.ConfigureMappings)
                .BuildSessionFactory();

            SessionFactories.Add(configurationType, sessionFactory);

            return sessionFactory;
        }
    }
}
