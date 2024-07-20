using BucketListData;
using BucketListModels;
using System.Collections.Generic;

namespace BucketListServices
{
    public class DestinationGetServices
    {
        SqlDbData sqlData = new SqlDbData();

        public List<Destination> GetAllDestinations()
        {
            return sqlData.GetAllDestinations();
        }

        public Destination GetDestinationByName(string name)
        {
            return sqlData.GetDestinationByName(name);
        }

        public void AddNewDestination(Destination newDestination)
        {
            sqlData.AddNewDestination(newDestination);
        }

        public void DeleteDestination(string name)
        {
            sqlData.DeleteDestinationByName(name);
        }

        public void UpdateDestination(Destination updatedDestination)
        {
            sqlData.UpdateDestination(updatedDestination);
        }
    }
} 