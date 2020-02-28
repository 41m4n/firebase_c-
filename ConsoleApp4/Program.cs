using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                new Program().Run().Wait();
                listenFirebase();

            }
            catch (Exception ex) {
                Console.WriteLine("Error:" + ex);
            }

            Console.ReadLine();
        }

        private async Task Run()
        {
            // Since the dinosaur-facts repo no longer works, populate your own one with sample data
            // in "sample.json"
            var firebase = new FirebaseClient("https://smarthome-86bf8.firebaseio.com/");

            var dinos = await firebase
              .Child("dinosaurs")
              .OrderByKey()
              .StartAt("pterodactyl")
              .LimitToFirst(3)
              .OnceAsync<Dinosaur>();

            foreach (var dino in dinos)
            {

                if (dino.Object.test == null) // check whether json field exist
                {
                    Console.WriteLine("test is Null");
                    Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high {dino.Object.Length}m length {dino.Object.Order} order.");
                }
                else {
                    Console.WriteLine("test exist");
                    Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high {dino.Object.Length}m length {dino.Object.Order} order {dino.Object.test["test1"]} child.");
                }

                
            }

            var dinos2 = firebase
             .Child("dinosaurs")
             .OrderByKey()
             .StartAt("pterodactyl")
             .LimitToFirst(2);

            //Console.WriteLine(dinos2.Client.Child());
            
        }

        private static void listenFirebase() {

            var firebase = new FirebaseClient("https://smarthome-86bf8.firebaseio.com/");
            var observable = firebase
              .Child("washer")
              .AsObservable<Dinosaur>()
              .Subscribe(d => {

                  Console.WriteLine(d.Key);
                  if (d.Object.test == null) // check whether json field exist
                  {
                      Console.WriteLine("test is Null");
                      Console.WriteLine($"{d.Key} is {d.Object.Height}m high {d.Object.Length}m length {d.Object.Order} order.");
                  }
                  else
                  {
                      Console.WriteLine("test exist");
                      Console.WriteLine($"{d.Key} is {d.Object.Height}m high {d.Object.Length}m length {d.Object.Order} order {d.Object.test["test1"]} child.");
                  }

              });

        }

        private void displayReceive(IObservable<Dinosaur> obs) {
            //Console.Write(obs.)
        }
    }
}
