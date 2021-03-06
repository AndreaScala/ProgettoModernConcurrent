﻿using Microsoft.AspNetCore.Mvc;
using ServiceAPI.Dal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

//Definizione gestione chiamate REST

namespace ServiceAPI
{
    [Route("api")]
    public class ServiceApiController : Controller
    {
        static readonly object setupLock = new object();
        static readonly SemaphoreSlim parallelism = new SemaphoreSlim(2);
        //Per creare il DB e le tabelle
        [HttpGet("setup")]
        public IActionResult SetupDatabase()
        {
            lock (setupLock)
            {
                using (var context = new AppDbContext())
                {
                    // Create database
                    context.Database.EnsureCreated();
                }
                return Ok("database created");
            }
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new AppDbContext())
                {
                    return Ok(context.Users.ToList());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }
        //Ricerca Utente tramite password
        [HttpGet("user")]
        public async Task<IActionResult> GetUser([FromQuery]string psw)
        {
            using (var context = new AppDbContext())
            {
                return Ok(await context.Users.FirstOrDefaultAsync(x => x.Password == psw));
            }
        }
        //Inserimento utente
        [HttpPut("users")]
        public async Task<IActionResult> CreateUser([FromBody]User user)
        {
            using (var context = new AppDbContext())
            {
                context.Users.Add(user);

                await context.SaveChangesAsync();

                return Ok();
            }
        }
        //Modifica Utente
        [HttpPost("users")]
        public async Task<IActionResult> UpdateUser([FromBody]User user)
        {
            using (var context = new AppDbContext())
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
                return Ok();
            }
        }   

        //Cancellazione Utente
        [HttpDelete("users")]
        public async Task<IActionResult> DeleteUser([FromQuery]int id)
        {
            using (var context = new AppDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                }
                return Ok();


            }
        }

        [HttpGet("concerts")]
        public async Task<IActionResult> GetConcerts()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new AppDbContext())
                {
                    return Ok(context.Concerts.ToList());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }
        //Ricerca Concerto per ID
        [HttpGet("concert")]
        public async Task<IActionResult> GetConcert([FromQuery]int id)
        {
            using (var context = new AppDbContext())
            {
                return Ok(await context.Concerts.FirstOrDefaultAsync(x => x.Id == id));
            }
        }
        //Inserimento Concerto
        [HttpPut("concerts")]
        public async Task<IActionResult> CreateConcert([FromBody]Concert concert)
        {
            using (var context = new AppDbContext())
            {
                context.Concerts.Add(concert);

                await context.SaveChangesAsync();

                return Ok();
            }
        }
        //Modifica Concerto
        [HttpPost("concerts")]
        public async Task<IActionResult> UpdateConcert([FromBody]Concert concert)
        {
            using (var context = new AppDbContext())
            {
                context.Concerts.Update(concert);
                await context.SaveChangesAsync();
                return Ok();
            }
        }

        //Cancellazione Concerto
        [HttpDelete("concerts")]
        public async Task<IActionResult> DeleteConcert([FromQuery]int id)
        {
            using (var context = new AppDbContext())
            {
                var concert = await context.Concerts.FirstOrDefaultAsync(x => x.Id == id);
                if (concert != null)
                {
                    context.Concerts.Remove(concert);
                    await context.SaveChangesAsync();
                }
                return Ok();


            }
        }

        [HttpGet("associations")]
        public async Task<IActionResult> GetAssociations()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new AppDbContext())
                {
                    return Ok(context.Associations.ToList());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }
        //Ricerca tutte le Associazioni per UID (ID dell'utente corrispondente)
        [HttpGet("associationss")]
        public async Task<IActionResult> GetAssociationss([FromQuery] int uid)
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new AppDbContext())
                {
                    return Ok(context.Associations.Where(x => x.UId == uid).ToList());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }
        //Ricerca Associazione per ID
        [HttpGet("association")]
        public async Task<IActionResult> GetAssociation([FromQuery]int id)
        {
            using (var context = new AppDbContext())
            {
                return Ok(await context.Associations.FirstOrDefaultAsync(x => x.Id == id));
            }
        }
        //Inserimento Associazione (Prenotazione)
        [HttpPut("associations")]
        public async Task<IActionResult> CreateAssociation([FromBody]Association association)
        {
            using (var context = new AppDbContext())
            {
                context.Associations.Add(association);

                await context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost("associations")]
        public async Task<IActionResult> UpdateAssociation([FromBody]Association association)
        {
            using (var context = new AppDbContext())
            {
                context.Associations.Update(association);
                await context.SaveChangesAsync();
                return Ok();
            }
        }


        [HttpDelete("associations")]
        public async Task<IActionResult> DeleteAssociation([FromQuery]int id)
        {
            using (var context = new AppDbContext())
            {
                var association = await context.Associations.FirstOrDefaultAsync(x => x.Id == id);
                if (association != null)
                {
                    context.Associations.Remove(association);
                    await context.SaveChangesAsync();
                }
                return Ok();


            }
        }
    }
}