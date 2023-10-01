using NitaVision.SPI.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.SPI.Entity
{
    // 文件
    public class VideoFile
    {
        /// <summary>
        /// 文件ID，唯一标识一个文件
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 视频源文件路径，存储视频源文件的位置
        /// </summary>
        public string SourcePath { get; set; }
        /// <summary>
        /// 转换后视频路径，存储转换后视频的位置
        /// </summary>
        public string ConvertedPath { get; set; }
        /// <summary>
        /// 最后一次解析时间，记录视频文件最后一次被解析的时间
        /// </summary>
        public DateTime LastParsedTime { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName; 
        /// <summary>
        /// 时长
        /// </summary>
        public TimeSpan Duration;
        /// <summary>
        /// 状态
        /// </summary>
        public Status Status;

    }
}
