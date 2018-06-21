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
    public class TaskPriorityController : Controller
    {
        private QPocDbEntities db = new QPocDbEntities();

        // GET: TaskPriority
        public async Task<ActionResult> Index()
        {
            return View(await db.TaskPriorities.ToListAsync());
        }

        // GET: TaskPriority/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskPriority taskPriority = await db.TaskPriorities.FindAsync(id);
            if (taskPriority == null)
            {
                return HttpNotFound();
            }
            return View(taskPriority);
        }

        // GET: TaskPriority/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskPriority/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AddedDate,ModifiedDate,Name")] TaskPriority taskPriority)
        {
            if (ModelState.IsValid)
            {
                taskPriority.AddedDate = DateTime.Now;
                taskPriority.ModifiedDate = null;
                db.TaskPriorities.Add(taskPriority);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taskPriority);
        }

        // GET: TaskPriority/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskPriority taskPriority = await db.TaskPriorities.FindAsync(id);
            if (taskPriority == null)
            {
                return HttpNotFound();
            }
            return View(taskPriority);
        }

        // POST: TaskPriority/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AddedDate,ModifiedDate,Name")] TaskPriority taskPriority)
        {
            if (ModelState.IsValid)
            {
                taskPriority.ModifiedDate = DateTime.Now;
                db.Entry(taskPriority).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskPriority);
        }

        // GET: TaskPriority/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskPriority taskPriority = await db.TaskPriorities.FindAsync(id);
            if (taskPriority == null)
            {
                return HttpNotFound();
            }
            return View(taskPriority);
        }

        // POST: TaskPriority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaskPriority taskPriority = await db.TaskPriorities.FindAsync(id);
            db.TaskPriorities.Remove(taskPriority);
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
