using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HLTest
{
    static class TFTHelper
    {

        public static List<byte> redList = new List<byte>();
        public static List<byte> greenList = new List<byte>();
        public static List<byte> blueList = new List<byte>();
        public static List<byte> whiteList = new List<byte>();
        static TFTHelper()
        {
            redList.Add(255);
            redList.Add(0);
            redList.Add(0);

            greenList.Add(0);
            greenList.Add(255);
            greenList.Add(0);

            blueList.Add(0);
            blueList.Add(0);
            blueList.Add(255);

            whiteList.Add(255);
            whiteList.Add(255);
            whiteList.Add(255);
        }
        /// <summary>
        /// 计算校验位
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte Cal_Xor(byte[] buffer)
        {
            short xorResult = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                xorResult ^= buffer[i];
            }
            return Convert.ToByte(xorResult);
        }

        /// <summary>
        /// 写入产品出生证、检测值、是否合格
        /// </summary>
        /// <param name="vin">产品出生证</param>
        /// <param name="detectValue">检测值</param>
        /// <param name="isQualified">是否合格</param>
        /// <param name="serialPort">串行端口</param>
        public static void WriteLCD(string vin, string OP, string detectValue, string standardValue, bool isQualified, SerialPort serialPort)
        {
            WriteVIN(vin, 0, 10, serialPort);
            Thread.Sleep(100);

            WriteOP(OP, 0, 45, serialPort);
            Thread.Sleep(100);

            WriteDetectValue(standardValue, 0, 80, serialPort);
            Thread.Sleep(100);
            WriteStandardValue(detectValue, 0, 115, serialPort);

            Thread.Sleep(100);
            WriteIsQualified(isQualified, 0, 150, serialPort);
        }

        /// <summary>
        /// 画表格
        /// </summary>
        /// <param name="serialPort"></param>
        public static void DrawSheet(SerialPort serialPort)
        {
            DrawLine(0, 50, 250, 50, serialPort);
            Thread.Sleep(100);
            DrawLine(0, 110, 250, 110, serialPort);
            Thread.Sleep(100);
            DrawLine(105, 0, 105, 250, serialPort);
        }

        /// <summary>
        /// 写产品出生证
        /// </summary>
        /// <param name="vin">产品出生证</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="serialPort">串行端口</param>
        public static void WriteVIN(string vin, short x, short y, SerialPort serialPort)
        {
            string str = string.Format("产品出生证:{0}", vin);
            SendStr(str, x, y, 2, serialPort);
        }
        /// <summary>
        /// 写当前位置
        /// </summary>
        /// <param name="vin">当前位置</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="serialPort">串行端口</param>
        public static void WriteOP(string OP, short x, short y, SerialPort serialPort)
        {
            string str = string.Format("当前位置:{0}", OP);
            SendStr(str, x, y, 3, serialPort);
        }
        /// <summary>
        /// 写标准值
        /// </summary>
        /// <param name="vin">标准值</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="serialPort">串行端口</param>
        public static void WriteStandardValue(string standardValue, short x, short y, SerialPort serialPort)
        {
            string str = string.Format("标准值:{0}", standardValue);
            SendStr(str, x, y, 3, serialPort);
        }

        /// <summary>
        /// 写检测值
        /// </summary>
        /// <param name="vin">检测值</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="serialPort">串行端口</param>
        public static void WriteDetectValue(string detectValue, short x, short y, SerialPort serialPort)
        {
            string str = string.Format("检测值: {0}", detectValue);
            SendStr(str, x, y, 3, serialPort);
        }
        /// <summary>
        /// 写是否合格
        /// </summary>
        /// <param name="vin">是否合格</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="serialPort">串行端口</param>
        public static void WriteIsQualified(bool isQualified, short x, short y, SerialPort serialPort)
        {
          
            if (isQualified)
            {
                string str;
                str = string.Format("是否合格: 合格");
                SendStr(str, x, y, 3, serialPort);
            }
            else
            {
                string str1 = string.Format("是否合格: ");
                SendStr(str1, x, y, 3, serialPort);
                Thread.Sleep(100);
                SetPenColor(redList,serialPort);
                Thread.Sleep(100);
                string str2 = string.Format("不合格");
                SendStr(str2, Convert.ToInt16(x+80), y, 3, serialPort);
                Thread.Sleep(100);
                SetPenColor(whiteList, serialPort);
            }           
        }


        /// <summary>
        /// 屏幕清屏为蓝色
        /// </summary>
        /// <param name="serialPort"></param>
        public static void ClearScreenToBlue(SerialPort serialPort)
        {
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(05);
            list.Add(01);
            list.Add(00);
            list.Add(0x1F);
            list.Add(Cal_Xor(list.ToArray()));
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 屏幕清屏为绿色
        /// </summary>
        /// <param name="serialPort"></param>
        public static void ClearScreenToGreen(SerialPort serialPort)
        {
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(05);
            list.Add(01);
            list.Add(07);
            list.Add(0xE0);
            list.Add(0xB9);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 设置操作区背景色
        /// </summary>
        /// <param name="serialPort"></param>
        public static void SetBackColor(SerialPort serialPort)
        {
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(05);
            list.Add(02);
            list.Add(07);
            list.Add(0xE0);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// LCD 背光显示开关,0关，1开
        /// </summary>
        /// <param name="type">开关操作</param>
        /// <param name="serialPort"></param>
        public static void SetBackLight(bool type, SerialPort serialPort)
        {       //5A 05 20 00 00 7F FF  关闭
                //5A 05 20 01 00 7E FF 打开          
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(05);
            list.Add(20);
            list.Add(Convert.ToByte(type));
            list.Add(00);
            list.Add(0x7E);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 设置横竖屏，0竖屏，1横屏
        /// </summary>
        /// <param name="type">横竖操作</param>
        /// <param name="serialPort"></param>
        public static void SetDirection(bool type, SerialPort serialPort)
        {

            //竖屏显示 发送5A 05 1C 00 00 43 FF
            //横屏显示 发送5A 05 1C 01 00 42 FF
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(05);
            list.Add(0x1C);
            list.Add(Convert.ToByte(type));
            list.Add(00);
            byte b = Cal_Xor(list.ToArray());
            list.Add(b);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 设置画笔颜色
        /// </summary>
        /// <param name="colorList">颜色的list</param>
        /// <param name="serialPort">串行端口</param>
        public static void SetPenColor(List<byte> colorList, SerialPort serialPort)
        {
            //5A 05 03 07 E0 (XOR) FF
            //5A 06 03 07 E0 00 B8 FF 
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(05);
            list.Add(03);
            byte[] colorByte = GetColorByte(colorList);
            list.AddRange(colorByte);
            byte b = Cal_Xor(list.ToArray());
            list.Add(b);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="serialPort"></param>
        public static void DrawPoint(short x, short y, SerialPort serialPort)
        {
            //5A 07 06 00 32 00 64 (XOR) FF
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(07);
            list.Add(06);
            list.Add(00);
            list.Add(Convert.ToByte(x));
            list.Add(00);
            list.Add(Convert.ToByte(y));

            byte b = Cal_Xor(list.ToArray());
            list.Add(b);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 区域填充颜色
        /// </summary>
        /// <param name="xStart">起点横坐标</param>
        /// <param name="yStart">起点纵坐标</param>
        /// <param name="xEnd">终点横坐标</param>
        /// <param name="yEnd">终点纵坐标</param>
        /// <param name="serialPort">端口</param>
        public static void SetAreaColor(short xStart, short yStart, short xEnd, short yEnd, SerialPort serialPort)
        {
            //5A 0D 04 00 32 00 64 00 64 00 32 07 E0(XOR) FF
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(0x0D);
            list.Add(04);
            list.Add(00);
            list.Add(Convert.ToByte(xStart));
            list.Add(00);
            list.Add(Convert.ToByte(yStart));
            list.Add(00);
            list.Add(Convert.ToByte(xEnd));
            list.Add(00);
            list.Add(Convert.ToByte(yEnd));
            list.Add(32);
            list.Add(07);
            byte b = Cal_Xor(list.ToArray());
            list.Add(b);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="xStart">起点横坐标</param>
        /// <param name="yStart">起点纵坐标</param>
        /// <param name="xEnd">终点横坐标</param>
        /// <param name="yEnd">终点纵坐标</param>
        /// <param name="serialPort">端口</param>
        public static void DrawLine(short xStart, short yStart, short xEnd, short yEnd, SerialPort serialPort)
        {
            //5A 0B 07 00 32 00 64 00 64 00 64 (XOR) FF
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(0x0B);
            list.Add(07);
            list.Add(00);
            list.Add(Convert.ToByte(xStart));
            list.Add(00);
            list.Add(Convert.ToByte(yStart));
            list.Add(00);
            list.Add(Convert.ToByte(xEnd));
            list.Add(00);
            list.Add(Convert.ToByte(yEnd));
            byte b = Cal_Xor(list.ToArray());
            list.Add(b);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="R">半径</param>
        /// <param name="serialPort"></param>
        public static void DrawCircle(short x, short y, short R, SerialPort serialPort)
        {
            //5A 09 09 00 32 00 64 00 10 (XOR) FF
            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(0x09);
            list.Add(09);
            list.Add(00);
            list.Add(Convert.ToByte(x));
            list.Add(00);
            list.Add(Convert.ToByte(y));
            list.Add(00);
            list.Add(Convert.ToByte(R));
            byte b = Cal_Xor(list.ToArray());
            list.Add(b);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }

        public static byte[] GetByte(short cmd, short dataLen, byte[] buffer)
        {

            List<byte> list = new List<byte>();
            list.Add(0x5A);
            list.Add(Convert.ToByte(dataLen));
            list.Add(Convert.ToByte(cmd));
            list.AddRange(buffer);
            list.Add(Cal_Xor(list.ToArray()));
            list.Add(0xFF);
            return buffer;
        }
        /// <summary>
        /// 将RGB颜色转换为串口的颜色
        /// </summary>
        /// <param name="RGBlist"></param>
        /// <returns></returns>
        public static byte[] GetColorByte(List<byte> RGBlist)
        {

            byte redByte = RGBlist[0];
            string strRed = Convert.ToString(redByte, 2).PadLeft(8, '0');
            string red = strRed.Substring(0, 5);

            byte greenByte = RGBlist[1];
            string strGreen = Convert.ToString(greenByte, 2).PadLeft(8, '0');
            string green = strGreen.Substring(0, 6);

            byte blueByte = RGBlist[2];
            string strBlue = Convert.ToString(blueByte, 2).PadLeft(8, '0');
            string blue = strBlue.Substring(0, 5);
            string str = red + green + blue;

            byte byte1 = Convert.ToByte(str.Substring(0, 8), 2);
            byte byte2 = Convert.ToByte(str.Substring(8, 8), 2);
            List<byte> list = new List<byte>();

            list.Add(byte1);
            list.Add(byte2);
            return list.ToArray();
        }
        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="size">字体号</param>
        /// <param name="serialPort">串行端口</param>
        public static void SendStr(string str, short x, short y, short size, SerialPort serialPort)
        {

            List<byte> list = new List<byte>();
            byte[] a = Encoding.Default.GetBytes(str);
            list.Add(0x5A);
            byte lenth = Convert.ToByte(8 + a.Length);
            list.Add(lenth);
            list.Add(0x0F);
            list.Add(0);
            list.Add(Convert.ToByte(x));
            list.Add(0);
            list.Add(Convert.ToByte(y));
            list.Add(Convert.ToByte(size));
            list.AddRange(a);
            byte b = Cal_Xor(list.ToArray());
            list.Add(b);
            list.Add(0xFF);
            byte[] buffer = list.ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }
    }
}
