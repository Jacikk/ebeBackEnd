﻿using System;
using MySql.Data.MySqlClient;
using System.Collections;

namespace EBE_Backend
{
    class Address
    {
        private int id;

        private string
            cep,
            city,
            street,
            country,
            state,
            district;
        public Address() 
        { 

        }
        public Address(
            int id,
            string cep,
            string city,
            string country,
            string street,
            string state,
            string district)
        {
            this.id = id;
            this.cep = cep;
            this.city = city;
            this.country = country;
            this.street = street;
            this.state = state;
            this.district = district;
        }

        public string Cep { get => cep; set => cep = value; }
        public string City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }
        public string State { get => state; set => state = value; }
        public string District { get => district; set => district = value; }
        public string Country { get => country; set => country = value; }
        public int Id { get => id; set => id = value; }


        ~Address()
        {
            Console.WriteLine("Address destructor was called. Open fire!");
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
                cmd.CommandText = "INSERT INTO Address (CEP, city, country, street, state, district) VALUES ( @cep, @city, @country, @street, @state, @district);";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@cep", this.cep);
                cmd.Parameters.AddWithValue("@city", this.city);
                cmd.Parameters.AddWithValue("@country", this.country);
                cmd.Parameters.AddWithValue("@street", this.street);
                cmd.Parameters.AddWithValue("@state", this.state);
                cmd.Parameters.AddWithValue("@district", this.district);

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows != 0)
                {
                    this.id = (int)cmd.LastInsertedId;
                }
                Console.WriteLine("Address cadastrado! Id: " + this.id);
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

            string statement = "select * from Address";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            ArrayList AddressList = new ArrayList();

            try
            {
                while (reader.Read())
                {
                    Address AdsToList = new Address();
                    AdsToList.id = reader.GetInt32(0);
                    AdsToList.cep = reader.GetString(1);
                    AdsToList.city = reader.GetString(2);
                    AdsToList.country = reader.GetString(3);
                    AdsToList.street = reader.GetString(4);
                    AdsToList.state = reader.GetString(5);
                    AdsToList.district = reader.GetString(6);

                    AddressList.Add(AdsToList);
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

            return AddressList;
        }

        public Address GetById(int id)
        {
            string url = @"server=localhost;userid=Jacik;password=1234;database=ebedata";

            using var connection = new MySqlConnection(url);

            var idFound = false;

            connection.Open();

            string statement = "select * from Address";

            using var cmd = new MySqlCommand(statement, connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if(reader.GetInt32(0) == id)
                    {
                        this.id = reader.GetInt32(0);
                        this.cep = reader.GetString(1);
                        this.city = reader.GetString(2); 
                        this.country = reader.GetString(3);
                        this.street = reader.GetString(4); 
                        this.state = reader.GetString(5); 
                        this.district = reader.GetString(6);

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
            if(idFound)
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
                cmd.CommandText = "update Address set cep= @cep, city = @city, country = @country, street = @street, state = @state, district = @district where id = @id;";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@cep", this.cep);
                cmd.Parameters.AddWithValue("@city", this.city);
                cmd.Parameters.AddWithValue("@country", this.country);
                cmd.Parameters.AddWithValue("@street", this.street);
                cmd.Parameters.AddWithValue("@state", this.state);
                cmd.Parameters.AddWithValue("@district", this.district);
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
                cmd.CommandText = "delete from Address where Id = @id;";
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
