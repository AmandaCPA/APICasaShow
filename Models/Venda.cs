using System;

namespace ProjetoShow.Models
{
    public class Venda 
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }          
        public int Valor { get; set; }
    }
}