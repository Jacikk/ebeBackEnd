using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace EBE_Backend.Classes
{
    class SubEvent
    {
        int id, eventId;

        private string
            name,
            starting,
            ending,
            description;
        
        public SubEvent ()
        {

        }
        public SubEvent(int id, int eventId, string name, string starting, string ending, 
            string description)
        {
            this.id = id;
            this.eventId = eventId;
            this.name = name;
            this.starting = starting;
            this.ending = ending;
            this.description = description;
        }

        public string Name { get => name; set => name = value; }
        public string Starting { get => starting; set => starting = value; }
        public string Ending { get => ending; set => ending = value; }
        public string Description { get => description; set => description = value; }
        public int Id { get => id; set => id = value; }
        public int EventId { get => eventId; set => eventId = value; }

        ~SubEvent()
        {
            Console.WriteLine("SubEvent destructor was called. Open fire!");
        }

        public void Create( )
        {

            using var connection = new MySqlConnection(@"server=localhost;userid=Jacik;password=1234;database=ebedata");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection
            };

            try
            {
                cmd.CommandText = "INSERT INTO SubEvent (idEvents, name, starting, ending, description) VALUES ( @idEvents, @name, @starting, @ending, @description);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@idEvents", this.eventId);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@starting", this.starting);
                cmd.Parameters.AddWithValue("@ending", this.ending);
                cmd.Parameters.AddWithValue("@description", this.description);

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows != 0)
                {
                    this.id = (int)cmd.LastInsertedId;
                }
                Console.WriteLine("SubEvent cadastrado! Id: " + this.id);
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

            string statement = "select * from SubEvents";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: {0}, idEvents: {1}, eventName: {2}, starting: {3}, ending: {4}, description: {5}",
                        reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(4), reader.GetString(5), reader.GetString(6));
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

        public SubEvent GetById(int id)
        {
            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            var idFound = false;

            using var connection = new MySqlConnection(url);

            connection.Open();

            string statement = "select * from SubEvent";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) == id)
                    {

                        this.id = reader.GetInt32(0);
                        this.eventId = reader.GetInt32(1);
                        this.name = reader.GetString(2);
                        this.starting = reader.GetString(3);
                        this.ending = reader.GetString(4);
                        this.description = reader.GetString(5); 

                    }

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
            if (idFound)
            {
                return this;
            }
            else
            {
                return null;
            }
            
        }

        public void Update( )
        {
            using var connection = new MySqlConnection(@"server=localhost;userid=Jacik;password=1234;database=ebedata");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection
            };

            try
            {
                cmd.CommandText = "update SubEvent set idEvents= @idEvents, name = @name, starting = @starting, ending = @ending, description = @description where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@idEvents", this.eventId);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@starting", this.starting);
                cmd.Parameters.AddWithValue("@ending", this.ending);
                cmd.Parameters.AddWithValue("@description", this.description);
                cmd.Parameters.AddWithValue("@id", this.id);
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
                cmd.CommandText = "delete from SubEvent where Id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
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
