using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace runnerDotNet
{
    public class dalTable_azfo_at_radamakerlaptop_dbo_tbl_ABCReports : tDALTable
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
        public static void Init()
        {
            XVar dalTable_ABCReports = XVar.Array();
            dalTable_ABCReports["id"] = new XVar("type", 3, "varname", "id");
            dalTable_ABCReports["record"] = new XVar("type", 202, "varname", "record");
            dalTable_ABCReports["batch_number"] = new XVar("type", 202, "varname", "batch_number");
            dalTable_ABCReports["species"] = new XVar("type", 202, "varname", "species");
            dalTable_ABCReports["date_submitted"] = new XVar("type", 135, "varname", "date_submitted");
            dalTable_ABCReports["date_observed"] = new XVar("type", 135, "varname", "date_observed");
            dalTable_ABCReports["location"] = new XVar("type", 202, "varname", "location");
            dalTable_ABCReports["url1"] = new XVar("type", 202, "varname", "url1");
            dalTable_ABCReports["url2"] = new XVar("type", 202, "varname", "url2");
            dalTable_ABCReports["url3"] = new XVar("type", 202, "varname", "url3");
            dalTable_ABCReports["url4"] = new XVar("type", 202, "varname", "url4");
            dalTable_ABCReports["secretary_comments"] = new XVar("type", 202, "varname", "secretary_comments");
            dalTable_ABCReports["secretary_private_comments"] = new XVar("type", 202, "varname", "secretary_private_comments");
            dalTable_ABCReports["date_approved"] = new XVar("type", 135, "varname", "date_approved");
            dalTable_ABCReports["accept"] = new XVar("type", 11, "varname", "accept");
            dalTable_ABCReports["nonaccept"] = new XVar("type", 11, "varname", "nonaccept");
            dalTable_ABCReports["status"] = new XVar("type", 202, "varname", "status");
            dalTable_ABCReports["originalObserver"] = new XVar("type", 202, "varname", "originalObserver");
            dalTable_ABCReports["submittedby"] = new XVar("type", 202, "varname", "submittedby");
            dalTable_ABCReports["additionalObservers"] = new XVar("type", 202, "varname", "additionalObservers");
            dalTable_ABCReports["numberObserved"] = new XVar("type", 202, "varname", "numberObserved");
            dalTable_ABCReports["countyCode"] = new XVar("type", 202, "varname", "countyCode");
            dalTable_ABCReports["beginDate"] = new XVar("type", 135, "varname", "beginDate");
            dalTable_ABCReports["endDate"] = new XVar("type", 135, "varname", "endDate");
            dalTable_ABCReports["age"] = new XVar("type", 202, "varname", "age");
            dalTable_ABCReports["sex"] = new XVar("type", 202, "varname", "sex");
            dalTable_ABCReports["plumage"] = new XVar("type", 202, "varname", "plumage");
            dalTable_ABCReports["subspecies"] = new XVar("type", 202, "varname", "subspecies");
            dalTable_ABCReports["photo"] = new XVar("type", 11, "varname", "photo");
            dalTable_ABCReports["video"] = new XVar("type", 11, "varname", "video");
            dalTable_ABCReports["audio"] = new XVar("type", 11, "varname", "audio");
            dalTable_ABCReports["comments"] = new XVar("type", 202, "varname", "comments");
            dalTable_ABCReports["NABReference"] = new XVar("type", 202, "varname", "NABReference");
            dalTable_ABCReports["hide"] = new XVar("type", 11, "varname", "hide");
            dalTable_ABCReports["parent_batch_number"] = new XVar("type", 202, "varname", "parent_batch_number");
            dalTable_ABCReports["published"] = new XVar("type", 11, "varname", "published");
            dalTable_ABCReports["publication"] = new XVar("type", 202, "varname", "publication");
            dalTable_ABCReports["votingCycle"] = new XVar("type", 202, "varname", "votingCycle");
            dalTable_ABCReports["groupNumber"] = new XVar("type", 202, "varname", "groupNumber");
            GlobalVars.dal_info["azfo_at_radamakerlaptop_dbo_tbl_ABCReports"] = dalTable_ABCReports;
        }

        public  dalTable_azfo_at_radamakerlaptop_dbo_tbl_ABCReports()
        {
            			this.m_TableName = "dbo._ABCReports";
            this.m_infoKey = "azfo_at_radamakerlaptop_dbo_tbl_ABCReports";
        }
    }
}