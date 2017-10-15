using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuraksApp
{
    public class CommandObject
    {
        public string Name;
        public string Command;

        public CommandObject(string Name, string Command)
        {
            this.Name = Name;
            this.Command = Command;
        }
    }
}
