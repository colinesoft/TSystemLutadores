using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteLuta.UI.App_Start;
using TesteLuta.UI.Models;
using TesteLuta.UI.Services.Interfaces;

namespace TesteLuta.UI.Services
{
    public class Tournament: ITournament
    {
        enum grp
        {
            A,
            B,
            C,
            D
        }

        private int[] _idFighters { get; set; }
        public List<GroupFighter> _groups { get; private set; }
        public List<GroupFighter> _quarterFinals { get; private set; }
        public List<Fighter> _semiFinals { get; private set; }
        public List<Fighter> _final { get; private set; }
        public Tournament()
        {

        }
        public Tournament( int[] fighters)
        {
            if (fighters.Count() != 20)
                throw new Exception("São necessários 20 lutadores para iniciar o torneio.");

            _idFighters = fighters;
            CreateGroups();
        }

        public void CreateGroups()
        {
            const int qtdeGrp = 5;
            List<GroupFighter> groups = new List<GroupFighter>();
            ApiAccess fighters = new ApiAccess();
            
            var compets = fighters.AllFighter.Where(a => _idFighters.Contains(a.Id))
                .OrderBy(a => a.Idade)
                .ToList();

            for (int g = 0; g < 4; g++)
            {
                GroupFighter group = new GroupFighter();
                string v = Enum.GetName(typeof(grp), g);
                group.IdGrupo = v;
                group.Fighters = compets.Take(qtdeGrp + (g * qtdeGrp))
                    .Skip(g * qtdeGrp)
                    .ToList();

                foreach (var fighter in group.Fighters)
                    fighter.DefinirForca();

                groups.Add(group);
            }

            _groups = groups;
        }

        public void QuarterFinals()
        {
            List<GroupFighter> QuarterFinals = new List<GroupFighter>();
            foreach (var group in _groups)
            {
                GroupFighter groupF = new GroupFighter();
                var classified = group.Fighters.OrderByDescending(c => c.Desempenho)
                    .Take(2)
                    .ToList();
                groupF.Fighters = classified;
                groupF.IdGrupo = group.IdGrupo;

                QuarterFinals.Add(groupF);
            }
            _quarterFinals = QuarterFinals;
        }

        public void SemiFinals()
        {
            _semiFinals = new List<Fighter>();
            _semiFinals.Add(Fight(_quarterFinals[(int)grp.A].Fighters[0], _quarterFinals[(int)grp.B].Fighters[1])[0]); //1A x 2B
            _semiFinals.Add(Fight(_quarterFinals[(int)grp.A].Fighters[1], _quarterFinals[(int)grp.B].Fighters[0])[0]); //2A x 1B
            _semiFinals.Add(Fight(_quarterFinals[(int)grp.C].Fighters[0], _quarterFinals[(int)grp.D].Fighters[1])[0]); //1C x 2D
            _semiFinals.Add(Fight(_quarterFinals[(int)grp.C].Fighters[1], _quarterFinals[(int)grp.D].Fighters[0])[0]); //2C x 1D
        }

        public List<Fighter> Final()
        {
            var SemiA = Fight(_semiFinals[0], _semiFinals[1]);
            var SemiB = Fight(_semiFinals[2], _semiFinals[3]);

            var FinalA = Fight(SemiA[0], SemiB[0]);
            var FinalB = Fight(SemiA[1], SemiB[1]);

            _final = new List<Fighter>();
            _final.Add(FinalA[0]); //Campeão
            _final.Add(FinalA[1]); //Vice
            _final.Add(FinalB[0]); //Terceiro
            return _final;
        }

        private List<Fighter> Fight(Fighter one, Fighter two)
        {
            List<Fighter> f = new List<Fighter>();
            if(one.Desempenho > two.Desempenho)
            {
                f.Add(one);
                f.Add(two);
            }
            else
            {
                f.Add(two);
                f.Add(one);
            }
            return f;
        }
    }
}