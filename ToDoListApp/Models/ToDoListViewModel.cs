using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoListApp.Models
{
    public class ToDoListViewModel
    {
        
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}