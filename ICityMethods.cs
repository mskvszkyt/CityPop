using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConPop
{
    interface ICityMethods
    {
        /// <summary>
        /// Betölti a városok adatait CSV fájlból
        /// </summary>
        /// <param name="path">A megadott útvonalon lévő fájlból olvas be</param>
        void LoadFromCSV(string path);

        /// <summary>
        /// Elmenti a városok adatait CSV fájlba
        /// </summary>
        /// <param name="path">A megadott útvonalon lévő fájlba írja ki az adatokat</param>
        /// <param name="cities">A városok listája</param>
        /// <param name="charCode">A karakterkódolást határozzsa meg</param>
        void SaveToCSV(string path, List<City> cities, string charCode="UTF-8");

        /// <summary>
        /// Visszaadja a beolvasott városok darabszámát
        /// </summary>
        int CityCount();

        /// <summary>
        /// Folyamatosan növekvő-e a megadott népességi adatok sorozata?
        /// </summary>
        /// <returns>true esetén igen</returns>
        bool IsContinuousGrowing(List<int> populationDatas);

        /// <summary>
        /// Visszaadja a 10 legnagyobb népességű várost a megadott évben
        /// </summary>
        List<City> Top10City(int year);
    }
}
