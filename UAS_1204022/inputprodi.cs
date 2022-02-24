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
    public partial class inputprodi : Form
    {
        koneksidb konn = new koneksidb();
        private MySqlCommand cmd;
        private MySqlCommand cmd1;
        private DataSet ds;
        private MySqlDataAdapter da;
        private MySqlDataReader rd;
        Decimal rupiah;

        public inputprodi()
        {
            InitializeComponent();
        }

        void keadaanawal()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            prodiotomatis();


        }

        void prodiotomatis()
        {
            long prodi;
            string urutan;
            MySqlDataReader rd;
            MySqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new MySqlCommand("select kode_prodi from ms_prodi where kode_prodi in(select max(kode_prodi) from ms_prodi) order by kode_prodi desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                prodi = Convert.ToInt64(rd[0].ToString().Substring(rd["kode_prodi"].ToString().Length - 3, 3)) + 1;
                string joinstr = "000" + prodi;
                urutan = "PRD" + joinstr.Substring(joinstr.Length - 3, 3);

            }
            else
            {
                urutan = "PRD001";
            }
            rd.Close();
            textBox1.Text = urutan;
            conn.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan semua telah terisi");
            }
            else
            {
                MySqlConnection conn = konn.GetConn();
                cmd = new MySqlCommand("insert into ms_prodi values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Prodi berhasil ditambah");
                keadaanawal();
                prodiotomatis();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            keadaanawal();
        }

        private void inputprodi_Load(object sender, EventArgs e)
        {
            keadaanawal();
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                int valueBefore = Int32.Parse(textBox4.Text, System.Globalization.NumberStyles.AllowThousands);
                textBox4.Text = String.Format(culture, "{0:N0}", valueBefore);
                textBox4.Select(textBox4.Text.Length, 0);
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            textBox4.Text = string.Format("{0:#,##0.00}", double.Parse(textBox4.Text));
        }
    }
}
