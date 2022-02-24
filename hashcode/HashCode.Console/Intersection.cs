using System.Collections.Generic;
using System.Linq;

namespace HashCode.Console
{
    public class Intersection
    {
        public int Id { get; set; }
        public List<Street> Incoming { get; set; } = new List<Street>();
        public List<int> GreenSeconds { get; set; } = new List<int>();
        public List<Street> Outgoing { get; set; } = new List<Street>();
        public List<CarPath> Cars { get; set; } = new List<CarPath>();
        public Street GreenStreet { get; set; }

        public Street IsGreen(int t)
        {
            var modT = t % GreenSeconds.Sum();

            var sum = 0;
            for (int i = 0; i < GreenSeconds.Count; i++)
            {
                if (modT < GreenSeconds[i] + sum)
                {
                    return Incoming[i];
                }
                sum += GreenSeconds[i];
            }
            return null;
        }
    }
}
