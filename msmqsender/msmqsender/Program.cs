using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
//using System.Runtime.Serialization; //unnecessary for this example at least
using System.Xml.Serialization; //needed for XmlSerializer
using System.IO; //needed for TextWriter

/*
 * Based off of the tutorial found here: http://fransiscuss.com/2012/06/01/msmq-basic-tutorial/
 * @bccain
 * 
 * Rewritten based of the following tutorial found here: http://support.microsoft.com/kb/815811
 * @hyates
 * 
 * Xml Serialization : http://msdn.microsoft.com/en-us/library/58a18dwa.aspx 
 * @csalazar
 */

namespace msmqsender
{

    public struct Plan
    {
        public int ID;
        public string planName;
        public int degreeProgramID;
        public int userID;
        public int semesterID;
    }

    class Program
    {

        //private List<String> serializeThis = null;
        //private XmlSerializer xs = null;
        //private TextWriter writer = null;

//        public Program()
//        {
//            serializeThis = new List<String>();
//            serializeThis.Add("one");
//            serializeThis.Add("two");
//            serializeThis.Add("three");
//
//            xs = new XmlSerializer(typeof(List<String>));
//            writer = new StringWriter(); //Instead of StreamWriter, we want to write to a string!
//        }

        private const string MESSAGE_QUEUE = @".\Private$\servicequeue";
        private MessageQueue _queue;


        //Method to send the struct using MSMQ
        private void sendStruct(Plan mystruct)
        {
            _queue = new MessageQueue(MESSAGE_QUEUE);
            Message msg = new Message();
            msg.Body = mystruct;
            _queue.Send(msg);
        }

        static void Main(string[] args)
        {
            List<Plan> planLst = new List<Plan>();

            for (int i = 0; i < 30; i++)
            {
                //Define a instance of DummyStruct
                Plan ds = new Plan();
                ds.ID = i;
                ds.planName = "The Plan " + i;
                ds.degreeProgramID = 1337;
                ds.userID = 1984;
                ds.semesterID = 2013;
                planLst.Add(ds);
            }

            //Send that instance of DummyStruct
            Program p = new Program();
            Console.WriteLine("Sending");
            //p.xs.Serialize(p.writer, p.serializeThis);
            //msg = p.writer.ToString();
            
            foreach (Plan pl in planLst)
            {
                p.sendStruct(pl);
            }
        }
    }

}
