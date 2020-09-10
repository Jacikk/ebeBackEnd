﻿using EBE_Backend.Classes;
using System;
using System.Dynamic;
using MySql.Data.MySqlClient;

namespace EBE_Backend
{
    class User
    {
        private int id, institutionId;
        private int addressNumber, addressId;
        private Address address;
        private Instituition instituition;

        private string
            name,
            sex,
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

        public User(
            int id,
            string name,
            string sex,
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

            address.GetById(this.addressId);
            instituition.GetById(this.instituitionId);
        }

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Sex { get => sex; set => sex = value; }
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
           
                cmd.CommandText = "INSERT INTO User (addressId, name, birthDate, sex, cpf, rg, institution, role, acessLevel, email, password, description, medicalCares, avatar, addressNumber, addressReference ) VALUES ( '" + this.addressId + "', '" + this.name + "', '" + this.birthDate + "', '" + this.sex + "', '" + this.cpf + "', '" + this.rg + "', '" + this.institutionId + "', '" + this.role + "', '" + this.acessLevel + "', '" + this.email + "', '" + this.password + "', '" + this.description + "', '" + this.medicalCares + "' , '" + this.avatar + "', '" + this.addressNumber + "', '" + this.addressReference + "');";
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

            string statement = "select * from User";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id {0}, name {1}, birthDate {2}, sex {3}, cpf {4}, rg {5}, role {6}, acessLevel {7} , email {8}, password {9}, description {10}, medicalCares {11}, avatar {12}, addressNumber {13}, addressReference {14}",
                        reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14), reader.GetString(15), reader.GetString(16));
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

        public Instituition GetById(int id)
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
                    if (reader.GetInt32(0) == id)
          
                    this.id = reader.GetInt32(0);
                    this.addressId = reader.GetInt32(1);
                    this.name = reader.GetString(2);
                    this.cnpj = reader.GetString(3);
                    this.email = reader.GetString(4);
                    this.password = reader.GetString(5);
                    this.discription = reader.GetString(6);
                    this.representantes[] = reader.GetInt32(7);
                    this.avatarUrl = reader.GetString(8);
                    this.addressNumber = reader.GetInt32(9);
                    this.addressReference = reader.GetString(10);
                    }
                    else
                    {
                        return null;
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

            return this;
        }
        public static void Update(
            int addresId,
            string name,
            string birthDate,
            string sex,
            string cpf,
            string rg,
            string institution,
            string role,
            int acessLevel,
            string email,
            string password,
            string description,
            string medicalCares,
            string avatar,
            int addressNumber,
            string addressReference,
            )
        {
            using var connection = new MySqlConnection(@"server=localhost;userid=Jacik;password=1234;database=ebedata");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection
            };

          

            try
            {
                cmd.CommandText = "update Instituition set addressId ='" + addressId + "', name= '" + name + "', birthDate= '" + birthDate + "', sex= '" + sex + "', cpf= '" + cpf + "', institution= '" + institution + "', role= '" + role + "', acessLevel= '" + acessLevel + "', email= '" + email + "', password= '" + password + "', description= '" + description + "', medicalCares= '" + medicalCares + "', avatar= '" + avatarUrl + "', addressNumber= '" + addressNumber + "', addressReference= '" + addressReference + "' where id =" + id + ";";
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
        public static void Delete(int id)
        {
            using var connection = new MySqlConnection(@"server=localhost;userid=Jacik;password=1234;database=ebedata");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection
            };

            try
            {
                cmd.CommandText = "delete from User where Id = " + id + ";";
                cmd.ExecuteNonQuery();
                Console.WriteLine("id" + id + "deletado");
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