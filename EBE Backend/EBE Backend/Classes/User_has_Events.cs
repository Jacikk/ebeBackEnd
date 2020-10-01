using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace EBE_Backend.Classes
{
    class User_has_Events
    {
        private int id, user_id, events_id;
        private bool confirmated;

        public User_has_Events()
        {

        }
        public User_has_Events(int id, int user_id, int events_id, bool confirmated)
        {
            this.id = id;
            this.user_id = user_id;
            this.events_id = events_id;
            this.confirmated = confirmated;
        }

        public int Id { get => id; set => id = value; }
        public int User_id { get => user_id; set => user_id = value; }
        public int Events_id { get => events_id; set => events_id = value; }
        public bool Confirmated { get => confirmated; set => confirmated = value; }

        ~User_has_Events()
        {
            Console.WriteLine("User_has_Events destructor was called. Open fire!");
        }
        public void Create()
        {
            using var connection = new MySqlConnection(@"server=localhost;userid=Jacik;password=1234;database=ebedata");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection
            };

            try
            {
                cmd.CommandText = "INSERT INTO User_has_Events (User_id, Events_id, confirmated) VALUES ( @User_id, @Events_id, @confirmated);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@User_id", this.user_id);
                cmd.Parameters.AddWithValue("@Events_id", this.events_id);
                cmd.Parameters.AddWithValue("@confirmated", this.confirmated);

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows != 0)
                {
                    this.id = (int)cmd.LastInsertedId;
                }
                Console.WriteLine("User_has_events cadastrado! Id: " + this.id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

        }
        public void ReadTable()
        {
            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            using var connection = new MySqlConnection(url);

            connection.Open();

            string statement = "select * from User_has_Events";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {   
                while (reader.Read())
                {
                    Console.WriteLine("id: {0}, User_id: {1}, Events_Id: {2}, confirmated: {3}",
                        reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),  reader.GetBoolean(3));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update()
        {
            using var connection = new MySqlConnection(@"server=localhost;userid=Jacik;password=1234;database=ebedata");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection
            };

            try
            {
                cmd.CommandText = "update User_has_Events set User_id= @User_id, Events_id = @Events_id, confirmated = @confirmated where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@User_id", this.user_id);
                cmd.Parameters.AddWithValue("@Events_id", this.events_id);
                cmd.Parameters.AddWithValue("@confirmated", this.confirmated);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Atualizado!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }
        public void Delete()
        {
            using var connection = new MySqlConnection(@"server=localhost;userid=Jacik;password=1234;database=ebedata");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection
            };

            try
            {
                cmd.CommandText = "delete from User_has_Events where Id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("id" + this.id + "deletado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
