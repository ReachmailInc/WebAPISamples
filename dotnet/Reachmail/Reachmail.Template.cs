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
{{#Modules}}
            {{Name}} = new {{Name}}();
{{/Modules}}        
        }
        
{{#Modules}}
        public {{Name}} {{Name}} { get; private set; }
{{/Modules}}
    }    
}
