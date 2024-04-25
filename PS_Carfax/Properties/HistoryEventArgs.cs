using PS_Carfax.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_Carfax.UI.Properties
{
    public class HistoryEventArgs : EventArgs
    {
        public History History { get; }

        public HistoryEventArgs(History history)
        {
            History = history;
        }
    }
}
