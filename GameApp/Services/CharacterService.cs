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
    }
}
