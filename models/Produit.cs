using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorneRestaurant.models
{
    public class Produit
    {
        public int id { get; set; }                
        public string nom { get; set; }
        public decimal prix { get; set; }
        public string description { get; set; }
        public int categorie_id { get; set; }
        public int quantite { get; set; }          
        public string statut { get; set; }
    }
}
                                                                                                                                                     