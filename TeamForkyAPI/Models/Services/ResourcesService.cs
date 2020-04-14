﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamForkyAPI.Data;
using TeamForkyAPI.DTOs;
using TeamForkyAPI.Models.Interfaces;

namespace TeamForkyAPI.Models.Services
{
    public class ResourcesService : IResources
    {
        private HospitalDbContext _context { get; }

        public ResourcesService(HospitalDbContext context)
        {
            _context = context;
        }

        public ResourcesDTO ConvertToDTO(Resources resources)
        {
            ResourcesDTO rDTO = new ResourcesDTO()
            {
                ID = resources.ID,
                Name = resources.Name,
                Description = resources.Description,
                ResourcesType = resources.ResourcesType.ToString()
            };
        return rDTO;
        }

        public async Task CreateResources(Resources resources)
        {
            _context.Add(resources);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResourcesDTO>> GetAllResources()
        {
            List<Resources> resources = await _context.Resources.ToListAsync();
            List<ResourcesDTO> rDTO = new List<ResourcesDTO>();
            foreach (var resource in resources)
            {
                ResourcesDTO RDTO = ConvertToDTO(resource);
                rDTO.Add(RDTO);
            }
            return rDTO;
        }

        public async Task<ActionResult<ResourcesDTO>> GetResourcesByID(int ID)
        {
            var resources = await _context.Resources.FindAsync(ID);
            ResourcesDTO rDTO = ConvertToDTO(resources);
            return rDTO;
        }

        public async Task RemoveResources(int ID)
        {
            Resources resources = await _context.Resources.FindAsync(ID);

            _context.Resources.Remove(resources);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResources(int ID, Resources resources)
        {
            _context.Entry(resources).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
