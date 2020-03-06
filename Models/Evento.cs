using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoShow.Models
{
    public class Evento
    {
        [Required]
        public int Id {get; set;}

        [Required(ErrorMessage="Campo obrigatório")]
        [Display(Name = "Nome do Evento")]
        public string NomeEvento {get; set;}

        [Required(ErrorMessage="Campo obrigatório")]      
        public int Capacidade{get; set;}

        [Required(ErrorMessage="Campo obrigatório")]       
        public DateTime Data{get; set;}

        [Required(ErrorMessage="Campo obrigatório")]      
        public int Valor{get; set;}

        [Required(ErrorMessage="Campo obrigatório")]     
        [Display(Name = "Local")] 
        public CasaShow CasaShow {get; set;}

       [Required(ErrorMessage="Campo obrigatório")]   
       [Display(Name = "Gênero")]
        public string Genero {get; set;}  

        public int Ingressos {get; set;}                 
    }
}