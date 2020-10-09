using Items;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsREST.DBUtil
{
    public class ManageItems
    {
        private const String connString = "@tcp:testing-benj5174.database.windows.net,1433;Initial Catalog = testing - benj5174; Persist Security Info=False;User ID = benj5174; Password={your_password}; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";

        private const String GET_ALL = "select* from Items";
        public IEnumerable<Item> Get()
        {
            IList<Item> retListe = new List<Item>();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        retListe.Add(ReadNextItem(reader));
                    }
                }
            }
            return retListe;
        }
        private const string GET_ONE = "select * from Item where Nr = @Nr";
        public Item Get(int id)
        {
            Item item = new Item();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
                {
                    cmd.Parameters.AddWithValue("@Nr", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        item = ReadNextItem(reader);
                    }
                }
            }

            return item;
        }
        private const string INSERT_SQL = "insert into Item (Nr, Desciption,Price) values(@Nr, @Desciption, @Price)";
        public bool Opret(Item item)
        {
            bool ok = false;

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(INSERT_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Nr", item.Nr);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    cmd.Parameters.AddWithValue("@Desciption", item.Desciption);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();

                        ok = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        ok = false;
                    }
                }
            }

            return ok;
        }
        private const string UPDATE_SQL = "update Item set Nr = @Nr, Price = @Price, Desciption= @Desciption where Nr = @Nr";
        public bool OpdaterItem(int id, Item item)
        {
            bool OK = true;
            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(UPDATE_SQL, conn))
                {
                    // NB In this case never set the id to a new value 
                    cmd.Parameters.AddWithValue("@Nr", item.Nr);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    cmd.Parameters.AddWithValue("@Desciption", item.Desciption);
                    cmd.Parameters.AddWithValue("@Nr", id);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        OK = false;
                    }
                }
            }

            return OK;
        }
        private const string DELETE_SQL = "delete from Item where Nr = @Nr";
        public Item DeleteItem(int id)
        {
            Item item = Get(id);

            if (item.Nr != -1)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(DELETE_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nr", id);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            return item
        }
            private Item ReadNextItem(SqlDataReader reader)
        {
            Item item = new Item();

            item.Nr = reader.GetInt32(0);
            item.Desciption = reader.GetString(1);
            item.Price = reader.GetInt32(2);


            return item;
        }
    }
}
