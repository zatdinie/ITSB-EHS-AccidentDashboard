using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class AccidentCauseRepository
    {
        public List<AccidentCause> GetAll()
        {
            using (var db = new EshDbContext())
                return db.AccidentCauses.OrderBy(a => a.Description).ToList();
        }
        public AccidentCause GetById(int id)
        {
            using (var db = new EshDbContext())
                return db.AccidentCauses.Find(id);
        }
        public void Add(AccidentCause entity)
        {
            using (var db = new EshDbContext())
            {
                db.AccidentCauses.Add(entity);
                db.SaveChanges();
            }
        }
        public void Update(AccidentCause entity)
        {
            using (var db = new EshDbContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var db = new EshDbContext())
            {
                var entity = db.AccidentCauses.Find(id);
                if (entity != null) { db.AccidentCauses.Remove(entity); db.SaveChanges(); }
            }
        }
    }
}