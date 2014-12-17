using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ordering_System.Models;
using System.IO;

namespace Ordering_System.Areas.Admin.Controllers
{
    public class ItemController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: Admin/Item
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Category);
            return View(items.ToList());
        }

        // GET: Admin/Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemModel itemModel = db.Items.Find(id);
            if (itemModel == null)
            {
                return HttpNotFound();
            }
            return View(itemModel);
        }

        // GET: Admin/Item/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return PartialView();
        }

        // POST: Admin/Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,ItemName,Description,UnitPrice,Discount,Unit,IsActive, CategoryID, ImageUrl")] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(itemModel);
                db.SaveChanges();
                TempData["notice"] = "Item Successfuly Created!";
                return Json(new { success = true });
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", itemModel.CategoryID);
            return PartialView(itemModel);
        }

        // GET: Admin/Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return PartialView();
            }
            ItemModel itemModel = db.Items.Find(id);
            if (itemModel == null)
            {
                return PartialView("_error");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", itemModel.CategoryID);
            return PartialView(itemModel);
        }

        // POST: Admin/Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemName,Description,UnitPrice,Discount,Unit,IsActive,CategoryID,ImageUrl")] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemModel).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "Item Successfuly Edited!";
                return Json(new { success = true });
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", itemModel.CategoryID);
            return PartialView(itemModel);
        }

        // GET: Admin/Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return PartialView();
            }
            ItemModel itemModel = db.Items.Find(id);
            if (itemModel == null)
            {
                return PartialView("_error");
            }
            return PartialView(itemModel);
        }

        // POST: Admin/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemModel itemModel = db.Items.Find(id);
            db.Items.Remove(itemModel);
            db.SaveChanges();
            TempData["notice"] = "Item Successfuly Deleted!";
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
    }
}





