using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NitaVision.SPI.Entity
{
    [Serializable]
    public class Result<T>
    {
        private string state;
        private T data;
        private string errmsg = string.Empty;
        private string message = string.Empty;
        private Exception exception = null;


        [DataMember(Name = "state")]
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        [DataMember(Name = "data")]
        public T Data
        {
            get { return data; }
            set { data = value; }
        }
        [DataMember(Name = "errmsg")]
        public String Errmsg
        {
            get { return errmsg; }
            set { errmsg = value; }
        }
        [DataMember(Name = "message")]
        public String Message
        {
            get { return message; }
            set { message = value; }
        }
        [DataMember(Name = "exception")]
        public Exception Exception
        {
            get { return exception; }
            set { exception = value; }
        }
        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="data">待返回的数据</param>
        /// <returns></returns>
        public static Result<T> Success(T data, string message = "操作成功")
        {
            Result<T> result = new Result<T>();
            result.state = "1";
            result.data = data;
            result.message = message;
            return result;
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="code">错误码，1:表示成功，非1表示失败</param>
        /// <param name="ex">异常消息</param>
        /// <returns></returns>
        public static Result<T> Fail(string code, Exception ex = null, string errmsg = "操作失败")
        {
            Result<T> result = new Result<T>();
            result.state = code;
            result.data = default(T);
            result.errmsg = errmsg;
            result.exception = ex;
            return result;
        }

    }
}
