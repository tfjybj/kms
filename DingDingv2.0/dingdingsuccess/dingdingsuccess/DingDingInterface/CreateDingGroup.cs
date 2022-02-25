using Newtonsoft.Json;
using System;
using dingdingsuccess.Log4;


namespace dingdingsuccess
{
    /// <summary>
    /// 钉钉创建群接口
    /// </summary>
    public class CreateDingGroup
    {
        /// <summary>
        /// 创建钉钉群
        /// </summary>
        /// <param name="code">userid</param>
        /// <returns>群ID</returns>
        public string CreateMessageGroup(string code)
        {
            //DingMessageModel model = new DingMessageModel();

            //model.dingIds =new string[] {code, };
            //model.messageUrl = string.Format("http://d-kms.tfjybj.com/CallRoom.aspx?{0}", CalendarEventId);
            //model.groupName = "会议室预约";
            //model.messageTitle = "可以看看有没有您想要的会议室";
            //model.messageContent = "请点击预约会议室";
            //model.picUrl = string.Format("http://d-kms.tfjybj.com/CallRoom.aspx?{0}", CalendarEventId);
            //model.sender = "ceshi";


            string url = String.Format("https://oapi.dingtalk.com/chat/create?access_token={0}", AccessToken.GetAccessToken());
            CreateGroupEntity createGroupModel = new CreateGroupEntity();
            createGroupModel.name = "会议室预约";
            createGroupModel.useridlist =new string[] { code};
            createGroupModel.owner= code;
            string jsonData = JsonConvert.SerializeObject(createGroupModel);
            HttpHelper helper = new HttpHelper();
            string text=  helper.HttpPost(url, jsonData);
            CreateGroupReturnEntity model = JsonConvert.DeserializeObject<CreateGroupReturnEntity>(text);
            LoggerHelper.Info("创建钉钉群:"+model);
            return model.chatid;
        }


    }
}