using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteLuta.UI.Models
{
    public class Fighter
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string[] ArtesMarciais { get; set; }
        public int Lutas { get; set; }
        public int Derrotas { get; set; }
        public int Vitorias { get; set; }

        public int Desempenho { get; private set; }

        public void DefinirForca()
        {
            try
            {
                //Calculo para determinar desempate entre lutadores
                var percent = (Vitorias / (float)Lutas) * 100;
                Desempenho = (int)percent * 100000 + ArtesMarciais.Count() * 1000 + Lutas ;
            }
            catch (Exception)
            {
                throw new Exception("Nenhum lutador definido");
            }
        }

    }
}