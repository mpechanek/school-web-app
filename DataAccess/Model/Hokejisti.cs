using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Hokejisti
    {
        private static List<Hokejista> hokejisti = null;
        private static int counter = 4;
        public static int Counter
        {
            get { return ++counter; }
        }

        public static List<Hokejista> GetFakeList
        {
            get
            {
                if(hokejisti == null)
                {
                    hokejisti = new List<Hokejista>();

                    hokejisti.Add(new Hokejista() { Jmeno = "Jaromír Jágr", Tym = "Rytiri Kladno", Id = 1, DatumNarozeni = 1972 });
                    hokejisti.Add(new Hokejista() { Jmeno = "Petr Koukal", Tym = "Mountfield HK", Id = 2, DatumNarozeni = 1989 });
                    hokejisti.Add(new Hokejista() { Jmeno = "David Pastřňák", Tym = "Boston", Id = 3, DatumNarozeni = 1980 });
                    hokejisti.Add(new Hokejista() { Jmeno = "Tomáš Rolinek", Tym = "Dynamo Pardubice", Id = 4, DatumNarozeni = 1979 });

                }

                return hokejisti;
            }
        }
    }
}
