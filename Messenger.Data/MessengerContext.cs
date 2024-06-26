﻿using Messenger.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Data
{
    public class MessengerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }

        public MessengerContext()
        {

        }

        public MessengerContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Messenger;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMessage>()
                .HasOne(um => um.Sender)
                .WithMany(u => u.UserMessagesAsSender)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserMessage>()
                .HasOne(um => um.Reciver)
                .WithMany(u => u.UserMessagesAsReciver)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GroupMessage>()
                .HasOne(gm => gm.Sender)
                .WithMany(g => g.GroupMessagesAsReciver)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GroupMessage>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.GroupMessagesAsAddressee)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
