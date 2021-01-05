using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Notes.Data
{
    public class UsersNotesContext : DbContext
    {
        public UsersNotesContext(DbContextOptions options) : base(options)
        {
        }
        public UsersNotesContext()
        {
        }
    }
}
