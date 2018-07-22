using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace WindowsFormsApplication4
{
    class SehirRepository : BaseRepository<Sehir>
    {

        public override Sehir Getir(int nesneId)
        {
            using (var baglanti = BaglantiyiAc())
            {
                var sehir = baglanti.Query<Sehir>("select " + 
                                                    "tSehir.SehirKodu," + 
                                                    "tSehir.Adi as SehirAdi" +
                                                " from " +
                                                    "tSehir" +
                                                " where " +
                                                    "SehirKodu=" + nesneId.ToString())
                                .SingleOrDefault();

                baglanti.Close();

                return sehir;
            }
        }

        public override int Kaydet(Sehir nesne)
        {
            throw new NotImplementedException();
        }

        public override void Sil(int nesneId)
        {
            throw new NotImplementedException();            
        }

        public override List<Sehir> TumKayitlariGetir()
        {
            using (var baglanti = BaglantiyiAc())
            {
                var liste = baglanti.Query<Sehir>("select " +
                                                    "tSehir.SehirKodu," +
                                                    "tSehir.Adi as SehirAdi" +
                                                " from " +
                                                    "tSehir")
                                .ToList();

                baglanti.Close();

                return liste;
            }
        }


    }
}
