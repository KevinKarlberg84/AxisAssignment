using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    public interface IWeatherService
    {
        Task<RootObject> GetBaseKeyInfoAboutWeatherLocations();
        Task<List<TempRootObject>> GetFullListOfTemperatures(RootObject obj);
        Task<PercepRootObject> GetLundPercipitation(RootObject obj);
    }
}
