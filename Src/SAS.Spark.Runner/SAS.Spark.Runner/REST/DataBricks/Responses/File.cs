using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.Spark.Runner.REST.DataBricks.Responses
{
    public class File
    {
        public string path { get; set; }
        public bool is_dir { get; set; }
        public int file_size { get; set; }
    }
}
