using BotSharp.Plugin.MongoStorage.Repository;
using EntityFrameworkCore.BootKit;
using System.Data.Common;
using System.Reflection;

namespace BotSharp.Plugin.MongoStorage;

/// <summary>
/// MongoDB as the repository
/// </summary>
public class MongoStoragePlugin : IBotSharpPlugin
{
    public string Name => "MongoDB Storage";
    public string Description => "MongoDB as the repository";

    public void RegisterDI(IServiceCollection services, IConfiguration config)
    {
        var dbSettings = new BotSharpDatabaseSettings();
        config.Bind("Database", dbSettings);

        if (dbSettings.Default == "MongoRepository")
        {
            services.AddScoped((IServiceProvider x) =>
            {
                var dbSettings = x.GetRequiredService<BotSharpDatabaseSettings>();
                return BuildMongoDbContext(dbSettings, x);
            });

            services.AddScoped<IBotSharpRepository, MongoRepository>();
        }
    }

    private MongoDbContext BuildMongoDbContext(BotSharpDatabaseSettings settings, IServiceProvider serviceProvider)
    {
        var dc = new MongoDbContext(settings.TablePrefix);
        var curAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        AppDomain.CurrentDomain.SetData("Assemblies", new string[] { curAssemblyName });

        dc.BindDbContext<IMongoCollection, DbContext4MongoDb>(new DatabaseBind
        {
            ServiceProvider = serviceProvider,
            MasterConnection = new MongoDbConnection(settings.BotSharpMongoDb),
            SlaveConnections = new List<DbConnection>(),
            IsRelational = false
        });
        return dc;
    }
}
