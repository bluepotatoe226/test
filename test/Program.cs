namespace ApplianceStore

{
    class Appliance
    {
        public string ItemNumber { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public int Wattage { get; set; }
        public string Colour { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return @$"
            Item Number: {ItemNumber}
            Brand: {Brand}
            Quantity: {Quantity}
            Wattage: {Wattage}W
            Color: {Colour}
            Price: ${Price}
            ";
        }
    }

    class Refrigerator : Appliance
    {
        public int NumberOfDoors { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public override string ToString()
        {
            string doorDescription = NumberOfDoors switch
            {
                2 => "Double Door",
                3 => "Three Doors",
                4 => "Four Doors",
                _ => "Unknown"
            };

            return base.ToString() + $@"
            Number of Doors: {doorDescription}
            Height: {Height} inches
            Width: {Width} inches
            ";
        }
    }
    class Vacuum : Appliance
    {
        public string Grade { get; set; }
        public int BatteryVoltage { get; set; }

        public override string ToString()
        {
            return base.ToString() + $@"
            Grade: {Grade}
            Battery Voltage: {BatteryVoltage}V
            ";
        }
    }
    class Microwave : Appliance
    {
        public double Capacity { get; set; }
        public char RoomType { get; set; }

        public override string ToString()
        {
            string roomTypeDescription = RoomType switch
            {
                'K' => "Kitchen",
                'W' => "Work Site",
                _ => "Unknown"
            };

            return base.ToString() + $@"
            Capacity: {Capacity} cu.ft
            Room Type: {roomTypeDescription}
            ";
        }
    }
    class Dishwasher : Appliance
    {
        public string Feature { get; set; }
        public string SoundRating { get; set; }

        public override string ToString()
        {
            return base.ToString() + $@"
            Feature: {Feature}
            Sound Rating: {SoundRating}
            ";
        }
    }
    class ApplianceStore
    {
        private List<Appliance> appliances = new List<Appliance>();

        public void ParseAppliances(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] data = line.Split(';');
                string itemNumber = data[0];
                string brand = data[1];
                int quantity = int.Parse(data[2]);
                int wattage = int.Parse(data[3]);
                string colour = data[4];
                double price = double.Parse(data[5]);

                Appliance appliance;
                switch (itemNumber[0])
                {
                    case '1':
                        int numberOfDoors = int.Parse(data[6]);
                        int height = int.Parse(data[7]);
                        int width = int.Parse(data[8]);
                        appliance = new Refrigerator
                        {
                            ItemNumber = itemNumber,
                            Brand = brand,
                            Quantity = quantity,
                            Wattage = wattage,
                            Colour = colour,
                            Price = price,
                            NumberOfDoors = numberOfDoors,
                            Height = height,
                            Width = width
                        };
                        break;
                    case '2':
                        string grade = data[6];
                        int batteryVoltage = int.Parse(data[7]);
                        appliance = new Vacuum
                        {
                            ItemNumber = itemNumber,
                            Brand = brand,
                            Quantity = quantity,
                            Wattage = wattage,
                            Colour = colour,
                            Price = price,
                            Grade = grade,
                            BatteryVoltage = batteryVoltage
                        };
                        break;
                    case '3':
                        double capacity = double.Parse(data[6]);
                        char roomType = char.Parse(data[7]);
                        appliance = new Microwave
                        {
                            ItemNumber = itemNumber,
                            Brand = brand,
                            Quantity = quantity,
                            Wattage = wattage,
                            Colour = colour,
                            Price = price,
                            Capacity = capacity,
                            RoomType = roomType
                        };
                        break;
                    case '4':
                    case '5':
                        string feature = data[6];
                        string soundRating = data[7];
                        appliance = new Dishwasher
                        {
                            ItemNumber = itemNumber,
                            Brand = brand,
                            Quantity = quantity,
                            Wattage = wattage,
                            Colour = colour,
                            Price = price,
                            Feature = feature,
                            SoundRating = soundRating
                        };
                        break;
                    default:
                        throw new Exception("Invalid item number.");
                }

                appliances.Add(appliance);
            }
        }

        public void PurchaseAppliance(string itemNumber)
        {
            Appliance appliance = appliances.FirstOrDefault(a => a.ItemNumber == itemNumber);
            if (appliance != null)
            {
                if (appliance.Quantity > 0)
                {
                    appliance.Quantity--;
                    Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                }
                else
                {
                    Console.WriteLine("The appliance is not available to be checked out.");
                }
            }
            else
            {
                Console.WriteLine("No appliances found with that item number.");
            }
        }
        public void SearchByBrand(string brand)
        {
            List<Appliance> matchingAppliances = appliances.Where(a => a.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchingAppliances.Any())
            {
                Console.WriteLine("Matching Appliances:");
                foreach (Appliance appliance in matchingAppliances)
                {
                    Console.WriteLine(appliance);
                }
            }
            else
            {
                Console.WriteLine($"No appliances found with brand '{brand}'.");
            }
        }
        public void DisplayAppliancesByType(int type)
        {
            switch (type)
            {
                case 1:
                    Console.WriteLine("Enter number of doors: 2 (double door), 3 (three doors) or 4 (four doors):");
                    int doors = int.Parse(Console.ReadLine());
                    DisplayRefrigeratorsByDoors(doors);
                    break;
                case 2:
                    Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high)");
                    int voltage = int.Parse(Console.ReadLine());
                    DisplayVacuumsByVoltage(voltage);
                    break;
                case 3:
                    DisplayMicrowaves();
                    break;
                case 4:
                    DisplayDishwashers();
                    break;
                default:
                    Console.WriteLine("Invalid appliance type.");
                    break;
            }
        }
        private void DisplayRefrigeratorsByDoors(int numberOfDoors)
        {
            List<Refrigerator> matchingRefrigerators = appliances.OfType<Refrigerator>().Where(r => r.NumberOfDoors == numberOfDoors).ToList();
            if (matchingRefrigerators.Any())
            {
                Console.WriteLine("Matching refrigerators:");
                foreach (Refrigerator refrigerator in matchingRefrigerators)
                {
                    Console.WriteLine(refrigerator);
                }
            }
            else
            {
                Console.WriteLine("No matching refrigerators found.");
            }
        }
        private void DisplayVacuumsByVoltage(int voltage)
        {
            List<Vacuum> matchingVacuums = appliances.OfType<Vacuum>().Where(v => v.BatteryVoltage == voltage).ToList();
            if (matchingVacuums.Any())
            {
                Console.WriteLine("Matching vacuums:");
                foreach (Vacuum vacuum in matchingVacuums)
                {
                    Console.WriteLine(vacuum);
                }
            }
            else
            {
                Console.WriteLine("No matching vacuums found.");
            }
        }
        private void DisplayMicrowaves()
        {
            List<Microwave> microwaves = appliances.OfType<Microwave>().ToList();
            if (microwaves.Any())
            {
                Console.WriteLine("All Microwaves:");
                foreach (Microwave microwave in microwaves)
                {
                    Console.WriteLine(microwave);
                }
            }
            else
            {
                Console.WriteLine("No microwaves found.");
            }
        }
        private void DisplayDishwashers()
        {
            List<Dishwasher> dishwashers = appliances.OfType<Dishwasher>().ToList();
            if (dishwashers.Any())
            {
                Console.WriteLine("All Dishwashers:");
                foreach (Dishwasher dishwasher in dishwashers)
                {
                    Console.WriteLine(dishwasher);
                }
            }
            else
            {
                Console.WriteLine("No dishwashers found.");
            }
        }
        public void DisplayRandomAppliances(int num)
        {
            Random random = new Random();
            List<Appliance> randomAppliances = appliances.OrderBy(x => random.Next()).Take(num).ToList();
            Console.WriteLine($"Random appliances ({num}):");
            foreach (Appliance appliance in randomAppliances)
            {
                Console.WriteLine(appliance);
            }
        }
        public void PersistAppliances(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Appliance appliance in appliances)
                {
                    writer.WriteLine($"{appliance.ItemNumber};{appliance.Brand};{appliance.Quantity};{appliance.Wattage};{appliance.Colour};{appliance.Price}");
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ApplianceStore store = new ApplianceStore();
            store.ParseAppliances("appliances.txt");

            Console.WriteLine("Welcome to Modern Appliances!");
            Console.WriteLine("How May We Assist You?");
            Console.WriteLine("1 – Check out appliance");
            Console.WriteLine("2 – Find appliances by brand");
            Console.WriteLine("3 – Display appliances by type");
            Console.WriteLine("4 – Produce random appliance list");
            Console.WriteLine("5 – Save & exit");
            int option;
            do
            {
                Console.WriteLine("Enter option:");
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option. Please enter a number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter item number of an Appliance:");
                        string itemNumber = Console.ReadLine();
                        store.PurchaseAppliance(itemNumber);
                        break;
                    case 2:
                        Console.WriteLine("Enter brand to search for:");
                        string brand = Console.ReadLine();
                        store.SearchByBrand(brand);
                        break;
                    case 3:
                        Console.WriteLine("Appliance Types");
                        Console.WriteLine("1 – Refrigerators");
                        Console.WriteLine("2 – Vacuums");
                        Console.WriteLine("3 – Microwaves");
                        Console.WriteLine("4 – Dishwashers");
                        Console.WriteLine("Enter type of appliance:");
                        int applianceType;
                        if (!int.TryParse(Console.ReadLine(), out applianceType))
                        {
                            Console.WriteLine("Invalid appliance type.");
                            break;
                        }
                        store.DisplayAppliancesByType(applianceType);
                        break;
                    case 4:
                        Console.WriteLine("Enter the number of random appliances to display:");
                        int num = int.Parse(Console.ReadLine());
                        store.DisplayRandomAppliances(num);
                        break;
                    case 5:
                        store.PersistAppliances("updated_appliances.txt");
                        Console.WriteLine("Appliances saved. Exiting program.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a number between 1 and 5.");
                        break;
                }
            } while (option != 5);
        }
    }
}
