using KHPPractice.MVC.Models;

namespace KHPPractice.MVC.Data.DataRepository.Interface
{
    public interface IArticleRepository
    {
        void SubmitArticle(Article article);
        void ApprovedArticle(List<int> articleIds);
        void RejectedArticle(List<int> articleIds);
        List<Article> GetAllArticlesForReview();
        List<Article> GetAllArticlesForBrowse();
        List<Article> GetArticlesByCatagoryId(int catagoryId);
        List<Article> GetSeachArticles(string str);
    }
}
