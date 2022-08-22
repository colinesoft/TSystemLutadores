using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TesteLuta.UI.Services.Interfaces;

namespace TesteLuta.UI.Models
{
    public class GroupFighter
    {
        public string IdGrupo { get; set; }
        public List<Fighter> Fighters { get; set; }
    }
}