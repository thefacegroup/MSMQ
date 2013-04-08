using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Runtime.Serialization;

namespace msmqreceiver
{
    class Program
    {
        private const string MESSAGE_QUEUE = @".\Private$\SampleQueue";

        private static void CheckMessage()
        {
            try
            {
                var queue = new MessageQueue(MESSAGE_QUEUE);
                var message = queue.Receive(new TimeSpan(0, 0, 1));

                message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                Console.WriteLine(message.Body.ToString());
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
