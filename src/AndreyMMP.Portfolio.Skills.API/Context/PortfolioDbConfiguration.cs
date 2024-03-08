namespace AndreyMMP.Portfolio.Skills.API.Context
{
    public static class PortfolioDbConfiguration
    {
        private static readonly string
            _dbHost = "localhost",
            _dbName = "portfolio",
            _dbPassword = "P@ssw0rd#111";

        public static string GetConnectionString()
        {
            return $"Data Source={_dbHost};Initial Catalog={_dbName};User ID=sa;Password={_dbPassword};Trusted_Connection=True;Encrypt=false;Integrated Security=false";
        }
    }
}