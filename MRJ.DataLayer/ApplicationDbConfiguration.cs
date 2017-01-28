using System.Data.Entity;

namespace MRJ.DataLayer
{
    public class ApplicationDbConfiguration : DbConfiguration
    {
        public ApplicationDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlServerExecutionStrategy());
        }
    }
}
