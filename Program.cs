
using System.Text;

namespace ConPop
{
    internal class Program
    {
        public class CityDataProvider() : ICityMethods
        {
            public List<City> cityList = new();
            public int CityCount()
            {
                return cityList.Count;
            }

            public bool IsContinuousGrowing(List<int> populationDatas)
            {
                int temp = populationDatas[0];
                foreach (int pop in populationDatas.Skip(1)) {
                    if (pop < temp)
                    {
                        return false;
                    }
                    temp = pop;
                }
                return true;
            }

            public void LoadFromCSV(string path)
            {
                foreach (var line in File.ReadAllLines(path).Skip(1))
                {
                    var data = line.Split(';');
                    var pop = new List<int>();
                    pop.Add(int.Parse(data[2]));
                    pop.Add(int.Parse(data[3]));
                    pop.Add(int.Parse(data[4]));
                    pop.Add(int.Parse(data[5]));
                    pop.Add(int.Parse(data[6]));
                    cityList.Add(new City
                    {
                        CityCode = data[0],
                        CityName = data[1],
                        PopInDecades = pop
                    });
                };
            }

            public void SaveToCSV(string path, List<City> cities, string charCode = "UTF-8")
            {
                var lines = new List<string>();
                foreach (var c in cities)
                {
                    lines.Add($"{c.CityCode};{c.CityName};{c.PopInDecades[0]};{c.PopInDecades[1]};{c.PopInDecades[2]};{c.PopInDecades[3]};{c.PopInDecades[4]}");
                }
                File.WriteAllLines(path, lines.ToArray(), Encoding.UTF8);
            }

            public List<City> Top10City(int year)
            {
                int index;
                if (year == 2010)
                {
                    index = 0;
                }
                else if (year == 2020)
                {
                    index = 1;
                }
                else if (year == 2030)
                {
                    index = 2;
                }
                else if (year == 2040)
                {
                    index = 3;
                }
                else if (year == 2050)
                {
                    index = 4;
                }
                else {
                    return null;
                }

                return cityList.OrderByDescending(x => x.PopInDecades[index]).Take(10).ToList();
            }
        }
        static void Main(string[] args)
        {
            CityDataProvider _provider = new CityDataProvider();

            _provider.LoadFromCSV("pop_city.csv");

            Console.WriteLine(_provider.CityCount());

            var top10 = _provider.Top10City(2020) ?? new();
            foreach (City city in top10) {
                Console.WriteLine(city.CityName);
            }

            var input = "";
            var inpCity = new City();
            do
            {
                Console.WriteLine("város: ");
                input = Console.ReadLine();
                inpCity = _provider.cityList.Find(c => c.CityName == input) ?? null;
            } while (inpCity == null);

            Console.WriteLine(_provider.IsContinuousGrowing(inpCity.PopInDecades));

            var millionCities = _provider.cityList.Where(c => c.PopInDecades[0] > 1000000).ToList();
            _provider.SaveToCSV("bigCities.csv",millionCities);


            var lessCities = _provider.cityList.Where(c => c.PopInDecades[0] > c.PopInDecades[4]).Select(y=> new { Name = y.CityName, Kulonbseg = y.PopInDecades[4] - y.PopInDecades[0] }).OrderBy(n=>n.Kulonbseg);
            foreach (var item in lessCities)
            {
                Console.WriteLine($"{nameof(item.Name)}: {item.Name} - {nameof(item.Kulonbseg)}: {item.Kulonbseg}");
            }
            //A kapott CSV állomány Európa nagyobb városainak a korábbi és a jövőbeli becsült népességszámait tartalmazza.
            // CITY_CODE;CITY_NAME;Y2010;Y2020;Y2030;Y2040;Y2050


            //todo: 1F Készítsen osztályt egy város adatainak tárolására! (City)
            //todo: 2F Készítsen (CityDataProvider) néven osztályt a városok adatainak kezelésére, ami a megadott (CitiesMethods) interfészt implementálja!

            //todo: Válaszoljon a következő kérdésekre a korábban létrehozott osztályok felhasználásával!

            //todo: 3F Hány város adatait tartalmazza a CSV fájl? (CityCount)
            //todo: 4F Melyik 10 város volt a legnépesebb 2020-ban? (Top10City)
            //todo: 5F Kérje be billentyűzetről egy város nevét! Ha nem létezik, akkor jelezze azt és kérje be újra! 
            //         Miután létező nevet adott meg, döntse el, hogy a város lakossága folyamatosan növekedett-e az évek alatt? (IsContinuousGrowing)
            //todo: 6F Írja (bigCities.CSV) fájlba a 2020-ban 1 millió főnél nagyobb népességgel rendelkező városokat! (SaveToCSV)
            //todo: 7F Írassa képernyőre azoknak a városoknak a nevét és népességváltozását, ahol 2050-ben kevesebben lesznek mint 2010-ben voltak!
            //todo:    A kiíratás népességcsökkenés szerint növevően rendezetten történjen! 
            //todo:    Tipp: Érdemes megfelelő metódussal vagy property-vel bővíteni az osztályt! (CityDataProvider)
        }
    }
}
