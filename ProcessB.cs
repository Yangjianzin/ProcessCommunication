using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessB
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessCommunication.MemoryMapped Client = new ProcessCommunication.MemoryMapped("TEST", 512, 512);
            Action<byte[]> RecivedEvent =
               (data) =>
               {
                   //TODO
                   //...

                   //Example
                   Console.Write("Receive :" + data.Length + ">>");
                   Console.WriteLine(Encoding.ASCII.GetString(data));
               };
            while (true)
            {
                string msg = Console.ReadLine();
                Client.ClientWrite(Encoding.ASCII.GetBytes(msg) , RecivedEvent);
         
            }
        }
    }
}
