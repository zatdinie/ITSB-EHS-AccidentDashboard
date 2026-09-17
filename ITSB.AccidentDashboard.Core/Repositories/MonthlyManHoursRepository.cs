using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class MonthlyManHoursRepository
    {
        public List<MonthlyManHours> GetAll()
        {
            using (var db = new EshDbContext())
                return db.MonthlyManHours
                    .Include(m => m.Plant)
                    .OrderByDescending(m => m.Year).ThenBy(m => m.Month).ThenBy(m => m.Plant.PlantCode)
                    .ToList();
        }

        public MonthlyManHours GetById(int id)
        {
            using (var db = new EshDbContext())
                return db.MonthlyManHours.Find(id);
        }

        public void Add(MonthlyManHours entry)
        {
            using (var db = new EshDbContext())
            {
                db.MonthlyManHours.Add(entry);
                db.SaveChanges();
            }
        }

        public void Update(MonthlyManHours entry)
        {
            using (var db = new EshDbContext())
            {
                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new EshDbContext())
            {
                var entry = db.MonthlyManHours.Find(id);
                if (entry != null)
                {
                    db.MonthlyManHours.Remove(entry);
                    db.SaveChanges();
                }
            }
        }
    }
}