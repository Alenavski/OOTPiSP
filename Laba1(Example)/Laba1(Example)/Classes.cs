using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_Example_
{ 

        [Serializable]
        public class CatalogItem
        {
            public enum TCategorys {Top, Skirt, Trousers,Outwear, Dress, Jumpsuit,Sleeves};
            public TCategorys Category;
            public enum TColor { Red,Orange,Yellow, Green, Cyan, Blue, Purple, Black, White, Grey,Pink,Brown};
            public string Name;
            public string Cloth;
            public int Length;
            public double Price;
            public TColor Color;

            public CatalogItem(string name, string cloth, int length, double price, TColor color)
            {
                this.Name = name;
                this.Cloth = cloth;
                this.Length = length;
                this.Price = price;
                this.Color = color;
            }
            
        }
        [Serializable]
        public class Sleeves: CatalogItem
        {
            public bool Transparancy;
            public bool isNeedAdding;
            public Sleeves(bool transparancy,bool isneedadding,string name, string cloth, int length, double price, TColor color) : base(name, cloth, length, price, color)
            {
                this.Transparancy = transparancy;
                this.isNeedAdding = isneedadding;
                this.Category = TCategorys.Sleeves;
            }
        }
        [Serializable]
        public class Top : CatalogItem
        {
            public enum TNecklineType {V_figure,U_figure,Insert,Neckline,Boat,Square,Envelope,Round,Loop,Clamp};
            public Sleeves sleeves;
            public TNecklineType NecklineType;
            public Top(Sleeves sleeves, TNecklineType necklinetype, string name, string cloth, int length, double price, TColor color) : base(name,cloth,length,price,color)
            {
                this.sleeves = sleeves;
                this.NecklineType = necklinetype;
                this.Category = TCategorys.Top;
            }

        }

        [Serializable]
        public class Skirt: CatalogItem
        {
            public enum TSilhouetteType {Straight,Wedge,Conical};
            public TSilhouetteType SilhouetteType;
            public enum TCutType {Tulip,Pencil,Bell,Sun,Half_sun,Flared_sun,Pack,Chanticleer,Conical,Wide};
            public TCutType CutType;
            public bool Pockets;
            public int Width;
            public Skirt(TSilhouetteType silhouettetype, TCutType cuttype, bool pockets, int width, string name, string cloth, int length, double price, TColor color) : base(name, cloth, length, price, color)
            {
                this.SilhouetteType =silhouettetype;
                this.CutType=cuttype;
                this.Pockets = pockets;
                this.Width = width;
                this.Category = TCategorys.Skirt;
            }
        }

        [Serializable]
        public class Trousers : CatalogItem
        {
            public bool Pockets;
            public bool Twists;
            public bool Arrows;
            public bool Holes;
            public Trousers(bool pockets, bool twists, bool arrows, bool holes, string name, string cloth, int length, double price, TColor color) : base(name, cloth, length, price, color)
            {
                this.Pockets = pockets;
                this.Twists = twists;
                this.Arrows = arrows;
                this.Holes = holes;
                this.Category = TCategorys.Trousers;
            }
        }

        [Serializable]
        public class Dress : CatalogItem
        {
            public Top top;
            public Skirt skirt;
            public bool isBelt;
            public enum TDecorShoulders {Collar,Lace,Fastener,Grid,Bow,Wave};
            public TDecorShoulders DecorShoulders;
            public Dress(Top top, Skirt skirt, bool isbelt, TDecorShoulders decorshoulders, string name, string cloth, int length, double price, TColor color) : base(name, cloth, length, price, color)
            {
                this.top = top;
                this.skirt = skirt;
                this.isBelt = isbelt;
                this.DecorShoulders = decorshoulders;
                this.Category = TCategorys.Dress;
            }
        }

        [Serializable]
        public class JumpSuit : CatalogItem
        {
            public Top top;
            public Trousers trousers;
            public enum TDecorBelt {Bow,Fastener,Rubber,Tape,Chain,Flagellum};
            public TDecorBelt DecorBelt;
            public JumpSuit(Top top, Trousers trousers, TDecorBelt decorbelt, string name, string cloth, int length, double price, TColor color) : base(name, cloth, length, price, color)
            {
                this.top = top;
                this.trousers = trousers;
                this.DecorBelt = decorbelt;
                this.Category = TCategorys.Jumpsuit;
            }
        }

        [Serializable]
        public class Outwear : Top
        {
            public bool isBelt;
            public enum TFastenerType{Buttons,Zipper,Snap,Tape};
            public TFastenerType FastenerType;
            public enum TCollarType {TurnDown,Stand_up_TurnDown,Jacket,Fancy};
            public TCollarType CollarType;
            public Outwear(bool isbelt, TFastenerType fastenertype, TCollarType collartype, Sleeves sleeves, TNecklineType necklinetype, string name, string cloth, int length, double price, TColor color) : base(sleeves,necklinetype,name,cloth,length,price,color)
            {
                this.isBelt = isbelt;
                this.FastenerType = fastenertype;
                this.CollarType = collartype;
                this.Category = TCategorys.Outwear;
            }
        }
   }
