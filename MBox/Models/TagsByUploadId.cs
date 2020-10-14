using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBox.Models
{
    public class TagsByUploadIdRootObject
    {
        public Error[] errors { get; set; }
        public TagsByUploadIdData data { get; set; }
    }

    public class TagsByUploadIdData
    {
        public Uploads uploads { get; set; }
        public Tags tags { get; set; }
    }
}
