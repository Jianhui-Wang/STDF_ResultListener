using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToStdf;

namespace StdfViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            StdfFile file = new StdfFile(@"c:\temp\test.stdf");
            IEnumerable<StdfRecord> records = file.GetRecords();

            int i = 0;
            foreach (var item in records)
            {
                i++;
                Console.WriteLine("STDF Record[{0}]: Type[{1}]", i, item.GetType().ToString());
            }
            Console.ReadKey();
        }
    }
}
