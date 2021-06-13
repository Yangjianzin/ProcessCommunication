# Process Communication
## Purpose
Let the two different process to communication.  
*****
Process A (Server)      |       Process B (Client)  
\[-----------------1024-----------------\]   
A Write byte\[512\]      |       B Write byte \[512\]  
B Read  byte \[512\]      |       A Read  byte \[512\]  
*****
## Technology
[MemoryMappedFile](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorymappedfiles.memorymappedfile?view=net-5.0 "Title") :According to MemoryMappedFile to communication.
## Environment
* IDE : **VS2015** 
* Programming language : **C#**
