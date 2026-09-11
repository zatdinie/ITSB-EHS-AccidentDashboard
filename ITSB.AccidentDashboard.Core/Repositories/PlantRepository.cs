using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

public class PlantRepository
{
    public List<Plant> GetAll()
    {
        using (var db = new EshDbContext())
            return db.Plants.OrderBy(p => p.Code).ToList();
    }
    
    public Plant GetById(int id)
    {
        using (var db = new EshDbContext())
            return db.Plants.Find(id);
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