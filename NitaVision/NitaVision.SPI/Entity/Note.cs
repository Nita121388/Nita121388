using NitaVision.SPI.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.SPI.Entity
{
    // 学习笔记
    public class Note
    {
        public long Id { get; set; }
        public string NotePath { get; set; }
        public DateTime CreatedTime { get; set; }
        public long StudyItemId { get; set; }
        public SourceType SourceType { get; set; }
    }
}
