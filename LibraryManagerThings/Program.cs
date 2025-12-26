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
                    Console.WriteLine($"Livro emprestado para o usuario {t.Nome}");
                }
            }
            
        }else if(escolha == i)
        {
            int id = int.TryParse(Console.ReadLine());
            var buscaId = listaDeUsuarios.FirstOrDefault(i => i.Id == id);
            if(buscaId == null)
            {
                Console.WriteLine("Tivemos um erro ao buscar o ID do usuario mencionado.");
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
                    Console.WriteLine($"Livro emprestado para o usuario {t.Nome}");
                }
            }
        }
        
    }

    static void DevolverLivro()
    {
        Console.WriteLine("Qual nome do usuario que ira devolver o livro? ");
        string nome = Console.ReadLine();
        var buscaUser = listaDeUsuarios.FirstOrDefault(u => u.Nome == nome);
        if(buscaUser == null)
        {
            Console.WriteLine("Usuario nao encontrado na base de dados.");
            return;
        }
        Console.WriteLine("Qual nome do livro que deseja devolver? ");
        string nomeLivro = Console.ReadLine();
        var LivroDev = buscaUser.LivrosEmprestados.FirstOrDefault(ld => ld.Titulo == nomeLivro);
        if(LivroDev == null)
        {
            Console.WriteLine("Livro nao encontrado na base de dados.");
            return;
        }
        buscaUser.LivrosEmprestados.Remove(LivroDev);
        Console.WriteLine("Livro removido com sucesso!");
    }

}