using System;
using System.Collections.Generic;
using System.Text;

namespace MyInfoCommonUtility.Configuration
{
    public class AppsettingsConfig
    {
        public MyInfoData MyInfoData { get; set; } = new MyInfoData();
        public EmailSetting EmailSetting { get; set; } = new EmailSetting();
        public bool RequestResponseLoggingEnabled { get; set; }
    }
}