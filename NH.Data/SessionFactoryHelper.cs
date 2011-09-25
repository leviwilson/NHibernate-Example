using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using Ninject;
using Environment = NHibernate.Cfg.Environment;

namespace NH.Data
{
    public class SessionFactoryContext
    {
        [ThreadStatic]
        private static Dictionary<Type, ISessionFactory> _sessionFactories;

        private static Dictionary<Type, ISessionFactory> SessionFactories
        {
            get
            {
                if (null == _sessionFactories)
                    _sessionFactories = new Dictionary<Type, ISessionFactory>();
                return _sessionFactories;
            }
        }

        public ISessionFactory Get<T>() where T : SessionConfiguration
        {
            var configurationType = typeof(T);
            if (SessionFactories.ContainsKey(configurationType))
                return SessionFactories[configurationType];

            var kernel = new StandardKernel();
            var configSettings = kernel.Get<T>();

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
