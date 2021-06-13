using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessA
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessCommunication.MemoryMapped Server = new ProcessCommunication.MemoryMapped("TEST", 512, 512);
            int receiveCount = 0;
            string msg = "";
            Func<byte[]> WriteEvent =
                () =>
                {
                    //TODO
                    //...


                    //Example
                    return Encoding.ASCII.GetBytes("OK!");
                };
            Action<byte[]> RecivedEvent =
                (data) =>
                {
                    //TODO
                    //...

                    //Example
                    receiveCount = data.Length;
                    msg = Encoding.ASCII.GetString(data);
                    Console.Write("Receive :" + receiveCount + ">>");
                    Console.WriteLine(msg);

                };
            Server.ServerCreate(WriteEvent, RecivedEvent);

        }
    }
}
