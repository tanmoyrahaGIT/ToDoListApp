using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    public class ToDoListController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            IList<ToDoListViewModel> list = null;

            using(var ctx = new ToDoListDBEntities())
            {
                list = ctx.tblToDoLists.Select(l => new ToDoListViewModel()
                {
                    ID = l.ID,
                    Title = l.Title,
                    Description = l.Description,
                    CreatedOn = l.CreatedOn
                }).ToList();
            }

            if(list == null)
            {
                return NotFound();
            }

            return Ok(list);


            
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            ToDoListViewModel _ToDoList = null;

            using (var ctx = new ToDoListDBEntities())
            {
                _ToDoList = ctx.tblToDoLists
                    .Where(s => s.ID == id)
                    .Select(s => new ToDoListViewModel()
                    {
                        ID = s.ID,
                        Title = s.Title,
                        Description = s.Description,
                        CreatedOn = s.CreatedOn
                    }).FirstOrDefault<ToDoListViewModel>();
            }

            if (_ToDoList == null)
            {
                return NotFound();
            }

            return Ok(_ToDoList);
        }

        // POST api/<controller>
        public IHttpActionResult Post(ToDoListViewModel toDoListViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            bool isSaved = false;
            using (var ctx = new ToDoListDBEntities())
            {
                ctx.tblToDoLists.Add(new tblToDoList()
                {
                    Title = toDoListViewModel.Title,
                    Description = toDoListViewModel.Description,
                    CreatedOn = DateTime.Now

                });

                ctx.SaveChanges();
                isSaved = true;
            }

            if (isSaved)
                return Ok(toDoListViewModel);
            else
                return BadRequest("Error occured while saving data");
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(ToDoListViewModel todoListModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new ToDoListDBEntities())
            {
                var existingRecord = ctx.tblToDoLists.Where(s => s.ID == todoListModel.ID)
                                                        .FirstOrDefault<tblToDoList>();

                if (existingRecord != null)
                {
                    existingRecord.Title = todoListModel.Title;
                    existingRecord.Description = todoListModel.Description;
                    existingRecord.ModifiedOn = DateTime.Now;

                    ctx.SaveChanges();
                }
                else
                {
                    return BadRequest("Record not found");
                }
            }

            return Ok(todoListModel);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            bool isDeleted = false;

            using (var ctx = new ToDoListDBEntities())
            {
                var toDoList = ctx.tblToDoLists
                    .Where(s => s.ID == id)
                    .FirstOrDefault();

                ctx.Entry(toDoList).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
                isDeleted = true;
            }

            if (isDeleted)
            {
                return Ok(isDeleted);
            }
            else
            {
                return BadRequest("Not deleted");
            }

        }
    }
}