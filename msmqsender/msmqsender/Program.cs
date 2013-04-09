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

    public struct DummyStruct
    {
        public int ID;
        public string planName;
        public int degreeProgramID;
        public int userID;
        public int semesterID;
    }

    class Program
    {

        private const string MESSAGE_QUEUE = @".\Private$\servicequeue";
        private MessageQueue _queue;


        //Method to send the struct using MSMQ
        private void sendStruct(DummyStruct mystruct)
        {
            _queue = new MessageQueue(MESSAGE_QUEUE);
            Message msg = new Message();
            msg.Body = mystruct;
            _queue.Send(msg);
        }

        static void Main(string[] args)
        {

            //Define a instance of DummyStruct
            DummyStruct ds = new DummyStruct();
            ds.ID = 42;
            ds.planName = "The Plan";
            ds.degreeProgramID = 1337;
            ds.userID = 1984;
            ds.semesterID = 2013;

            //Send that instance of DummyStruct
            Program p = new Program();
            Console.WriteLine("Sending");
            p.sendStruct(ds);


        }
    }

}
