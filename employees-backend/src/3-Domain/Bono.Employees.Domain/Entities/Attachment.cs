using System;
using System.Collections.Generic;
using System.Text;

namespace Bono.Employees.Domain.Entities
{
    public class Attachment
    {
        public string FileName { get; set; }

        public string MimeType { get; set; }

        public byte[] File { get; set; }

        public Attachment()
        {
        }

        public Attachment(string FileName, string MimeType, byte[] File)
        {
            this.FileName = FileName;
            this.MimeType = MimeType;
            this.File = File;
        }
    }
}
