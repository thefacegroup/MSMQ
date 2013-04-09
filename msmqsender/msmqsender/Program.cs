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
 * Xml Serialization : http://msdn.microsoft.com/en-us/library/58a18dwa.aspx 
 * @csalazar
 */

namespace msmqsender
{
    class Program
    {
        private List<String> serializeThis = null;
        private XmlSerializer xs = null;
        private TextWriter writer = null;

        public Program()
        {
            serializeThis = new List<String>();
            serializeThis.Add("one");
            serializeThis.Add("two");
            serializeThis.Add("three");

            xs = new XmlSerializer(typeof(List<String>));
            writer = new StringWriter(); //Instead of StreamWriter, we want to write to a string!
        }


        //private const string MESSAGE_QUEUE = @".\Private$\visualizerqueue";
        private const string MESSAGE_QUEUE = @".\Private$\servicequeue";

        //private const string REMOTE_QUEUE = "FormatName:Direct=TCP:10.27.41.4\\private$\\receiveQueue";

        private MessageQueue _queue;

        private void SendMessage(string message)
        {
            _queue = new MessageQueue(MESSAGE_QUEUE);
            Message msg = new Message();
            msg.Body = message;
            msg.Label = "Presentation at " + DateTime.Now.ToString();
            _queue.Send(msg);

            //lblError.Text = "Message already sent";
        }

        static void Main(string[] args)
        {
            Console.Write("Enter something to send: ");
            int n = 0;

            Program p = new Program();
            //while (true)
            //{
            //    p.SendMessage("Heeellloooo wooorrrlllddddd message number #" + n++);
            //}
            String msg = "";
            //msg = Console.ReadLine();

            p.xs.Serialize(p.writer, p.serializeThis);
            msg = p.writer.ToString();
            //Console.WriteLine(msg);
            p.SendMessage(msg);
            while (true)
            {
                //derp
            }
            
        }
    }
}
