namespace PetFamily.Infrastructure.UnitTests
{
    //System.Threading.
    public class UnitTest1
    {
        public async Task Test1()
        {
            var count = 20;
            var elements = new List<Element>();
            for(int i = 0; i < count; i++)
            {
                elements.Add(new Element(i));
            }
            var semafore = new System.Threading.SemaphoreSlim(3);
            var taskList = new List<Task>();
            foreach(var element in elements)
            {
                await semafore.WaitAsync();

                Console.WriteLine("Add new element " + element.Name);
                var task = Task.Run(() =>
                {
                    try
                    {
                        element.Execute();
                    }
                    finally
                    {
                        semafore.Release();
                        Console.WriteLine(" semafore.Release() " + element.Name);
                    }
                });
                taskList.Add(task);
            }

            Console.WriteLine("before WhenAll");
            await Task.WhenAll(taskList);
            Console.WriteLine("after WhenAll");
        }

        public class Element
        {
            public Element(int i)
            {
                Name = i.ToString();
            }
            public string Name { get; set; }

            public async void Execute()
            {
                await Task.Delay(3000);
                Console.WriteLine($"Long Task {Name} completed");
            }
        }
    }
}