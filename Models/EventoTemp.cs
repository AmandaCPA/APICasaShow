using System;

namespace ProjetoShow.Models
{
    public class EventoTemp
    {   
        public int Id {get; set;}
                    

        public string NomeEvento {get; set;}
             
        public int Capacidade{get; set;}
           
        public DateTime Data{get; set;}
        
        public int Valor{get; set;}

        public int  IdCasaShow{get; set;}
       
        public string Genero {get; set;}  

        public int Ingressos {get; set;}       
    }
}