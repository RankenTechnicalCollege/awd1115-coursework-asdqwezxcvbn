using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurningObjects
{
    public class Corner : ITurnable
    {
        public string Turn()
        {
            return "Corner - Walk Up To The Just Before The Edge Of The Corner, Turn 90° To The Left Or Right, Then Proceed To Walk Foward.";
        }
    }
}
