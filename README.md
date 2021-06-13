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
* IDE : **Visual Studio 2015** 
* Programming language : **C#**
