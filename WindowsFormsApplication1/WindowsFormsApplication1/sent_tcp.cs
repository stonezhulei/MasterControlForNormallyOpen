using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using WindowsFormsApplication1;


namespace WindowsFormsApplication4
{
    class sent_tcp
    {
        private TcpListener myTcpListener;  // TCP连接侦听器
        private NetworkStream ns;                 // 网络数据流
        private TcpClient tcpClient;              // TCP客户端
        private Thread TcpClientAcceptThread;   // 客户端连接监听线程
        IPEndPoint ipEndPoint;
        string[,] cxtcp_torobot = new string[1000, 10];     //给机器人的程序
        int zongshu_robot = 0;                 //机器人程序总的步数
        int zhixing_num = 0;                   //机器人程序执行到哪一步
        string robot_go = " ";
        int robot_speed = 0;
        double distance_robot = 0;
        string gopoint;
        int flag_zidongyunxing = 0;
        int flag_dashoudong = 0;

        int flag_send = 0;           //   使得机器人程序顺序执行注意多线程共用此变量8*******************

        public delegate void diaoyon();
        public diaoyon dy;
    
        public sent_tcp()
        {


        }
        public void tcp_torobotdata(string[,] aa, int y)
        {
            cxtcp_torobot = aa;
            zongshu_robot = y;

        }
        public void zidongyunxing(int yt)
        {
            flag_zidongyunxing = yt;
            if (yt == 1) { flag_send = 1; }
            zhixing_num = 0;
        }


        ~sent_tcp()
        {
            try
            {
                // 停止监听
                if (myTcpListener != null)
                {
                    myTcpListener.Stop();
                }
                // 终止监听线程

                if (TcpClientAcceptThread != null)
                {
                    if (TcpClientAcceptThread.IsAlive == true)
                    {
                        TcpClientAcceptThread.Abort();
                        while (TcpClientAcceptThread.ThreadState != ThreadState.Aborted) ;
                        TcpClientAcceptThread.Join();

                    }
                }
                if (tcpClient != null)
                { tcpClient.Close(); }
                if (ns != null)
                { ns.Dispose(); }
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void stop_tcp()
        {

            try
            {
                // 停止监听
                if (myTcpListener != null)
                {
                    myTcpListener.Stop();
                }
                // 终止监听线程
                if (TcpClientAcceptThread != null)
                {
                    if (TcpClientAcceptThread.IsAlive == true)
                    {
                        TcpClientAcceptThread.Abort();
                        while (TcpClientAcceptThread.ThreadState != ThreadState.Aborted) ;
                        TcpClientAcceptThread.Join();

                    }
                }
                if (ns != null)
                { ns.Dispose(); }
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void sent_connect()   //Parse
        {
            //IPAddress.Parse();
            try
            {
                // 创建并实例化IP终端结点
                ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.250.253"), Convert.ToInt32(5002));//192.168.250.15
                // 实例化TCP连接侦听器
                myTcpListener = new TcpListener(ipEndPoint);
                // 启动TCP连接侦听器
                myTcpListener.Start();
                // 创建并启动TCP连接侦听接受线程
                TcpClientAcceptThread = new Thread(new ParameterizedThreadStart(jieshoushuju));
                TcpClientAcceptThread.IsBackground = true;
                TcpClientAcceptThread.Start();
                // 修改控件状态  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void jieshoushuju(object tcpaddress)
        {
            string aa = "CONNECTOK\r\n";
            string send = "ok\r\n";
            int flag_okng = 0;


            try
            {


                // 循环接收消息
                tcpClient = myTcpListener.AcceptTcpClient();
                //  MessageBox.Show("通讯成功");
                while (true)
                {
                    // 接收TCP客户端
                    ns = null;
                    //     
                    while (ns == null)
                    {
                        ns = tcpClient.GetStream();
                        Thread.Sleep(50);

                       
                    }


                    int readLen = tcpClient.Available;
                    if (readLen >= 214)
                    {
                        //  MessageBox.Show("ok");
                        // 创建并实例化用于存放数据的字节数组
                        byte[] getData = new byte[readLen];
                        // 从网络数据流中读取数据
                        ns.Read(getData, 0, getData.Length);
                        data_class.byteData = getData;
                        dy();
                        // 将字节数组转换为文本形式
                        string getMsg = Encoding.Default.GetString(getData);
                        aa = "ok";
                        ns.Write(Encoding.Default.GetBytes(aa), 0, aa.Length);
                     //   MessageBox.Show(getMsg);          

                        if (getMsg == "connect\r")     //发送链接成功"connect\r"
                        {
                            ns.Write(Encoding.Default.GetBytes(aa), 0, aa.Length);
                            MessageBox.Show("通讯握手成功");

                        }
                        else if (getMsg == "next\r")              //继续命令
                        {
                            flag_send = 1;
                        
                            ns.Write(Encoding.Default.GetBytes(send), 0, send.Length);
                        }                                                               
                        else                           //拍照失败需要换料
                        {
                            //string ss;
                            //string[] sz;
                            //ss = getMsg;
                            //sz = ss.Split(',');
                            //if (sz[0] == "posdangqian")
                            //{

                            //}
                            data_class.tcp_data = getMsg;
                        }                    
                        ns.Flush();
                    }


                }


            }
            catch (ThreadAbortException)
            {
                // 人为终止线程
            }
            catch (Exception ex)
            {
                // 连接发生异常
                MessageBox.Show(ex.Message);
                // 释放相关系统资源
                if (tcpClient != null)
                    tcpClient.Close();
                if (ns != null)
                    ns.Dispose();
                myTcpListener.Stop();
                // 终止监听线程
                TcpClientAcceptThread.Abort();
            }
        }

       
      

      
      

    }
}
