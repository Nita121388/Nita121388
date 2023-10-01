using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.SPI.Entity
{
    // 学习记录
    public class Record
    {
        public int Id { get; set; }
        public int StudyItemId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int VideoFileId { get; set; }
    }
}
