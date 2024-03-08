namespace AndreyMMP.Portfolio.Skills.API.Context
{
    public static class PortfolioDbConfiguration
    {
        private static readonly string
            _dbHost = Environment.GetEnvironmentVariable("DB_HOST"),
            _dbName = Environment.GetEnvironmentVariable("DB_NAME"),
            _dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

        public static string GetConnectionString()
        {
            return $"Data Source={_dbHost};Initial Catalog={_dbName};User ID=sa;Password={_dbPassword};Trusted_Connection=True;Encrypt=false;Integrated Security=false";
        }
    }
}