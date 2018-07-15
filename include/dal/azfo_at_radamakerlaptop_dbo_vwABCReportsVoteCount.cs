using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace runnerDotNet
{
    public class dalTable_azfo_at_radamakerlaptop_dbo_vwABCReportsVoteCount : tDALTable
    {
        public XVar id;
        public XVar record;
        public XVar batch_number;
        public XVar species;
        public XVar date_submitted;
        public XVar date_observed;
        public XVar location;
        public XVar url1;
        public XVar url2;
        public XVar url3;
        public XVar url4;
        public XVar secretary_comments;
        public XVar secretary_private_comments;
        public XVar date_approved;
        public XVar accept;
        public XVar nonaccept;
        public XVar status;
        public XVar originalObserver;
        public XVar submittedby;
        public XVar additionalObservers;
        public XVar numberObserved;
        public XVar countyCode;
        public XVar beginDate;
        public XVar endDate;
        public XVar age;
        public XVar sex;
        public XVar plumage;
        public XVar subspecies;
        public XVar photo;
        public XVar video;
        public XVar audio;
        public XVar comments;
        public XVar NABReference;
        public XVar hide;
        public XVar parent_batch_number;
        public XVar published;
        public XVar publication;
        public XVar votingCycle;
        public XVar groupNumber;
        public XVar Votecount;
        public static void Init()
        {
            XVar dalTablevwABCReportsVoteCount = XVar.Array();
            dalTablevwABCReportsVoteCount["id"] = new XVar("type", 3, "varname", "id");
            dalTablevwABCReportsVoteCount["record"] = new XVar("type", 202, "varname", "record");
            dalTablevwABCReportsVoteCount["batch_number"] = new XVar("type", 202, "varname", "batch_number");
            dalTablevwABCReportsVoteCount["species"] = new XVar("type", 202, "varname", "species");
            dalTablevwABCReportsVoteCount["date_submitted"] = new XVar("type", 135, "varname", "date_submitted");
            dalTablevwABCReportsVoteCount["date_observed"] = new XVar("type", 135, "varname", "date_observed");
            dalTablevwABCReportsVoteCount["location"] = new XVar("type", 202, "varname", "location");
            dalTablevwABCReportsVoteCount["url1"] = new XVar("type", 202, "varname", "url1");
            dalTablevwABCReportsVoteCount["url2"] = new XVar("type", 202, "varname", "url2");
            dalTablevwABCReportsVoteCount["url3"] = new XVar("type", 202, "varname", "url3");
            dalTablevwABCReportsVoteCount["url4"] = new XVar("type", 202, "varname", "url4");
            dalTablevwABCReportsVoteCount["secretary_comments"] = new XVar("type", 202, "varname", "secretary_comments");
            dalTablevwABCReportsVoteCount["secretary_private_comments"] = new XVar("type", 202, "varname", "secretary_private_comments");
            dalTablevwABCReportsVoteCount["date_approved"] = new XVar("type", 135, "varname", "date_approved");
            dalTablevwABCReportsVoteCount["accept"] = new XVar("type", 11, "varname", "accept");
            dalTablevwABCReportsVoteCount["nonaccept"] = new XVar("type", 11, "varname", "nonaccept");
            dalTablevwABCReportsVoteCount["status"] = new XVar("type", 202, "varname", "status");
            dalTablevwABCReportsVoteCount["originalObserver"] = new XVar("type", 202, "varname", "originalObserver");
            dalTablevwABCReportsVoteCount["submittedby"] = new XVar("type", 202, "varname", "submittedby");
            dalTablevwABCReportsVoteCount["additionalObservers"] = new XVar("type", 202, "varname", "additionalObservers");
            dalTablevwABCReportsVoteCount["numberObserved"] = new XVar("type", 202, "varname", "numberObserved");
            dalTablevwABCReportsVoteCount["countyCode"] = new XVar("type", 202, "varname", "countyCode");
            dalTablevwABCReportsVoteCount["beginDate"] = new XVar("type", 135, "varname", "beginDate");
            dalTablevwABCReportsVoteCount["endDate"] = new XVar("type", 135, "varname", "endDate");
            dalTablevwABCReportsVoteCount["age"] = new XVar("type", 202, "varname", "age");
            dalTablevwABCReportsVoteCount["sex"] = new XVar("type", 202, "varname", "sex");
            dalTablevwABCReportsVoteCount["plumage"] = new XVar("type", 202, "varname", "plumage");
            dalTablevwABCReportsVoteCount["subspecies"] = new XVar("type", 202, "varname", "subspecies");
            dalTablevwABCReportsVoteCount["photo"] = new XVar("type", 11, "varname", "photo");
            dalTablevwABCReportsVoteCount["video"] = new XVar("type", 11, "varname", "video");
            dalTablevwABCReportsVoteCount["audio"] = new XVar("type", 11, "varname", "audio");
            dalTablevwABCReportsVoteCount["comments"] = new XVar("type", 202, "varname", "comments");
            dalTablevwABCReportsVoteCount["NABReference"] = new XVar("type", 202, "varname", "NABReference");
            dalTablevwABCReportsVoteCount["hide"] = new XVar("type", 11, "varname", "hide");
            dalTablevwABCReportsVoteCount["parent_batch_number"] = new XVar("type", 202, "varname", "parent_batch_number");
            dalTablevwABCReportsVoteCount["published"] = new XVar("type", 11, "varname", "published");
            dalTablevwABCReportsVoteCount["publication"] = new XVar("type", 202, "varname", "publication");
            dalTablevwABCReportsVoteCount["votingCycle"] = new XVar("type", 202, "varname", "votingCycle");
            dalTablevwABCReportsVoteCount["groupNumber"] = new XVar("type", 202, "varname", "groupNumber");
            dalTablevwABCReportsVoteCount["Votecount"] = new XVar("type", 3, "varname", "Votecount");
            GlobalVars.dal_info["azfo_at_radamakerlaptop_dbo_vwABCReportsVoteCount"] = dalTablevwABCReportsVoteCount;
        }

        public  dalTable_azfo_at_radamakerlaptop_dbo_vwABCReportsVoteCount()
        {
            			this.m_TableName = "dbo.vwABCReportsVoteCount";
            this.m_infoKey = "azfo_at_radamakerlaptop_dbo_vwABCReportsVoteCount";
        }
    }
}