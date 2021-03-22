namespace WpfApp1.DataContextEF_Dir
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voiture")]
    public partial class Voiture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Marque { get; set; }

        [Required]
        [StringLength(50)]
        public string Modele { get; set; }

        [StringLength(50)]
        public string Couleur { get; set; }

        public int? VitesseMax { get; set; }

        public decimal? Prix { get; set; }
    }
}
