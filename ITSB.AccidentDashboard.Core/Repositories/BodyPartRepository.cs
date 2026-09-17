using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class BodyPartRepository
    {
        public List<BodyPart> GetAll()
        {
            using (var db = new EshDbContext())
                return db.BodyParts.OrderBy(b => b.Name).ToList();
        }
        public BodyPart GetById(int id)
        {
            using (var db = new EshDbContext())
                return db.BodyParts.Find(id);
        }
        public void Add(BodyPart entity)
        {
            using (var db = new EshDbContext())
            {
                db.BodyParts.Add(entity);
                db.SaveChanges();
            }
        }
        public void Update(BodyPart entity)
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
                var entity = db.BodyParts.Find(id);
                if (entity != null) { db.BodyParts.Remove(entity); db.SaveChanges(); }
            }
        }
    }
}