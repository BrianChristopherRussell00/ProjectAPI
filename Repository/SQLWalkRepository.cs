﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjectAPI.Data;
using ProjectAPI.Models.Domain;

namespace ProjectAPI.Repository
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly BrianRussellDbContext dbContext;

        public SQLWalkRepository(BrianRussellDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
          var existingWalk =  await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk != null)
            {
                return null;
            }   
            dbContext.Walks.Remove(existingWalk);
    await dbContext.SaveChangesAsync(); 
            return existingWalk;
        
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk != null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync(); 
            return existingWalk;
        }
    }
}