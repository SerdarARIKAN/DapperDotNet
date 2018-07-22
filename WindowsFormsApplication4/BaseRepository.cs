using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.OleDb;

namespace WindowsFormsApplication4
{
    abstract class BaseRepository<T>
    {

        private static string baglantiSatiri = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\kursatcayirli\Desktop\calisanlar.mdb";

        protected static int YeniId(IDbConnection connection)
        {
            int yeniId = connection.Query<int>("SELECT @@IDENTITY AS Id").Single();
            return yeniId;
        }

        protected static IDbConnection BaglantiyiAc()
        {
            IDbConnection baglanti = new OleDbConnection(baglantiSatiri);
            baglanti.Open();
            return baglanti;
        }

        public abstract T Getir(int nesneId);

        public abstract int Kaydet(T nesne);

        public abstract void Sil(int nesneId);

        public abstract List<T> TumKayitlariGetir();


    }
}
