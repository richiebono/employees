using System;
using System.ComponentModel.DataAnnotations;


namespace Bono.Employees.Application.ViewModels
{
    public class FilterViewModel
    {        
        public string sort { get; set; }
        public string order { get; set; }
        public string search { get; set; }
        public string type { get; set; }
        public int start { get; set; }
        public int size { get; set; }
        
    }
}
