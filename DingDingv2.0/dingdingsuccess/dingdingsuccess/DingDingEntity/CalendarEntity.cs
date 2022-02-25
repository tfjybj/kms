using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess
{

    public class CalendarEntity
    {
        public string CorpId { get; set; }
        public string EventType { get; set; }
        public long EventTime { get; set; }
        public string CalendarEventId { get; set; }
        public string[] UnionIdList { get; set; }
        public string ChangeType { get; set; }
        public string CalendarId { get; set; }
    }

}