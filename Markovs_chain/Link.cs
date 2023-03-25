using System;

namespace Markovs_chain
{
    internal class Link
    {
        float[] p;
        Random random;
        public Link(float[] p ) { 
            this.p = p;
            random = new Random();
        }
        public int NextElement() {
           
            float r = (float)random.NextDouble();
            float sum = 0;
            for (int i = 0; i < p.Length; i++)
            {
                sum += p[i];
                if(sum >= r)
                {
                    return i;
                } 
            }
            return -1;
        }
    }
}