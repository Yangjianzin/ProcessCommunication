# Process Communication
## Purpose
#### Let the two different process to communication.  
　 | Process A            (Server) | Process B       (Client)
------------- |------------ | -------------        
Memory Size **\[bytes\]** | 512 | 512    
Write in process | A         |       B   
Receive in process | B      |       A  
Function | MemoryMappedServerByte |   MemoryMappedClientByte
Params |  (Func<byte\[\]>,Action<byte\[\]>) | (Action<byte\[\]>, byte\[\])  
Return type | void | bool
Running Mode | Loop | Once
## Technology
#### [MemoryMappedFile](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorymappedfiles.memorymappedfile?view=net-5.0 "Title") :According to MemoryMappedFile to communication.
## Environment
* IDE : **VS2015** 
* Programming language : **C#**
