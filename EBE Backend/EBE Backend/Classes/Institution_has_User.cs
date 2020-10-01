using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace EBE_Backend.Classes
{
    class Institution_has_User
    {
        private int institution_Id, user_Id, id;

        public Institution_has_User ()
        {

        }
        public Institution_has_User(int id, int instituiton_id, int user_id)
        {
            this.id = id;
            this.institution_Id = instituiton_id;
            this.user_Id = user_id;
        }

        public int Instituiton_id { get => institution_Id; set => institution_Id = value; }
        public int User_id { get => user_Id; set => user_Id = value; }
        public int Id { get => id; set => id = value; }

        ~Institution_has_User()
        {
            Console.WriteLine("Institution_has_User destructor was called. Open fire!");
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
                cmd.CommandText = "INSERT INTO Institution_has_User (Institution_Id, User_Id) VALUES ( @Institution_Id, @User_Id);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Institution_Id", this.institution_Id);
                cmd.Parameters.AddWithValue("@User_Id", this.user_Id);

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows != 0)
                {
                    this.id = (int)cmd.LastInsertedId;
                }
                Console.WriteLine("Institution_has_User cadastrado! Id: " + this.id);
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

            string statement = "select * from Institution_has_User";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("id: {0}, Institution_Id: {1}, User_Id: {2}",
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
                cmd.CommandText = "update Institution_has_User set Institution_Id= @Institution_Id, User_Id = @User_Id where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@Institution_Id", this.institution_Id);
                cmd.Parameters.AddWithValue("@User_Id", this.user_Id);
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
                cmd.CommandText = "delete from Institution_has_User where Id = @id;";
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
