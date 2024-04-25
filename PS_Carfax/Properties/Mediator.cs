using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_Carfax.UI.Properties
{
    public class Mediator
    {
        public static readonly Mediator Instance = new Mediator();

        private Mediator() { }

        public event EventHandler<HistoryEventArgs> SearchCompleted;

        public void OnSearchCompleted(object sender, HistoryEventArgs e)
        {
            SearchCompleted?.Invoke(sender, e);
        }
    }

}
