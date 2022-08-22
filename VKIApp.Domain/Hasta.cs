using System.Text;

namespace VKIApp.Domain
{
    public class Hasta
    {
     
        public Hasta()
        {
            sayac++;
            hastaNo = sayac;
        }
        private static ushort sayac;
        public ushort hastaNo;
        public string hastaAdi;
        public string hastaSoyadi;
        public double kilo;
        public double boy;

        public double VKIHesabi()
        {
            return (kilo/((boy/100)*(boy / 100)));
        }
        public string VKIDurum(double vki)
        {
            if (vki < 25 && vki >= 18.5)
                return "İDEAL";
            else if (vki < 30 && vki >= 25)
                return "HAFİF KİLOLU";
            else if (vki > 30)
                return "OBEZ";
            else if (vki < 18.5)
                return "ZAYIF";
            else
                return "";
        }
        public string HastaInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Hasta No: {hastaNo}\n");
            sb.Append($"Hasta Adı: {hastaAdi}\n");
            sb.Append($"Hasta Soyadı: {hastaSoyadi}\n");
            sb.Append($"Hastanın Kilosu: {kilo}\n");
            sb.Append($"Hastanın Boyu: {boy}\n");
            sb.Append($"Hastanın Vücut Kitle İndeksi: {Math.Round(VKIHesabi(),2)}\t{VKIDurum(VKIHesabi())}\n");

            return sb.ToString();

        }

    }
}