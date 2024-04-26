using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace KoboldBikers2023
{
    internal class database
    {
        private string server = "SERVER=localhost;DATABASE=koboldrental;UID=root;PASSWORD=";

        private MySqlConnection con;
        private MySqlCommand command;
        private MySqlDataReader dr;

        public MySqlDataReader Dr { get => dr; set => dr = value; }

        public database(string querry) 
        {
            con = new MySqlConnection(server);
            command = new MySqlCommand(querry,con);
            con.Open();
            dr = command.ExecuteReader();
        }
        public void Close() 
        {
            con.Close();
        }
        ~database()
        {
            con.Close();
        }
    }
}
