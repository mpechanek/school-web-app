using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
   public class HokejistaLiga : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Nazev { get; set; }
        public virtual string Popis { get; set; }


    }
}
