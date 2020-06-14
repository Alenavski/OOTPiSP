using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laba1_Example_.MyForms;

namespace Laba1_Example_
{

    public abstract class Creator
    {
        public abstract Form Create(Form1.UpdateMethod method, object obj, int index);
    }

    class TopCreator : Creator
    {
        public override Form Create(Form1.UpdateMethod method, object obj, int index)
        {
            return new TopForm(method, obj, index);
        }
    }

    class SkirtCreator : Creator
    {
        public override Form Create(Form1.UpdateMethod method, object obj, int index)
        {
            return new SkirtForm(method, obj, index);
        }
    }

    class TrousersCreator : Creator
    {
        public override Form Create(Form1.UpdateMethod method, object obj, int index)
        {
            return new TrousersForm(method, obj, index);
        }
    }

    class DressCreator : Creator
    {
        public override Form Create(Form1.UpdateMethod method, object obj, int index)
        {
            return new DressForm(method, obj, index);
        }
    }

    class JumpSuitCreator : Creator
    {
        public override Form Create(Form1.UpdateMethod method, object obj, int index)
        {
            return new JumpsuitForm(method, obj, index);
        }
    }

    class OutwearCreator : Creator
    {
        public override Form Create(Form1.UpdateMethod method, object obj, int index)
        {
            return new OutwearForm(method, obj, index);
        }
    }

    class SleevesCreator : Creator
    {
        public override Form Create(Form1.UpdateMethod method, object obj, int index)
        {
            return new SleeveForm(method, obj, index);
        }
    }
}
