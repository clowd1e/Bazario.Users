﻿using Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Emails;
using Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers;
using Bazario.Users.Domain.Users;
using Bazario.Users.Domain.Users.Bans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazario.Users.Infrastructure.Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.Id)
                .IsUnique();

            builder.HasIndex(user => user.Role);

            builder.Property(user => user.Id).HasConversion(
                 id => id.Value,
                 value => new UserId(value));

            builder.Property(user => user.FirstName)
                .HasMaxLength(FirstName.MaxLength)
                .HasConversion(
                    firstName => firstName.Value,
                    value => FirstName.Create(value).Value);

            builder.Property(user => user.LastName)
                .HasMaxLength(LastName.MaxLength)
                .HasConversion(
                    lastName => lastName.Value,
                    value => LastName.Create(value).Value);

            builder.Property(user => user.BirthDate)
                .HasConversion(
                    birthDate => birthDate.Value,
                    value => BirthDate.Create(value).Value);

            builder.Property(user => user.Email)
                .HasMaxLength(Email.MaxLength)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value).Value);

            // This value is just for DB as the length is checked in the Value Object's regex.
            var phoneNumberMaxLength = 50;

            builder.Property(user => user.PhoneNumber)
                .HasMaxLength(phoneNumberMaxLength)
                .HasConversion(
                    phoneNumber => phoneNumber.Value,
                    value => PhoneNumber.Create(value).Value);

            builder.OwnsOne(user => user.BanDetails, banDetails =>
            {
                banDetails.Property(b => b.Reason)
                    .HasMaxLength(BanDetails.MaxReasonLength)
                    .IsRequired();

                banDetails.Property(b => b.BlockedAt)
                    .IsRequired();

                banDetails.Property(b => b.ExpiresAt)
                    .IsRequired(false);
            });
        }
    }
}
