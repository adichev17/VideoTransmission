using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
            var client = new UdpClient(port); //UDP (User Datagram Protocol) представляет сетевой протокол, который позволяет доставить данные на удаленный узел.

            while (true) {
                var data = await client.ReceiveAsync(); // получаем данные
                using(var ms = new MemoryStream(data.Buffer)) //данные в байтах
                {
                    pictureBox1.Image = new Bitmap(ms); //отрисовываем пиксели
                }
                Text = $"Bytes received: {data.Buffer.Length * sizeof(byte)}"; // считаем объём данных в байт
            }
        }

        // dns - Предоставляет простые функциональные возможности разрешения доменных имен.Для взаимодействия с dns-сервером и получения ip-адреса применяется класс Dns. 
        //Для получения информации о хосте компьютера и его адресах у него имеется метод GetHostEntry().gethostname() возвращает стандартное имя локальной машины.
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName()); //

            MessageBox.Show(string.Join("\n", host.AddressList.Where(i => i.AddressFamily == AddressFamily.InterNetwork))); // по двоёному клику выводим наш ip
        }
    }
}
