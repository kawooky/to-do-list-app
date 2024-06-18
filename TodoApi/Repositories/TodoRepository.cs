using TodoApi.Models;
using TodoApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApi.Repositories
{
    public class TodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
