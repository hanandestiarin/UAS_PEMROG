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
using System.Globalization;

namespace UAS_1204022
{
    public partial class transaksi : Form
    {
        koneksidb konn = new koneksidb();
        private MySqlCommand cmd;
        private MySqlCommand cmd1;
        private DataSet ds;
        private MySqlDataAdapter da;
        private MySqlDataReader rd;
        double number1 = 0;
        double result = 0;

        public transaksi()
        {
            InitializeComponent();
        }

        void keadaanawal()
        {
            label9.Text = "0";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

        }

        void mhs()
        {

            MySqlDataReader rd;
            MySqlConnection conn = konn.GetConn();
            conn.Open();

            cmd = new MySqlCommand("select * from ms_mhs where npm='" + textBox1.Text + "'", conn);
            cmd.CommandType = CommandType.Text;
            rd = cmd.ExecuteReader();
            rd.Read();

            if (rd.HasRows)


            {
                textBox1.Text = rd[0].ToString();
                textBox2.Text = rd[1].ToString();
                textBox3.Text = rd[2].ToString();
                jurusan();

            }
        }

        void jurusan()
        {

            MySqlDataReader rd;
            MySqlConnection conn = konn.GetConn();
            conn.Open();

            cmd1 = new MySqlCommand("select * from ms_prodi where kode_prodi='" + textBox3.Text + "'", conn);
            cmd1.CommandType = CommandType.Text;
            rd = cmd1.ExecuteReader();
            rd.Read();

            if (rd.HasRows)


            {

                label9.Text = rd[3].ToString();


            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void transaksi_Load(object sender, EventArgs e)
        {
            keadaanawal();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                mhs();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            jurusan();
        }

        void diskona()
        {
            textBox5.Text = string.Format(new CultureInfo("id-ID"), "{0:C}", double.Parse(label9.Text, NumberStyles.Currency) * 0.5);
            textBox6.Text = string.Format(new CultureInfo("id-ID"), "{0:C}", double.Parse(label9.Text, NumberStyles.Currency) - double.Parse(textBox5.Text, NumberStyles.Currency));

        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            diskona();
        }

        void diskonb()
        {
            textBox5.Text = string.Format(new CultureInfo("id-ID"), "{0:C}", double.Parse(label9.Text, NumberStyles.Currency) * 0.25);
            textBox6.Text = string.Format(new CultureInfo("id-ID"), "{0:C}", double.Parse(label9.Text, NumberStyles.Currency) - double.Parse(textBox5.Text, NumberStyles.Currency));
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            diskonb();
        }

        void diskonc()
        {
            textBox5.Text = string.Format(new CultureInfo("id-ID"), "{0:C}", double.Parse(label9.Text, NumberStyles.Currency) * 0.1);
            textBox6.Text = string.Format(new CultureInfo("id-ID"), "{0:C}", double.Parse(label9.Text, NumberStyles.Currency) - double.Parse(textBox5.Text, NumberStyles.Currency));
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            diskonc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox6.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan semua terisi");
            }
            else
            {
                string npm = textBox1.Text.Trim();
                double totalbiaya = double.Parse(textBox6.Text, NumberStyles.Currency);
                string grade = string.Empty;
                if (radioButton1.Checked)
                {
                    grade = "A";
                }
                else if (radioButton2.Checked)
                {
                    grade = "B";
                }
                else if (radioButton3.Checked)
                {
                    grade = "C";
                }
                MySqlConnection conn = konn.GetConn();
                cmd = new MySqlCommand("INSERT INTO tr_daftar_ulang (npm, grade, total_biaya) VALUES(@NPM, @Grade, @Total_Biaya)");
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@NPM", npm);
                cmd.Parameters.AddWithValue("@Grade", grade);
                cmd.Parameters.AddWithValue("@Total_Biaya", totalbiaya);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Daftar Ulang Berhasil");
                keadaanawal();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            keadaanawal();
        }
    }
    }

