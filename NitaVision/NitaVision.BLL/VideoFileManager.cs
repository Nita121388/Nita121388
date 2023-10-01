using NitaVision.DAC;
using NitaVision.SPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.BLL
{
    public class VideoFileManager 
    {
        public static VideoFileManager Current;

        private static readonly object locker = new object();

        private VideoFileManager()
        {

        }
        public static VideoFileManager GetInstance()
        {
            if (Current == null)
            {
                lock (locker)
                {
                    if (Current == null)
                    {
                        Current = new VideoFileManager();
                    }
                }
            }
            return Current;
        }
        public void AddVideoFile(VideoFile videoFile)
        {
            try 
            {
                using var db = new VideoFileContext();
                db.Add(videoFile);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"添加视频失败！失败原因：{ex.Message}");
            }
        }
        // 更新视频文件
        public void UpdateVideoFile(VideoFile videoFile)
        {
            try
            {
                using var db = new VideoFileContext();
                // 根据Id查找要更新的视频文件
                var oldVideoFile = db.VideoFile.Find(videoFile.Id);
                if (oldVideoFile == null)
                {
                    throw new Exception($"找不到Id为{videoFile.Id}的视频文件！");
                }
                // 更新视频文件的属性
                oldVideoFile.FileName = videoFile.FileName;
                oldVideoFile.Duration = videoFile.Duration;
                oldVideoFile.Status = videoFile.Status;
                oldVideoFile.SourcePath = videoFile.SourcePath;
                oldVideoFile.ConvertedPath = videoFile.ConvertedPath;
                oldVideoFile.LastParsedTime = videoFile.LastParsedTime;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"更新视频失败！失败原因：{ex.Message}");
            }
        }

        // 删除视频文件
        public void DeleteVideoFile(long id)
        {
            try
            {
                using var db = new VideoFileContext();
                // 根据Id查找要删除的视频文件
                var videoFile = db.VideoFile.Find(id);
                if (videoFile == null)
                {
                    throw new Exception($"找不到Id为{id}的视频文件！");
                }
                // 从数据库中删除视频文件
                db.VideoFile.Remove(videoFile);
                // 保存更改
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"删除视频失败！失败原因：{ex.Message}");
            }
        }
        public long GetMaxID()
        {
            try
            {
                using var db = new VideoFileContext();
                long maxId = db.VideoFile.Max(f => f.Id);
                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception($"获取VideoFile失败！失败原因：{ex.Message}");
            }
        }
    }
}
