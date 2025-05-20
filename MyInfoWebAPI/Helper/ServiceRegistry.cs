
using MyInfoCommonUtility.Configuration;
using MyInfoCommonUtility.Logger;
using MyInfoCommonUtility.Paging;
using MyInfoDAL.DataModel;
using MyInfoDAL.EmailRepo;
using Microsoft.EntityFrameworkCore;
using MyInfoBAL.MyInfo;
using MyInfoDAL.InfoRepo;

namespace MyInfoWebAPI.Helper
{
    public class ServiceRegistry
    {
        public void ConfigureDependencies(IServiceCollection services, AppsettingsConfig appSettings)
        {
            #region Bussiness Layer
            services.AddScoped<IInfoService, InfoService>();
            #endregion

            #region Data Layer
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IInfoRepo, InfoRepo>();
            #endregion

            #region Common Layer
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<IPagingParameter, PagingParameter>();
            #endregion
        }
        public void ConfigureDataContext(IServiceCollection services, AppsettingsConfig appSettings)
        {
            //Added LogFactory
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            });
            var connString = appSettings.MyInfoData.ConnectToDb.ConnectionString;
            services.AddDbContext<IMyInfoContext, MyInfoContext>(options =>
            {
                options.UseMySql(connString, new MySqlServerVersion(new Version(8, 0, 37))).EnableSensitiveDataLogging().UseLoggerFactory(loggerFactory);
            });
        }
    }
}
