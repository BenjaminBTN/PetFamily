using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetFamily.Infrastructure.Extensions;

public static class FluentApiExtensions
{
    public static PropertyBuilder<IReadOnlyList<TValueObject>> HasVOCollectionToJsonConversion<TValueObject, TDto>(
        this PropertyBuilder<IReadOnlyList<TValueObject>> builder,
        Func<TValueObject, TDto> convertToProviderFunc,
        Func<TDto, TValueObject> convertFromProviderFunc)
    {
        return builder
            .HasConversion(
                collection => JsonSerializer
                    .Serialize(collection.Select(convertToProviderFunc), JsonSerializerOptions.Default),

                json => JsonSerializer
                    .Deserialize<IEnumerable<TDto>>(json, JsonSerializerOptions.Default)!
                    .Select(convertFromProviderFunc).ToList(),

                new ValueComparer<IEnumerable<TValueObject>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()))

            .HasColumnType("jsonb");
    }
}
