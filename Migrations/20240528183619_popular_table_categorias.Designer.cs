﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api_catalogo_curso.infra.data;

#nullable disable

namespace api_catalogo_curso.Migrations
{
    [DbContext(typeof(ConnectionContext))]
    [Migration("20240528183619_popular_table_categorias")]
    partial class popular_table_categorias
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("api_catalogo_curso.modules.categoria.models.entity.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagemUrl")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("imagem_url");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("api_catalogo_curso.modules.produto.models.entity.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("integer")
                        .HasColumnName("categoria_id");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_cadastro");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasColumnName("descricao");

                    b.Property<float>("Estoque")
                        .HasColumnType("real")
                        .HasColumnName("estoque");

                    b.Property<string>("ImagemUrl")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasColumnName("imagem_url");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(80)")
                        .HasColumnName("nome");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("preco");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("produtos");
                });

            modelBuilder.Entity("api_catalogo_curso.modules.produto.models.entity.Produto", b =>
                {
                    b.HasOne("api_catalogo_curso.modules.categoria.models.entity.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("api_catalogo_curso.modules.categoria.models.entity.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
