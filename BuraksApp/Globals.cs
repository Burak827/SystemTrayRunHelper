using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuraksApp
{

    public static class Globals
    {
       public static List<CommandObject> myCommandObjects;

        static void init()
        {

            myCommandObjects = new List<CommandObject>();
        }
    }
}
