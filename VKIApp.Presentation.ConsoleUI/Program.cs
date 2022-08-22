using VKIApp.Domain;

namespace VKIApp.Presentation.ConsoleUI
{
    internal class Program
    {
        //public static ushort sayac = 0;
        public static void Main()
        {
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Yeni Hasta Kaydı\n2. Hasta Listesi\n3. Hasta Filtrele (Soyad)\n4. Hasta Filtrele (Doktor)\n5. Hasta Sil\n6. Cikis");
            MenuSelection();
        }

        private static void MenuSelection()
        {
            Console.Write("Seçiminiz : ");
            string choose = Console.ReadLine();
            switch (choose)
            {
                case "1":
                    YeniHasta();
                    break;
                case "2":
                    HastaListele();
                    break;
                case "3":
                    HastaFiltreleSoyad();
                    break;
                case "4":
                    HastaFiltreleDoktor();
                    break;
                case "5":
                    HastaSil();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    MenuSelection();
                    break;
            }
        }

        private static void HastaSil()
        {
            Console.WriteLine("Kacinci siradaki Hasta silinecek?: ");
            int silIndex= Convert.ToInt32(Console.ReadLine());
            HastaService.HastayiSil(silIndex);
            Again();
        }

        static void Again()
        {
            Console.WriteLine("Menüye dönmek için Enter");
            Console.ReadLine();

            Menu();
        }
        private static void HastaFiltreleSoyad()
        {
            Console.WriteLine("Hasta Soyadının içinde geçen kelimeyi yazın: ");
            string filterKeyword = Console.ReadLine();
            var data = HastaService
                .FilterByChannelName(filterKeyword);
            PrintList(data);
        }
        private static void HastaFiltreleDoktor()
        {

            string doktorAdi;
            Console.WriteLine("Hangi doktorun hastalari görüntülensin? Numarasini girin: ");
            foreach (string doktor in Doktorlar.doktorlar)
            {
                Console.WriteLine(doktor);
            }
            string doktorSecim = Console.ReadLine();
            switch (doktorSecim)
            {
                case "1":
                    doktorAdi = Doktorlar.doktorlar[0];
                    break;
                case "2":
                    doktorAdi = Doktorlar.doktorlar[1];
                    break;
                case "3":
                    doktorAdi = Doktorlar.doktorlar[2];
                    break;
                case "4":
                    doktorAdi = Doktorlar.doktorlar[3];
                    break;
                default:
                    doktorAdi = null;
                    break;
            }
            
            
            
            var data = HastaService
                .FilterByDoctorName(doktorAdi);
            PrintList(data);


        }
        private static void HastaListele()
        {
            var list = HastaService.GetAllHasta();
            PrintList(list);
        }
        static void PrintList(IReadOnlyCollection<Hasta> list)
        {
            Console.WriteLine("----------- Liste Başlangıcı ----------");
            foreach (Hasta h in list)
            {
                Console.WriteLine(h.HastaInfo());
                Console.WriteLine("--------------------------------------");
            }
            Console.WriteLine("----------- Liste Sonu ----------");
            Again();
        }
        public static void YeniHasta()
        {
            //Console.WriteLine("Hasta No: " + sayac++);
            Console.WriteLine("\nHangi Doktor? Numarasini gir: ");
            foreach(string doktor in Doktorlar.doktorlar  )
            {
                Console.WriteLine(doktor);
            }
            string doktorSecim = Console.ReadLine();
            string doktorAdi;
            switch (doktorSecim)
            {
                case "1":
                    doktorAdi = Doktorlar.doktorlar[0];
                    break;
                case "2":
                    doktorAdi = Doktorlar.doktorlar[1];
                    break;
                case "3":
                    doktorAdi = Doktorlar.doktorlar[2];
                    break;
                case "4":
                    doktorAdi = Doktorlar.doktorlar[3];
                    break;
                default:
                    doktorAdi = null;
                    break;
            }
            Console.WriteLine("Hasta Adı: ");
            string hastaAdi = Console.ReadLine();
            Console.WriteLine("Hasta Soyadı: ");
            string hastaSoyadi = Console.ReadLine();
            Console.WriteLine("Hastanın Kilosu (kg): ");
            double kilo = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Hastanın Boyu (cm): ");
            double boy = Convert.ToDouble(Console.ReadLine());


            var data = HastaService.HastaKaydet(doktorAdi,hastaAdi,hastaSoyadi,kilo,boy);

            Console.WriteLine($"Hesaplanan VKI Değeri: {Math.Round(data.VKIHesabi(),2)}\t{data.VKIDurum(data.VKIHesabi())}");
            Again();
        }
    }
}