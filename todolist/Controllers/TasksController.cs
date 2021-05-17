using Microsoft.AspNetCore.Mvc;
using System;
using todolist.Middleware;
using todolist.Models;
using todolist.Models.Db;

namespace todolist.Controllers
{
    public class TasksController : Controller
    {
        private readonly TodoListDbContext context;

        public TasksController(TodoListDbContext context)
        {
            this.context = context;
        }

        //
        // GET: /Tasks/Edit/
        [SignedIn]
        public IActionResult Edit(int? id, [FromQuery(Name = "TodoListId")] int todoListId)
        {
            if (id == null)
            {
                TodoTask task = new TodoTask();
                task.TodoListId = todoListId;
                return View(task);
            }
            else
            {
                return View(context.Tasks.Find(id));
            }
        }

        //
        // POST: /Tasks/Edit/
        [HttpPost]
        [SignedIn]
        public IActionResult Edit(int? id, [Bind("Name,Priority,Deadline,TodoListId")] TodoTask model)
        {
            if (ModelState.IsValid)
            {
                TodoTask task;
                if (id == null)
                {
                    task = new TodoTask();
                    task.TodoListId = model.TodoListId;
                    task.Created = DateTime.Now;
                    task.IsCompleted = false;
                }
                else
                {
                    task = context.Tasks.Find(id);
                }
                task.Name = model.Name;
                task.Priority = model.Priority;
                task.Deadline = model.Deadline;
                task.Modified = DateTime.Now;

                if (id == null)
                {
                    context.Tasks.Add(task);
                }
                else
                {
                    context.Tasks.Update(task);
                }
                context.SaveChanges();

                return RedirectToAction("Manage", "TodoLists", new { id = task.TodoListId });
            }

            return View(model);
        }

        //
        // GET: /Tasks/Delete/
        [SignedIn]
        public IActionResult Delete(int id)
        {
            return View(context.Tasks.Find(id));
        }

        //
        // POST: /Tasks/Delete/
        [HttpPost, ActionName("Delete")]
        [SignedIn]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = context.Tasks.Find(id);
            context.Tasks.Remove(task);
            context.SaveChanges();
            return RedirectToAction("Manage", "TodoLists", new { id = task.TodoListId });
        }
    }
}
