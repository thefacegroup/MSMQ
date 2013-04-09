using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
//using System.Runtime.Serialization;
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

namespace msmqreceiver
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

        private static void checkMessage()
        {
            try
            {

                var queue = new MessageQueue(MESSAGE_QUEUE);

                DummyStruct ds = new DummyStruct();
                Object o = new Object();
                System.Type[] arryTypes = new System.Type[2];
                arryTypes[0] = ds.GetType();
                arryTypes[1] = o.GetType();
                queue.Formatter = new XmlMessageFormatter(arryTypes);
                ds = ((DummyStruct)queue.Receive().Body);
                Console.WriteLine(ds.ID);
                Console.WriteLine(ds.planName);
                Console.WriteLine(ds.degreeProgramID);
                Console.WriteLine(ds.userID);
                Console.WriteLine(ds.semesterID);

            }
            catch (Exception e)
            {
                //Console.WriteLine(e);

            }

        }


        static void Main(string[] args)
        {
            Console.WriteLine("Recieve me");
            while (true)
            {
                checkMessage();
            }

        }
    }

}