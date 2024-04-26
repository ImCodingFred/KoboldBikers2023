using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KoboldBikers2023
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadType();
        }

        private void LoadType()
        {
            type_cb.Items.Clear();
            type_cb.Items.Add("");
            database db = new database("SELECT types.name FROM types GROUP BY name");
            while (db.Dr.Read())
            {
                type_cb.Items.Add(db.Dr[0]);
            }
        }

        private void LoadTable()
        {
            DGV.Rows.Clear();
            database db = new database("SELECT kobolds.name,types.name,bikes.color,types.size,rentals.start,rentals.days,types.price " +
                "FROM kobolds,rentals,bikes,types " +
                "WHERE kobolds.id=rentals.koboldID AND rentals.bikeID=bikes.id AND bikes.typeID=types.id");
            while (db.Dr.Read())
            {
                char size;
                if (db.Dr[3].ToString() == "0")
                {
                    size = 'S';
                }
                else
                {
                    size = 'M';
                }
                DateTime end = DateTime.Parse(db.Dr[4].ToString());
                end=end.AddDays(int.Parse(db.Dr[5].ToString()));
                int price = int.Parse(db.Dr[5].ToString())* int.Parse(db.Dr[6].ToString());
                DGV.Rows.Add(db.Dr[0], $"{db.Dr[1]} ({db.Dr[2]},{size})", db.Dr[4], end,$"{price} GP");
            }
        }

        private void LoadTable(string type)
        {
            if (type!="")
            {
                DGV.Rows.Clear();
                database db = new database("SELECT kobolds.name,types.name,bikes.color,types.size,rentals.start,rentals.days,types.price " +
                    "FROM kobolds,rentals,bikes,types " +
                    $"WHERE kobolds.id=rentals.koboldID AND rentals.bikeID=bikes.id AND bikes.typeID=types.id AND types.name ='{type}'");
                while (db.Dr.Read())
                {
                    char size;
                    if (db.Dr[3].ToString() == "0")
                    {
                        size = 'S';
                    }
                    else
                    {
                        size = 'M';
                    }
                    DateTime end = DateTime.Parse(db.Dr[4].ToString());
                    end.AddDays(double.Parse(db.Dr[5].ToString()));
                    int price = int.Parse(db.Dr[5].ToString()) * int.Parse(db.Dr[6].ToString());
                    DGV.Rows.Add(db.Dr[0], $"{db.Dr[1]} ({db.Dr[2]},{size})", db.Dr[4], end, $"{price} GP");
                }
            }
        }

        private void type_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTable(type_cb.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewRental rental = new NewRental();
            rental.ShowDialog();
        }
    }
}
