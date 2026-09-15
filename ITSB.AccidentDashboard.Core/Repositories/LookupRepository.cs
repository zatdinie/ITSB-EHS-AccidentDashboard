using System.Collections.Generic;
using System.Linq;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class LookupRepository
    {
        public List<Plant> GetPlants()
        {
            using (var db = new EshDbContext())
                return db.Plants.OrderBy(p => p.PlantCode).ToList();
        }

        public List<AccidentCause> GetAccidentCauses()
        {
            using (var db = new EshDbContext())
                return db.AccidentCauses.OrderBy(a => a.Description).ToList();
        }
        public List<BodyPart> GetBodyParts()
        {
            using (var db = new EshDbContext())
                return db.BodyParts.OrderBy(b => b.Name).ToList();
        }

        public List<InjuryType> GetInjuryTypes()
        {
            using (var db = new EshDbContext())
                return db.InjuryTypes.OrderBy(i => i.Name).ToList();
        }
    }
}