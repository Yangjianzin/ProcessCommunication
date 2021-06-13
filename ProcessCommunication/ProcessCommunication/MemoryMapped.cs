using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace ProcessCommunication
{
      public class MemoryMapped
    {
        public string mapName = "MAPNAME";

        private long capacity
        {
            get
            {
                return ServeMemorySize + ClientMemorySize;
            }
        }

        public long ServeMemorySize = 512;

        public long ClientMemorySize = 512;

        public MemoryMapped()
        {

        }

        public MemoryMapped(string mapName, long ServerSize, long ClientSize)
        {
            this.mapName = mapName;
            this.ServeMemorySize = ServerSize;
            this.ClientMemorySize = ClientSize;
        }

        public void ServerCreate(Func<byte[]> WriteEvent, Action<byte[]> ReceivedEvent, int LoopDelayTime = 30)
        {
            try
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew(mapName, capacity))
                {
                    while (true)
                    {
                        //Write Client Process Memory 
                        using (var stream = mmf.CreateViewStream())
                        {
                            byte[] data = WriteEvent();
                            using (BinaryWriter bw = new BinaryWriter(stream))
                            {
                                bw.Write(data.Length);
                                bw.Write(data);
                            }
                        }
                        //Receive Client Process Memory 
                        using (MemoryMappedViewStream stream = mmf.CreateViewStream(ServeMemorySize, ClientMemorySize))
                        {
                            using (var br = new BinaryReader(stream))
                            {
                                var len = br.ReadInt32();
                                var data = br.ReadBytes(len);
                                //Check the recived data is change
                                ReceivedEvent(data);
                            }
                        }

                        Thread.Sleep(LoopDelayTime);
                    }
                }
            }
            catch (Exception e)
            {
                //something Error
                Console.WriteLine("Error" + e.ToString());
            }
        }

        public bool ClientWrite(byte[] WriteData, Action<byte[]> ReceiveEvent)
        {
            try
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(mapName))
                {
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream(0, ServeMemorySize))
                    {
                        using (var br = new BinaryReader(stream))
                        {
                            var len = br.ReadInt32();
                            var data = br.ReadBytes(len);
                            ReceiveEvent(data);
                        }
                    }
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream(ServeMemorySize, ClientMemorySize))
                    {
                        using (var bw = new BinaryWriter(stream))
                        {
                            var data = WriteData;
                            bw.Write(data.Length);
                            bw.Write(data);
                        }
                    }
                }
                return true;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Memory-mapped file does not exist.");
                return false;
            }
        }
    }
}
