﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shopway.Persistence.Constants;
using Shopway.Domain.Enumerations;
using static Shopway.Domain.Utilities.EnumUtilities;

namespace Shopway.Persistence.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permission, SchemaNames.Master);

        builder.HasKey(p => p.Id);

        builder.Property(r => r.Id)
            .HasColumnType(ColumnTypes.TinyInt);

        builder.Property(r => r.Name)
            .HasColumnType(ColumnTypes.VarChar(128));

        var permissionsFromEnumeration = Permission.GetNames();
        var permissionsFromEnum = GetNamesOf<Domain.Enums.Permission>();

        bool areEnumPermisionsEquivalentToEnumerationPermissions =
            permissionsFromEnumeration.SetEquals(permissionsFromEnum);

        if (areEnumPermisionsEquivalentToEnumerationPermissions is false)
        {
            throw new Exception($"{nameof(Permission)} enum values are not equivalent to {nameof(Permission)} enumeration values");
        }

        //Inserting static data (data that are not related to other)
        builder.HasData(Permission.List);
    }
}