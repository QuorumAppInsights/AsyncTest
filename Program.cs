using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var watch = new Stopwatch();

            watch.Start();
            Console.WriteLine(AsyncMethod1().Result);
            watch.Stop();
            Console.WriteLine("Time Elapased: " + watch.Elapsed + "\n\n");         
            
            watch.Restart();
            Console.WriteLine(AsyncMethod2().Result);
            watch.Stop();
            Console.WriteLine("Time Elapased: " + watch.Elapsed + "\n\n");
            
            // Console.ReadKey(); // uncomment this if your using visual studios
        }

        public static async Task<string> AsyncMethod1()
        {
            var rand = new Random();

            for(int i = 0; i < 10; i++)
            {
                await AsyncRequest(i, rand.Next(0, 3000));
            }

            return "AsyncMethod1 Completed Successfully";
        }

        public static async Task AsyncRequest(int i, int timeDelay)
        {
            Console.WriteLine(string.Format("Request {0}: {1} ms", i, timeDelay));
            await Task.Delay(timeDelay);
        }

        public static async Task<string> AsyncMethod2()
        {
            var rand = new Random();
            var requestList = new List<Task>();

            for(int i = 0; i < 10; i++)
            {
                requestList.Add(AsyncRequest(i, rand.Next(0, 3000)));
            }
            
            await Task.WhenAll(requestList.ToArray());

            return "AsyncMethod2 Completed Successfully";
        }
    }
}
