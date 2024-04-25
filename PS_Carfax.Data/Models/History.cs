using Microsoft.EntityFrameworkCore;
using PS_Carfax.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_Carfax.Data.Models
{
    public class History
    {
        public int Id { get; set; }
        [ForeignKey("VIN")]
        [ValidVIN]
        public string VIN { get; set; }
        public Vehicle Vehicle { get; set; }
        public int YearGenerated { get; set; }
        public ICollection<Owner> Owners { get; set; }
        public ICollection<Accident> Accidents { get; set; }
        public ICollection<ServiceRecord> ServiceRecords { get; set; }

        public History()
        {
            Owners = new List<Owner>();
            Accidents = new List<Accident>();
            ServiceRecords = new List<ServiceRecord>();
        }

        public void AddOwner(Owner owner)
        {
            Owners.Add(owner);
        }

        public void AddAccident(Accident accident)
        {
            Accidents.Add(accident);
        }

        public void AddServiceRecord(ServiceRecord serviceRecord)
        {
            ServiceRecords.Add(serviceRecord);
        }
    }
}
