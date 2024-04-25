using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_Carfax.Data.Models
{
    public class ServiceRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int MileageAtService { get; set; }
        public double Cost { get; set; }
    }
}
