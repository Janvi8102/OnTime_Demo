using Dapper;
using Npgsql;
using OnTime_Demo.Entities;
using OnTime_Demo.IRepository;
using System.Data;

namespace OnTime_Demo.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;
        private readonly string connectionString;
        public IConfiguration Configuration { get; }
        public UserRepository(IDbConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            connectionString = _connection.ConnectionString;
            Configuration = configuration;
        }
        public List<ConfigurationInfoModel> GetConfigurationInfo()
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                List<ConfigurationInfoModel> configurationInformation = dbConnection.Query<ConfigurationInfoModel>("SELECT * FROM Public.\"ConfigurationInfo\"").ToList();
                return configurationInformation;
            }
        }

        public JiraTokenModel GetJiraToken(int userId)
        {
            string query = @"select * FROM ""User""";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                JiraTokenModel jiraTokens = dbConnection.Query<JiraTokenModel>(query).FirstOrDefault();
                jiraTokens.JiraAuthToken = "Bearer " + jiraTokens.JiraAuthToken;
                return jiraTokens;
            }
        }

        public async Task<bool> UpdateJiraTokens(JiraTokenModel jiramodel)
        {
            string sql = @" UPDATE ""User"" SET ""JiraAuthToken"" = @JiraAuthToken , ""JiraRefreshToken"" = @JiraRefreshToken
                where ""UserId""= 2 ";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var generalTaskStatus = await dbConnection.ExecuteAsync(sql, new { JiraAuthToken = jiramodel.JiraAuthToken, JiraRefreshToken = jiramodel.JiraRefreshToken, UserId = jiramodel.UserId });
                return generalTaskStatus > 0;
            }
        }
    }
}
