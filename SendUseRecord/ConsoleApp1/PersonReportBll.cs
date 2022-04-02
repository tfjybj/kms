using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

using 定时任务;

namespace ConsoleApp1
{

    public  class PersonReportBll
    {
      
        PersonReportDAL prd = new PersonReportDAL();

        #region 7天需要发送报表的用户，把报表中没有的用户筛选出来，后面会添加到报表中
        public void RemoveWeekDuplication()
        {
            
            PersonReportDAL prd = new PersonReportDAL();
                
            Dictionary<string,string> caldar_id = prd.WeekddID();//获取需要推送周报的数据用户
            
             Dictionary<string ,string > report_id = prd.Report();//报表中所有的钉钉id
            //Dictionary<string ,string > sameID = null;//存放两个表中相同的数据id
           
            //把caldar表和report表中相同的数据，从caldar表中删除
            foreach (var item in caldar_id.ToArray ())
            {
                if (report_id.ContainsKey(item.Key ))
                {
                    caldar_id.Remove(item.Key );
                }
                LoggerHelper.Info("周报 相同数据移除：" + caldar_id + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数:" + LoggerHelper.GetLineNum());
            }
            
            
            //两个表不同的数据添加到report表中
            foreach (var item in caldar_id)
            {
                prd.AddOrganizerID(item.Key,item.Value  );
                LoggerHelper.Info("周报 添加不同的钉id：" + caldar_id + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数:" + LoggerHelper.GetLineNum());
            }
            
        }
        #endregion

        #region 30天需要发送报表的用户，把报表中没有的用户筛选出来，后面会添加到报表中
        public void RemoveMonthDuplication()
        {
            Dictionary<string, string> caldar_id = prd.MonthddID();//获取日程表中需要推送月报的用户
            Dictionary<string, string> report_id = prd.Report();//报表中所有的钉钉id
           // List<string> sameID = null;//存放两个集合重复的数据
           //把caldar表和report表中相同的数据，从caldar表中删除
            foreach (var item in caldar_id.ToArray ())
            {
                if (report_id.ContainsKey(item.Key))
                {
                    caldar_id.Remove(item.Key);
                }
                LoggerHelper.Info("月报 相同数据移除：" + caldar_id + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数:" + LoggerHelper.GetLineNum());
            }
            //两个表不同的数据添加到report表中
            foreach (var item in caldar_id)
            {
                prd.Month(item.Key);
                LoggerHelper.Info("月报 添加不同的钉id：" + caldar_id + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数:" + LoggerHelper.GetLineNum());
            }
        }
        #endregion
    }
}
