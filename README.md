# Process Communication
## Purpose
Let the two different process to communication.  
_ | Process A (Server) | Process B (Client)
------------- |------------ | -------------        
Memory Size **\[bytes\]** | 512 | 512    
Write Process | A         |       B   
Receive Process | B      |       A  
Function | MemoryMappedServerByte(Write,Receive) |   MemoryMappedClientByte(Receive,Write)
Running Type | Loop | Once
## Technology
[MemoryMappedFile](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorymappedfiles.memorymappedfile?view=net-5.0 "Title") :According to MemoryMappedFile to communication.
## Environment
* IDE : **VS2015** 
* Programming language : **C#**
