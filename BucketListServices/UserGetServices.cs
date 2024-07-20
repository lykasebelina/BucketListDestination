using BucketListData;

namespace BucketListServices
{
    public class UserGetServices
    {
        SqlDbData sqlData = new SqlDbData();

        public bool ValidateUser(string username, string password)
        {
            return sqlData.ValidateUser(username, password);
        }
    }
} 