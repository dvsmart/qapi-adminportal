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
    public class MenuGroupsController : Controller
    {
        private QPocDbEntities db = new QPocDbEntities();

        // GET: MenuGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.MenuGroups.ToListAsync());
        }

        // GET: MenuGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuGroup menuGroup = await db.MenuGroups.FindAsync(id);
            if (menuGroup == null)
            {
                return HttpNotFound();
            }
            return View(menuGroup);
        }

        // GET: MenuGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,IsVisible")] MenuGroup menuGroup)
        {
            if (ModelState.IsValid)
            {
                menuGroup.AddedDate = DateTime.Now;
                menuGroup.ModifiedDate = null;
                db.MenuGroups.Add(menuGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(menuGroup);
        }

        // GET: MenuGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuGroup menuGroup = await db.MenuGroups.FindAsync(id);
            if (menuGroup == null)
            {
                return HttpNotFound();
            }
            return View(menuGroup);
        }

        // POST: MenuGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,IsVisible")] MenuGroup menuGroup)
        {
            if (ModelState.IsValid)
            {
                menuGroup.ModifiedDate = DateTime.Now;
                db.Entry(menuGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuGroup);
        }

        // GET: MenuGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuGroup menuGroup = await db.MenuGroups.FindAsync(id);
            if (menuGroup == null)
            {
                return HttpNotFound();
            }
            return View(menuGroup);
        }

        // POST: MenuGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MenuGroup menuGroup = await db.MenuGroups.FindAsync(id);
            db.MenuGroups.Remove(menuGroup);
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
