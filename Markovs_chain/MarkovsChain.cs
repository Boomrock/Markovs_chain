using System;
using System.Collections.Generic;
using System.Text;

namespace Markovs_chain
{
    internal class MarkovsChain
    {
        int currentElement;
        Link[] links;
        object[] content;
        public MarkovsChain(Link[] links, int startElement = 0, object[] content = null)
        {
            this.links = links;
            this.content = content;
            currentElement = startElement;
        }
        public object next()
        {
            currentElement = links[currentElement].NextElement();
            if (content != null)
                return content[currentElement];
            else
                return currentElement;

        }
  }
}
