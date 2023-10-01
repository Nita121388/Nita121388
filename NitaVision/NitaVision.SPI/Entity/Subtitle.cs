using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.SPI.Entity
{
    // 字幕
    public class Subtitle
    {
        public long Id { get; set; }
        public long VideoFileId { get; set; }
        public string Path { get; set; }
        public DateTime LastParsedTime { get; set; }
    }
}
