//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dotNET.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            this.Characters = new HashSet<Characters>();
        }

        public int playerID { get; set; }
        [DisplayName("Imię")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string firstName { get; set; }
        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string lastName { get; set; }
        [RegularExpression("([0-z]*)\\@([0-z]*)\\.([0-z.]*)", ErrorMessage = "Podaj poprawny adres e-mail")]
        [DisplayName("Adres e-mail")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string email { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Niepoprawny format")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string psd { get; set; }
        public int isAdmin { get; set; }
        public int isBanned { get; set; }
        public string LoginErrorMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Characters> Characters { get; set; }
    }
}
