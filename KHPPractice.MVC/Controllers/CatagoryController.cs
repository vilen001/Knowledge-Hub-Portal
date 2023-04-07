using KHP.Data.DataRepository.Interface;
using KHP.Domain.Model;
using KHPPractice.MVC.Data.DataRepository.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KHPPractice.MVC.Controllers
{
   // [Authorize(Roles ="admin")]
    public class CatagoryController : Controller
    {

        //In any Web Application specially in MVC 
        //we are going very frequntly on CRUD operation 
        // C = 2 action method we required and 1 View 
        // R = 1 action method we required and 1 View 
        // U = 2 action method we required and 1 View 
        // D = 2 action method we required and 1 View 

        ICatagoryRepository repo = new CatagoryRepository();

        //Read------------------------------------------------------------------------------------------------------
        //If we want to pass the data from cotroller to view 
        //there is several views 

        //2.ViewBag    = Sends the small Amount of Data
        //1.ViewData   = Sends the small Amount of Data
        //3.TempData   = Sends the small Amount of Data
        //4.ModelData  = Sends the Large Amount of Data
        //[Authorize(Roles = "admin")]
        public IActionResult Index(string searchText = null!) //this is an action method // This is home page of catagory
        {
            List<Catagory> catagories = null!;
            if (searchText != null && searchText.Length>=0)
            {
                catagories = repo.GetSeachCatagories(searchText);
            }else
            {
               catagories = repo.GetCatagories();   
            }
            return View(catagories); // This is method is returing a view
        }
        //Read------------------------------------------------------------------------------------------------------






        //Create ----------------------------------------------------------------------------------------------
        // catagroy/create
        [Authorize]
        public IActionResult  Create()
        {
            return View();
        }

        public IActionResult Save(Catagory catagory)
        {
            //before sending the data to the backened we need to validate it
            if (!ModelState.IsValid)
            {
                // return View(); // it rerturns only save view 
                return View("Create"); //it returns create view
            }
            repo.Save(catagory);
            return View();  
        }
        //Create---------------------------------------------------------------------------------------------------


        //Edit------------------------------------------------------------------------------------------------------

        //asp-route-id id this both should be same
        //Edit(int id) id this both should be same


        //[Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            Catagory cat = repo.GetCatagory(id);
            return View(cat);   
        }
        //[Authorize(Roles = "admin")]
        public IActionResult Update(Catagory catagory)
        {
            if (!ModelState.IsValid && catagory == null)
            {
                return View("Edit");
            }

            repo.Edit(catagory);
            //return View("Index");
            //If we want show temporary message to user then we used the tempdata
            TempData["Message"] = $" {catagory.CatagoryId} Id updated succesfully ";
            return RedirectToAction("Index");
        }
        //Edit------------------------------------------------------------------------------------------------------


        //Delete----------------------------------------------------------------------------------------------------
        public IActionResult Delete(int id)
        {
            repo.DeleteCatagory(id);
            TempData["Message"] = $"{id} deleted";
            return RedirectToAction("Index");   
        }
        

        //public IActionResult Reject(List<int> ArticleID)
        //{
        //    articlesRepo.RejectArticles(ArticleID);
        //    TempData["Message"] = "Articles Rejected";
        //    return RedirectToAction("ReviewArticles");

        //}

    }
}
