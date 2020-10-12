using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBox.Models
{

    public class TagRootobject
    {
        public TagData data { get; set; }
    }

    public class TagData
    {
        public Tags tags { get; set; }
    }

    public class Tags
    {
        public Tag[] values { get; set; }
    }

    public class Tag
    {
        public int id { get; set; }
        public string image { get; set; }
    }

}
