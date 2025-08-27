using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurningObjects
{
    public class Pancake : ITurnable
    {
        public string Turn()
        {
            return "Pancake - You Use A Spatula By First Sliding The Flat End Under The Pancake Then Flip The Pancake Over.";
        }
    }
}
