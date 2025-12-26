using System;
using System.Collections.Generic;
using System.Data.Common;

public class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public bool EstaEmprestado { get; set; }
    public Livro(string titulo, string autor, string isbn)
    {
        Titulo = titulo;
        Autor = autor;
        ISBN = isbn;
        EstaEmprestado = false; 
    }
}

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public List<Livro> LivrosEmprestados { get; set; }

    public Usuario(string nome)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        LivrosEmprestados = new List<Livro>();
    }
}

class Program
{
    static List<Livro> listaDeLivros = new List<Livro>();
    static List<Usuario> listaDeUsuarios = new List<Usuario>();
    static void Main()
    {
    }

    static void AddLivro()
    {
        Console.WriteLine("Qual o titulo do livro? ");
        string titulo = Console.ReadLine();
        Console.WriteLine("Qual o nome do autor do livro? ");
        string autor = Console.ReadLine();
        Console.WriteLine("Qual o ISBN do livro? ");
        string ISBN = Console.ReadLine();
        var novoLivro = new Livro(titulo, autor, ISBN);
        listaDeLivros.Add(novoLivro);
        Console.WriteLine("Livro Adicionado!");
    }

    static void CriaUsuario()
    {
        Console.WriteLine("Nome do usuario: ");
        string nomeUser = Console.ReadLine();
    }

    static void EmprestarLivro()
    {
        Console.WriteLine("Qual voce deseja informar, o nome do usuario ou o ID do usuario?  n -> nome, i -> iD ");
        string escolha = Console.ReadLine();
        if(escolha == "n")
        {    
            Console.WriteLine("Qual o nome do usuario? ");
            string nome = Console.ReadLine();
            var buscaIdentificar = listaDeUsuarios.FirstOrDefault(t => t.Nome == nome);
            if(buscaIdentificar == null)
            {
                Console.WriteLine("Problema ao encontrar usuario.");
                return;
            }else
            {
                Console.WriteLine("Qual nome do livro que voce deseja? ");
                string livro = Console.ReadLine();
                var buscaLivro = listaDeLivros.FirstOrDefault(l => l.Titulo == livro);
                if(buscaLivro == null)
                {
                    Console.WriteLine("Problema ao encontrar livro mencionado.");
                    return;
                }
                bool jaEmprestado = buscaIdentificar.LivrosEmprestados
                    .Exists(l => l.Titulo == buscaLivro.Titulo);
                if(jaEmprestado == true)
                {
                    Console.WriteLine("Esse livro ja foi emprestado para o usuario");
                    return;
                }
                else
                {
                    buscaIdentificar.LivrosEmprestados.Add(buscaLivro);
                    Console.WriteLine($"Livro emprestado para o usuario {nome}");
                }
            }
            
        }else if(escolha == i)
        {
            int.TryParse(Console.ReadLine(), out int id);
        }
        
    }
}