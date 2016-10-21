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
            for(int i = 0; i < 100; i++)
            {
                await AsyncRequest2(i, rand.Next(0, 10));
            }
            return "AsyncMethod1 Completed Successfully";
        }

        public static async Task<string> AsyncMethod2()
        {
            var rand = new Random();
            var requestList = new List<Task>();
            for(int i = 0; i < 100; i++)
            {
                requestList.Add(AsyncRequest2(i, rand.Next(0, 100)));
            }
            await Task.WhenAll(requestList);
            return "AsyncMethod2 Completed Successfully";
        }

        public static async Task AsyncRequest(int i, int timeDelay)
        {
            await Task.Delay(timeDelay);
            Console.WriteLine(string.Format("Request {0}: {1} ms", i, timeDelay));
        }

        public static async Task AsyncRequest2(int i, int timeDelay)
        {
            await Task.Delay(0);
            
            long count = 0;
            var rand = new Random();
            long max = (long)(99999*timeDelay);

            var watch = new Stopwatch();
            watch.Start();
            for(long j = 0; j < max; j++)
            {
                count += j;
                count %= 15;
            }
            watch.Stop();

            Console.WriteLine(string.Format("Request {0}: {1} ms, max: {2}", i, watch.Elapsed, max));
        }

    }
}
