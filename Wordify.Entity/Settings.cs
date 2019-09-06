using System;
using System.Collections.Generic;
using System.Text;

namespace Wordify.Entity
{
    public class Settings
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string SiteDescription { get; set; }
        public string SmtpAddress { get; set; }
        public string Port { get; set; }
        public string MailUserName { get; set; }
        public string MailPassword { get; set; }
    }
}
