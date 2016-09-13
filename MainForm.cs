using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quobject.SocketIoClientDotNet.Client;

namespace AbraControlPanel
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        public void SetSocket()
        {

            var socket = IO.Socket("http://192.168.100.104:8000");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                socket.Emit("hi");

            });

            socket.On("hi", (data) =>
            {

                textBox1.Text += data + Environment.NewLine;
                socket.Disconnect();
            });

        }


    }
}
