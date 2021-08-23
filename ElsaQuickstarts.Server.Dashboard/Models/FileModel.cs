using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElsaQuickstarts.Server.Dashboard.Models
{
       public class FileModel
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
    }
}
