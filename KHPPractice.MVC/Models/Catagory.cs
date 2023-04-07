using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHP.Domain.Model
{
    public class Catagory
    {
        public int CatagoryId { get; set; }

        [Required(ErrorMessage = "Please enter the Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please enter the Description")]
        public string Description { get; set; }
    }
}
