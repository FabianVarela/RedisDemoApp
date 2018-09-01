namespace RedisConsole
{
    using RedisManager.Class;
    using RedisManager.Entity;
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var data = new CacheManager<List<Person>>();

                if (data.Exists("people"))
                {
                    Console.WriteLine("Loading data, please wait... \n");

                    var people = data.GetValue("people");

                    foreach (var item in people)
                        Console.WriteLine($"Document Id: {item.DocumentId}; Name: {item.Name}; Last Name: {item.LastName}; Address: {item.Address} \n");

                    Console.WriteLine("Data loaded successfully");
                }
                else
                {
                    Console.WriteLine("Writing data, please wait... \n");

                    var people = new List<Person>
                    {
                        new Person
                        {
                            DocumentId = "1234567890",
                            Name = "Jhon",
                            LastName = "Doe",
                            Address = "742 Evergreen Terrace"
                        },
                        new Person
                        {
                            DocumentId = "9876543210",
                            Name = "Contoso",
                            LastName = "Contoso",
                            Address = "123 Fake Street"
                        }
                    };

                    data.SetValue("people", people);

                    Console.WriteLine("Data written successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
