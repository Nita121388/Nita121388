using NitaVision.SPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.SPI.SPI
{
    internal interface IVideoFile
    {
        Result<string> AddVideoFile();
        Result<string> RemoveVideoFile();
        Result<int> GetVideoFile();
        Result<int> UpdateVideoFile();
    }
}
