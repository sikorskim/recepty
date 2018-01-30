using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace recepty
{
    public class Patient
    {
        [DisplayName("Id")]
        public int PatientId { get; private set; }
        [DisplayName("Nazwisko")]
        public string Lastname { get; private set; }
        [DisplayName("Imię")]
        public string Name { get; private set; }
        public string PESEL { get; private set; }
        [DisplayName("Data urodzenia")]
        public DateTime DateOfBirth { get { return getBirthDateFromPESEL(PESEL); } }
        public int AddressId { get; private set; }
        [ForeignKey("AddressId")]
        public Address Address { get; private set; }
        [DisplayName("Oddział NFZ")]
        public string Kod { get; private set; }
        [ForeignKey("Kod")]
        public virtual OddzialNFZ NFZDepartament { get; private set; }
        public string Uprawnienie { get; private set; }
        public virtual string FullName { get { return getFullName(); } }
        public virtual string FullAddress { get { return getFullAddress(); } }

        public Patient()
        { }

        public Patient(string lastname, string name, string pesel, Address address,
            OddzialNFZ nfzDepartament, Uprawnienie uprawnienie)
        {
            Lastname = lastname;
            Name = name;
            PESEL = pesel;
            Address = address;
            Kod=nfzDepartament.Kod;
            Uprawnienie = uprawnienie.Kod;
        }

        public void insertToDb()
        {
            Model1 model = new Model1();
            model.Patient.Add(this);
            model.SaveChanges();
        }

        public static Patient get(int pacjentId)
        {
            Model1 model = new Model1();
            Patient patient = model.Patient.Single(p => p.PatientId == pacjentId);
            patient.Address = model.Address.Single(p => p.AddressId == patient.AddressId);            
            return patient;
        }

        public static List<Patient> getAll()
        {
            Model1 model = new Model1();
            List<Patient> lista = model.Patient.ToList();
            return lista;
        }

        public static bool remove(int pacjentId)
        {
            try
            {
                Model1 model = new Model1();
                Patient pacjent = model.Patient.Single(p => p.PatientId == pacjentId);
                model.Patient.Remove(pacjent);
                model.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool edit(Patient patient)
        {
            try
            {
                Model1 model = new Model1();
                Name = patient.Name;
                Lastname = patient.Lastname;
                PESEL = patient.PESEL;
                model.Entry(patient).State = EntityState.Modified;
                model.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static bool checkPESEL(string pesel)
        {
            try
            {
                int[] nums = new int[11];
                int i = 0;
                foreach (char c in pesel)
                {
                    nums[i] = Int32.Parse(c.ToString());
                    i++;
                }

                int checksum = 9 * nums[0] + 7 * nums[1] + 3 * nums[2] + nums[3] + 9 * nums[4] + 7 * nums[5] + 3 * nums[6] + nums[7] + 9 * nums[8] + 7 * nums[9];

                if (checksum == 0)
                {
                    return false;
                }

                if (nums[10] == checksum % 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        DateTime getBirthDateFromPESEL(string pesel)
        {
            return DateTime.ParseExact(pesel.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);
        }

        public static List<Patient> search(string query, int queryType)
        {
            Model1 model = new Model1();
            List<Patient> lista = new List<Patient>();
            if (queryType == 0)
            {
                lista = model.Patient.Where(p => p.Lastname.Contains(query)).ToList();
            }
            else if (queryType == 1)
            {
                lista = model.Patient.Where(p => p.PESEL.Contains(query)).ToList();
            }
            return lista;
        }

        string getFullName()
        {
            return Name + " " + Lastname;
        }

        string getFullAddress()
        {
            return Address.FullAddress;
        }
    }
}