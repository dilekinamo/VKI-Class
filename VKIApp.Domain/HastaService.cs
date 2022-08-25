using System.Diagnostics;
using System.Text.Json;
using VKIApp.Data.IO;

namespace VKIApp.Domain
{
    public class HastaService
    {
        public static List<Hasta> liste = new List<Hasta>();

        public static void HastayiSil(int index)
        {
            LoadListFromFile();
            liste.RemoveAt(index - 1);
            string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { IncludeFields = true });
            FileOperation.Write(json);
        }
        public static Hasta HastaKaydet(string doktorAdi,string adi,string soyadi,double kilo, double boy)
        {
            Hasta hasta = new Hasta();
            hasta.doktor = doktorAdi;
            hasta.hastaNo = (ushort)(GetLastHastaNo()+1);
            hasta.hastaAdi = adi;
            hasta.hastaSoyadi = soyadi;
            hasta.kilo = kilo;
            hasta.boy = boy;

            liste.Add(hasta);

            string json=JsonSerializer.Serialize(liste,new JsonSerializerOptions { IncludeFields=true});

            FileOperation.Write(json);

            return hasta;
        }
        public static IReadOnlyCollection<Hasta> GetAllHasta()
        {
            LoadListFromFile();
            return liste.AsReadOnly();
        }
        public static IReadOnlyCollection<Hasta> FilterByChannelName(string soyadi)
        {
            LoadListFromFile();
            List<Hasta> filteredHasta = new List<Hasta>();
            foreach (Hasta h in liste)
            {
                if(h.hastaSoyadi.ToLower().Contains(soyadi.ToLower()))
                    filteredHasta.Add(h);
            }
            return filteredHasta.AsReadOnly();
        }

        public static IReadOnlyCollection<Hasta> FilterByDoctorName(string doktor)
        {
            LoadListFromFile();
            List<Hasta> filteredHasta = new List<Hasta>();
            foreach (Hasta h in liste)
            {
                if (h.doktor.ToLower().Contains(doktor.ToLower()))
                    filteredHasta.Add(h);
            }
            return filteredHasta.AsReadOnly();
        }

        public static void LoadListFromFile()
        {
            string json = FileOperation.Read();
            liste = JsonSerializer.Deserialize<List<Hasta>>(json,
                new JsonSerializerOptions { IncludeFields = true });
        }
        public static int GetLastHastaNo()
        {
            Hasta lastHasta = liste.Last<Hasta>();
            return lastHasta.hastaNo;
        }
    }
}
