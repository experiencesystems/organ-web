using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Telefone
{
    [Table("tbDDD")]
    public class DDD
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 99)]
        public int Valor { get; set; }
    }
}