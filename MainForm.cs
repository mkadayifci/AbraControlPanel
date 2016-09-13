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
        Socket socket = IO.Socket("http://192.168.100.104:8000");

        public MainForm()
        {
            InitializeComponent();
            SetSocket();

        }


        public void SetSocket()
        {

            //socket.Connect();

            socket.On(Socket.EVENT_CONNECT, () =>
            {
                socket.Emit("tweet");

            });

            socket.On("tweet", (data) =>
            {

                AppendTextBox(data + Environment.NewLine);
                //socket.Disconnect();
            });

        }


        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBox1.Text += value;
        }

        private void light1_Scroll(object sender, EventArgs e)
        {
            this.Text = light1.Value.ToString();

            string message = "{ \"Action\" : \"SetLightBrightness\",\"LightId\":\"1\", \"Value\" : " + light1.Value.ToString() + "}";

            socket.Emit("Command",message);


        }
    }
}
