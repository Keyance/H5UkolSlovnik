﻿var slovnik = new Dictionary<string, string>();

string cesta = @"e:\text\czechitas\slovnik.txt";

//kontroluji jestli soubor vůbec je
if (File.Exists(cesta))
{
    Console.WriteLine("Soubor existuje.");
}
else
{
    Console.WriteLine("Soubor neexistuje.");
    File.Create(cesta);
}

string[] radky = File.ReadAllLines(cesta);
string celyText;

//kontrola zda je v souboru vůbec něco, pokud není, necháváme jej prázdný, ale informujeme uživatele
if (radky.Length > 0)
{
    celyText = File.ReadAllText(cesta);

    string[] pary = celyText.Split(';');

    foreach (string par in pary)
    {
        if (par != "")
        { 
            string[] jednotlive = par.Split("="); 
            slovnik.Add(jednotlive[0], jednotlive[1]);
        }
    }
}

else
{
    Console.WriteLine("Soubor je zatím prázdný.");
}


bool jeKonec = false;

while (!jeKonec)
{
    Console.WriteLine("1 - Přidat položku do slovníku");
    Console.WriteLine("2 - Vypsat celý slovník");
    Console.WriteLine("3 - Vyhledej dle českého slova");
    Console.WriteLine("4 - Vyhledej dle anglického slova");
    Console.WriteLine("0 - Ukončit a nahrát do souboru");

    int volba = Convert.ToInt32(Console.ReadLine());

    //key = český výraz, value = anglický výraz 
    switch (volba)
    {
        case 0:
            File.WriteAllText(cesta, "");
            foreach (var polozka in slovnik)
            {
                string jedenZaznam = polozka.Key + "="+ polozka.Value + ";";
                File.AppendAllText(cesta, jedenZaznam);

            }
            jeKonec = true;
            break;
        case 1:
            {
                Console.WriteLine("Zadej český výraz:");
                string cz = Console.ReadLine().ToLower();
                if (!slovnik.ContainsKey(cz))
                {
                    Console.WriteLine("Zadej anglické synonymum:");
                    string ang = Console.ReadLine().ToLower();
                    slovnik.Add(cz, ang);
                }
                else { Console.WriteLine("Tento výraz již v seznamu existuje."); }
                break;
            }
        case 2:
            Console.WriteLine("Česky\tAnglicky");
            foreach (KeyValuePair<string, string> kontakt in slovnik)
            {
                Console.WriteLine($"{kontakt.Key}\t{kontakt.Value}");
            }
            break;
        case 3:
            {
                Console.WriteLine("Zadej počátek hledaného českého výrazu:");
                string pocatek = Console.ReadLine();
                var jmeno = slovnik.Where(x => x.Key.StartsWith(pocatek)).Select(z => z).ToList();
                foreach (var jm in jmeno)
                {
                    Console.WriteLine(jm.Key + " " + jm.Value);
                }
            }
            break;
        case 4:
            {
                Console.WriteLine("Zadej počátek hledaného hesla v angličtině:");
                string pocatek = Console.ReadLine();
                var jmeno = slovnik.Where(x => x.Value.StartsWith(pocatek)).Select(z => z).ToList();
                foreach (var jm in jmeno)
                {
                    Console.WriteLine(jm.Value + " " + jm.Key);
                }
            }
            break;
    }
}
