using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurningObjects
{
    public class Leaf : ITurnable
    {
        public string Turn()
        {
            return "Leaf - You Grab The Stem Of The Leaf And Rotate It In A Circular Motion.";
        }
    }
}
