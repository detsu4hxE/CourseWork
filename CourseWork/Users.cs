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
    using System.IO;
    using System.Linq;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Answers = new HashSet<Answers>();
            this.History = new HashSet<History>();
        }
        public int user_id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }
        public string surname { get; set; }
        public string firstname { get; set; }
        public string patronymic { get; set; }
        public string email { get; set; }
        public string image { get; set; }
        public string roleName
        {
            get
            {
                var roles = App.Context.Roles.ToList();
                var role = roles.Where(r => r.role_id == role_id).FirstOrDefault();
                return role.name;
            }
        }
        public string correctimage
        {
            get
            {
                string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
                if (String.IsNullOrEmpty(image) || String.IsNullOrWhiteSpace(image) || image == null)
                {
                    return path + "default_ava.png";
                }
                else
                {
                    return path + image;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answers> Answers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History> History { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
