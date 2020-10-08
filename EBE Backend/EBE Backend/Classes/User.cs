using EBE_Backend.Classes;
using System;
using System.Dynamic;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text.Json.Serialization;

namespace EBE_Backend
{
    class User
    {
        private int id, institutionId;
        private int addressNumber, addressId;
        private Address address = new Address();
        private Institution institution = new Institution();

        private bool sex;
        private string
            name,
            birthDate,
            cpf,
            rg,
            role,
            nivelDeAcesso,
            email,
            password,
            description,
            medicalCares,
            avatarUrl,
            addressReference;
        public User ()
        {
        }
        public User(
            int id,
            string name,
            bool sex,
            string birthDate,
            string cpf,
            string rg,
            int institutionId,
            string role,
            string nivelDeAcesso,
            string email,
            string password,
            string description,
            string medicalCares,
            string avatarUrl,
            string addressReference,
            int addressNumber,
            int addressId)
        {
            this.id = id;
            this.name = name;
            this.sex = sex;
            this.birthDate = birthDate;
            this.cpf = cpf;
            this.rg = rg;
            this.institutionId = institutionId;
            this.role = role;
            this.nivelDeAcesso = nivelDeAcesso;
            this.email = email;
            this.password = password;
            this.description = description;
            this.medicalCares = medicalCares;
            this.avatarUrl = avatarUrl;
            this.addressReference = addressReference;
            this.addressNumber = addressNumber;
            this.addressId = addressId;

            address.GetById(addressId);
            institution.GetById(institutionId);
        }


        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public bool Sex { get => sex; set => sex = value; }
        public string BirthDate { get => birthDate; set => birthDate = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Rg { get => rg; set => rg = value; }
        public int InstitutionId { get => institutionId; set => institutionId = value; }
        public string Role { get => role; set => role = value; }
        public string NivelDeAcesso { get => nivelDeAcesso; set => nivelDeAcesso = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Description { get => description; set => description = value; }
        public string MedicalCares { get => medicalCares; set => medicalCares = value; }
        public string AvatarUrl { get => avatarUrl; set => avatarUrl = value; }
        public string AddressReference { get => addressReference; set => addressReference = value; }
        public int AddressNumber { get => addressNumber; set => addressNumber = value; }
        public int AddresId { get => addressId; set => addressId = value; }

        ~User()
        {
            Console.WriteLine("User destructor was called. Open fire!");
        }
        
        public User Create()
        {

            using var connection = new MySqlConnection(@"server=localhost;userid=Jacik;password=1234;database=ebedata;UseAffectedRows=True");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection
            };

            try
            {
                Console.WriteLine("Email create " + this.email);
                cmd.CommandText = "INSERT INTO User (addressId, name, birthDate, sex, cpf, rg, institution, role, acesslevel, email, password, description, medicalCares, avatar, addressNumber, addressReference ) VALUES (@addressId, @name, @birthDate, @sex, @cpf, @rg, @institution, @role, @nivelDeAcesso, @email, @password, @description, @medicalCares, @avatar, @addressNumber, @addressReference);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@addressId", this.addressId);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@birthDate", this.birthDate);
                cmd.Parameters.AddWithValue("@sex", this.sex);
                cmd.Parameters.AddWithValue("@cpf", this.cpf);
                cmd.Parameters.AddWithValue("@rg", this.rg);
                cmd.Parameters.AddWithValue("@institution", this.institutionId);
                cmd.Parameters.AddWithValue("@role", this.role);
                cmd.Parameters.AddWithValue("@nivelDeAcesso", this.nivelDeAcesso);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@password", this.password);
                cmd.Parameters.AddWithValue("@description", this.description);
                cmd.Parameters.AddWithValue("@medicalCares", this.medicalCares);
                cmd.Parameters.AddWithValue("@avatar", this.avatarUrl);
                cmd.Parameters.AddWithValue("@addressNumber", this.addressNumber);
                cmd.Parameters.AddWithValue("@addressReference", this.addressReference);

                int affectedRows = cmd.ExecuteNonQuery();
                if(affectedRows != 0)
                {
                    this.id = (int)cmd.LastInsertedId;
                }
                Console.WriteLine("User cadastrado! Id: " + this.id);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                
            }
            finally
            {
            connection.Close();
            }
            return this;

        }

        public ArrayList ReadTable()
        {
            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            using var connection = new MySqlConnection(url);

            connection.Open();

            string statement = "select * from User";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            ArrayList UserList = new ArrayList();

            try
            {
                while (reader.Read())
                {
                    User UserToList = new User ();
                    UserToList.id = reader.GetInt32(0);
                    UserToList.addressId = reader.GetInt32(1);
                    UserToList.name = reader.GetString(2);
                    UserToList.birthDate = reader.GetString(3);
                    UserToList.sex = reader.GetBoolean(4);
                    UserToList.cpf = reader.GetString(5);
                    UserToList.rg = reader.GetString(6);
                    UserToList.institutionId = reader.GetInt32(7);
                    UserToList.role = reader.GetString(8);
                    UserToList.nivelDeAcesso = reader.GetString(9);
                    UserToList.email = reader.GetString(10);
                    UserToList.password = reader.GetString(11);
                    UserToList.description = reader.GetString(12);
                    UserToList.medicalCares = reader.GetString(13);
                    UserToList.avatarUrl = reader.GetString(14);
                    UserToList.addressNumber = reader.GetInt32(15);
                    UserToList.addressReference = reader.GetString(16);

                    UserList.Add(UserToList);
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
            return UserList;
        }

        public User GetById(int id)
        {
            bool idFound = false;

            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            using var connection = new MySqlConnection(url);

            connection.Open();

            string statement = "select * from user";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) == id) { 
          
                        this.id = reader.GetInt32(0);
                        this.addressId = reader.GetInt32(1);
                        this.name = reader.GetString(2);
                        this.birthDate = reader.GetString(3);
                        this.sex = reader.GetBoolean(4);
                        this.cpf = reader.GetString(5);
                        this.rg = reader.GetString(6);
                        this.institutionId = reader.GetInt32(7);
                        this.role = reader.GetString(8);
                        this.nivelDeAcesso = reader.GetString(9);
                        this.email = reader.GetString(10);
                        this.password = reader.GetString(11);
                        this.description = reader.GetString(12);
                        this.medicalCares = reader.GetString(13);
                        this.avatarUrl = reader.GetString(14);
                        this.addressNumber = reader.GetInt32(15);
                        this.addressReference = reader.GetString(16);

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
            }else
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
                cmd.CommandText = "update User set addressId = @addressId, name = @name, birthDate= @birthDate, sex = @sex, cpf = @cpf, rg = @rg, institution = @institution, role = @role, acesslevel = @nivelDeAcesso, email = @email, password = @password, description = @description, medicalCares = @medicalCares, avatar = @avatar, addressNumber = @addressNumber, addressReference = @addressReference where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@addressId", this.addressId);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@birthDate", this.birthDate);
                cmd.Parameters.AddWithValue("@sex", this.sex);
                cmd.Parameters.AddWithValue("@cpf", this.cpf);
                cmd.Parameters.AddWithValue("@rg", this.rg);
                cmd.Parameters.AddWithValue("@institution", this.institutionId);
                cmd.Parameters.AddWithValue("@role", this.role);
                cmd.Parameters.AddWithValue("@nivelDeAcesso", this.nivelDeAcesso);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@password", this.password);
                cmd.Parameters.AddWithValue("@description", this.description);
                cmd.Parameters.AddWithValue("@medicalCares", this.medicalCares);
                cmd.Parameters.AddWithValue("@avatar", this.avatarUrl);
                cmd.Parameters.AddWithValue("@addressNumber", this.addressNumber);
                cmd.Parameters.AddWithValue("@addressReference", this.addressReference);
                cmd.Parameters.AddWithValue("@id",this.id);
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
                cmd.CommandText = "delete from User where Id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("id " + this.id + " deletado");
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