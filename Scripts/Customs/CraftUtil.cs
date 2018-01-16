using System;

namespace Server
{
    public static class CraftUtil
    {
        public static int GetBonusProps(int maxProps)
        {
            int p0 = 0, p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0, p6 = 0, p7 = 0;

            switch (maxProps)
            {
                case 1: p0 = 3; p1 = 1; break;
                case 2: p0 = 6; p1 = 3; p2 = 1; break;
                case 3: p0 = 10; p1 = 6; p2 = 3; p3 = 1; break;
                case 4: p0 = 16; p1 = 12; p2 = 6; p3 = 5; p4 = 1; break;
                case 5: p0 = 30; p1 = 25; p2 = 20; p3 = 15; p4 = 9; p5 = 1; break;
                case 6: p1 = 30; p2 = 25; p3 = 20; p4 = 15; p5 = 9; p6 = 1; break;
                case 7: p2 = 30; p3 = 25; p4 = 20; p5 = 15; p6 = 9; p7 = 1; break;
            }

            int pc = p0 + p1 + p2 + p3 + p4 + p5 + p6 + p7;

            int rnd = Utility.Random(pc);

            if (rnd < p7)
                return 7;
            else
                rnd -= p7;

            if (rnd < p6)
                return 6;
            else
                rnd -= p6;

            if (rnd < p5)
                return 5;
            else
                rnd -= p5;

            if (rnd < p4)
                return 4;
            else
                rnd -= p4;

            if (rnd < p3)
                return 3;
            else
                rnd -= p3;

            if (rnd < p2)
                return 2;
            else
                rnd -= p2;

            if (rnd < p1)
                return 1;

            return 0;
        }
    }
}