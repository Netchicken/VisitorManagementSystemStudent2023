using Microsoft.EntityFrameworkCore;

using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Services
{
    public class DBCalls : IDBCalls
    {





        private ApplicationDbContext _context { get; }
        public DBCalls(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Visitors> ShowJohnEntriesQuery()
        {
            //          from[identifier] in [data source]
            //          let[expression]
            //          where[boolean expression]
            //          order by[[expression] (ascending/descending)], [optionally repeat]
            //          select[expression]
            //          group[expression] by[expression] into[expression]

            var query = from v in _context.Visitors
                        where v.FirstName == "John"
                        select v;

            var john = _context.Visitors.Where(v => v.FirstName == "John").ToListAsync();

            return query;
        }


        public async Task<IEnumerable<Visitors>> VisitorsLoggedInAsync()
        {
            var date = DateTime.Parse("1/1/0001");
            var visitors = await _context.Visitors.Where(v => v.DateOut == date).Include(q => q.StaffNames).ToListAsync();
            return visitors;
        }



    }
}
