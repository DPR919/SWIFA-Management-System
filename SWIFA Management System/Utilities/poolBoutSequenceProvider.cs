using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWIFA_Management_System.Utilities
{
    public static class poolBoutSequenceProvider
    {
        public static readonly Dictionary<int, List<(int left, int right)>> Sequences
            = new Dictionary<int, List<(int left, int right)>>()
        {
            [4] = new List<(int, int)>
            {
                        (1,4), (2,3),
                        (1,3), (2,4),
                        (3,4), (1,2)
            },
            [5] = new List<(int, int)>()
            {
                        (1,2), (3,4),
                        (5,1), (2,3),
                        (5,4), (1,3),
                        (2,5), (4,1),
                        (3,5), (4,2)
            },
            [6] = new List<(int, int)>()
            {
                        (1,2), (4,5), (2,3),
                        (5,6), (3,1), (6,4),
                        (2,5), (1,4), (5,3),
                        (1,6), (4,2), (3,6),
                        (5,1), (3,4), (6,2)
            },
            [7] = new List<(int, int)>()
            {
                        (1,4), (2,5), (3,6),
                        (7,1), (5,4), (2,3),
                        (6,7), (5,1), (4,3),
                        (6,2), (5,7), (3,1),
                        (4,6), (7,2), (3,5),
                        (1,6), (2,4), (7,3),
                        (6,5), (1,2), (4,7)
            },
            [8] = new List<(int, int)>()
            {
                        (2,3), (1,5), (7,4), (6,8),
                        (1,2), (3,4), (5,6), (8,7),
                        (4,1), (5,2), (8,3), (6,7),
                        (4,2), (8,1), (7,5), (3,6),
                        (2,8), (5,4), (6,1), (3,7),
                        (4,8), (2,6), (3,5), (1,7),
                        (4,6), (8,5), (7,2), (1,3)
            }
        };
    }
}
