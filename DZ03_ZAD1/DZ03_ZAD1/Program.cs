using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ03_ZAD1
{
    class Program
    {

        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=TodoItemDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        static void Main(string[] args)
        {
            TodoDbContext context = new TodoDbContext(ConnectionString);
            TodoSqlRepository todoRepository = new TodoSqlRepository(context);

            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            TodoItem item1 = new TodoItem("item1", user1);
            TodoItem item2 = new TodoItem("item2", user1);
            TodoItem item3 = new TodoItem("item3", user2);
            TodoItem item4 = new TodoItem("item4", user2);
            todoRepository.Add(item1);
            todoRepository.Add(item2);
            todoRepository.Add(item3);
            todoRepository.Add(item4);

            Console.WriteLine("User1 items:");
            List<TodoItem> user1Items = todoRepository.GetAll(user1);
            user1Items.ForEach(i => Console.WriteLine(i.Text));
            Console.WriteLine();

            Console.WriteLine("User1 items after update item2-->updated_item2:");
            //update item2 -> updated_item2
            item2.Text = "updated_item2";
            todoRepository.Update(item2, user1);
            List<TodoItem> updatedUser1Items = todoRepository.GetAll(user1);
            updatedUser1Items.ForEach(i => Console.WriteLine(i.Text));
            Console.WriteLine();


            Console.WriteLine("User2 items:");
            List<TodoItem> user2Items = todoRepository.GetAll(user2);
            user2Items.ForEach(i => Console.WriteLine(i.Text));
            Console.WriteLine();

            //remove item3
            Console.WriteLine("User2 items after removing item3:");
            todoRepository.Remove(item3.Id, user2);
            user2Items = todoRepository.GetAll(user2);
            user2Items.ForEach(i => Console.WriteLine(i.Text));
            Console.WriteLine();

            Console.Write("Press any key to exit.");
            Console.ReadKey();




        }
    }
}
