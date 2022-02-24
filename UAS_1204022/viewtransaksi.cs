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

namespace UAS_1204022
{
    public partial class viewtransaksi : Form
    {
        koneksidb konn = new koneksidb();
        private MySqlCommand cmd;
        private MySqlCommand cmd1;
        private DataSet ds;
        private MySqlDataAdapter da;
        private MySqlDataReader rd;

        public viewtransaksi()
        {
            InitializeComponent();
        }

        void datatransaksi()
        {
            MySqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new MySqlCommand("SELECT * FROM tr_daftar_ulang;", conn);
            ds = new DataSet();
            da = new MySqlDataAdapter(cmd);

            da.Fill(ds, "tr_daftar_ulang");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tr_daftar_ulang";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();
        }

        private void viewtransaksi_Load(object sender, EventArgs e)
        {
            datatransaksi();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Transakis dihapus?", "APAKAH DATA INGIN DIHAPUS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells["npm"].FormattedValue.ToString();
                MySqlConnection conn = konn.GetConn();
                conn.Open();
                MySqlCommand com = new MySqlCommand("Delete from tr_daftar_ulang where npm='" + id + "'", conn);
                com.ExecuteNonQuery();
                MessageBox.Show("DATA SUKSES DIHAPUS");
                conn.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = konn.GetConn();
            MySqlCommand com = new MySqlCommand(" Select * from tr_daftar_ulang", conn);
            MySqlDataAdapter d = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
