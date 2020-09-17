using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace EBE_Backend.Classes
{
    class Palestrantes
    {
        private int id, palestrante, subevent_id;

        public Palestrantes(int id, int palestrante, int subevent_id)
        {
            this.id = id;
            this.palestrante = palestrante;
            this.subevent_id = subevent_id;
        }

        public int Id { get => id; set => id = value; }
        public int Palestrante { get => palestrante; set => palestrante = value; }
        public int Subevent_id { get => subevent_id; set => subevent_id = value; }
        ~Palestrantes()
        {
            Console.WriteLine("Palestrantes destructor was called. Open fire!");
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
                cmd.CommandText = "INSERT INTO Palestrantes (Palestrante, Subevent_id) VALUES ( @Palestrante, @Subevent_id);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Palestrante", this.palestrante);
                cmd.Parameters.AddWithValue("@Subevent_id", this.subevent_id);

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

            string statement = "select * from Palestrantes";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("id: {0}, palestrante: {1}, subenvent_Id: {2}",
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
                cmd.CommandText = "update Palestrantes set Palestrante= @Palestrante, Subevent_id = @Subevent_id where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@Palestrante", this.palestrante);
                cmd.Parameters.AddWithValue("@Subevent_id", this.subevent_id);
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
                cmd.CommandText = "delete from Palestrantes where Id = @id;";
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
