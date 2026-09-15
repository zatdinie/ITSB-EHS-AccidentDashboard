using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class AccidentIncidentRepository
    {
        public List<AccidentIncident> GetAll()
        {
            using (var db = new EshDbContext())
                return db.AccidentIncidents
                    .Include(i => i.Plant)
                    .Include(i => i.BodyPart)
                    .Include(i => i.AccidentCause)
                    .Include(i => i.InjuryType)
                    .OrderByDescending(i => i.DateOfOccurrence)
                    .ToList();
        }

        public AccidentIncident GetById(int id)
        {
            using (var db = new EshDbContext())
                return db.AccidentIncidents.Find(id);
        }

        public void Add(AccidentIncident incident)
        {
            using (var db = new EshDbContext())
            {
                db.AccidentIncidents.Add(incident);
                db.SaveChanges();
            }
        }

        public void Update(AccidentIncident incident)
        {
            using (var db = new EshDbContext())
            {
                db.Entry(incident).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new EshDbContext())
            {
                var incident = db.AccidentIncidents.Find(id);
                if (incident != null)
                {
                    db.AccidentIncidents.Remove(incident);
                    db.SaveChanges();
                }
            }
        }
    }
}