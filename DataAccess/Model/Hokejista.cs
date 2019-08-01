using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccess.Model
{
    public class Hokejista : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Jméno hráče je vyžadováno!")]

        public virtual string Jmeno { get; set; }

        [Required(ErrorMessage = "Tým je vyžadován!")]

        public virtual string Tym { get; set; }

        [Required(ErrorMessage = "Rok narození je vyžadován!")]
        [Range(1950, 2019, ErrorMessage = "Rok narození může být od 1950 do 2019!")]

        public virtual int DatumNarozeni { get; set; }

        public virtual int PocetBodu { get; set; }

        [AllowHtml]

        public virtual string Popis { get; set; }

        public virtual string ImageName { get; set; }

        public virtual HokejistaLiga Liga { get; set; }

        public virtual HokejistaPost Post { get; set; }

    }
}
