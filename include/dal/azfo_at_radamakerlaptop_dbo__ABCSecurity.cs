using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace runnerDotNet
{
    public class dalTable_azfo_at_radamakerlaptop_dbo_tbl_ABCSecurity : tDALTable
    {
        public XVar ID;
        public XVar username;
        public XVar password;
        public XVar admin;
        public XVar byear;
        public XVar eyear;
        public XVar role;
        public static void Init()
        {
            XVar dalTable_ABCSecurity = XVar.Array();
            dalTable_ABCSecurity["ID"] = new XVar("type", 3, "varname", "ID");
            dalTable_ABCSecurity["username"] = new XVar("type", 202, "varname", "username");
            dalTable_ABCSecurity["password"] = new XVar("type", 202, "varname", "password");
            dalTable_ABCSecurity["admin"] = new XVar("type", 11, "varname", "admin");
            dalTable_ABCSecurity["byear"] = new XVar("type", 3, "varname", "byear");
            dalTable_ABCSecurity["eyear"] = new XVar("type", 3, "varname", "eyear");
            dalTable_ABCSecurity["role"] = new XVar("type", 202, "varname", "role");
            GlobalVars.dal_info["azfo_at_radamakerlaptop_dbo_tbl_ABCSecurity"] = dalTable_ABCSecurity;
        }

        public  dalTable_azfo_at_radamakerlaptop_dbo_tbl_ABCSecurity()
        {
            			this.m_TableName = "dbo._ABCSecurity";
            this.m_infoKey = "azfo_at_radamakerlaptop_dbo_tbl_ABCSecurity";
        }
    }
}