using KHP.Data.DataRepository.Interface;
using KHP.Domain.Model;
using KHPPractice.MVC.Data.DataAccess;

namespace KHPPractice.MVC.Data.DataRepository.Implementations
{
    public class CatagoryRepository : ICatagoryRepository
    {
        KHPDbContext db = new KHPDbContext();

        public void DeleteCatagory(int id)
        {
            var delete = db.Catagories.Find(id);
            db.Catagories.Remove(delete);
            db.SaveChanges();
        }

        public void Edit(Catagory catagory)
        {
            db.Catagories.Update(catagory);  
            db.SaveChanges();
        }

        public List<Catagory> GetCatagories()
        {
            return db.Catagories.ToList();
        }

        public Catagory GetCatagory(int id)
        {
            return db.Catagories.Find(id);
        }

        public List<Catagory> GetSeachCatagories(string str)
        {
            var cat = from c in db.Catagories
                      where c.Name.Contains(str) || c.Description.Contains(str)
                      select c;
            return cat.ToList();
        }

        public void Save(Catagory catagory)
        {
            db.Catagories.Add(catagory);
            db.SaveChanges();
        }
    }
}
