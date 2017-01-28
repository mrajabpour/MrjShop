﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MRJ.DataLayer
{
    public static class ChangeTrackerExtenstions
    {
        public static string[] GetChangedEntityNames(this DbContext dbContext)
        {
            var typesList = new List<Type>();
            foreach (var type in dbContext.getChangedEntityTypes())
            {
                typesList.Add(type);
                typesList.AddRange(type.getBaseTypes().ToList());
            }

            var changedEntityNames = typesList
                .Select(type => System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(type).FullName)
                .Distinct()
                .ToArray();

            return changedEntityNames;
        }

        private static IEnumerable<Type> getBaseTypes(this Type type)
        {
            if (type.BaseType == null) return type.GetInterfaces();

            return Enumerable.Repeat(type.BaseType, 1)
                             .Concat(type.GetInterfaces())
                             .Concat(type.GetInterfaces().SelectMany(getBaseTypes))
                             .Concat(type.BaseType.getBaseTypes());
        }

        private static IEnumerable<Type> getChangedEntityTypes(this DbContext dbContext)
        {
            return dbContext.ChangeTracker.Entries().Where(
                            dbEntityEntry => dbEntityEntry.State == EntityState.Added ||
                            dbEntityEntry.State == EntityState.Modified ||
                            dbEntityEntry.State == EntityState.Deleted)
                .Select(dbEntityEntry => System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(dbEntityEntry.Entity.GetType()));
        }
    }
}
