using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UAS_1204022
{
    class koneksidb
    {
        private MySqlCommand cmd;
        private MySqlDataAdapter da;
        
        int result;
        private string url = "server=localhost;userid=root;password=;database=uas";
        public MySqlConnection GetConn()
        {
            MySqlConnection Conn = new MySqlConnection(url);
            return Conn;
        }
    }
}
