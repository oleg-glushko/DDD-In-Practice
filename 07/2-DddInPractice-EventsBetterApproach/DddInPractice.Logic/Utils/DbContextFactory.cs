﻿using Microsoft.EntityFrameworkCore;

namespace DddInPractice.Logic.Utils;

public static class DbContextFactory
{
    private static DbContextOptions<DddInPracticeDbContext>? _dbContextOptions;

    public static void Init(string connectionString)
    {
        _dbContextOptions = BuildDbContextOptionsFactory(connectionString);
    }

    public static DddInPracticeDbContext GetDbContext()
    {
        if (_dbContextOptions == null)
            throw new InvalidOperationException("Factory is not initialized, run the Init() method before.");
        return new DddInPracticeDbContext(_dbContextOptions);
    }

    private static DbContextOptions<DddInPracticeDbContext> BuildDbContextOptionsFactory(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DddInPracticeDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return optionsBuilder.Options;
    }
}
