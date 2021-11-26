using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;
using System.Media;
using System.Threading;

namespace game1
{
    public partial class Form1 : Form
    {
        private NetworkStream stream;
        private TcpClient tcpClient = new TcpClient();
        private int picture = 1;
        Socket socket_send;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
               
                //向指定的IP地址的服务器发出连接请求
                tcpClient.Connect("10.1.230.74", 3900);
                listBox1.Items.Add("连接成功！");
                stream = tcpClient.GetStream();
                byte[] data = new byte[1024];
                //判断网络流是否可读            
                if (stream.CanRead)
                {
                    int len = stream.Read(data, 0, data.Length);
                    string msg = Encoding.Default.GetString(data, 0, data.Length);
                    string str = "\r\n";
                    char[] str1 = str.ToCharArray();
                    string[] msg1 = msg.Split(str1);
                    

                    for (int j = 0; j < msg1.Length; j++)
                    {
                        listBox1.Items.Add(msg1[j]);
                    }
                }
            }
            catch
            {
                listBox1.Items.Add("连接失败！");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            music_play();
            if (tcpClient.Connected)
            {
                //向服务器发送数据
                string msg = textBox1.Text;
                Byte[] outbytes = System.Text.Encoding.Default.GetBytes(msg + "\n");
                stream.Write(outbytes, 0, outbytes.Length);
                byte[] data = new byte[1024];
                //接收服务器回复数据
                if (stream.CanRead)
                {
                    int len = stream.Read(data, 0, data.Length);
                    string msg1 = Encoding.Default.GetString(data, 0, data.Length);
                    string str = "\r\n";
                    char[] str1 = str.ToCharArray();
                    string[] msg2 = msg1.Split(str1);
                    for (int j = 0; j < msg2.Length; j++)
                    {
                        listBox1.Items.Add(msg2[j]);
                    }
                }
            }
            else
            {
                listBox1.Items.Add("连接已断开");
            }
            textBox1.Clear();
        }
        void pic_play()
        {         
             picture++;   //记得在前面定义变量picture
                string picturePath = @"D:\wuxia\" + picture + ".jpg";//图片的保存路径
                                                                     //设置图片填充
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Image.FromFile(picturePath);
                if (picture == 4)
                { picture = 0; } 
        }

        private void button11_Click(object sender, EventArgs e)
        {
           
                if (stream != null)//关闭连接，关闭流
                {
                    stream.Close();
                    tcpClient.Close();
                    socket_send.Close();
                }
                listBox1.Items.Add("已经退出游戏");          
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Thread th = new Thread(pic_play);
            th.IsBackground = true;
            th.Start();
        }

        private void music_play()
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = @"D:\wuxia\金庸梦.wav";       //你的音乐文件名称，且注意必须是wav文件
            sp.PlayLooping();
        }
    }
}
