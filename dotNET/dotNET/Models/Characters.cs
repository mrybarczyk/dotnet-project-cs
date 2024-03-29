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

    public partial class Characters
    {
        public int characterID { get; set; }
        [DisplayName("Imi�")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string firstName { get; set; }
        [DisplayName("Nazwisko")]
        public string lastName { get; set; }
        [DisplayName("Kampania")]
        public string campaign { get; set; }
        public int playerID { get; set; }
        [DisplayName("Historia")]
        public string historia { get; set; }
        [DisplayName("Uniwersum")]
        public string uniwersum { get; set; }
        [DisplayName("Poziom")]
        public Nullable<int> poziom { get; set; }
        [DisplayName("Wiek")]
        public Nullable<int> wiek { get; set; }
        [DisplayName("Ekwipunek")]
        public string ekwipunek { get; set; }
        [DisplayName("Umiej�tno�ci")]
        public string umiejetnosci { get; set; }

        public virtual Player Player { get; set; }
    }
}
