using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ItemDetails
    {       
        public int Id { get; set; }     
        
        public string? Name { get; set; }
 
        public int ShelfLifeInDays { get; set; }        
       
        public string? Description { get; set; }            
    }
}
