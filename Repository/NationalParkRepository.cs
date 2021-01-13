using System.Collections.Generic;
using System.Linq;
using ZurumPark.Data;
using ZurumPark.Entities;
using ZurumPark.Repository.IRepository;

namespace ZurumPark.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {

        private readonly ApplicationDbContext _context;

        public NationalParkRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            return Save();
            
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _context.NationalParks.FirstOrDefault(x => x.Id == nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _context.NationalParks.OrderBy(x => x.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            bool isPresent = _context.NationalParks.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return isPresent;
        }

        public bool NationalParkExists(int id)
        {
            return _context.NationalParks.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false ;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
             _context.NationalParks.Update(nationalPark);
             return Save();
        }
    }
}