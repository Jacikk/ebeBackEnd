using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections;

namespace EBE_Backend.Classes
{
    class Institution
    {
        private string
            name,
            cnpj,
            email,
            password,
            description,
            avatarUrl,
            addressReference;

        private int id, addressNumber, addressId;

        private Address address = new Address();
        public Institution ()
        {

        }
        public Institution(
            int id,
            int addressId,
            string name,
            string cnpj,
            string email,
            string password,
            string description,
            string avatarUrl,
            string addressReference,
            int addressNumber)
        {
            this.id = id;
            this.addressId = addressId;
            this.name = name;
            this.cnpj = cnpj;
            this.email = email;
            this.password = password;
            this.description = description;
            this.avatarUrl = avatarUrl;
            this.addressNumber = addressNumber;
            this.addressReference = addressReference;

            address.GetById(addressId);
        }

        public string Name { get => name; set => name = value; }
        public string Cnpj { get => cnpj; set => cnpj = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Description { get => description; set => description = value; }
        public string AvatarUrl { get => avatarUrl; set => avatarUrl = value; }
        public string AddressReference { get => addressReference; set => addressReference = value; }
        public int AddressNumber { get => addressNumber; set => addressNumber = value; }
        public int Id { get => id; set => id = value; }
        public int AddressId { get => addressId; set => addressId = value; }

        ~Institution()
        {
            Console.WriteLine("Institution destructor was called. Open fire!");
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

                cmd.CommandText = "INSERT INTO Institution (addressId, name, CNPJ, email, password, description, avatar, addressNumber, addressReference ) VALUES ( @addressId, @name, @cnpj, @email, @password, @description, @avatarUrl, @addressNumber, @addressReference)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@addressId", this.addressId);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@cnpj", this.cnpj);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@password", this.password);
                cmd.Parameters.AddWithValue("@description", this.description);
                cmd.Parameters.AddWithValue("@avatarurl", this.avatarUrl);
                cmd.Parameters.AddWithValue("@addressNumber", this.addressNumber);
                cmd.Parameters.AddWithValue("@addressReference", this.addressReference);

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows != 0)
                {
                    this.id = (int)cmd.LastInsertedId;
                }
                Console.WriteLine("Institution cadastrado! Id: " + this.id);
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

        public ArrayList ReadTable()
        {
            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            using var connection = new MySqlConnection(url);

            connection.Open();

            string statement = "select * from Institution";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();
            ArrayList InsList = new ArrayList();
            try
            {
                while (reader.Read())
                {
                    Institution inst = new Institution();

                    inst.id = reader.GetInt32(0);
                    inst.addressId = reader.GetInt32(1);
                    inst.name = reader.GetString(2);
                    inst.cnpj = reader.GetString(3);
                    inst.email = reader.GetString(4);
                    inst.password = reader.GetString(5);
                    inst.description = reader.GetString(6);
                    inst.avatarUrl = reader.GetString(7);
                    inst.addressNumber = reader.GetInt32(8);
                    inst.addressReference = reader.GetString(9);

                    InsList.Add(inst);
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
            return InsList;
        }

        public Institution GetById(int id)
        {
            var idFound = false;
            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            using var connection = new MySqlConnection(url);

            connection.Open();

            string statement = "select * from Institution";

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
                        this.name = reader.GetString(2);
                        this.cnpj = reader.GetString(3);
                        this.email = reader.GetString(4);
                        this.password = reader.GetString(5);
                        this.description = reader.GetString(6);
                        this.avatarUrl = reader.GetString(7);
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
                cmd.CommandText = "update Institution set addressId = @addressId, name = @name, CNPJ = @cnpj, email = @email, password = @password, discription = @description, avatar = @avatarUrl, addressNumber = @addressNumber, addressReference = @addressReference, where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@addressId", this.addressId);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@cnpj", this.cnpj);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@password", this.password);
                cmd.Parameters.AddWithValue("@description", this.description);
                cmd.Parameters.AddWithValue("@avatar", this.avatarUrl);
                cmd.Parameters.AddWithValue("@addressNumber", this.addressNumber);
                cmd.Parameters.AddWithValue("@addressReference", this.addressReference);
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
                cmd.CommandText = "delete from Institution where Id = @id;";
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
