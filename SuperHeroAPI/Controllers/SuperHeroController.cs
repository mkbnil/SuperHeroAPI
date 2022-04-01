using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public SuperHeroController(DataContext _context)
        {
            _dataContext = _context;
        }      
                
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>>Get()
        {            
            return Ok(await _dataContext.SuperHeroes.ToListAsync()); 
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>>AddHero(SuperHero hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();
             
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero req)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(req.Id);
            if (hero == null)
                return BadRequest("hero not found");
            hero.Name = req.Name;
            hero.FirstName = req.FirstName;
            hero.LastName = req.LastName;
            hero.Place = req.Place;
            _dataContext.SuperHeroes.Update(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>>RemoveHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("hero not found");
            _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());

        }

    }
    }
