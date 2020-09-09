using EBE_Backend.Classes;
using System;
using System.Dynamic;
using MySql.Data.MySqlClient;

namespace EBE_Backend
{
    class User
    {
        private int id, institutionId;

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
            reference;

        private int number;

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
            string reference,
            int number)
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
            this.reference = reference;
            this.number = number;
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
        public string Reference { get => reference; set => reference = value; }
        public int Number { get => number; set => number = value; }

        ~User()
        {
            Console.WriteLine("User destructor was called. Open fire!");
        }
    }
}
