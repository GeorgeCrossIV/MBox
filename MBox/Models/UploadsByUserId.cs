using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBox.Models
{
    public class UploadsByUserIdRootObject
    {
        public Error[] errors { get; set; }
        public UploadsByUserIdData data { get; set; }
    }

    public class UploadsByUserIdData
    {
        public Users users { get; set; }
        public Uploads uploads { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public string[] path { get; set; }
        public Extensions extensions { get; set; }
    }

    public class Extensions
    {
        public string classification { get; set; }
    }

}
