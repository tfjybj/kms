/*
 * 创建人：邓礼梅
 * 创建日期：2022年1月11日19:45:39
 * 描述：限制判断
 */
using KmsService.Entity;
using KmsService.Log4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KmsService.KeyBLL.RoomConfigurationHandler
{
    //限制判断类
    class JudgeBLL : RoomConfigurationHandlerBLL
    {
        /// <summary>
        /// 限制判断
        /// </summary>
        /// <param name="basicDataStr">会议室信息字符串</param>
        /// <param name="newBasicData">修改之后的基本数据实体</param>
        /// <param name="oldBasicData">修改前的基本数据实体</param>
        /// <param name="allLockNumber">所有锁号</param>
        /// <returns>bool</returns>
        public override bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber)
        {
            LoggerHelper.Info("【管理员会议室配置】限制判断：" + "要限制的字符串：" + basicDataStr);

            //正则表达式数字字符串
            const string judgeNum = "^[0-9]*$";
            //正则表达式中文字符串
            string judgeCh = "[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）——|{}【】‘；：”“'。，、？]";

            //选择要切割的字符
            char[] chr = new char[] { ' ', ',' };
            //用字符串数组接受split关键字切割之后的会议室信息
            string[] resultt = basicDataStr.Split(chr, StringSplitOptions.RemoveEmptyEntries);

            //正则表达式是否是中文
            System.Text.RegularExpressions.Regex rxCh = new System.Text.RegularExpressions.Regex(judgeCh);
            //正则表达式是否是数字
            System.Text.RegularExpressions.Regex rxNum = new System.Text.RegularExpressions.Regex(judgeNum);

            //判断输入的是否是中文
            if (rxCh.IsMatch(resultt[0]))
            {
                throw new Exception("会议室名称只能输入中文");
            }
            // 判断输入的是否是数字                                               
            else
            {
                for (int i = 1; i < resultt.Length - 2; i++)
                {
                    if ((!rxNum.IsMatch(resultt[i])) || (resultt[i] == ""))
                    {
                        throw new Exception("您输入的不是数字，请重新输入");
                    }
                }
                //调用下一个职责链
                successor.ModifyRoom(basicDataStr, newBasicData, oldBasicData,allLockNumber);
            }
            return true;
        }
    }
}
