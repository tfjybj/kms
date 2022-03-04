using FluentScheduler;
using System;
using System.Diagnostics;

namespace KmsService.KeyBLL
{
    public class Demo : IJob
    {
        void IJob.Execute()
        {
            Trace.WriteLine("开始定时任务了，现在时间是：" + DateTime.Now);
        }

        #region 暂时不用

        //void IJob.Execute()
        //{
        //    var str = "循环每2秒执行一次；现在时间是：" + DateTime.Now.ToString();
        //    System.IO.StreamWriter writer = null;
        //    try
        //    {
        //        //写入日志
        //        string path = Server.MapPath("~/logs");

        //        //不存在则创建错误日志文件夹
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        path += string.Format(@"\{0}.txt", DateTime.Now.ToString("yyyy-MM-dd"));
        //        writer = !System.IO.File.Exists(path) ? System.IO.File.CreateText(path) : System.IO.File.AppendText(path); //判断文件是否存在，如果不存在则创建，存在则添加
        //        writer.WriteLine(str);
        //        writer.WriteLine("********************************************************************************************");
        //    }
        //    finally
        //    {
        //        if (writer != null)
        //        {
        //            writer.Close();
        //        }
        //    }
        //}
    }

    #endregion 暂时不用

}