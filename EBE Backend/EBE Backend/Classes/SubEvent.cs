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
            description,
            palestrantes,
            manager;
        

        public SubEvent(int id, int eventId, string name, string palestrantes, string starting, string ending, 
            string description, string manager)
        {
            this.id = id;
            this.eventId = eventId;
            this.name = name;
            this.starting = starting;
            this.ending = ending;
            this.description = description;
            this.manager = manager;
            this.palestrantes = palestrantes;
        }

        public string Name { get => name; set => name = value; }
        public string Starting { get => starting; set => starting = value; }
        public string Ending { get => ending; set => ending = value; }
        public string Description { get => description; set => description = value; }
        public string Palestrantes { get => palestrantes; set => palestrantes = value; }
        public int Id { get => id; set => id = value; }
        public int EventId { get => eventId; set => eventId = value; }
        public string Manager { get => manager; set => manager = value; }

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
                cmd.CommandText = "INSERT INTO SubEvent (idEvents, name, palestrantes_User_id, starting, ending, discription, manager) VALUES ( '" + this.eventId + "', '" +  this.name + "', '" + this.palestrantes + "', '" + this.starting + "', '" + this.ending + "', '" + this.description + "', '" + this.manager + "');";
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

            string statement = "select * from SubEvents";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: {0}, idEvents: {1}, eventName: {2}, idPalestrantes: {3}, starting: {4}, ending: {5}, description: {6}, manager: {7}",
                        reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
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
                        this.palestrantes = reader.GetString(3);
                        this.starting = reader.GetString(4);
                        this.ending = reader.GetString(5);
                        this.description = reader.GetString(6); 
                        this.manager = reader.GetString(7);

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
                cmd.CommandText = "update SubEvent set idEvents= '" + this.eventId + "', name = '" + this.name + "', palestrantes = '" + this.palestrantes 
                    + "', starting = '" + this.starting + "', ending = '" + this.ending + "', description = '" + this.description + "', manager = '" + this.manager + "' where id =" + this.id + ";";
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
                cmd.CommandText = "delete from SubEvent where Id = " + this.id + ";";
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
