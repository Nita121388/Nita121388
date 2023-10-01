using NitaVision.SPI.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.SPI.Entity
{
    //收藏
    public class Collection
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public string Phrase { get; set; }
        public string Answer { get; set; }
        public long SubtitleFileId { get; set; }
        public SourceType SourceType { get; set; }
        public long SourceId { get; set; }
        public bool IsExistsAudio { get; set; }
        public long AudioId { get; set; }
    }
}
