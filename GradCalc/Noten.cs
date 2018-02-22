using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradCalc
{
    public class Noten
    {
        List<int> zensur = new List<int>();
        List<string> kurs = new List<string>();
        Dictionary<string,int> fach = new Dictionary<string,int>();
        Form1 hauptfenster;
        int count = 0;

        public Noten(Form1 caller)
        {
            hauptfenster = caller;
            for (int i = 1; i < hauptfenster.fächer.Length - 1; i++)
                fach.Add(hauptfenster.fächer[i], -1);
        }

        public void Clear()
        {
            this.zensur.Clear();
            this.kurs.Clear();
            this.count = 0;
            for (int i = 1; i < hauptfenster.fächer.Length - 1; i++)
                fach[hauptfenster.fächer[i]] = -1;
        }

        public void ZensurImp(int zensur, string kurs, string fach)
        {
            this.zensur.Add(zensur);
            this.kurs.Add(kurs);
            this.fach[fach] = this.count;
            this.count++;
        }

        public int Zensur(int a)
        {
            return this.zensur[a];
        }

        public void ZensurLower(int a)
        {
            this.zensur[a] = this.zensur[a] - 1;
        }

        public string Kurs(int a)
        {
            return this.kurs[a];
        }

        public int Fach(string a)
        {
            return this.fach[a];
        }

        public int Length
        {
            get { return this.zensur.Count; }
        }
    }
}
