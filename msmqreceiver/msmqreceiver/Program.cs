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
 * Xml Serialization : http://msdn.microsoft.com/en-us/library/58a18dwa.aspx 
 * @csalazar
 */

namespace msmqreceiver
{
    class Program
    {
        private static List<String> thisWasSerialized = null;
        private static XmlSerializer xs = new XmlSerializer(typeof(List<String>));


        private const string MESSAGE_QUEUE = @".\Private$\servicequeue";
        //private const string REMOTE_QUEUE = "FormatName:Direct=TCP:10.27.41.4\\private$\\receiveQueue";

        private static void CheckMessage()
        {
            try
            {
                var queue = new MessageQueue(MESSAGE_QUEUE);
                var message = queue.Receive(new TimeSpan(0, 0, 1));

                message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });

                // Lets just assume for this baby example that we know the message will be xml.

                TextReader reader = new StringReader(message.Body.ToString());
                thisWasSerialized = (List<String>)xs.Deserialize(reader);

                foreach (String s in thisWasSerialized)
                {
                    Console.WriteLine(s);
                }

                //Console.WriteLine(message.Body.ToString());
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
                CheckMessage();
            }
        }
    }
}
