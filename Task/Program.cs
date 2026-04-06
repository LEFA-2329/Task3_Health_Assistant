using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace work
{

    class assistant
    {
        public void Message(string messages)
        {
            foreach (char text in messages)
            {
                Console.Write(text);
                Thread.Sleep(40);
            }
            Console.WriteLine();

        }
    }
    class health : assistant
    {
        public string respond;

        public void assist()
        {

            while (true)
            {
                Console.Write("You : ");
                respond = Console.ReadLine();

                if (respond.Contains("good"))
                {
                    Message("Great! Keep maintaining a healthy lifestyle dear");
                }
                else if (respond.Contains("sick"))
                {
                    Message("Please consider resting and visiting a doctor if necessary.");

                }
                else if (respond.Contains("tired"))
                {
                    Message("Make sure you get enough sleep and drink water.");
                }
                else if (respond.Contains("exit"))
                {
                    break;
                }
                else
                {
                    Message("Sorry, I do not understand your response");
                }
            }

        }

    }
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("How are you feeling today?(good/sick/tired)");
            Console.WriteLine("\n Type exit to exit the program\n");

            health info = new health();

            info.assist();


        }

    }
}

