using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.Spark.Runner.REST.DataBricks.Responses
{
    public class State
    {
        public string life_cycle_state { get; set; }
        public string state_message { get; set; }
    }
}
