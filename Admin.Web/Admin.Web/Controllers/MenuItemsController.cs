using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin.Web.DAL;

namespace Admin.Web.Controllers
{
    public class MenuItemsController : Controller
    {
        private DbModel db = new DbModel();

        // GET: MenuItems
        public async Task<ActionResult> Index()
        {
            var menuItems = db.MenuItems.Include(m => m.MenuGroup).Include(m => m.MenuItem1);
            return View(await menuItems.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = await db.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // GET: MenuItems/Create
        public ActionResult Create()
        {
            ViewBag.MenuGroupId = new SelectList(db.MenuGroups, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.MenuItems, "Id", "Caption");
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Caption,Route,HasChildren,ClassName,IconName,IsVisible,SortOrder,MenuGroupId,ParentId")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                menuItem.AddedDate = DateTime.Now;
                menuItem.ModifiedDate = null;
                db.MenuItems.Add(menuItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MenuGroupId = new SelectList(db.MenuGroups, "Id", "Name", menuItem.MenuGroupId);
            ViewBag.ParentId = new SelectList(db.MenuItems, "Id", "Caption", menuItem.ParentId);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = await db.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuGroupId = new SelectList(db.MenuGroups, "Id", "Name", menuItem.MenuGroupId);
            ViewBag.ParentId = new SelectList(db.MenuItems, "Id", "Caption", menuItem.ParentId);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Caption,Route,HasChildren,ClassName,IconName,IsVisible,SortOrder,MenuGroupId,ParentId")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                menuItem.ModifiedDate = DateTime.Now;
                db.Entry(menuItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MenuGroupId = new SelectList(db.MenuGroups, "Id", "Name", menuItem.MenuGroupId);
            ViewBag.ParentId = new SelectList(db.MenuItems, "Id", "Caption", menuItem.ParentId);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = await db.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MenuItem menuItem = await db.MenuItems.FindAsync(id);
            db.MenuItems.Remove(menuItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
