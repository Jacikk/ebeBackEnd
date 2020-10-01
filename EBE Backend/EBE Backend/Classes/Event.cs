using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace EBE_Backend.Classes
{
    class Event
    {
        private string
            eventName,
            starting,
            ending,
            description,
            manager,
            avatar,
            addressReference;

        private int id, addressId, addressNumber;

        private Address address;
        public Event()
        {

        }
        public Event(
            int id,
            int addressId,
            string eventName, 
            string starting, 
            string ending, 
            string description, 
            string manager,
            string avatar,
            int addressNumber,
            string addressReference)
        {
            this.id = id;
            this.addressId = addressId;
            this.eventName = eventName;
            this.starting = starting;
            this.ending = ending;
            this.description = description;
            this.manager = manager;
            this.avatar = avatar;
            this.addressNumber = addressNumber;
            this.addressReference = addressReference;

            address.GetById(addressId);
            
        }

        public string EventName { get => eventName; set => eventName = value; }
        public string Starting { get => starting; set => starting = value; }
        public string Ending { get => ending; set => ending = value; }
        public string Description { get => description; set => description = value; }
        public string Manager { get => manager; set => manager = value; }
        public int Id { get => id; set => id = value; }
        public int AddressId { get => addressId; set => addressId = value; }
        public string Avatar { get => avatar; set => avatar = value; }
        public string AddressReference { get => addressReference; set => addressReference = value; }
        public int AddressNumber { get => addressNumber; set => addressNumber = value; }

        ~Event() 
        {
            Console.WriteLine("Event destructor was called. Open fire!");
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
                cmd.CommandText = "INSERT INTO Events (addressId, eventName, starting, ending, description, manager, avatar, addressNumber, addressReference) VALUES (@addressId, @eventName, @starting, @ending, @description, @manager, @avatar, @addressNumber, @addressReference);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@addressId", this.address);
                cmd.Parameters.AddWithValue("@eventName", this.eventName);
                cmd.Parameters.AddWithValue("@starting", this.starting);
                cmd.Parameters.AddWithValue("@ending", this.ending);
                cmd.Parameters.AddWithValue("@description", this.description);
                cmd.Parameters.AddWithValue("@avatar", this.avatar);
                cmd.Parameters.AddWithValue("@addressNumber", this.addressNumber);
                cmd.Parameters.AddWithValue("@addressReference", this.addressReference);
                cmd.Parameters.AddWithValue("@manager", this.manager);

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows != 0)
                {
                    this.id = (int)cmd.LastInsertedId;
                }
                Console.WriteLine("Event cadastrado! Id: " + this.id);
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

            string statement = "select * from Events";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: {0}, eventId: {1}, eventName: {2}, starting: {3}, ending: {4}, discription: {5}, manager: {6}, avatar: {7}, addressNumber: {8}, addressReference: {9}",
                        reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetString(9));
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

        public Event GetById(int id)
        {
            var idFound = false;
            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            using var connection = new MySqlConnection(url);

            connection.Open();

            string statement = "select * from Events";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) == id)
                    {

                        this.id = reader.GetInt32(0);
                        this.addressId = reader.GetInt32(1);
                        this.eventName = reader.GetString(2);
                        this.starting = reader.GetString(3);
                        this.ending = reader.GetString(4);
                        this.description = reader.GetString(5);
                        this.manager = reader.GetString(6);
                        this.avatar = reader.GetString(7);
                        this.addressNumber = reader.GetInt32(8);
                        this.addressReference = reader.GetString(9);

                        idFound = true;
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
                cmd.CommandText = "update Events set addressId= @addressId, eventName = @eventName, starting = @starting, ending = @ending, discription = @description, manager = @manager, avatar = @avatar, addressNumber = @addressNumber, addressReference = @addressReference where id =@id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@addressId", this.address);
                cmd.Parameters.AddWithValue("@eventName", this.eventName);
                cmd.Parameters.AddWithValue("@starting", this.starting);
                cmd.Parameters.AddWithValue("@ending", this.ending);
                cmd.Parameters.AddWithValue("@description", this.description);
                cmd.Parameters.AddWithValue("@avatar", this.avatar);
                cmd.Parameters.AddWithValue("@addressNumber", this.addressNumber);
                cmd.Parameters.AddWithValue("@addressReference", this.addressReference);
                cmd.Parameters.AddWithValue("@manager", this.manager);
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
                cmd.CommandText = "delete from Events where Id = @id;";
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
