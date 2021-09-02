﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurante.Infra.Common.Persistence;

namespace Restaurante.Infra.Migrations
{
    [DbContext(typeof(RestauranteDbContext))]
    [Migration("20210902001041_AddingBank")]
    partial class AddingBank
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Restaurante.Domain.Users.Entregadores.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BankId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("RG")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AddressId");

                    b.ToTable("Funcionarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Funcionario");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FuncionarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Entregadores.Models.Entregador", b =>
                {
                    b.HasBaseType("Restaurante.Domain.Users.Funcionarios.Models.Funcionario");

                    b.Property<int?>("MotoId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("MotoId");

                    b.HasDiscriminator().HasValue("Entregador");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Account", b =>
                {
                    b.HasOne("Restaurante.Domain.Users.Funcionarios.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId");

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Funcionario", b =>
                {
                    b.HasOne("Restaurante.Domain.Users.Funcionarios.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("Restaurante.Domain.Users.Funcionarios.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Account");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Phone", b =>
                {
                    b.HasOne("Restaurante.Domain.Users.Funcionarios.Models.Funcionario", null)
                        .WithMany("Telefones")
                        .HasForeignKey("FuncionarioId");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Entregadores.Models.Entregador", b =>
                {
                    b.HasOne("Restaurante.Domain.Users.Entregadores.Models.Veiculo", "Moto")
                        .WithMany()
                        .HasForeignKey("MotoId");

                    b.Navigation("Moto");
                });

            modelBuilder.Entity("Restaurante.Domain.Users.Funcionarios.Models.Funcionario", b =>
                {
                    b.Navigation("Telefones");
                });
#pragma warning restore 612, 618
        }
    }
}
