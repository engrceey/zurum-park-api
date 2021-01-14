using System.Collections.Generic;
using ZurumPark.Entities;

namespace ZurumPark.Repository.IRepository
{
    public interface ITrailRepository
    {

        bool CreateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        Trail GetTrail(int trailId);
        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailsInNationalPark(int nationalParkId);
        bool TrailExists(string name);
        bool TrailExists(int id);
        bool Save();
        bool UpdateTrail(Trail trail);

    }
}