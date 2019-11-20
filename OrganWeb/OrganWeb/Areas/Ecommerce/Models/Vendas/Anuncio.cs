using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using OrganWeb.Areas.Ecommerce.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbAnuncio")]
    public class Anuncio : AnuncioRepository
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome do anúncio")]
        [StringLength(30, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 10)]
        public string Desc { get; set; }
        
        public bool Status { get; set; }
        
        public byte[] Foto { get; set; }

        [Range(0.00, 100.00)]
        [Display(Name = "Desconto (%)")]
        public double Desconto { get; set; }
        
        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        
        [ForeignKey("Usuario")]
        public string IdAnunciante { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        [NotMapped]
        public List<Avaliacao> Avaliacoes { get; set; }

        [NotMapped]
        public Avaliacao Avaliacao { get; set; }

        [NotMapped]
        public int? Estrelas { get; set; }

        [NotMapped]
        public double MediaAvaliacoes { get; set; }
    }
}