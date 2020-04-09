using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletroestimulador
{
    class Protocolos
    {
        public const string amplitude = "IAM";
        public const string frequencia = "FRQ";
        public const string larguraPulso = "BDW";
        public const string duracaoTotal = "TDR";
        public const string duracaoBurst = "BRW";
        public const string intervaloBurst = "BRI";
        public const string duracaoTB = "BTW";
        public const string intervaloTB = "BTI";

        public const string intervaloRndOFF = "RNDOFF";
        public const string intervaloTBmin = "RNDMIN";
        public const string intervaloTBmax = "RNDMAX";

        public const string iniciar = "STA";
        public const string parar = "STO";
        public const string relatorio = "REP";
        public const string atualizar = "ATT";


        public List<string> Cods(bool rnd_on)
        {
            List<string> prot_base = new List<string>();
            prot_base.Add(amplitude);
            prot_base.Add(frequencia);
            prot_base.Add(larguraPulso);
            prot_base.Add(duracaoTotal);
            prot_base.Add(duracaoBurst);
            prot_base.Add(intervaloBurst);
            prot_base.Add(duracaoTB);

            if(!rnd_on)
            {
                prot_base.Add(intervaloTB);
            }
            else
            {
                prot_base.Add(intervaloTBmin);
                prot_base.Add(intervaloTBmax);
            }


            return prot_base;
        }
    }

    
}
