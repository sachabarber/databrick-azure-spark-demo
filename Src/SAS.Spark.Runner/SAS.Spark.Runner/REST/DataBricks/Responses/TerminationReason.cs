using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.Spark.Runner.REST.DataBricks.Responses
{
    public class TerminationReason
    {
        public string code { get; set; }
        public Parameters parameters { get; set; }
    }
}
