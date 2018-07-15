using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace runnerDotNet
{
    public class dalTable_azfo_at_radamakerlaptop_dbo_tbl_ABCVotes : tDALTable
    {
        public XVar id;
        public XVar record;
        public XVar committee_member;
        public XVar date_voted;
        public XVar vote;
        public XVar comment;
        public static void Init()
        {
            XVar dalTable_ABCVotes = XVar.Array();
            dalTable_ABCVotes["id"] = new XVar("type", 3, "varname", "id");
            dalTable_ABCVotes["record"] = new XVar("type", 202, "varname", "record");
            dalTable_ABCVotes["committee_member"] = new XVar("type", 202, "varname", "committee_member");
            dalTable_ABCVotes["date_voted"] = new XVar("type", 135, "varname", "date_voted");
            dalTable_ABCVotes["vote"] = new XVar("type", 202, "varname", "vote");
            dalTable_ABCVotes["comment"] = new XVar("type", 202, "varname", "comment");
            GlobalVars.dal_info["azfo_at_radamakerlaptop_dbo_tbl_ABCVotes"] = dalTable_ABCVotes;
        }

        public  dalTable_azfo_at_radamakerlaptop_dbo_tbl_ABCVotes()
        {
            			this.m_TableName = "dbo._ABCVotes";
            this.m_infoKey = "azfo_at_radamakerlaptop_dbo_tbl_ABCVotes";
        }
    }
}