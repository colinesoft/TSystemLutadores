using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuta.UI.Models;

namespace TesteLuta.UI.Services.Interfaces
{
    interface ITournament
    {
        void CreateGroups();
        void QuarterFinals();
        void SemiFinals();
        List<Fighter> Final();
    }
}
