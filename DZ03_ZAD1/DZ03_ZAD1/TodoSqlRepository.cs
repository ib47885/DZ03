using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ03_ZAD1
{
    class TodoSqlRepository : ITodoRepository
    {

        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {

            {
                TodoItem item = _context.TodoItem.Where(t => t.Id.Equals(todoId)).FirstOrDefault();
                if (item == null)
                {
                    return null;
                }
                else if (!item.UserId.Equals(userId))
                {
                    throw new TodoAccessDeniedException(String.Format("The user that is trying to fetch the data is not the owner of the requested Todo item."));
                }
                else
                {
                    return item;
                }
            }
        }

        public void Add(TodoItem todoItem)
        {

            {
                TodoItem item = _context.TodoItem.Where(t => t.Id.Equals(todoItem.Id)).FirstOrDefault();
                if (item == null)
                {
                    _context.TodoItem.Add(todoItem);
                    _context.SaveChanges();
                }
                else
                {
                    throw new DuplicateTodoItemException(String.Format("TodoItem you're trying to add already exists."));
                }
            }

        }

        public bool Remove(Guid todoId, Guid userId)
        {

            {
                TodoItem item = _context.TodoItem.Where(t => t.Id.Equals(todoId)).FirstOrDefault();
                if (item == null)
                {
                    return false;
                }
                else if (!item.UserId.Equals(userId))
                {
                    throw new TodoAccessDeniedException(String.Format("The user that is trying to remove the data is not the owner of the requested Todo item.", userId));
                }
                else
                {
                    Console.WriteLine("Removing requested TodoItem...");
                    _context.TodoItem.Remove(item);
                    _context.SaveChanges();
                    return true;
                }
            }
        }

        public void Update(TodoItem todoItem, Guid userId)
        {

            {
                TodoItem item = _context.TodoItem.Where(t => t.Id.Equals(todoItem.Id)).FirstOrDefault();
                if (item == null)
                {
                    _context.TodoItem.Add(todoItem);
                    _context.SaveChanges();
                }
                else if (!item.UserId.Equals(userId))
                {
                    throw new TodoAccessDeniedException(String.Format("The user that is trying to modified the data is not the owner of the requested Todo item.", userId));
                }
                else
                {
                    /*update properties of the item of _context to match the item recieved as an argument*/
                    /*the rest of the properties (id, userId and date created) should already match */
                    item.Text = todoItem.Text;
                    item.IsCompleted = todoItem.IsCompleted;
                    item.DateCompleted = todoItem.DateCompleted;

                    _context.Entry(todoItem).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
            }

        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {

            {
                TodoItem item = _context.TodoItem.Where(t => t.Id.Equals(todoId)).FirstOrDefault();
                if (item == null)
                {
                    return false;
                }
                else if (!item.UserId.Equals(userId))
                {
                    throw new TodoAccessDeniedException(String.Format("The user that is trying to modify the data is not the owner of the requested Todo item.", userId));
                }
                else
                {
                    item.DateCompleted = DateTime.Now;
                    item.IsCompleted = true;
                    _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
            }
        }

        public List<TodoItem> GetAll(Guid userId)
        {

            {
                List<TodoItem> items = _context.TodoItem.Where(t => t.UserId.Equals(userId)).OrderByDescending(t => t.DateCreated).ToList();
                return items;
            }
        }

        public List<TodoItem> GetActive(Guid userId)
        {

            {
                List<TodoItem> items = _context.TodoItem.Where(t => t.UserId.Equals(userId) && t.DateCompleted == null).ToList();
                return items;
            }

        }

        public List<TodoItem> GetCompleted(Guid userId)
        {

            {
                List<TodoItem> items = _context.TodoItem.Where(t => t.UserId.Equals(userId) && t.DateCompleted != null).ToList();
                return items;
            }
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {

            {
                List<TodoItem> items = _context.TodoItem.Where(t => t.UserId.Equals(userId) && filterFunction(t)).ToList();
                return items;
            }
        }




    }
}
