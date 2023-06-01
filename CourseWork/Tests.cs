//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseWork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public partial class Tests
    {
        public string description
        {
            get
            {
                var tasks = App.Context.Tasks.ToList();
                var task = tasks.Where(t => t.task_id == task_id).FirstOrDefault();
                return task.description;
            }
        }
        public string shortDescription
        {
            get
            {
                var task = App.Context.Tasks.Where(t => t.task_id == task_id).FirstOrDefault();
                var tsd = task.description.Substring(0, Math.Min(task.description.Length, 100));
                if (tsd != task.description)
                {
                    tsd += "...";
                }
                return tsd;
            }
        }
        public int test_id { get; set; }
        public int task_id { get; set; }
        public string input { get; set; }
        public string output { get; set; }
    
        public virtual Tasks Tasks { get; set; }
    }
}