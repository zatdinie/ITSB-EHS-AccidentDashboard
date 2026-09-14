using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class PlantRepository
    {
        public List<Plant> GetAll()
        {
            using (var db = new EshDbContext())
                return db.Plants.OrderBy(p => p.PlantCode).ToList();
        }

        public Plant GetById(int id)
        {
            using (var db = new EshDbContext())
                return db.Plants.Find(id);
        }

        public void Add(Plant plant)
        {
            using (var db = new EshDbContext())
            {
                db.Plants.Add(plant);
                db.SaveChanges();
            }
        }

        public void Update(Plant plant)
        {
            using (var db = new EshDbContext())
            {
                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new EshDbContext())
            {
                var plant = db.Plants.Find(id);
                if (plant != null)
                {
                    db.Plants.Remove(plant);
                    db.SaveChanges();
                }
            }
        }
    }
}