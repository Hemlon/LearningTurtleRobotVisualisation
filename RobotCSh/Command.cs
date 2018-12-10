using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Command
    {
        public String Action;
        public double Value;
        public double Value2;

        public Command()
        {
         this.Action = "";
         this.Value = 0.0f;
         this.Value2 = 0.0f;
        }

        public Command(string action, double value)
        {
           new Command(action, value, 0);
        }

        public Command(string action, double value, double value2)
        {
            this.Action = action;
            this.Value2 = 0;
            if (this.Action == "end")
            {
                this.Value = 0;
            }
            else if (this.Action == "repeat")
            {
                int n;
                if (int.TryParse(value.ToString(), out n) == true)
                {
                    this.Value = n;
                }
                else
                {
                    this.Value = 0;
                }
            }
            else
            {
                this.Value = value;
                this.Value2 = value2;
            }

        }

        public Command(Command command)
        {
            new Command(command.Action, command.Value, command.Value2);
        }
    }
}
