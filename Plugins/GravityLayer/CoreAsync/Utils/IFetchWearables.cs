using System.Threading.Tasks;

namespace GravityLayer.Utils
{
    public interface IFetchWearables
    {
        public Task<string> FetchWearablesByMetaverseId(string metaverseId);
    }

}
