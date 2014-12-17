using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ordering_System.Models;

namespace Ordering_System.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categoryModel = db.Categories.Find(id);
            if (categoryModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] CategoryModel categoryModel)
        {

            if (ModelState.IsValid)
            {
                db.Categories.Add(categoryModel);
                db.SaveChanges();
                TempData["notice"] = "Successfully created!";
                return Json(new { success = true });

            }

            return PartialView(categoryModel);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return PartialView();
            }
            CategoryModel categoryModel = db.Categories.Find(id);
            if (categoryModel == null)
            {
                return PartialView("_error");
            }
            return PartialView(categoryModel);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryModel).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "Data Edited!";
                return Json(new { success = true });
            }
            return PartialView(categoryModel);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return PartialView();
            }
            CategoryModel categoryModel = db.Categories.Find(id);
            if (categoryModel == null)
            {
                return PartialView("_error");
            }
            return PartialView(categoryModel);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryModel categoryModel = db.Categories.Find(id);
            db.Categories.Remove(categoryModel);
            db.SaveChanges();
            TempData["notice"] = "Data Deleted!";
            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public PartialViewResult GetSandwiches()
        {
            ViewBag.Tiffin = "Veggie Sandwiches";
            return PartialView("Create");
        }
        public ActionResult Search(string keyword)
        {
            var data = db.Categories.Where(f => f.CategoryName.Contains(keyword)).ToList();
            return PartialView(data);
        }

    }
}
