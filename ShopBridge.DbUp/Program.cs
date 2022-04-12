using DbUp;
using System;
using System.Linq;
using System.Reflection;

namespace ShopBridge.DbUp
{
    class Program
    {
        public static int Main(string[] args)
        {

            var connectionString =
                args.FirstOrDefault() ??
                "Server=localdb;Database=ShopBridgeDb;Trusted_Connection=True;";

            if (!args.Any())
                EnsureDatabase.For.SqlDatabase(connectionString, 0);

            var upgrader = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithScriptNameComparer(StringComparer.OrdinalIgnoreCase)
                .JournalToSqlTable("dbo", "SchemaVersions")
                .WithTransactionPerScript()
                .LogToConsole()
                .LogScriptOutput()
                .Build();


            if (upgrader.IsUpgradeRequired())
            {
                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.Error);
                    Console.ResetColor();
#if DEBUG
#endif
                    return -1;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No upgrade required.");
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
            }
#if DEBUG
            Console.ReadKey();
#endif
            return 0;

        }
    }
}
