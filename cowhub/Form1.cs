using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace cowhub
{
    public partial class Form1 : Form
    {

        static SerialPort serialPort;
        static string sertialStatus;

        public Form1()
        {
            InitializeComponent();
            ConfigureListView();
            LoadCows();
        }

        private void arduino()
        {

            serialPort = new SerialPort("COM6", 9600); // Cambia "COM3" al puerto al que está conectado tu Arduino

            serialPort.DataReceived += SerialPort_DataReceived; // Evento para manejar la recepción de datos

            try
            {
                serialPort.Open(); // Abre el puerto serial
                //MessageBox.Show("Conexión establecida. Esperando datos...");
                richTextBox2.Text = "Conexión establecida. Esperando datos...";



            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al abrir el puerto serial: " + ex.Message);
                richTextBox2.Text = "Error al abrir el puerto serial: " + ex.Message;
            }

            /*finally
            {
                serialPort.Close(); // Cierra el puerto serial al finalizar
                richTextBox2.Text = "Puerto COM cerrado";
            }*/
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender;
            string data = serialPort.ReadLine(); // Lee una línea de datos del puerto serial

            // Actualiza el control de la interfaz de usuario utilizando Invoke
            Invoke(new Action(() =>
            {
                richTextBox2.AppendText(data + "\n");
                richTextBox2.ScrollToCaret();
            }));

            if (int.TryParse(data, out int fillHeight))
            {
                fillBottle(fillHeight);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string code = textBox2.Text;
            string description = richTextBox1.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new CowhubDbContext())
            {
                var newCow = new Cow
                {
                    Name = name,
                    Code = code,
                    Description = description,
                };

                context.Cows.Add(newCow);
                context.SaveChanges();
            }

            MessageBox.Show("Registro añadido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            // Manejo del clic en la pestaña, si es necesario
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Manejo del clic en la etiqueta, si es necesario
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ConfigureListView()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("Name", 150);
            listView1.Columns.Add("Code", 100);
            listView1.Columns.Add("Description", 200);
        }


        private void LoadCows()
        {
            using (var context = new CowhubDbContext())
            {
                var cows = context.Cows.ToList();

                listView1.Items.Clear();

                foreach (var cow in cows)
                {
                    var item = new ListViewItem(cow.Id.ToString());
                    item.SubItems.Add(cow.Name);
                    item.SubItems.Add(cow.Code);
                    item.SubItems.Add(cow.Description);
                    listView1.Items.Add(item);
                }
            }
        }

        private void fillBottle(int distance)
        {
            Invoke(new Action(() =>
            {
                distance = distance - 14;
                distance = (distance > 17) ? 17 : distance;
                panel3.Height = 170 - ((distance) * 10);
                panel3.Location = new Point(panel3.Location.X, 56 + (218 - panel3.Height));
                label11.Text = (((3.1416) * (64) * (17 - (distance))) / 1000).ToString($"F{2}") + 'L';
            }));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.arduino();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.TabIndex = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.TabIndex = 2;
        }
    }
}
