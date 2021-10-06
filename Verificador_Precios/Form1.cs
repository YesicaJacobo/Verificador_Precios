using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Verificador_Precios
{
    public partial class Form1 : Form
    {
        private String codigo = "";
        private int segundos = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pictureBox1.Location = new Point(this.Width / 2 - pictureBox1.Width / 2, 0);
            //label1.Location = new Point(this.Width / 2 - label1.Width / 2, pictureBox1.Height + 10);
            //pictureBox2.Location = new Point(this.Width/2 - pictureBox2.Width/2,this.Height/2);
            label6.Visible = false;
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    MySqlConnection servidor;
                    servidor = new MySqlConnection("server = 127.0.0.1; user = root; database = verificador_de_precios; SSL Mode = None; ");
                    servidor.Open();
                    
                    String query = "SELECT producto_nombre, producto_cantidad, producto_precio, producto_imagen, producto_desc FROM productos WHERE producto_codigo ="+codigo+";";
                    
                    MySqlCommand consulta;
                    consulta = new MySqlCommand(query, servidor);
                    
                    MySqlDataReader resultado = consulta.ExecuteReader();
                    if (resultado.HasRows)
                    {
                        resultado.Read();

                        label1.Visible = false;
                        label3.Visible = false;
                        pictureBox1.Visible = false;
                        pictureBox2.Visible=false;
                        pictureBox4.Visible = false;

                        label2.Visible = true;
                        label4.Visible = true;
                        label5.Visible = true;
                        label7.Visible = true;
                        pictureBox3.Visible = true;
                        pictureBox6.Visible = true;

                        label6.Visible = false;
                        pictureBox7.Visible = false;

                        //label2.Text = resultado.GetString(0)+Environment.NewLine+"Precio:"+resultado.GetString(2)+
                        // Environment.NewLine + "Cantidad:" + resultado.GetString(1);
                        label2.Text = resultado.GetString(0);
                        label4.Text = "$" + resultado.GetString(2);
                        label5.Text = "Disponible en surcursal: " + resultado.GetString(1);
                        label7.Text = resultado.GetString(4);


                        pictureBox3.Image = Image.FromFile(resultado.GetString(3)); //imglocal
                        //pictureBox3.ImageLocation = resultado.GetString(3); //imgweb
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                        
                        segundos = 0;
                        timer1.Enabled = true;
                    }
                    else
                    {
                        //MessageBox.Show("Llame al supervisor el producto no fue encontrado");
                        label1.Visible = false;
                        label3.Visible = false;
                        label7.Visible = false;
                        pictureBox1.Visible = false;
                        pictureBox2.Visible = false;
                        pictureBox4.Visible = false;

                        label6.Visible = true;
                        pictureBox7.Visible = true;

                        segundos = 0;
                        timer1.Enabled = true;
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString(), "Titulo", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
                codigo = "";
            }
            else
            {
                codigo += e.KeyChar;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            segundos++;
            if (segundos == 4)
            {
                timer1.Enabled = false;

                label1.Visible = true;
                label3.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox4.Visible = true;

                label2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
                pictureBox3.Visible = false;
                pictureBox6.Visible = false;
                label2.Text = "";
                label4.Text = "";
                label5.Text = "";
                label7.Text = "";

                label6.Visible = false;
                pictureBox7.Visible = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
