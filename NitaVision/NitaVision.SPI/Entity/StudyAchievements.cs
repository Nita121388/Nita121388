using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.SPI.Entity
{
    // 学习成果
    public class StudyAchievements
    {
        //掌握单词个数
        public int MasteredWordCount { get; set; }
        //学习视频个数
        public int StudiedVideoCount { get; set; }
        //视频学习完成个数
        public int VideoStudiedCompletedCount { get; set; }
        //学习完成/解析占比
        public int CompletedToParsedRatio { get; set; }
        //单词个数
        public int CollectWordCount { get; set; }
        //笔记个数
        public int NoteCount { get; set; }
    }
}
