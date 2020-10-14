using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBox.Models
{

    public class UploadRootobject
    {
        public UploadData data { get; set; }
    }

    public class UploadData
    {
        public Uploads uploads { get; set; }
    }

    public class Uploads
    {
        public Upload[] values { get; set; }
    }

    public class Upload
    {
        [Key]
        public int id { get; set; }
        public string filename { get; set; }
        public string ip { get; set; }
        public string userid { get; set; }
    }

}
