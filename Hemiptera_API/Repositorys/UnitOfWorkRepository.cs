﻿using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;

namespace Hemiptera_API.Services
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly ApplicationDbContext _context;
        public IProjectRepository Project { get; }

        public UnitOfWorkRepository(ApplicationDbContext context,
            IProjectRepository projectService)
        {
            _context = context;
            Project = projectService;
        }

        private bool _disposed;

        public void Save() 
        {
            _context.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}