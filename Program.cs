using System;
using System.Collections.Generic;

class Program
{
    static List<Urun> depo = new List<Urun>();
    static decimal kullaniciParasi = 1000m; // Kullanıcının başlangıç parası

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Depo Yönetim Sistemi ===");
            Console.WriteLine("1. Ürün Ekle");
            Console.WriteLine("2. Ürün Satış");
            Console.WriteLine("3. Depoyu Görüntüle");
            Console.WriteLine("4. Çıkış");
            Console.Write("Seçiminiz: ");

            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":
                    UrunEkle();
                    break;
                case "2":
                    UrunSatis();
                    break;
                case "3":
                    DepoyuGoruntule();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim! Devam etmek için bir tuşa basın.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void UrunEkle()
    {
        Console.Clear();
        Console.WriteLine("=== Ürün Ekleme ===");
        DepoyuGoruntule(false);

        Console.WriteLine("1. Yeni Ürün Ekle");
        Console.WriteLine("2. Var Olan Ürünü Güncelle");
        Console.Write("Seçiminiz: ");
        string secim = Console.ReadLine();

        if (secim == "1")
        {
            Console.Write("Ürün adı: ");
            string ad = Console.ReadLine();
            Console.Write("Birim fiyatı: ");
            decimal birimFiyat = decimal.Parse(Console.ReadLine());
            Console.Write("Adet: ");
            int adet = int.Parse(Console.ReadLine());

            depo.Add(new Urun { Ad = ad, BirimFiyat = birimFiyat, Adet = adet });
            Console.WriteLine($"{ad} ürünü depoya eklendi.");
        }
        else if (secim == "2")
        {
            Console.Write("Güncellemek istediğiniz ürünün adını girin: ");
            string ad = Console.ReadLine();
            var urun = depo.Find(u => u.Ad.Equals(ad, StringComparison.OrdinalIgnoreCase));

            if (urun != null)
            {
                Console.Write("Eklenecek adet: ");
                int eklenenAdet = int.Parse(Console.ReadLine());
                urun.Adet += eklenenAdet;
                Console.WriteLine($"{ad} ürününe {eklenenAdet} adet eklendi.");
            }
            else
            {
                Console.WriteLine("Bu ürün depoda bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz seçim.");
        }
        Console.ReadKey();
    }

    static void UrunSatis()
    {
        Console.Clear();
        Console.WriteLine("=== Ürün Satış ===");
        DepoyuGoruntule(false);

        Console.Write("Satın almak istediğiniz ürünün adını girin: ");
        string ad = Console.ReadLine();
        var urun = depo.Find(u => u.Ad.Equals(ad, StringComparison.OrdinalIgnoreCase));

        if (urun != null)
        {
            Console.Write("Kaç adet almak istiyorsunuz: ");
            int miktar = int.Parse(Console.ReadLine());

            if (miktar > urun.Adet)
            {
                Console.WriteLine("Depoda yeterli miktarda ürün yok!");
            }
            else
            {
                decimal toplamTutar = miktar * urun.BirimFiyat;
                if (toplamTutar > kullaniciParasi)
                {
                    Console.WriteLine($"Paranız yetmiyor! Gerekli miktar: {toplamTutar:C}, Elinizdeki para: {kullaniciParasi:C}");
                }
                else
                {
                    urun.Adet -= miktar;
                    kullaniciParasi -= toplamTutar;
                    Console.WriteLine($"{miktar} adet {urun.Ad} satın alındı. Kalan paranız: {kullaniciParasi:C}");
                }
            }
        }
        else
        {
            Console.WriteLine("Bu ürün depoda bulunamadı.");
        }
        Console.ReadKey();
    }

    static void DepoyuGoruntule(bool bekle = true)
    {
        Console.WriteLine("=== Depo Listesi ===");
        if (depo.Count == 0)
        {
            Console.WriteLine("Depo boş.");
        }
        else
        {
            foreach (var urun in depo)
            {
                Console.WriteLine($"Ürün: {urun.Ad}, Birim Fiyat: {urun.BirimFiyat:C}, Adet: {urun.Adet}");
            }
        }
        if (bekle)
        {
            Console.ReadKey();
        }
    }
}

class Urun
{
    public string Ad { get; set; }
    public decimal BirimFiyat { get; set; }
    public int Adet { get; set; }
}
