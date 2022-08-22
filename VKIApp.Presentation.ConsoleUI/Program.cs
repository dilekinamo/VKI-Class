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
            Console.WriteLine("1. Yeni Hasta Kaydı\n2. Hasta Listesi\n3. Hasta Filtrele\n4. Hasta Sil\n5. Cikis");
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
                    HastaFiltrele();
                    break;
                case "4":
                    HastaSil();
                    break;
                case "5":
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
        private static void HastaFiltrele()
        {
            Console.WriteLine("Hasta Soyadının içinde geçen kelimeyi yazın : ");
            string filterKeyword = Console.ReadLine();
            var data = HastaService
                .FilterByChannelName(filterKeyword);
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
            Console.WriteLine("Hasta Adı: ");
            string hastaAdi = Console.ReadLine();
            Console.WriteLine("Hasta Soyadı: ");
            string hastaSoyadi = Console.ReadLine();
            Console.WriteLine("Hastanın Kilosu (kg): ");
            double kilo = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Hastanın Boyu (cm): ");
            double boy = Convert.ToDouble(Console.ReadLine());


            var data = HastaService.HastaKaydet(hastaAdi,hastaSoyadi,kilo,boy);

            Console.WriteLine($"Hesaplanan VKI Değeri: {Math.Round(data.VKIHesabi(),2)}\t{data.VKIDurum(data.VKIHesabi())}");
            Again();
        }
    }
}