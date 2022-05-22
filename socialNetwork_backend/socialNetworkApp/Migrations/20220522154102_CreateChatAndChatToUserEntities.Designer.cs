﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.db;

#nullable disable

namespace socialNetworkApp.Migrations
{
    [DbContext(typeof(BaseBdConnection))]
    [Migration("20220522154102_CreateChatAndChatToUserEntities")]
    partial class CreateChatAndChatToUserEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "all_mods_enum", new[] { "msg_reader", "msg_creator", "msg_updater", "msg_deleter", "chat_reader", "chat_creator", "chat_updater", "chat_deleter", "user_reader", "user_creator", "user_updater", "user_deleter", "post_reader", "post_creator", "post_updater", "post_deleter" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "chat_creator_type", new[] { "user", "group" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "chat_to_user_role", new[] { "user", "admin", "creator", "black_list" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "chat_type", new[] { "simple", "secret", "fantom" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("socialNetworkApp.api.controllers.chat.ChatDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("InvitationUrl")
                        .HasColumnType("text")
                        .HasColumnName("invitation_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Photo")
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.HasKey("Id");

                    b.ToTable("chats", "public");
                });

            modelBuilder.Entity("socialNetworkApp.api.controllers.chat.ChatToUserDb", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid")
                        .HasColumnName("chat_id");

                    b.HasKey("UserId", "ChatId");

                    b.ToTable("chats_to_users", "public");
                });

            modelBuilder.Entity("socialNetworkApp.api.controllers.users.UserDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hashed_password");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<List<AllModsEnum>>("Mods")
                        .IsRequired()
                        .HasColumnType("all_mods_enum[]")
                        .HasColumnName("mods");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.Property<string>("Username")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "Index__UniqueEmail")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "Index__UniqueUsername")
                        .IsUnique();

                    b.ToTable("users", "public");
                });
#pragma warning restore 612, 618
        }
    }
}
