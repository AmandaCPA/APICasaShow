using System;

namespace ProjetoShow.Models
{
    public class Compra
    {
        public int Id {get; set;}
        public DateTime Data {get; set;}
        public int Quantidade {get; set;}
        public float Valor {get; set;}
        public Evento Evento {get; set;}
    }
}