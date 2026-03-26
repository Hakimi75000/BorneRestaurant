using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BorneRestaurant.data
{
    public static class Db
    {
        
        public static string ConnStr =
            "Server=localhost;Port=3306;Database=borne_restaurant;Uid=root;Pwd=;";



        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnStr);
        }
    }
}

