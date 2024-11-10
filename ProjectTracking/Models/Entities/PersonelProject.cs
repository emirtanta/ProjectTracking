using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTracking.Models.Entities
{
    public class PersonelProject
    {
        //null hatalarını önlemek için tanımlandı
        public PersonelProject()
        {
            this.PersonnelInformations = new HashSet<PersonnelInformation>();
        }

        [Key]
        public int PersonelProjectId { get; set; }

        [Required(ErrorMessage ="Proje başlığı zorunludur!")]
        [Display(Name ="Proje Başlığı")]
        [StringLength(50,ErrorMessage ="Proje başlığı 50 karakterden fazla olamaz!")]
        public string ProjectTitle { get; set; }

        [Display(Name ="Proje Açıklaması")]
        [StringLength(500)]
        public string ProjectDescription { get; set; }

        [Display(Name ="Oluşturulma Tarihi")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name ="Öncelik Durumu")]
        public string PriorityStatus { get; set; }

        [Display(Name = "Tamamlanma Oranı")]
        public int CompletionRate { get; set; }

        [Display(Name = "Tamamlanma Tarihi")]
        public DateTime? CompletionDate { get; set; }

        [Display(Name = "Tamamlanma Durumu")]
        public bool CompletionStatus { get; set; }
        public virtual ICollection<PersonnelInformation> PersonnelInformations { get; set; }
    }
}