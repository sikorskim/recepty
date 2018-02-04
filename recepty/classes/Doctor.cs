using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class Doctor
    {
        public int DoctorId { get; private set; }
        public string Lastname { get; private set; }
        public string Name { get; private set; }
        public string PESEL { get; private set; }
        public string RightToPracticeNumber { get; private set; }
        public string Title { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public virtual string FullName { get { return getFullName() ; } }

        public Doctor()
        { }

        public Doctor(string lastname, string name, string pesel, string rightToPractice, string title, string login, string password)
        {
            Lastname = lastname;
            Name = name;
            PESEL = pesel;
            RightToPracticeNumber = rightToPractice;
            Title = title;
            Login = login;
            Password = computeHash(password);
        }

        public bool insertToDb()
        {
            try
            {
                var db = new Model1();
                db.Doctor.Add(this);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        
        public bool login(string login, string haslo)
        {
            Model1 db = new Model1();

            try
            {
                Doctor lekarz = db.Doctor.FirstOrDefault(p => p.Login == login);

                if (lekarz.Password == computeHash(haslo))
                {
                    return true;
                }
                else
                {
                    // wrong password
                    return false;
                }
            }
            catch (Exception)
            {
                // wrong username
                return false;
            }
        }

        public static string computeHash(string input)

        {
            HashAlgorithm algo = HashAlgorithm.Create("SHA512");
            byte[] hashBytes = algo.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();

            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        string getFullName()
        {
            return Title + " " + Name + " " + Lastname;
        }

    }
}
