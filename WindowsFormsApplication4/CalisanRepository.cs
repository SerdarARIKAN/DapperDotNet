using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace WindowsFormsApplication4
{
    class CalisanRepository : BaseRepository<Calisan>
    {

        public override Calisan Getir(int nesneId)
        {
            using (var baglanti = BaglantiyiAc())
            {
                var calisan = baglanti.Query<Calisan>("select " +
                                                    "tCalisan.Id," +
                                                    "tCalisan.Ad," +
                                                    "tCalisan.Soyad," +
                                                    "tCalisan.Telefon," +
                                                    "tCalisan.Adres," +
                                                    "tCalisan.SehirKodu," +
                                                    "tSehir.Adi as SehirAdi" +
                                                " from " +
                                                    "tCalisan left outer join " +
                                                    "tSehir on tCalisan.SehirKodu=tSehir.SehirKodu" +
                                                " where " +
                                                    "Id=" + nesneId.ToString())
                                .SingleOrDefault();

                baglanti.Close();

                return calisan;
            }
        }

        public override int Kaydet(Calisan nesne)
        {
            using (var baglanti = BaglantiyiAc())
            {

                if (nesne.Id == 0)
                {
                    const string sorgu = "INSERT INTO tCalisan(Ad, Soyad, Telefon, Adres, SehirKodu) " +
                             "VALUES (@Ad, @Soyad, @Telefon, @Adres, @SehirKodu)";

                    var parametreler = new
                    {
                        Ad = nesne.Ad,
                        Soyad = nesne.Soyad,
                        Telefon = nesne.Telefon,
                        Adres = nesne.Adres,
                        SehirKodu = nesne.SehirKodu
                    };

                    baglanti.Execute(sorgu, parametreler);

                    var yeniId = YeniId(baglanti);

                    return yeniId;
                }
                else
                {
                    const string sorgu = "UPDATE tCalisan " +
                                "SET Ad=@Ad, Soyad=@Soyad, Telefon=@Telefon, Adres=@Adres, SehirKodu=@SehirKodu " +
                                "WHERE Id=@Id";

                    var parametreler = new
                    {
                        Ad = nesne.Ad,
                        Soyad = nesne.Soyad,
                        Telefon = nesne.Telefon,
                        Adres = nesne.Adres,
                        SehirKodu = nesne.SehirKodu,
                        Id = nesne.Id
                    };

                    baglanti.Execute(sorgu, parametreler);

                    return nesne.Id;
                }
            }
        }

        public override void Sil(int nesneId)
        {
            using (var baglanti = BaglantiyiAc())
            {
                const string sorgu = "DELETE FROM tCalisan " +
                            "WHERE Id=@Id";

                var parametreler = new
                {
                    Id = nesneId
                };

                baglanti.Execute(sorgu, parametreler);
            }
        }

        public override List<Calisan> TumKayitlariGetir()
        {
            using (var baglanti = BaglantiyiAc())
            {
                var liste = baglanti.Query<Calisan>("select " +
                                                    "tCalisan.Id," +
                                                    "tCalisan.Ad," +
                                                    "tCalisan.Soyad," +
                                                    "tCalisan.Telefon," +
                                                    "tCalisan.Adres," +
                                                    "tCalisan.SehirKodu," +
                                                    "tSehir.Adi as SehirAdi" +
                                                " from " +
                                                    "tCalisan left outer join " +
                                                    "tSehir on tCalisan.SehirKodu=tSehir.SehirKodu")
                                .ToList();

                baglanti.Close();

                return liste;
            }
        }

    }
}
