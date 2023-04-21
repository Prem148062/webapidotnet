using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext _dbcontext){
            this.dbContext = _dbcontext;
        }
        //GET ALL
        [HttpGet]
        public async Task<IActionResult> GetContacts(){
            return Ok(await dbContext.Contacts.ToListAsync());
        }
        //GET BY ID
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContactById ([FromRoute] Guid id){
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact == null){
                return NotFound();
            }
            return Ok(contact);
        }
        //CREATE 
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequests){
            var contact = new Contact(){
                Id = Guid.NewGuid(),
                Address = addContactRequests.Address,
                Email = addContactRequests.Email,
                Fullname = addContactRequests.Fullname,
                Phone = addContactRequests.Phone
            };
            await dbContext.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }
        //UPDATE
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactRequest updateContactRequest){
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null){
                contact.Fullname = updateContactRequest.Fullname;
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;

                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id){
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null){
               dbContext.Remove(contact);
               await dbContext.SaveChangesAsync();
               return Ok(contact);
            }
            return NotFound();
        }
    }
}