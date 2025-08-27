using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurningObjects
{
    public class Page : ITurnable
    {
        public string Turn()
        {
            return "Page - You Grab Any Edge Of The Page And Drag It To The Left Or Right.";
        }
    }
}
