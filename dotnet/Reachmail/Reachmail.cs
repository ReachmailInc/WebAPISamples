using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reachmail
{
    public class Reachmail
    {
        public Reachmail() 
        {
            Administration = new Administration();
            Campaigns = new Campaigns();
            Contacts = new Contacts();
            Content Library = new Content Library();
            Data = new Data();
            Mailings = new Mailings();
            Reports = new Reports();
        }
        
        public Administration Administration { get; private set; }
        public Campaigns Campaigns { get; private set; }
        public Contacts Contacts { get; private set; }
        public Content Library Content Library { get; private set; }
        public Data Data { get; private set; }
        public Mailings Mailings { get; private set; }
        public Reports Reports { get; private set; }
    }    
}
