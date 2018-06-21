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
    public class TaskStatusController : Controller
    {
        private QPocDbEntities db = new QPocDbEntities();

        // GET: TaskStatus
        public async Task<ActionResult> Index()
        {
            return View(await db.TaskStatuses.ToListAsync());
        }

        // GET: TaskStatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.TaskStatus taskStatus = await db.TaskStatuses.FindAsync(id);
            if (taskStatus == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus);
        }

        // GET: TaskStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,IsActive")] DAL.TaskStatus taskStatus)
        {
            if (ModelState.IsValid)
            {
                taskStatus.AddedDate = DateTime.Now;
                taskStatus.ModifiedDate = null;
                db.TaskStatuses.Add(taskStatus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taskStatus);
        }

        // GET: TaskStatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.TaskStatus taskStatus = await db.TaskStatuses.FindAsync(id);
            if (taskStatus == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus);
        }

        // POST: TaskStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,IsActive")] DAL.TaskStatus taskStatus)
        {
            if (ModelState.IsValid)
            {
                taskStatus.ModifiedDate = DateTime.Now;
                db.Entry(taskStatus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskStatus);
        }

        // GET: TaskStatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.TaskStatus taskStatus = await db.TaskStatuses.FindAsync(id);
            if (taskStatus == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus);
        }

        // POST: TaskStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DAL.TaskStatus taskStatus = await db.TaskStatuses.FindAsync(id);
            db.TaskStatuses.Remove(taskStatus);
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
