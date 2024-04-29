using System.Net;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace csharp_lista_indirizzi
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //creo il path del percorso
            string path = "C:\\Users\\andre\\Desktop\\Boolean\\C#\\addresses.txt";

            //path del secondo percorso (dove memorizzo gli indirizzi)
            string path2 = "C:\\Users\\andre\\Desktop\\Boolean\\C#\\addresses2.txt";


            // legge gli indirizzi dal file e li memorizza in una lista
            List<Address> addresses = LeggiDaTesto(path);


            //stampa gli indirizzi
            foreach (var address in addresses)
            {
                Console.WriteLine();
                Console.WriteLine($"{address.Name} {address.Surname}, {address.Street}, {address.City}, {address.Province}, {address.ZIP}");
            }

            //richiamo della funzione per la stampa della nuova lista in un nuovo file
            StampaLista(addresses, path2);


        }

        //metodo per leggere il file
        public static List<Address> LeggiDaTesto(string path)
        {

            // lista per memorizzare gli indirizzi letti dal file
            List<Address> addresses = new List<Address>();

            //apre il file in sola lettura
            var stream = File.OpenText(path);

            //contatore per tenere traccia del numero di righe
            int i = 0;

            //ciclo per generare ogni singola riga
            while (stream.EndOfStream == false)
            {
                var linea = stream.ReadLine();

                i++;

                //riduco il file di una riga (dato che non serve)
                if (i <= 1)
                {
                    continue;
                }

                // provo a leggere e elaborare i dati dalla riga corrente
                try
                {

                    //dato che il separatore è una virgola
                    var dati = linea.Split(',');

                    // estrae i dati relativi all'indirizzo dalla riga
                    string name = dati[0];
                    string surname = dati[1];
                    string street = dati[2];
                    string city = dati[3];
                    string province = dati[4];
                    string zipString = dati[5];


                    //condizioni se il campo è nullo o inesistente
                    if (name == null || name == "")
                    {
                        throw new ArgumentException("Il campo Nome non può essere vuoto.");
                    } 

                    else if (surname == null || surname == "")
                    {
                        throw new ArgumentException("Il campo Cognome non può essere vuoto.");
                    }

                    else if (street == null || street == "")
                    {
                        throw new ArgumentException("Il campo Via non può essere vuoto.");
                    }

                    else if (city == null || city == "")
                    {
                        throw new ArgumentException("Il campo Città non può essere vuoto.");
                    }

                    else if (province == null || province == "")
                    {
                        throw new ArgumentException("Il campo Provincia non può essere vuoto.");
                    }


                    //condizioni se il campo è differente da un numero intero
                    int zip;
                    if (!int.TryParse(zipString, out zip))
                    {
                        throw new FormatException("Il campo Zip non è valido.");
                    }

                    addresses.Add(new Address(name, surname, street, city, province, zip));
                }

                // gestione delle eccezioni durante l'elaborazione della riga
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Errore nei dati nella riga {i}: {e.Message}");
                    Console.WriteLine($"Riga errata: {linea}");
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Errore nei dati nella riga {i}: {e.Message}");
                    Console.WriteLine($"Riga errata: {linea}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Errore generico nella riga {i}: {e.Message}");
                    
                }
            }

            // chiudo lo stream del file
            stream.Dispose();

            //ritorna la lista
            return addresses;
        }


        //creao un metodo per stampare la lista su un nuovo file
        public static void StampaLista(List<Address>addresses, string path)
        {

            //creo il nuovo file
            using StreamWriter stream = File.CreateText(path);

            //itero gli elemento che verranno inseriti all'interno del nuovo file
            foreach (var address in addresses)
            {
                //scrivo le righe
                stream.WriteLine(address.ToString());
            }
        }
    }
}
