using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace NH.Data.Testing
{
    class InMemoryDatabaseBuilder
    {
        public static Configuration Configuration { get; private set; }

        public static void Intercept(Configuration configuration)
        {
            Configuration = configuration;
            Configuration.SetProperty(Environment.FormatSql, "true");
            Configuration.SetProperty(Environment.ReleaseConnections, "on_close");
        }

        public static void Build(ISession session)
        {
            new SchemaExport(Configuration).Execute(false, true, false, session.Connection, null);
        }
    }
}