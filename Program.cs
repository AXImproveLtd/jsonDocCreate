using System;
using System.IO;
using nanoJSON;

namespace Demo
{
    class Program
    {
        void performance()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int repetitions = 100000;
            for(int n = 0 ; n < repetitions ; n++)
            {
                using(var doc = JSONElement.JSONDocument.newDoc(true))
                {
                    using(var root = doc.addObject())
                    {                           
                        root.addNumber("Id",n);
                        root.addString("Name", "Smith");
                    }
                    string result = doc.getOutput();
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(String.Format("Time to serialize {0} is {1} ms",repetitions,elapsedMs));            
        }

        void sample()
        {
            var doc = JSONElement.JSONDocument.newDoc(true);

            using (var root = doc.addObject())
            {
                root.addString("firstName", "John");
                root.addString("lastName", "Smith");
                root.addBoolean("isAlive", true);
                root.addNumber("age", 27);
                using (var addres = root.addObject("address"))
                {
                    addres.addString("streetAddress", "21 2nd Street");
                    addres.addString("city", "New York");
                    addres.addString("state", "NY");
                    addres.addString("postalCode", "10021-3100");                    
                }
                using (var phoneNumbers = root.addArray("phoneNumbers"))
                {
                    using (var phone = phoneNumbers.addObject())
                    {
                        phone.addString("type", "home");
                        phone.addString("number", "212 555-1234");
                    }
                    using (var phone = phoneNumbers.addObject())
                    {
                        phone.addString("type", "office");
                        phone.addString("number", "646 555-4567");
                    }
                    using (var phone = phoneNumbers.addObject())
                    {
                        phone.addString("type", "mobile");
                        phone.addString("number", "123 456-7890");
                    }
                }
                root.addArray("children").Dispose();
                root.addNull("spouse");
            }
            string res = doc.getOutput();
            File.WriteAllText("test.json", res);
        }

        static void Main(string[] args)
        {
            Program program = new Program();

            program.performance();
            program.sample();
        }
    }
}
