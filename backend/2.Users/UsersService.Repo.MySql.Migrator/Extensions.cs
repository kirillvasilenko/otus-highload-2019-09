using DbUp.Builder;
using DbUp.Engine;
using DbUp.MySql;

namespace UsersService.Repo.MySql.Migrator
{
    public static class Extensions
    {
        public static UpgradeEngineBuilder JournalToMySqlTable(
            this UpgradeEngineBuilder builder,
            string schema,
            string table)
        {
            builder.Configure(c => c.Journal = (IJournal) new MySqlTableJournal(() => c.ConnectionManager, () => c.Log, schema, table));
            return builder;
        }
    }
}