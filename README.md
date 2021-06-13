# Process Communication
## Description
Let the two different process to communication.  
　 | Process A            (Server) | Process B       (Client)
------------- |------------ | -------------        
Memory Size **\[bytes\]** | 512 | 512    
Write in Process | A         |       B   
Receive in Process | B      |       A  
Function | `ServerCreate` |   `ClientWrite`
Params |  (Func<byte\[\]>,Action<byte\[\]>) | (byte\[\],Action<byte\[\]>)  
Return Type | void | bool
Running Mode | Loop | Once
## Technology
#### [MemoryMappedFile](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorymappedfiles.memorymappedfile?view=net-5.0 "Title") :According to MemoryMappedFile to share memory.
## Environment
* IDE : **Visual Studio 2019** 
* Programming language : **C#**
## Application
*Process A*  
 ```C#
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
 ```
*Process B*  
 ```C#
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
 ```
 ## How to test  
 * Step1:Start Process A  
 
 * Step2:Start Process B  
 
 * Step3:Console input "Hello"  
 
 *Process A*
 ![image](https://user-images.githubusercontent.com/22924622/121806546-df5a8b80-cc82-11eb-88d1-7548adb1edce.png)
 
 *Process B*
 ![image](https://user-images.githubusercontent.com/22924622/121806568-ee413e00-cc82-11eb-8fe3-15c78ba00596.png)


