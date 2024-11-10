using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTracking.Models.Entities
{
    public class PersonnelInformation
    {
        public PersonnelInformation()
        {
            this.PersonelProjects = new HashSet<PersonelProject>();   
        }

        [Key]
        public int PersonelInformationId { get; set; }

        [Display(Name ="Resim")]
        [StringLength(1000)]
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Kimlik no alanı zorunludur")]
        [Display(Name ="Kimlik No")]
        [StringLength(15,ErrorMessage ="Kimlik numarası 15 karakterden fazla olamaz")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage ="Ad alanı zorunludur")]
        [Display(Name = "Ad")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [Display(Name = "Soyad")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime Birthdate { get; set; }

        [Display(Name = "E-Posta")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress,ErrorMessage ="Geçerli bir email adresi girin")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Medeni hal alanı zorunludur")]
        [Display(Name = "Medeni Hali")]
        [StringLength(50)]
        public string CivilStatus { get; set; }

        [Display(Name = "Bölüm")]
        [StringLength(50)]
        public string Department { get; set; }

        [Display(Name = "Telefon")]
        [StringLength(15, ErrorMessage = "Telefon 15 karakterden fazla olamaz")]
        public string Phone { get; set; }

        [Display(Name = "Görevi")]
        [StringLength(150)]
        public string Task { get; set; }

        [Display(Name = "Adres")]
        [StringLength(50)]
        public string Address { get; set; }

        [Display(Name = "Yetki")]
        [StringLength(50)]
        public string Authority { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(500)]
        public string Description { get; set; }
        

        [Display(Name ="Yakınlık Bilgisi")]
        [StringLength(50)]
        public string IntimateKnowledge { get; set; }

        [Display(Name = "Yakın Kimlik No")]
        [StringLength(15)]
        public string RelativeIdentityNumber { get; set; }

        [Display(Name = "Yakın Ad")]
        [StringLength(15)]
        public string RelativeName { get; set; }

        [Display(Name = "Yakın Soyad")]
        [StringLength(15)]
        public string RelativeSurname { get; set; }

        [Display(Name = "Yakın Telefon No")]
        [StringLength(15, ErrorMessage = "Telefon 15 karakterden fazla olamaz")]
        public string RelativePhone { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        public DateTime? Hiredate { get; set; }
        public virtual ICollection<PersonelProject> PersonelProjects { get; set; }
    }
}