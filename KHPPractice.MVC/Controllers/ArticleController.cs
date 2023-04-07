using KHP.Data.DataRepository.Interface;
using KHP.Domain.Model;
using KHPPractice.MVC.Data.DataRepository.Implementations;
using KHPPractice.MVC.Data.DataRepository.Interface;
using KHPPractice.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Configuration;
using System.Security.Cryptography.Xml;

namespace KHPPractice.MVC.Controllers
{
    public class ArticleController : Controller
    {
        IArticleRepository articleRepo = new ArticleRepository();
        ICatagoryRepository CatagoryRepo = new CatagoryRepository();

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult SubmitArticle()
        {
            var catagories = CatagoryRepo.GetCatagories();
            var selectListCatagories = from c in catagories
                                       select new SelectListItem { Text = c.Name, Value = c.CatagoryId.ToString() };
            ViewBag.CatagoryId = selectListCatagories;

            return View();
        }


        [Authorize]
        [HttpPost]
        public IActionResult SubmitArticle(Article articleToSubmit)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            articleToSubmit.DateSubmmited = DateTime.Now;
            articleToSubmit.IsApproved = false;

            if (User.Identity!.Name == null)
            {
                articleToSubmit.PostedBy = "Unknown";
            }
            else
            {
                articleToSubmit.PostedBy = User.Identity.Name;
            }

            articleRepo.SubmitArticle(articleToSubmit);
            TempData["Message"] = $"Article {articleToSubmit.Title} submitted succesfully for review ";
            return RedirectToAction("SubmitArticle");
        }

        [HttpGet]
       // [Authorize(Roles = "admin")]
        public IActionResult ReviewArticle()
        {
            var articleForReview = articleRepo.GetAllArticlesForReview();
            return View(articleForReview);
        }


       // [Authorize(Roles = "admin")]
        public IActionResult Approve(List<int> ArticleID)
        {
            articleRepo.ApprovedArticle(ArticleID);
            TempData["Message"] = "Article Approved";
            return RedirectToAction("ReviewArticle");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Reject(List<int> ArticleID)
        {
            articleRepo.RejectedArticle(ArticleID);
            TempData["Message"] = "Article Rejected";
            return RedirectToAction("ReviewArticle");
        }

        public IActionResult BrowseArticle(string searchText = null!) //this is an action method // This is home page of catagory
        {
            List<Article> articles = null!;
            if (searchText != null && searchText.Length >= 0)
            {
                articles = articleRepo.GetSeachArticles(searchText);
            }
            else
            {
                articles = articleRepo.GetAllArticlesForBrowse();
            }
            return View(articles); // This is method is returing a view
        }
        //public async Task<IActionResult> AssignRoleToUser(string userId, string roleName)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await _userManager.AddToRoleAsync(user, roleName);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return BadRequest(result.Errors);
        //}

    }
}
