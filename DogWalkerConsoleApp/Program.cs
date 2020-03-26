using System;
using DogWalkerConsoleApp.Data;

namespace DogWalkerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine();

            Console.WriteLine("Listing all dogs:");
            Console.WriteLine();

            var dogRepo = new DogRepository();

            var allDogs = dogRepo.GetAllDogs();

            foreach (var dog in allDogs)
            {
                Console.WriteLine($"Dog Info: {dog.Name} is a {dog.Breed}! Notes: {dog.Notes}");
            }

            Console.WriteLine();

            Console.WriteLine("Listing all owners:");
            Console.WriteLine();

            var ownerRepo = new OwnerRepository();
            var allOwners = ownerRepo.getAllOwners();

            foreach (var owner in allOwners)
            {
                Console.WriteLine($"Name: {owner.Name}");
                Console.WriteLine($"Neighberhood: {owner.Neighborhood.Name}");
                Console.WriteLine($"Phone: {owner.Phone}");
                Console.WriteLine($"Address: {owner.Address}");
                Console.WriteLine();
            }

            var walkerRepo = new WalkerRepository();
            var allWalkers = walkerRepo.getAllWalkers();

            Console.WriteLine("Listing all walkers:");
            Console.WriteLine();

            foreach (var walker in allWalkers)
            {
                Console.WriteLine($"Name: {walker.Name}");
                Console.WriteLine($"Neighborhood: {walker.Neighborhood.Name}");
            }

            Console.WriteLine();

            Console.WriteLine("Listing all neighborhoods:");

            Console.WriteLine();

            var neighborhoodRepo = new NeighborhoodRepository();
            var allNeighborhoods = neighborhoodRepo.GetAllNeighborhoods();

            foreach (var neighborhood in allNeighborhoods)
            {
                Console.WriteLine($"Neighborhood: {neighborhood.Name}");
            }
        }

    }
}
