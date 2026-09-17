using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class InjuryTypeRepository
    {
        public List<InjuryType> GetAll()
        {
            using (var db = new EshDbContext())
                return db.InjuryTypes.OrderBy(i => i.Name).ToList();
        }
        public InjuryType GetById(int id)
        {
            using (var db = new EshDbContext())
                return db.InjuryTypes.Find(id);
        }
        public void Add(InjuryType entity)
        {
            using (var db = new EshDbContext())
            {
                db.InjuryTypes.Add(entity);
                db.SaveChanges();
            }
        }
        public void Update(InjuryType entity)
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
                var entity = db.InjuryTypes.Find(id);
                if (entity != null) { db.InjuryTypes.Remove(entity); db.SaveChanges(); }
            }
        }
    }
}