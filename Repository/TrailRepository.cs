using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ZurumPark.Data;
using ZurumPark.Entities;
using ZurumPark.Repository.IRepository;

namespace ZurumPark.Repository
{
    public class TrailRepository : ITrailRepository
    {

        private readonly ApplicationDbContext _context;

        public TrailRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public bool CreateTrail(Trail trail)
        {
            _context.Trails.Add(trail);
            return Save();
            
        }

        public bool DeleteTrail(Trail trail)
        {
            _context.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int trailId)
        {
            return _context.Trails.Include(x => x.NationalPark)
                    .FirstOrDefault(x => x.Id == trailId);
        }

        public ICollection<Trail> GetTrails()
        {
            return _context.Trails.Include(x => x.NationalPark)
                    .OrderBy(x => x.Name).ToList();
        }

        public bool TrailExists(string name)
        {
            bool isPresent = _context.Trails.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return isPresent;
        }

        public bool TrailExists(int id)
        {
            return _context.Trails.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false ;
        }

        public bool UpdateTrail(Trail trail)
        {
             _context.Trails.Update(trail);
             return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int nationalParkId)
        {
            return _context.Trails.Include(x => x.NationalPark)
                    .Where(x => x.NationalParkId == nationalParkId).ToList();
        }
    }
}