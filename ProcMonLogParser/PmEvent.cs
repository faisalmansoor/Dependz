using System;
using System.Xml;
using System.Xml.Linq;

namespace ProcMonLogParser
{
    public class PmEvent
    {
        public int ProcessIndex {get;  set;}
        public DateTime Time_of_Day {get;  set;}
        public string Process_Name {get;  set;}
        public int PID {get;  set;}
        public string Operation {get;  set;}
        public string Path {get;  set;}
        public string Result {get;  set;}
        public string Detail {get;  set;}
        public DateTime Date___Time {get;  set;}
        public DateTime Relative_Time {get;  set;}
        public double Duration {get;  set;}
        public string Event_Class {get;  set;}
        public string Image_Path {get;  set;}
        public string Company {get;  set;}
        public string Description {get;  set;}
        public Version Version {get;  set;}
        public string User {get;  set;}
        public string Authentication_ID {get;  set;}
        public string Session {get;  set;}
        public string Command_Line {get;  set;}
        public int TID {get;  set;}
        public bool Virtualized {get;  set;}
        public string Integrity {get;  set;}
        public string Category {get;  set;}
        public int Parent_PID {get;  set;}
        public string Architecture {get;  set;}        

        public static PmEvent Create(XmlReader xEvnt)
        {
            XElement xe = XElement.Load(xEvnt);
            var pmEvent = new PmEvent
                              {
                                  ProcessIndex = xe.Value<int>("ProcessIndex"),
                                  Time_of_Day = xe.Value<DateTime>("Time_of_Day"),
                                  Process_Name = xe.Value<string>("Process_Name"),
                                  PID = xe.Value<int>("PID"),
                                  Operation = xe.Value<string>("Operation"),
                                  Path = xe.Value<string>("Path"),
                                  Result = xe.Value<string>("Result"),
                                  Detail = xe.Value<string>("Detail"),
                                  Date___Time = xe.Value<DateTime>("Date___Time"),
                                  Relative_Time = xe.Value<DateTime>("Relative_Time"),
                                  Duration = xe.Value<double>("Duration"),
                                  Event_Class = xe.Value<string>("Event_Class"),
                                  Image_Path = xe.Value<string>("Image_Path"),
                                  Company = xe.Value<string>("Company"),
                                  Description = xe.Value<string>("Description"),
                                  Version = xe.Value<Version>("Version"),
                                  User = xe.Value<string>("User"),
                                  Authentication_ID = xe.Value<string>("Authentication_ID"),
                                  Session = xe.Value<string>("Session"),
                                  Command_Line = xe.Value<string>("Command_Line"),
                                  TID = xe.Value<int>("TID"),
                                  Virtualized = xe.Value<bool>("Virtualized"),
                                  Integrity = xe.Value<string>("Integrity"),
                                  Category = xe.Value<string>("Category"),
                                  Parent_PID = xe.Value<int>("Parent_PID"),
                                  Architecture = xe.Value<string>("Architecture")
                              };

            return pmEvent;
        }

        public string PathExt
        {
            get
            {
                try
                {
                    return System.IO.Path.GetExtension(Path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\tError: {0}", ex.Message);
                    return null;
                }
            }
        }

        public string PathFileName
        {
            get
            {
                try
                {
                    return System.IO.Path.GetFileName(Path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\tError: {0}", ex.Message);
                    return null;
                }
            }
        }
    }
}
