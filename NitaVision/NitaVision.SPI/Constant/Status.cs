using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.SPI.Constant
{
    public enum Status
    {
        Undo, //未解析
        Resolving, //解析中
        Failed, //解析失败
        Resolved, //已解析
        New, //未学习
        Ing, //学习中
        Playing, //学习中
        Down, //已完成
    }
}
