using KHP.Domain.Model;
using KHPPractice.MVC.Data.DataAccess;
using KHPPractice.MVC.Data.DataRepository.Interface;
using KHPPractice.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace KHPPractice.MVC.Data.DataRepository.Implementations
{
    public class ArticleRepository : IArticleRepository
    {
        private KHPDbContext db = new KHPDbContext();   
        

        public List<Article> GetAllArticlesForBrowse()
        {
            return db.Articles.Where(a => a.IsApproved).ToList();
        }

        public List<Article> GetAllArticlesForReview()
        {
            var articles = from a in db.Articles.Include("Catagory")
                           where a.IsApproved == false
                           select a;
            return articles.ToList();
        }

        public List<Article> GetArticlesByCatagoryId(int catagoryId)
        {
            return db.Articles.Where(a => a.CatagoryId == catagoryId).ToList();
        }

        public void ApprovedArticle(List<int> articleIds)
        {
            foreach (var id in articleIds)
            {
                var articleToApprove = db.Articles.Find(id);
                articleToApprove.IsApproved = true;
            }
            db.SaveChanges();
        }

        public void RejectedArticle(List<int> articleIds)
        {
            foreach (var id in articleIds)
            {
                var articleToReject = db.Articles.Find(id);
                db.Articles.Remove(articleToReject);
            }
            db.SaveChanges();
        }

        public void SubmitArticle(Article article)
        {
            db.Articles.Add(article);
            db.SaveChanges();
        }
        public List<Article> GetSeachArticles(string str)
        {
            var cat = from c in db.Articles
                      where c.Description.Contains(str) || c.Description.Contains(str)
                      select c;
            return cat.ToList();
        }
    }
}
