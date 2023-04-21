using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Data
{
    public class ContactsAPIDbContext:DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options):base(options){
            Contacts = Set<Contact>();
        }
        public DbSet<Contact> Contacts {get; set;}
    }
}