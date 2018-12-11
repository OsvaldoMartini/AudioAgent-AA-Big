using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAgent.Data.FactoryDB
{
    public abstract class IFactory
    {
        public abstract DBase GetDataAccessLayer(DataProviderType dataProviderType);
    }
    public enum DataProviderType
    {
        SQLServer,
        MySQL,
        SQLite
    }
    

}
