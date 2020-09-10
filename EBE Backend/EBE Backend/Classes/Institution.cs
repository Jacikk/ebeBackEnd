using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MySql.Data.MySqlClient;

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

        private int[] representantes;

        private int id, addressNumber, addressId;

        private Address address;

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
            int addressNumber,
            int[] representantes)//max 10
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
            this.representantes = representantes;

            address.GetById(this.addressId);
        }

        public string Name { get => name; set => name = value; }
        public string Cnpj { get => cnpj; set => cnpj = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Description { get => description; set => description = value; }
        public string AvatarUrl { get => avatarUrl; set => avatarUrl = value; }
        public int[] Representantes { get => representantes; set => representantes = value; }
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
                string staff = this.representantes[0].ToString();

                for (int i = 1; i < 10; i++)
                {
                    staff += "," + this.representantes[i].ToString();
                } 
                cmd.CommandText = "INSERT INTO Instituition (addressId, name, CNPJ, email, password, description, avatar, addressNumber, addressReference, staff ) VALUES ( @addressId, @name, @cnpj, @email, @password, @description, @avatarUrl, @addressNumber, @addressReference, @staf)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@addressId", this.addressId);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@cnpj", this.cnpj);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@password", this.password);
                cmd.Parameters.AddWithValue("@description", this.description);
                cmd.Parameters.AddWithValue("@avatar", this.avatarUrl);
                cmd.Parameters.AddWithValue("@addressNumber", this.addressNumber);
                cmd.Parameters.AddWithValue("@addressReference", this.addressReference);
                cmd.Parameters.AddWithValue("@staff", staff);
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

            string statement = "select * from Instituition";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id {0}, Name {1}, CNPJ {2}, email {3}, password {4}, discription {5}, avatarUrl {6}, staff {7} ",
                        reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(8), reader.GetString(7));
                    this.address.GetById(reader.GetInt32(1));
                    Console.WriteLine("cep:" + this.address.Cep + " Numero: " + reader.GetInt32(9) + "cidade: " + this.address.City + " pais: " + this.address.Country + " rua: " + this.address.Street + " estado: " + this.address.State + " bairro: " + this.address.District + " referencia: " + reader.GetString(10));
                   
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

        public Institution GetById(int id)
        {
            var idFound = false;
            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            using var connection = new MySqlConnection(url);

            connection.Open();

            string statement = "select * from Instituition";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) == id)
                    {

                        string[] staff = reader.GetString(7).Split(',');
                        

                        for (int i = 0; i < 10; i++)
                        {
                            if (this.representantes[i] != default)
                            {
                                this.representantes[i] = Int32.Parse(staff[i]);
                            }
                            else
                            {
                                this.representantes[i] = 0;
                            }

                        }
                        this.id = reader.GetInt32(0);
                        this.addressId = reader.GetInt32(1);
                        this.name = reader.GetString(2);
                        this.cnpj = reader.GetString(3);
                        this.email = reader.GetString(4);
                        this.password = reader.GetString(5);
                        this.description = reader.GetString(6);
                        this.avatarUrl = reader.GetString(8);
                        this.addressNumber = reader.GetInt32(9);
                        this.addressReference = reader.GetString(10);

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

            string staff = new string (this.representantes[0].ToString());

            for (int i = 1; i < 10; i++)
            {
                if (this.representantes[i] != default)
                {
                    staff += "," + this.representantes[i].ToString();
                }
                else
                {
                    staff += ",0";
                }

            }

            try
            {
                cmd.CommandText = "update Instituition set addressId = @addressId, name = @name, CNPJ = @cnpj, email = @email, password = @password, discription = @description, avatar = @avatarUrl, addressNumber = @addressNumber, addressReference = @addressReference, staff = @staff where id = @id;";
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
                cmd.Parameters.AddWithValue("@staff", staff);
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
                cmd.CommandText = "delete from Instituition where Id = @id;";
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
