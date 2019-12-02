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
        [StringLength(100, MinimumLength = 2)]
        public string Desc { get; set; }
        
        public bool Status { get; set; }
        
        public byte[] Foto { get; set; }

        public double Quantidade { get; set; }

        [Range(0, 100)]
        [Display(Name = "Desconto (%)")]
        public int Desconto { get; set; }

        public int? DuracaoDesc { get; set; }        
        public DateTime? DataDesc { get; set; }
        public DateTime? Data { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        
        [ForeignKey("Anunciante")]
        public string IdAnunciante { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Anunciante Anunciante { get; set; }

        [NotMapped]
        public List<Avaliacao> Avaliacoes { get; set; }

        [NotMapped]
        public List<Comentario> Comentarios { get; set; }

        [NotMapped]
        public Avaliacao Avaliacao { get; set; }

        [NotMapped]
        public int? Estrelas { get; set; }

        [NotMapped]
        public int? NumAvaliacoes { get; set; }

        [NotMapped]
        public double MediaAvaliacoes { get; set; }
    }
}