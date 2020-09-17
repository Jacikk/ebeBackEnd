using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace EBE_Backend.Classes
{
    class Managers
    {
        private int id,
                events_id,
                user_id;

        public Managers(int id, int events_id, int user_id)
        {
            this.id = id;
            this.events_id = events_id;
            this.user_id = user_id;
        }

        public int Id { get => id; set => id = value; }
        public int Events_id { get => events_id; set => events_id = value; }
        public int User_id { get => user_id; set => user_id = value; }
        ~Managers()
        {
            Console.WriteLine("Managers destructor was called. Open fire!");
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
                cmd.CommandText = "INSERT INTO Managers (Events_Id, User_Id) VALUES ( @Events_Id, @User_Id);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Events_Id", this.events_id);
                cmd.Parameters.AddWithValue("@User_Id", this.user_id);

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

            string statement = "select * from Managers";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("id: {0}, Events_Id: {1}, User_Id: {2}",
                        reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
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
                cmd.CommandText = "update Managers set Events_Id= @Events_Id, User_Id = @User_Id where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@Institution_Id", this.events_id);
                cmd.Parameters.AddWithValue("@User_Id", this.user_id);
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
                cmd.CommandText = "delete from Managers where Id = @id;";
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
