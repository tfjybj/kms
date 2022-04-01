/*
 * 创建人：盖鹏军
 * 时间：2022年4月1日10点30分
 * 描述：接收权限用户资源信息
 */
using System.Collections.Generic;

namespace dingdingsuccess.AuthEntity
{
    /// <summary>
    /// 权限用户资源信息
    /// </summary>
    public class AuthUserResourceEntity
    {
        public string code { get; set; }
        public string message { get; set; }
        public UserResourceData data { get; set; }
        public int count { get; set; }
    }

    public class UserResourceData
    {
        public string productEnglishName { get; set; }
        public string tenantId { get; set; }
        public int timeStamp { get; set; }
        public Role[] roles { get; set; }
        public List<Resourcestree> resourcesTree { get; set; }
        public Interfaceresourcelist[] interfaceResourceList { get; set; }
        public Resourcesmaplist resourcesMapList { get; set; }
    }

    public class Resourcesmaplist
    {
        public Module module { get; set; }
        public Page page { get; set; }
        public Interface _interface { get; set; }
    }

    public class Module
    {
        public bool MeetingDetail { get; set; }
        public bool login { get; set; }
        public bool PersonalInformation { get; set; }
        public bool ReceivedLike { get; set; }
        public bool TechnologyCircle { get; set; }
        public bool SentNegotiation { get; set; }
        public bool NewMeeting { get; set; }
        public bool HomePage { get; set; }
        public bool ReceivedNegotiation { get; set; }
        public bool MyProject { get; set; }
        public bool Meeting { get; set; }
        public bool DetailsPage { get; set; }
        public bool RestoreMine { get; set; }
        public bool VideoUpLoad { get; set; }
        public bool VideoPlay { get; set; }
    }

    public class Page
    {
        public bool MeetingDetail { get; set; }
        public bool login { get; set; }
        public bool Brainstorm { get; set; }
        public bool PersonalInformation { get; set; }
        public bool ReceivedLike { get; set; }
        public bool TechnologyCircle { get; set; }
        public bool NewMeeting { get; set; }
        public bool SentNegotiation { get; set; }
        public bool HomePage { get; set; }
        public bool ReceivedNegotiation { get; set; }
        public bool MyProject { get; set; }
        public bool Meeting { get; set; }
        public bool DetailsPage { get; set; }
        public bool RestoreMine { get; set; }
        public bool VideoUpLoad { get; set; }
        public bool VideoPlay { get; set; }
    }

    public class Interface
    {
        public bool _44 { get; set; }
        public bool _45 { get; set; }
        public bool _48 { get; set; }
        public bool _49 { get; set; }
        public bool _111 { get; set; }
        public bool _112 { get; set; }
        public bool _113 { get; set; }
        public bool _114 { get; set; }
        public bool _50 { get; set; }
        public bool _51 { get; set; }
        public bool _52 { get; set; }
        public bool _53 { get; set; }
        public bool _10 { get; set; }
        public bool _54 { get; set; }
        public bool _11 { get; set; }
        public bool _55 { get; set; }
        public bool _56 { get; set; }
        public bool _12 { get; set; }
        public bool _13 { get; set; }
        public bool _14 { get; set; }
        public bool _15 { get; set; }
        public bool _16 { get; set; }
        public bool _17 { get; set; }
        public bool _18 { get; set; }
        public bool _19 { get; set; }
        public bool _1 { get; set; }
        public bool _2 { get; set; }
        public bool _3 { get; set; }
        public bool _4 { get; set; }
        public bool _400 { get; set; }
        public bool _5 { get; set; }
        public bool _6 { get; set; }
        public bool _20 { get; set; }
        public bool _21 { get; set; }
        public bool _22 { get; set; }
        public bool _23 { get; set; }
        public bool _25 { get; set; }
        public bool _26 { get; set; }
        public bool _27 { get; set; }
        public bool _28 { get; set; }
        public bool _29 { get; set; }
        public bool _30 { get; set; }
        public bool _31 { get; set; }
        public bool _32 { get; set; }
        public bool _33 { get; set; }
        public bool _34 { get; set; }
        public bool _35 { get; set; }
        public bool _36 { get; set; }
        public bool _37 { get; set; }
        public bool _38 { get; set; }
        public bool _39 { get; set; }
        public bool _222 { get; set; }
        public bool _223 { get; set; }
        public bool _82 { get; set; }
        public bool _83 { get; set; }
        public bool _84 { get; set; }
        public bool _40 { get; set; }
        public bool _41 { get; set; }
        public bool _42 { get; set; }
        public bool _43 { get; set; }
    }

    public class Role
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Resourcestree
    {
        public string id { get; set; }
        public string productId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int sort { get; set; }
        public string type { get; set; }
        public int state { get; set; }
        public long createTime { get; set; }
        public long updateTime { get; set; }
        public string _operator { get; set; }
        public string pId { get; set; }
        public Child[] child { get; set; }
    }

    public class Child
    {
        public string id { get; set; }
        public string productId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int sort { get; set; }
        public string type { get; set; }
        public int state { get; set; }
        public long createTime { get; set; }
        public long updateTime { get; set; }
        public string _operator { get; set; }
        public string pId { get; set; }
        public Child1[] child { get; set; }
    }

    public class Child1
    {
        public string id { get; set; }
        public string productId { get; set; }
        public string brand { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int sort { get; set; }
        public string type { get; set; }
        public int state { get; set; }
        public long createTime { get; set; }
        public long updateTime { get; set; }
        public string _operator { get; set; }
        public string pId { get; set; }
        public string requestType { get; set; }
        public object[] child { get; set; }
    }

    public class Interfaceresourcelist
    {
        public string uri { get; set; }
        public string type { get; set; }
    }



}