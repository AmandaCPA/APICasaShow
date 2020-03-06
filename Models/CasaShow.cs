using System.ComponentModel.DataAnnotations;

namespace ProjetoShow.Models
{
    public class CasaShow
    {
        [Required]
        public int Id {get; set;}

        [Required(ErrorMessage="Campo obrigatório")]
        public string Nome {get; set;}

        [Required(ErrorMessage="Campo obrigatório")]
        public string Endereco {get; set;}
    }
}