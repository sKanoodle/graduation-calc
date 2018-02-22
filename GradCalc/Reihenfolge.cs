using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradCalc
{
    public class Reihenfolge
    {
        ArrayList fächer = new ArrayList();
        ArrayList kurse = new ArrayList();
        ArrayList fächerInd = new ArrayList();

        public void Clear()
        {
            this.fächer.Clear();
            this.kurse.Clear();
            this.fächerInd.Clear();
        }

        public void FächerImp(Object obj)
        {
            this.fächer.Add(obj);
        }

        public void KurseImp(Object obj)
        {
            this.kurse.Add(obj);
        }

        public void FächerIndImp(Object obj)
        {
            this.fächerInd.Add(obj);
        }

        public int Length()
        {
            return this.fächer.Count;
        }

        public string FächerExp(int a)
        {
            return Convert.ToString(this.fächer[a]);
        }

        public bool KurseExp(int a)
        {
            return Convert.ToBoolean(this.kurse[a]);
        }

        public int FächerIndExp(int a)
        {
            return Convert.ToInt32(this.fächerInd[a]);
        }
    }
}
