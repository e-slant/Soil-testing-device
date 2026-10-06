using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace smartAgriculture.SerialPort485
{
   public  class Rtu485
    {
        public Action<List<byte>> ResponseData;

        private static Rtu485 _instance;
        private static SerialInfo _serialInfo;

        SerialPort _serialPort;
        bool isBusing = false;

        int _currentSlave;
        int _wordLen;
        int _funcCode;
        int _startAddr;

        public Rtu485(SerialInfo serialInfo)
        {
            _serialPort = new SerialPort();
            _serialInfo = serialInfo;
        }
        public bool Iscreate()
        {
            if (_serialPort == null)
            {
                _instance = null;
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public static Rtu485 GetInstance(SerialInfo serialInfo)
        {
            lock ("rtu485")
            {
                if (_instance == null)
                {
                    _instance = new Rtu485(serialInfo);

                }
                return _instance;
            }
        }
        public  void UpSerialinfo(SerialInfo newserialInfo)
        {
            _serialInfo = newserialInfo;
        }
        public bool Connection()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _serialPort.PortName = _serialInfo.PortName;
                _serialPort.BaudRate = _serialInfo.BaudRate;
                _serialPort.DataBits = _serialInfo.DataBit;
                _serialPort.Parity = _serialInfo.Parity;
                _serialPort.StopBits = _serialInfo.StopBits;

                _serialPort.ReceivedBytesThreshold = 1;
                _serialPort.DataReceived += _serialPort_DataReceived;


                _serialPort.Open();
                Models.SystemGlobalVariable.is485Connected = true;
            }
            catch (Exception)
            {
                Models.SystemGlobalVariable.is485Connected = false;
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = null;
                Models.SystemGlobalVariable.is485Connected = false;
            }

        }

        //int _receiveByteCount = 0;
        //byte[] _byteBuffer = new byte[50];

        //private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    byte _receiveByte;

        //    if(_serialPort.BytesToRead >= 24)//29
        //    {
        //        _receiveByte = (byte)_serialPort.ReadByte();
        //        if (_receiveByte == 0x01)
        //        {
        //            _receiveByteCount = 0;
        //            _byteBuffer[_receiveByteCount] = _receiveByte;

        //            while (true)
        //            {
        //                _receiveByteCount++;
        //                _receiveByte = (byte)_serialPort.ReadByte();
        //                _byteBuffer[_receiveByteCount] = _receiveByte;
        //                if (_receiveByteCount >= 24)
        //                {
        //                    _receiveByteCount = 0;
        //                    _serialPort.DiscardInBuffer();
        //                    ResponseData?.Invoke(new List<byte>(_byteBuffer));
        //                    return;
        //                }
        //            }
        //        }

        //    }
        //}
        static byte[] _byteBuffer = new byte[24]; // 确保缓冲区大小与期望接收的数据大小一致31  
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            // 在串口接收到数据时执行  
            if (_serialPort.BytesToRead >= 24)//31
            {
                //int arrycount = _serialPort.BytesToRead;
                //byte[] buffer = new byte[arrycount];
                //_serialPort.Read(buffer, 0, arrycount);//全读
                //int[] posstion = new int[10];//存位置
                //int indexposstion = 0;//索引或数量
                //for (int i = 0; i < buffer.Length; i++)
                //{
                //if (buffer[i] == 0x01)
                //{
                //    if (i - posstion[indexposstion] < 7 && indexposstion != 0&&buffer[posstion[indexposstion]+2]!=0x08&&buffer[posstion[indexposstion]+2]!=0x06&&buffer[posstion[indexposstion]+2]!=0x02)
                //    {
                //        continue;
                //    }
                //    posstion[indexposstion] = i;
                //    indexposstion++;
                //}
                //}
                //if (indexposstion > 2)
                //{
                //    int gap;
                //    for (int i = 0; i + 1 < indexposstion; i++)
                //    {
                //        gap = posstion[i + 1] - posstion[i];
                //        if (gap == 13 && posstion[i + 2] - posstion[i + 1] == 11 && buffer.Length - posstion[i+2]>=7)
                //        {
                //            for (int j = 0; j < 31; j++)
                //            {
                //                _byteBuffer[j] = buffer[posstion[i] + j];
                //            }
                //        }
                //        else if (gap == 13 && posstion[i + 2] - posstion[i+1]==11 &&buffer.Length - posstion[i + 2] < 7)
                //        {
                //            int miss = buffer.Length - posstion[i + 2];
                //            while (true)
                //            {
                //                if (_serialPort.BytesToRead >= miss)
                //                {
                //                    byte[] buffer2 = new byte[miss];
                //                    _serialPort.Read(buffer2, 0, miss);//全读
                //                    int o = 0;
                //                    for (int j = 31 - miss - 1; j < 31; j++)
                //                    {
                //                        _byteBuffer[j] = buffer2[o];
                //                    }
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //}
                int arrycount = _serialPort.BytesToRead;
                byte[] buffer = new byte[arrycount];
                _serialPort.Read(buffer, 0, arrycount);//全读
                int[] posstion = new int[10];//存位置
                int indexposstion = 0;//索引或数量
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == 0x01)
                    {
                        if (i - posstion[indexposstion] <11&&indexposstion!=0)
                        {
                            continue;
                        }
                        posstion[indexposstion] = i;
                        indexposstion++;
                    }
                }
                if (indexposstion > 1)//3
                {
                    int gap;
                    for (int i = 0; i + 1 < indexposstion; i++)
                    {
                        gap = posstion[i + 1] - posstion[i];
                        if (gap == 13 && buffer.Length - posstion[i + 1] >= 11)
                        {
                            for (int j = 0; j < 24; j++)
                            {
                                _byteBuffer[j] = buffer[i + j];
                            }
                        }
                        else if (gap == 13 && buffer.Length - posstion[i + 1] < 11)
                        {
                            int miss = buffer.Length - posstion[i + 1];
                            while (true)
                            {
                                if (_serialPort.BytesToRead >= miss)
                                {
                                    byte[] buffer2 = new byte[miss];
                                    _serialPort.Read(buffer2, 0, miss);//全读
                                    int o = 0;
                                    for (int j = 24 - miss - 1; j < 24; j++)
                                    {
                                        _byteBuffer[j] = buffer2[o];
                                    }
                                    break;
                                }
                            }
                        }else if (gap == 11 && buffer[posstion[i + 2]]==0x01)//如果0x01与下一个0x01差距为11时且
                        {
                            int a = 0;
                            for(int j=13;j<24; j++)
                            {
                                _byteBuffer[j] = buffer[a];
                                a++;
                            }
                            a = 0;
                            for(int j = 0; j < 13;j++)
                            {
                                _byteBuffer[j]= buffer[a+11];
                                a++;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
                ResponseData?.Invoke(new List<byte>(_byteBuffer));
                //_serialPort.DiscardInBuffer();
                Array.Clear(_byteBuffer,0, _byteBuffer.Length);
            }
        }
        public async Task<bool> Send(
            int waterupper,int waterlower,
            int temperatrueupper,int temperatruelower,
            int phupper,int phlower,
            int nitrogenupper, int nitrogenlower,
            int phosphorusupper,int phosphoruslower,
            int potassiumupper,int potassiumlower,
            int room_temperatureupper, int room_temperaturelower)
        {
            List<byte> sendBuffer = new List<byte>();


            sendBuffer.Add((byte)0x01);
            sendBuffer.Add((byte)0x9);
            sendBuffer.Add((byte)(27));
            convertsend(sendBuffer, waterupper);
            convertsend(sendBuffer, waterlower);

            convertsend(sendBuffer, temperatrueupper);
            convertsend(sendBuffer, temperatruelower);

            convertsend(sendBuffer, phupper);
            convertsend(sendBuffer, phlower);

            convertsend(sendBuffer, nitrogenupper);
            convertsend(sendBuffer, nitrogenlower);

            convertsend(sendBuffer, phosphorusupper);
            convertsend(sendBuffer, phosphoruslower);

            convertsend(sendBuffer, potassiumupper);
            convertsend(sendBuffer, potassiumlower);

            convertsend(sendBuffer, room_temperatureupper);
            convertsend(sendBuffer, room_temperaturelower);


            try
            {
                while (isBusing) { };

                isBusing = true;
                _serialPort.Write(sendBuffer.ToArray(), 0, 27);
                await Task.Delay(200);
                isBusing = false;
            }
            catch (Exception e)
            {

                return false;
            }
            //_receiveByteCount = 0;
            return true;
        }
        public void convertsend(List<byte> bytes,int num)
        {
            uint unint=(uint)num;
            bytes.Add((byte)(unint >> 8));
            bytes.Add((byte)(unint & 0xFF));
        }



        private byte[] Crc16(byte[] temp, int len)
        {
            byte[] by = new byte[10];

            by[0] = 0xc5;
            by[1] = 0xcd;
            return by;
        }
    }
}
