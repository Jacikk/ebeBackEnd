using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace EBE_Backend.Classes
{
    class Subevent_has_User
    {
        private int id,
            user_has_Events_id,
            subevent_id,
            user_id;

        private bool confirmated;

        public Subevent_has_User(int id, int user_has_Events_id, int subevent_id, int user_id, bool confirmated)
        {
            this.id = id;
            this.user_has_Events_id = user_has_Events_id;
            this.subevent_id = subevent_id;
            this.user_id = user_id;
            this.confirmated = confirmated;
        }

        public int Id { get => id; set => id = value; }
        public int User_has_Events_id { get => user_has_Events_id; set => user_has_Events_id = value; }
        public int Subevent_id { get => subevent_id; set => subevent_id = value; }
        public int User_id { get => user_id; set => user_id = value; }
        public bool Confirmated { get => confirmated; set => confirmated = value; }

        ~Subevent_has_User()
        {
            Console.WriteLine("Subevent_has_User destructor was called. Open fire!");
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
                cmd.CommandText = "INSERT INTO Subevent_has_User (User_has_events_id, Subevent_id, confirmated, user_id) VALUES ( @User_has_events_id, @Subevent_id, @confirmated, @user_id);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@User_has_events_id", this.user_has_Events_id);
                cmd.Parameters.AddWithValue("@Subevent_id", this.subevent_id);
                cmd.Parameters.AddWithValue("@confirmated", this.confirmated);
                cmd.Parameters.AddWithValue("@user_id", this.user_id);

                cmd.ExecuteNonQuery();
                Console.WriteLine("cadastrado!");
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

            string statement = "select * from Subevent_has_User";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("id: {0}, User_has_Events_Id: {1}, Subevent_Id: {2}, User_id: {3}, confirmated: {4}",
                        reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetBoolean(4));
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
                cmd.CommandText = "update Subevent_has_User set User_has_events_id= @User_has_events_id, Subevent_id = @Subevent_id, user_id = @user_id, confirmated = @confirmated where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@User_has_events_id", this.user_has_Events_id);
                cmd.Parameters.AddWithValue("@Subevent_id", this.subevent_id);
                cmd.Parameters.AddWithValue("@confirmated", this.confirmated);
                cmd.Parameters.AddWithValue("@user_id", this.user_id);
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
                cmd.CommandText = "delete from Subevent_has_User where Id = @id;";
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
