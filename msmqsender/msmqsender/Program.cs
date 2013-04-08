using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Runtime.Serialization;

/*
 * Based off of the tutorial found here: http://fransiscuss.com/2012/06/01/msmq-basic-tutorial/
 * @bccain
 */

namespace msmqsender
{
    class Program
    {
        private const string MESSAGE_QUEUE = @".\Private$\SampleQueue";
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
            Console.WriteLine("Send me");
            int n = 0;

            Program p = new Program();
            while (true)
            {
                p.SendMessage("Heeellloooo wooorrrlllddddd message number #" + n++);
            }
            
        }
    }
}
