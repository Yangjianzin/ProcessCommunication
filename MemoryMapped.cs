public class MemoryMapped
{
        /// <summary>
        /// 記憶體對應檔的名稱
        /// </summary>
        public string mapName = "MAPNAME";
        /// <summary>
        /// 要配置給記憶體對應檔的大小上限 (以位元組為單位)
        /// </summary>
        public long capacity = 1024;
        //            512                     |             512
        //           Host Write               |           Client Write
        //           Client Read              |           Host Read
        /// <summary>
        /// 持續性讀取
        /// </summary>
        /// <param name="ServerWrite"></param>
        /// <param name="ServerRecived"></param>
        /// 
        public void MemoryMappedServerByte(Func<byte[]> ServerWrite, Action<byte[]> ServerRecived)
        {
            try
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew(mapName, capacity))
                {
                    byte[] ClientMessage = null;
                    while (true)
                    {
                        //寫入Client Process Memory 
                        using (var stream = mmf.CreateViewStream())
                        {
                            byte[] msg = ServerWrite();
                            using (BinaryWriter bw = new BinaryWriter(stream))
                            {
                                bw.Write(msg.Length); //先寫Length
                                bw.Write(msg); //再寫byte[]
                            }
                        }
                        //接收Client Process Memory 
                        using (MemoryMappedViewStream stream = mmf.CreateViewStream(512, 512))
                        {
                            using (var br = new BinaryReader(stream))
                            {
                                //先讀取長度，再讀取内容
                                var len = br.ReadInt32();
                                var msg = br.ReadBytes(len);
                                if (ClientMessage != msg)
                                {
                                    ServerRecived(msg);
                                    ClientMessage = msg;
                                }
                            }
                        }
                        
                        Thread.Sleep(30);
                    }
                }
            }
            catch
            {
                MessageBox.Show(MethodInfo.GetParentInfo(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool MemoryMappedClientByte(Action<byte[]> ClientRecived, byte[] ClientWrite)
        {
            try
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(mapName))
                {
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream(0, 512))
                    {
                        using (var br = new BinaryReader(stream))
                        {
                            //先讀取長度，再讀取内容
                            var len = br.ReadInt32();
                            var word = br.ReadBytes(len);
                            ClientRecived(word);
                            //Console.WriteLine($"訊息＝{word}");
                        }
                    }
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream(512, 512))
                    {
                        using (var bw = new BinaryWriter(stream))
                        {
                            var msg = ClientWrite;
                            bw.Write(msg.Length);
                            bw.Write(msg);
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
