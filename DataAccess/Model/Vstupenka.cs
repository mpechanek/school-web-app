using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Vstupenka : IEntity
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Jméno zájemce je vyžadováno!")]

        public virtual string Jmeno { get; set; }

        [Required(ErrorMessage = "Adresa je vyžadována!")]

        public virtual string Adresa { get; set; }


        public virtual int PocetVstupenek{ get; set; }
    }
}
