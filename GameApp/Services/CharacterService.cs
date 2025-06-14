using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameApp.Data;
using GameApp.Models;
using Microsoft.EntityFrameworkCore;


namespace GameApp.Services
{
    public class CharacterService
    {
        private readonly AppDbContext _context;

        public CharacterService()
        {
            _context = new AppDbContext();
        }

        public async Task<List<Character>> GetCharactersAsync()
        {
            return await _context.Characters.Include(c => c.Class).ToListAsync();
        }

        public async Task AddCharacterAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCharacterAsync(Character character)
        {
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCharacterAsync(int characterId)
        {
            var character = await _context.Characters.FindAsync(characterId);
            if (character != null)
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
            }
        }

      internal async Task<List<Class>> GetClassesAsync()
{
    return await _context.Classes.ToListAsync();
}

internal async Task AddClassAsync(Class @class)
{
    _context.Classes.Add(@class);
    await _context.SaveChangesAsync();
}

internal async Task UpdateClassAsync(Class updatedClass)
{
    _context.Classes.Update(updatedClass);
    await _context.SaveChangesAsync();
}

internal async Task DeleteClassAsync(int id)
{
    var classToDelete = await _context.Classes.FindAsync(id);
    if (classToDelete != null)
    {
        _context.Classes.Remove(classToDelete);
        await _context.SaveChangesAsync();
    }
}
    }
}
