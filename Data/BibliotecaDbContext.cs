using BibliotecaASPNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaASPNet.Data
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
        {
        }

        public DbSet<Autor> Autores => Set<Autor>();
        public DbSet<Livro> Livros => Set<Livro>();
        public DbSet<Emprestimo> Emprestimos => Set<Emprestimo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("Autores");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Nome)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(a => a.Nacionalidade)
                    .HasMaxLength(60)
                    .IsRequired();
                entity.Property(a => a.Email)
                    .HasMaxLength(150)
                    .IsRequired();
                entity.HasIndex(a => a.Email).IsUnique();
                entity.Property(a => a.DataNascimento)
                    .HasColumnType("date");
                entity.Property(a => a.Biografia)
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Livro>(entity =>
            {
                entity.ToTable("Livros");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Titulo)
                    .HasMaxLength(150)
                    .IsRequired();
                entity.Property(l => l.Isbn)
                    .HasMaxLength(20)
                    .IsRequired();
                entity.HasIndex(l => l.Isbn).IsUnique();
                entity.Property(l => l.Genero)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(l => l.Autor)
                    .WithMany(a => a.Livros)
                    .HasForeignKey(l => l.AutorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Emprestimo>(entity =>
            {
                entity.ToTable("Emprestimos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeLeitor)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(e => e.DataEmprestimo)
                    .HasColumnType("date");
                entity.Property(e => e.DataDevolucaoPrevista)
                    .HasColumnType("date");
                entity.Property(e => e.DataDevolucaoReal)
                    .HasColumnType("date");

                entity.HasOne(e => e.Livro)
                    .WithMany(l => l.Emprestimos)
                    .HasForeignKey(e => e.LivroId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
