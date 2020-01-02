using System;
using System.Collections.Generic;
using System.Text;

namespace OtherLanguageImprovements
{
    public interface IAnimalWidget
    {
        private static int AmountToFeed = 10;

        string Name { get; }
        int Happiness { get; set; }

        void Feed()
        {
            Happiness += AmountToFeed;
        }
    }
}
