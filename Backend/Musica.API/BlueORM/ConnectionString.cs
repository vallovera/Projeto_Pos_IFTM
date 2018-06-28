using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web;
//using Classes.Dal;

namespace BlueORM
{
    public class ConnectionString
    {
        private string connString;
        public string ConnString
        {
            get { return connString; }
            set { connString = value; }
        }

        private string providerName;
        public string ProviderName
        {
            get { return providerName; }
            set { providerName = value; }
        }

        public ConnectionString()
        {
            //this.connString = "server=belga.mysql.uhserver.com;User Id=belga_user;password=Belga@13;Persist Security Info=True;database=belga;Convert Zero Datetime=True;SslMode=none";
            this.connString = "server=localhost;User Id=root;password=;Persist Security Info=True;database=musica;Convert Zero Datetime=True;SslMode= none";
            this.providerName = "MySql.Data.MySqlClient";
        }
    }
}
