using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using BibliotecaFilmes.Modelos;

namespace BibliotecaFilmes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            //criar Dicionario (key->Festival, Value->Lista de filmes desse festival)

            // Criar lista de filmes
            List<Filme> filmes = new List<Filme>();
            string caminho = "../../../Ficheiros/";

            // Importar filmes txt
            ImportarFilmes(filmes, caminho);

            //Importar realizadores txt
            List<Realizador> realizadores = new List<Realizador>();
            ImportarRealizadores(realizadores, caminho, filmes);

            static void ImportarRealizadores(List<Realizador> realizadores, string caminho, List<Filme> filmes)
            {
                StreamReader F = null;
                string linha;
                string[] partes;
                string ficheiroimp = caminho + "realizador.txt";

                try
                {
                    F = new StreamReader(ficheiroimp);
                    while ((linha = F.ReadLine()) != null)
                    {
                        partes = linha.Split(";");
                        Realizador r = new Realizador();
                        List<Filme> lista = new List<Filme>();

                        r.Idrealizador = Convert.ToInt32(partes[0]);
                        r.Nome = (partes[1].Trim());
                        r.Pais = (partes[2].Trim());
                        foreach (Filme filme in filmes)
                        {
                            if (filme.Idrealizador == r.Idrealizador)
                            {
                                lista.Add(filme);
                            }
                        }
                        realizadores.Add(r);

                        //}
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.Write("Ficheiro não encontrado!");
                }
                catch (IOException erro) //excecoes
                {
                    Console.WriteLine("Erro de Input/Output" + erro);
                }
                catch (Exception erro) //excecoes - definidas da forma mais especifica para
                {
                    Console.Write("Erro generico" + erro);
                }
                finally //sempre executado
                {
                    if (F != null)
                        F.Close();
                    Console.WriteLine("Realizadores lidos com sucesso!");

                }
            }

            // Importar festivais txt
            List<Festival> festivais = new List<Festival>();
            ImportarFestivais(festivais, caminho);
            static void ImportarFestivais(List<Festival> festivais, string caminho)
            {
                StreamReader F = null;
                string linha;
                string[] partes;
                string ficheiroimp = caminho + "festivais.txt";

                try
                {
                    F = new StreamReader(ficheiroimp);
                    while ((linha = F.ReadLine()) != null)
                    {
                        partes = linha.Split(";");
                        Festival f = new Festival();
                        f.Idfestival = Convert.ToInt32(partes[0]);
                        f.Nome = (partes[1].Trim());
                        f.Local = (partes[2].Trim());
                        f.Datainicio = Convert.ToDateTime(partes[3].Trim());
                        f.Datafim = Convert.ToDateTime(partes[4].Trim());
                        f.Filmesinscritos = partes[5].Split(',').Select(g => g.Trim()).ToList();
                        festivais.Add(f);

                    }
                }
                catch (FileNotFoundException)
                {
                    Console.Write("Ficheiro não encontrado!");
                }
                catch (IOException erro) //excecoes
                {
                    Console.WriteLine("Erro de Input/Output" + erro);
                }
                catch (Exception erro) //excecoes - definidas da forma mais especifica para
                {
                    Console.Write("Erro generico" + erro);
                }
                finally //sempre executado
                {
                    if (F != null)
                        F.Close();
                    Console.WriteLine("Festivais lidos com sucesso!");

                }
            }

            //Lista generos admissiveis
            List<string> generosAdmissiveis = new List<string>();
            LerFicheiroGeneros(generosAdmissiveis, caminho + "generos.txt");
            static void LerFicheiroGeneros(List<string> generosAdmissiveis, string nomeF)
            {
                StreamReader F = null;
                string linha;
                try
                {

                    F = new StreamReader(nomeF);
                    while ((linha = F.ReadLine()) != null)
                    {
                        generosAdmissiveis.Add(linha);

                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Ficheiro não encontrado!");
                }
                catch (IOException e)
                {
                    Console.WriteLine("Erro de I/O: " + e.Message);
                }
                catch (Exception erro)
                {
                    Console.WriteLine("Erro geral de execução: " + erro.Message);
                }
                finally //sempre executado
                {
                    if (F != null)
                    {
                        F.Close();
                        Console.WriteLine("Ficheiro lido com sucesso!");
                    }
                }
            }

            //Lista paises admissiveis
            List<string> paisesAdmissiveis = new List<string>();
            string ficheiroP = caminho + "paises.txt";
            LerFicheiroPaises(paisesAdmissiveis, ficheiroP);

            int opcao;
            int opcaoF;
            int opcaoR;
            int opcaoFt;


            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                MenuPrincipal();
                opcao = Convert.ToInt32(Console.ReadLine());
                switch (opcao)
                {
                    case 0:
                        break;

                    case 1:
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            MenuFilme();
                            opcaoF = Convert.ToInt32(Console.ReadLine());
                            switch (opcaoF)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Console.WriteLine("-----------");
                                    MostrarFilmes(filmes, realizadores);
                                    break;
                                case 2:
                                    AdicionarFilmes(filmes, generosAdmissiveis, realizadores);
                                    break;
                                case 3:
                                    Console.WriteLine("Qual filme quer remover?");
                                    string nomeF = Console.ReadLine();
                                    RemoverFilmes(filmes, nomeF);
                                    break;
                                case 4:
                                    EditarFilme(filmes, generosAdmissiveis, realizadores);
                                    break;
                                case 5: ListarFilmesPorAno(filmes);
                                    break;
                                case 6:
                                    ListarFilmesPorGenero(filmes, generosAdmissiveis);
                                break;
                                case 7:
                                    ListarFilmesPorGeneroEPais(filmes, realizadores, generosAdmissiveis);
                                    break;
                                case 8:
                                    GuardarFilmesXML(filmes, "../../../Ficheiros/Filme.xml");
                                    break;
                                case 9:
                                    GuardarFilmesTxt(filmes, "../../../Ficheiros/FilmesGuardados.txt");
                                    break;
                                case 10:
                                    
                                    break;
                                default: Console.WriteLine("Opção Incorreta!"); break;
                            }

                        } while (opcaoF != 10);
                        break;

                    case 2:
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            MenuRealizador();
                            opcaoR = Convert.ToInt32(Console.ReadLine());
                            switch (opcaoR)
                            {
                                case 0:
                                    break;
                                case 1:
                                    MostrarRealizadores(realizadores);
                                    break;
                                case 2:
                                    AdicionarRealizador(realizadores, paisesAdmissiveis);
                                    break;
                                case 3:
                                    Console.WriteLine("Qual realizador quer remover?");
                                    string nomeR = Console.ReadLine();
                                    RemoverRealizador(realizadores, nomeR);
                                    break;
                                case 4:
                                    EditarRealizador(realizadores);

                                    break;
                                case 5:
                                    ListarRealizadoresPorPais(realizadores);
                                    break;
                                case 6:
                                    MostrarTop3Realizadores(filmes, realizadores);
                                    break;
                                case 7:
                                    MostrarRealizadoresEFilmes(realizadores, filmes);
                                    break;
                                case 8:
                                    GuardarRealizadoresComFilmesTxt(realizadores, filmes, "../../../Ficheiros/FilmesPorRealizador.txt");
                                    break;
                                case 9:
                                    break;
                                default: Console.WriteLine("Opção Incorreta!"); break;
                            }

                        } while (opcaoR != 9);
                        break;
                    case 3:
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            MenuFestival();
                            opcaoFt = Convert.ToInt32(Console.ReadLine());
                            switch (opcaoFt)
                            {
                                case 0:
                                    break;
                                case 1:
                                    MostrarFestivais(festivais);
                                    break;
                                case 2:
                                    AdicionarFestival(festivais, filmes);
                                    break;
                                case 3:
                                    
                                    RemoverFestival(festivais);
                                    break;
                                case 4:
                                    EditarFestival(festivais, filmes);
                                    break;
                                case 5:
                                    AdicionarFilmeAFestival(festivais, filmes, realizadores);
                                    break;
                                case 6:
                                    ListarFestivaisComFilmes(festivais, filmes);
                                    break;
                                case 7:
                                    ListarFestivaisFilmesPorPais(festivais, filmes, realizadores);
                                    break;
                                case 8:
                                    ListarFestivaisPorRealizadorEPeriodo(festivais, filmes, realizadores);
                                    break;
                                case 9:
                                    break;
                                default: Console.WriteLine("Opção Incorreta!"); break;
                            }

                        } while (opcaoFt != 9);
                        break;
                    case 4:
                        
                        break;
                    case 5:
                        break;
                    default: Console.WriteLine("Opção Incorreta!"); break;
                }

            } while (opcao != 0);
        }
        static void MenuPrincipal()
        {
            Console.WriteLine("\n**********  MENU **********");
            Console.WriteLine("0. TERMINAR PROGRAMA");
            Console.WriteLine("1. MENU FILMES");
            Console.WriteLine("2. MENU REALIZADORES");
            Console.WriteLine("3. MENU FESTIVAIS");
            Console.WriteLine("****************************");
        }
        static void MenuFilme()
        {
            Console.WriteLine("\n**********  GESTOR DE FILMES **********");
            Console.WriteLine("1. Mostrar");
            Console.WriteLine("2. Adicionar");
            Console.WriteLine("3. Remover");
            Console.WriteLine("4. Editar");
            Console.WriteLine("5. Procurar pelo ano");
            Console.WriteLine("6. Mostrar os filmes por género");
            Console.WriteLine("7. Mostrar filme de um determinado género e pais de realizador");
            Console.WriteLine("8. Guardar filmes em XML");
            Console.WriteLine("9. Guardar filmes em TXT");
            Console.WriteLine("10. Voltar");
            Console.WriteLine("****************************");
        }
        static void MenuRealizador()
        {
            Console.WriteLine("\n**********  GESTOR DE REALIZADORES **********");
            Console.WriteLine("1. Mostrar");
            Console.WriteLine("2. Adicionar");
            Console.WriteLine("3. Remover");
            Console.WriteLine("4. Editar");
            Console.WriteLine("5. Mostrar por país");
            Console.WriteLine("6. Top 3 com mais filmes");
            Console.WriteLine("7. Mostrar filmes de cada realizador");
            Console.WriteLine("8. Guardar filmes de cada realizador");
            Console.WriteLine("9. VOLTAR");
            Console.WriteLine("****************************");
        }
        static void MenuFestival()
        {
            Console.WriteLine("\n**********  GESTOR DE FESTIVAIS **********");
            Console.WriteLine("1. Mostrar");
            Console.WriteLine("2. Adicionar");
            Console.WriteLine("3. Remover");
            Console.WriteLine("4. Editar");
            Console.WriteLine("5. Adicionar filme num determinado festival");
            Console.WriteLine("6. Mostrar filmes de cada festival");
            Console.WriteLine("7. Mostrar por pais de realizador");
            Console.WriteLine("8. Mostrar por realizador e por data");
            Console.WriteLine("9. VOLTAR");
            Console.WriteLine("****************************");
        }
        static void ImportarFilmes(List<Filme> filmes, string caminho)
        {
            StreamReader F = null;
            string linha;
            string[] partes;
            string ficheiroimp = caminho + "filmes.txt";

            try
            {
                F = new StreamReader(ficheiroimp);
                while ((linha = F.ReadLine()) != null)
                {
                    partes = linha.Split(";");
                    Filme p = new Filme();
                    p.Idfilme = Convert.ToInt32(partes[0].Trim());
                    p.Titulo = (partes[1].Trim());
                    p.Ano = Convert.ToInt32(partes[2]);
                    p.Duracao = Convert.ToInt32(partes[3]);
                    p.Genero = partes[4].Split(",").Select(g => g.Trim()).ToList();
                    p.Idrealizador = Convert.ToInt32(partes[5]);
                    filmes.Add(p);

                }
            }
            catch (FileNotFoundException)
            {
                Console.Write("Ficheiro não encontrado!");
            }
            catch (IOException erro) //excecoes
            {
                Console.WriteLine("Erro de Input/Output" + erro);
            }
            catch (Exception erro) //excecoes - definidas da forma mais especifica para
            {
                Console.Write("Erro generico" + erro);
            }
            finally //sempre executado
            {
                if (F != null)
                    F.Close();
                Console.WriteLine("Filmes lidos com sucesso!");

            }
        }
        static void LerFicheiroPaises(List<string> paisesAdmissiveis, string nomeF)
        {
            StreamReader F = null;
            string linha;

            try
            {

                F = new StreamReader(nomeF);
                while ((linha = F.ReadLine()) != null)
                {
                    paisesAdmissiveis.Add(linha);

                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ficheiro não encontrado!");
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro de I/O: " + e.Message);
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro geral de execução: " + erro.Message);
            }
            finally //sempre executado
            {
                if (F != null)
                {
                    F.Close();
                    Console.WriteLine("Ficheiro lido com sucesso!");
                }
            }
        }
        static void MostrarFilmes(List<Filme> filmes, List<Realizador> realizadores)
        {
            Console.WriteLine("Lista de filmes:");
            foreach (Filme f in filmes)
            {
                Console.WriteLine(f.Idfilme);
                Console.WriteLine(f.Titulo);
                Console.WriteLine(f.Ano);
                Console.WriteLine(f.Duracao);
                Console.WriteLine(string.Join(", ", f.Genero)); //junta todos os géneros da lista com vírgulas
                string nomeRealizador = "Desconhecido";
                foreach (Realizador r in realizadores)
                {
                    if (r.Idrealizador == f.Idrealizador)
                    {
                        nomeRealizador = r.Nome;
                        break;
                    }
                }
                Console.WriteLine("Nome do realizador:" + nomeRealizador);
            }
            if (filmes.Count == 0)
            {
                Console.WriteLine("Não há filmes registados.");
                return;
            }
        }
        static void MostrarRealizadores(List<Realizador> realizadores)
        {
            Console.WriteLine("Lista de Realizadores:");
            foreach (Realizador r in realizadores)
            {
                Console.WriteLine(Convert.ToInt32(r.Idrealizador));
                Console.WriteLine(r.Nome);
                Console.WriteLine(r.Pais);
            }
            if (realizadores.Count == 0)
            {
                Console.WriteLine("Não há realizadores registados.");
                return;
            }
        }
        static void MostrarFestivais(List<Festival> festivais)
        {
            Console.WriteLine("Lista de festivais:");
            foreach (Festival f in festivais)
            {
                Console.WriteLine(f.Idfestival);
                Console.WriteLine(f.Nome);
                Console.WriteLine(f.Local);
                Console.WriteLine("Inicio: " + f.Datainicio.ToString("dd-MM-yyyy")); // .ToString para converter para 
                Console.WriteLine("Fim: " + f.Datafim.ToString("dd-MM-yyyy"));      // "dd-MM-yyyy" e nao aparecer 00:00
                Console.WriteLine(string.Join(", ", f.Filmesinscritos)); ;
            }
            if (festivais.Count == 0)
            {
                Console.WriteLine("Não há festivais registados.");
                return;
            }
        }
        static void AdicionarFilmes(List<Filme> filmes, List<string> generosAdmissiveis, List<Realizador> realizadores)
        {
            Console.WriteLine("------ ADICIONAR NOVO FILME ------");
            Console.WriteLine("Quantos filmes queres adicionar?");
            int numF = Convert.ToInt32(Console.ReadLine());
            bool existe = false;
            for (int i = 0; i < numF; i++)
            {
                Filme novoFilme = new Filme();

                do
                {
                    existe = false;
                    Console.WriteLine("Qual o nome do filme que quer adicionar?");
                    string nome = Console.ReadLine();
                    foreach (Filme f in filmes)
                    {
                        if (f.Titulo.Trim().ToUpper().Equals(nome.Trim().ToUpper()))
                        {
                            Console.WriteLine("Esse nome já existe");
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                        novoFilme.Titulo = nome;
                } while (existe == true);


                //id filme
                novoFilme.Idfilme = filmes.Count + 1;

                //ano do filme
                do
                {

                    Console.WriteLine("Qual o ano do filme? ");
                    novoFilme.Ano = Convert.ToInt32(Console.ReadLine());

                } while (novoFilme.Ano <= 1850);
                //duracao do filme
                do
                {
                    Console.WriteLine("Qual a duracao do filme em minutos? ");
                    novoFilme.Duracao = Convert.ToInt32(Console.ReadLine());

                } while (novoFilme.Duracao < 0);

                //generos do filme
                Console.WriteLine("Quantos generos tem o filme?");
                int numGeneros = Convert.ToInt32(Console.ReadLine());
                novoFilme.Genero = new List<string>();
                for (int j = 0; j < numGeneros; j++)
                {
                    string generoInserido;
                    bool valido;
                    do
                    {
                        Console.WriteLine("Insira o género desta lista:");
                        foreach (string genero in generosAdmissiveis)
                        {
                            Console.WriteLine(" -" + genero); // mostra a lista de generos admissiveis
                        }
                        generoInserido = Console.ReadLine();
                        valido = false;
                        foreach (string genero in generosAdmissiveis)
                        {
                            if (genero.ToUpper().Trim() == generoInserido.ToUpper())
                            {
                                novoFilme.Genero.Add(genero);
                                valido = true;
                                break;
                            }

                        }
                        if (!valido)
                            Console.WriteLine("Genero inválido! Tente novamente");
                    }
                    while (!valido);

                }
                //realizador 
                Console.WriteLine("Insira o nome do realizador: ");
                string nomeRealizador = Console.ReadLine();
                bool existeRealizador = false;
                foreach (Realizador r in realizadores)
                {
                    if (r.Nome.Trim().ToUpper() == nomeRealizador.ToUpper())
                    {
                        novoFilme.Idrealizador = r.Idrealizador;
                        existeRealizador = true;
                        break;
                    }
                }
                if (!existeRealizador)
                {
                    Realizador novoRealizador = new Realizador();
                    novoRealizador.Idrealizador = realizadores.Count + 1;
                    novoRealizador.Nome = nomeRealizador.ToUpper();

                    Console.WriteLine(" Qual o país deste realizador?");
                    novoRealizador.Pais = Console.ReadLine().Trim();
                    realizadores.Add(novoRealizador);
                    novoFilme.Idrealizador = novoRealizador.Idrealizador;
                }

                //Console.WriteLine("Nome do realizador:" + nomeRealizador);

                filmes.Add(novoFilme);
                GuardarFilmesTxt(filmes, "../../../Ficheiros/Filmes.txt");
                GuardarRealizadoresTxt(realizadores, "../../../Ficheiros/Realizador.txt");
            }
        }
        static void AdicionarRealizador(List<Realizador> realizadores, List<string> paisesAdmissiveis)
        {
            Console.WriteLine("Quantos realizadores queres adicionar?");
            int numR = Convert.ToInt32(Console.ReadLine());
            bool existe = false;
            for (int i = 0; i < numR; i++)
            {
                Realizador novoRealizador = new Realizador();

                do
                {
                    existe = false;
                    Console.WriteLine("Qual o nome do realizador que quer adicionar?");
                    string nomeR = Console.ReadLine();
                    foreach (Realizador r in realizadores)
                    {
                        if (r.Nome.Trim().ToUpper().Equals(nomeR.Trim().ToUpper()))
                        {
                            Console.WriteLine("Esse nome já existe");
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                        novoRealizador.Nome = nomeR;
                } while (existe == true);


                //id realizador
                novoRealizador.Idrealizador = realizadores.Count + 1;
                //pais realizador
                string paisInserido;
                bool valido;

                do
                {
                    Console.WriteLine("Insira o país de origem do realizador. Países válidos:");

                    foreach (string p in paisesAdmissiveis)
                    {
                        Console.WriteLine("- " + p);
                    }

                    paisInserido = Console.ReadLine();
                    valido = false;

                    foreach (string p in paisesAdmissiveis)
                    {
                        if (p.ToUpper() == paisInserido.Trim().ToUpper())
                        {
                            novoRealizador.Pais = p; // guarda o nome exatamente como está no ficheiro
                            valido = true;
                            break;
                        }
                    }

                    if (!valido)
                    {
                        Console.WriteLine("País inválido. Tente novamente.");
                    }

                } while (!valido);

                realizadores.Add(novoRealizador);
                GuardarRealizadoresTxt(realizadores, "../../../Ficheiros/Realizador.txt");
            }
        }
        static void AdicionarFestival(List<Festival> festivais, List<Filme> filmes)
        {
            Console.WriteLine("Quantos festivais queres adicionar?");
            int numFt = Convert.ToInt32(Console.ReadLine());
            bool existe = false;
            for (int i = 0; i < numFt; i++)
            {
                Festival novoFestival = new Festival();

                do
                {
                    existe = false;
                    Console.WriteLine("Qual o nome do festival que quer adicionar?");
                    string nome = Console.ReadLine();
                    foreach (Festival ft in festivais)
                    {
                        if (ft.Nome.Trim().ToUpper().Equals(nome.Trim().ToUpper()))
                        {
                            Console.WriteLine("Esse festival já existe");
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                        novoFestival.Nome = nome;
                } while (existe == true);


                //id festival
                novoFestival.Idfestival = festivais.Count + 1;

                //local festival
                Console.WriteLine("Qual o local do festival");
                novoFestival.Local = Console.ReadLine();

                Console.WriteLine("Qual a data de inicio do festival (dia-mes-ano)");
                novoFestival.Datainicio = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Qual a data de fim do festival (dia-mes-ano)");
                novoFestival.Datafim = Convert.ToDateTime(Console.ReadLine());

                //filmes inscritos
                Console.WriteLine("Quantos filmes tem o festival?");
                int numFilmes = Convert.ToInt32(Console.ReadLine());
                novoFestival.Filmesinscritos = new List<String>();
                for (int j = 0; j < numFilmes; j++)
                {
                    string filmeInserido;
                    bool valido;
                    do
                    {
                        Console.WriteLine("Escolhe o filme desta lista:");
                        foreach (Filme f in filmes)
                        {
                            Console.WriteLine(" -" + f.Titulo); // mostra a lista de filmes admissiveis
                        }
                        Console.Write("Insere o título exato do filme: ");
                        filmeInserido = Console.ReadLine();
                        valido = false;
                        foreach (Filme f in filmes)
                        {
                            if (f.Titulo.ToUpper().Trim() == filmeInserido.Trim().ToUpper())
                            {
                                if (novoFestival.Filmesinscritos.Contains(f.Titulo))
                                {
                                    Console.WriteLine("Filme já foi adicionado ao festival. Escolha outro.");
                                    valido = false;
                                    break;
                                }
                                else
                                { 
                                    novoFestival.Filmesinscritos.Add(f.Titulo);
                                    valido = true;
                                }
                                break;
                                
                            }
                            
                        }
                        if (!valido)
                            Console.WriteLine("Filme inválido! Tente novamente");
                    }
                    while (!valido);
                    
                }

                festivais.Add(novoFestival);
                GuardarFestivalTxt(festivais, "../../../Ficheiros/Festivais.txt");
            }
        }
        static void RemoverFilmes(List<Filme> filmes, string nomeF)
        {

            List<Filme> aux = new List<Filme>();
            foreach (Filme f in filmes)
            {
                if (f.Titulo.ToUpper().Trim() == nomeF.ToUpper().Trim())
                {
                    aux.Add(f);
                }
            }
            foreach (Filme f in aux)
            {
                filmes.Remove(f); //remover, da lista principal, o filme que estão em aux
            }
            GuardarFilmesTxt(filmes, "../../../Ficheiros/Filmes.txt");
        }
        static void RemoverRealizador(List<Realizador> realizadores, string nomeR)
        {

            List<Realizador> aux = new List<Realizador>();
            foreach (Realizador r in realizadores)
            {
                if (r.Nome.ToUpper().Trim() == nomeR.ToUpper().Trim())
                {
                    aux.Add(r);
                }
            }
            foreach (Realizador r in aux)
            {
                realizadores.Remove(r); //remover, da lista principal, o filme que estão em aux
            }
                GuardarRealizadoresTxt(realizadores, "../../../Ficheiros/Realizador.txt");
        }
        static void RemoverFestival(List<Festival> festivais)
        {
            Console.WriteLine("Qual festival quer remover?");
            string nomeF = Console.ReadLine();
            List<Festival> aux = new List<Festival>();
            foreach (Festival f in festivais)
            {
                if (f.Nome.ToUpper().Trim() == nomeF.ToUpper().Trim())
                {
                    aux.Add(f);
                }
            }
            foreach (Festival f in aux)
            {
                festivais.Remove(f); //remover, da lista principal, o filme que estão em aux
            }
            GuardarFestivalTxt(festivais, "../../../Ficheiros/Festivais.txt");
        }
        static void GuardarFilmesTxt(List<Filme> filmes, string nomeF)
        {

            StreamWriter F = null;
            try
            {
                F = new StreamWriter(nomeF); //cria o objeto da classe StreamWriter
                foreach (Filme item in filmes)
                {
                    string generos = string.Join(",", item.Genero);
                    F.WriteLine(item.Idfilme + "; " + item.Titulo + "; " + item.Ano + "; " + item.Duracao + "; " + generos + "; " + item.Idrealizador); //escreve no ficheiro
                }

            }
            catch (FileNotFoundException)
            {
                Console.Write("Ficheiro não encontrado!");
            }
            catch (IOException erro) //excecoes
            {
                Console.WriteLine("Erro de Input/Output" + erro);
            }
            catch (Exception erro) //excecoes - definidas da forma mais especifica para
            {
                Console.Write("Erro generico" + erro);
            }
            finally //sempre executado
            {
                if (F != null)
                    F.Close();
                Console.WriteLine("Filmes gravados com sucesso!");

            }

        }
        static void GuardarRealizadoresTxt(List<Realizador> realizadores, string nomeF)
        {

            StreamWriter F = null;
            try
            {
                F = new StreamWriter(nomeF); //cria o objeto da classe StreamWriter
                foreach (Realizador item in realizadores)
                {
                    F.WriteLine(item.Idrealizador + "; " + item.Nome + "; " + item.Pais); //escreve no ficheiro
                }

            }
            catch (FileNotFoundException)
            {
                Console.Write("Ficheiro não encontrado!");
            }
            catch (IOException erro) //excecoes
            {
                Console.WriteLine("Erro de Input/Output" + erro);
            }
            catch (Exception erro) //excecoes - definidas da forma mais especifica para
            {
                Console.Write("Erro generico" + erro);
            }
            finally //sempre executado
            {
                if (F != null)
                    F.Close();
                Console.WriteLine("Realizadores gravados com sucesso!");

            }

        }
        static void GuardarFestivalTxt(List<Festival> festivais, string nomeF)
        {

            StreamWriter F = null;
            try
            {
                F = new StreamWriter(nomeF); //cria o objeto da classe StreamWriter
                foreach (Festival item in festivais)
                {
                    string filmesinscritos = string.Join(",", item.Filmesinscritos);
                    F.WriteLine(item.Idfestival + "; " + item.Nome + "; " + item.Local + "; " + item.Datainicio + "; " + item.Datafim + "; " + filmesinscritos); //escreve no ficheiro
                }

            }
            catch (FileNotFoundException)
            {
                Console.Write("Ficheiro não encontrado!");
            }
            catch (IOException erro) //excecoes
            {
                Console.WriteLine("Erro de Input/Output" + erro);
            }
            catch (Exception erro) //excecoes - definidas da forma mais especifica para
            {
                Console.Write("Erro generico" + erro);
            }
            finally //sempre executado
            {
                if (F != null)
                    F.Close();
                Console.WriteLine("Festivais gravados com sucesso!");

            }

        }
        static void EditarFilme(List<Filme> filmes, List<string> generosAdmissiveis, List<Realizador> realizadores)
        {
            Console.WriteLine(" Insira o título do filme que quer editar: ");
            string titulo = Console.ReadLine();

            Filme filme = null;
            foreach (Filme f in filmes)
            {
                if (f.Titulo.Trim().ToUpper() == titulo.Trim().ToUpper())
                {
                    filme = f;
                    break;
                }
            }

            if (filme == null)
            {
                Console.WriteLine("Filme não encontrado");
                return;
            }

            Console.WriteLine("Filme encontrado");
            Console.WriteLine(" Alterar título do filme (ENTER para manter): ");
            string novoTitulo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoTitulo)) //caso não esteja vazio vai assumir o novo titulo 
            {
                filme.Titulo = novoTitulo;
            }
            //ALTERAR ANO
            Console.WriteLine(" Alterar ano de lançamento do filme (ENTER para manter): ");
            string novoAno = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoAno)) //caso não esteja vazio vai assumir o novo ano 
            {
                filme.Ano = Convert.ToInt32(novoAno);
            }
            //ALTERAR DURACAO
            Console.WriteLine(" Alterar duração  em minutos do filme (ENTER para manter): ");
            string novaDuracao = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaDuracao)) //caso não esteja vazio vai assumir o novo ano 
            {
                filme.Duracao = Convert.ToInt32(novaDuracao);
            }

            //ALTERAR GENERO
            Console.WriteLine(" Alterar generos do filme? (sim ou nao");
            string resposta = Console.ReadLine().ToLower();
            if (resposta == "sim")
            {
                filme.Genero.Clear(); //Limpa os generos desse filme

                Console.WriteLine("Quantos géneros pretende adicionar?");
                int numGeneros = Convert.ToInt32(Console.ReadLine());
                for (int j = 0; j < numGeneros; j++)
                {
                    string generoInserido;
                    bool valido;
                    do
                    {
                        Console.WriteLine("Insira o género desta lista:");
                        foreach (string genero in generosAdmissiveis)
                        {
                            Console.WriteLine(" -" + genero); // mostra a lista de generos admissiveis
                        }
                        generoInserido = Console.ReadLine();
                        valido = false;
                        foreach (string genero in generosAdmissiveis)
                        {
                            if (genero.ToUpper().Trim() == generoInserido.ToUpper())
                            {
                                filme.Genero.Add(genero);
                                valido = true;
                                break;
                            }

                        }
                        if (!valido)
                            Console.WriteLine("Genero inválido! Tente novamente");
                    }
                    while (!valido);
                }

            }

            // REALIZADOR
            Console.WriteLine("Pretende alterar o realizador? (sim ou nao)");
            string alterarRealizador = Console.ReadLine().ToLower();
            if (alterarRealizador == "sim")
            {
                Console.WriteLine("Insira o nome do realizador:");
                string nomeRealizador = Console.ReadLine().Trim().ToUpper();
                bool existeRealizador = false;

                foreach (Realizador r in realizadores)
                {
                    if (r.Nome.Trim().ToUpper() == nomeRealizador.ToUpper())
                    {
                        filme.Idrealizador = r.Idrealizador;
                        existeRealizador = true;
                        break;
                    }
                }

                if (!existeRealizador)
                {
                    Realizador novoRealizador = new Realizador();
                    novoRealizador.Idrealizador = realizadores.Count + 1;
                    novoRealizador.Nome = nomeRealizador;

                    Console.WriteLine("Qual o país deste realizador?");
                    novoRealizador.Pais = Console.ReadLine().Trim();

                    realizadores.Add(novoRealizador);
                    filme.Idrealizador = novoRealizador.Idrealizador;

                    // Guardar no ficheiro
                    GuardarRealizadoresTxt(realizadores, "../../../Ficheiros/Realizadores.txt");
                }
            }

            Console.WriteLine("Filme atualizado com sucesso.");
            GuardarFilmesTxt(filmes, "../../../Ficheiros/Filmes.txt");
        }
        static void EditarRealizador(List<Realizador> realizadores)
        {
            Console.WriteLine(" Insira o realizador que quer editar: ");
            string nome = Console.ReadLine();

            Realizador realizador = null;
            foreach (Realizador r in realizadores)
            {
                if (r.Nome.Trim().ToUpper() == nome.Trim().ToUpper())
                {
                    realizador = r;
                    break;
                }
            }

            if (realizador == null)
            {
                Console.WriteLine("Realizador não encontrado");
                return;
            }

            Console.WriteLine("Realizador encontrado");
            Console.WriteLine(" Alterar nome do realizador (ENTER para manter): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome)) //caso não esteja vazio vai assumir o novo nome
            {
                realizador.Nome = novoNome;
            }
            Console.WriteLine(" Alterar país do realizador (ENTER para manter): ");
            string novoPais = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoPais)) //caso não esteja vazio vai assumir o novo pais
            {
                realizador.Pais = novoPais;
            }


            Console.WriteLine("Realizador atualizado com sucesso.");
            GuardarRealizadoresTxt(realizadores, "../../../Ficheiros/Realizador.txt");
        }
        static void EditarFestival(List<Festival> festivais, List<Filme> filmes)
        {
            Console.WriteLine("Insira o nome do festival a editar:");
            string nome = Console.ReadLine();
            Festival festival = null;

            foreach (Festival f in festivais)
            {
                if (f.Nome.Trim().ToUpper() == nome.Trim().ToUpper())
                {
                    festival = f;
                    break;
                }
            }

            if (festival == null)
            {
                Console.WriteLine("Festival não encontrado.");
                return;
            }

            Console.WriteLine("Alterar nome (ENTER para manter):");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome)) 
                festival.Nome = novoNome;

            Console.WriteLine("Alterar local (ENTER para manter):");
            string novoLocal = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoLocal)) 
                festival.Local = novoLocal;

            Console.WriteLine("Alterar data de início (ENTER para manter):");
            string novaDataInicio = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaDataInicio))
                festival.Datainicio = Convert.ToDateTime(novaDataInicio);

            Console.WriteLine("Alterar data de fim (ENTER para manter):");
            string novaDataFim = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaDataFim))
                festival.Datafim = Convert.ToDateTime(novaDataFim);

            Console.WriteLine("Deseja alterar os filmes inscritos? (sim/nao)");
            string resposta = Console.ReadLine().ToLower();
            if (resposta == "sim")
            {
                festival.Filmesinscritos.Clear();
                Console.WriteLine("Quantos filmes deseja inscrever?");
                int num = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < num; i++)
                {
                    Console.WriteLine("Título do filme:");
                    string titulo = Console.ReadLine();
                    bool existe = false;
                    foreach (Filme f in filmes)
                    {
                        if (f.Titulo.Trim().ToUpper() == titulo.Trim().ToUpper())
                        {
                            festival.Filmesinscritos.Add(f.Titulo);
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        Console.WriteLine("Filme não encontrado. Tente novamente.");
                        i = i - 1;  // Repetir a mesma posição — como se fosse uma "segunda tentativa" para preencher a mesma entrada
                    }
                }
            }

            Console.WriteLine("Festival atualizado com sucesso.");
            GuardarFestivalTxt(festivais, "../../../Ficheiros/Festivais.txt");
        }
        static void GuardarFilmesXML(List<Filme> filmes, string caminho)
        {
            StreamWriter F = null;
            try
            {
                F = new StreamWriter(caminho);
                F.WriteLine("<ListaFilmes>");
                foreach (Filme filme in filmes)
                {
                    F.WriteLine("<Filme>");
                    F.WriteLine("<Nome>" + filme.Titulo + "</Nome>");
                    F.WriteLine("<Ano>" + filme.Ano + "</Ano>");
                    F.WriteLine("<Duração>" + filme.Duracao + "</Duração>");
                    F.WriteLine("</Filme>");
                }
                F.WriteLine("</ListaFilmes>");
            }
            catch (FileNotFoundException)
            {
                Console.Write("Ficheiro não encontrado!");
            }
            catch (IOException e)
            {
                Console.Write("Erro de I/O: " + e);
            }

            catch (Exception e)
            {
                Console.Write("Erro genérico " + e);
            }
            finally
            {
                if (F != null)
                {
                    F.Close();
                    Console.Write("Ficheiro gravado com sucesso!");
                }
            }

        }
        static void MostrarRealizadoresEFilmes(List<Realizador> realizadores, List<Filme> filmes)
        {
            foreach (Realizador r in realizadores)
            {
                Console.WriteLine(r.Nome + ":");
                bool temfilme = false;
                foreach (Filme f in filmes)
                {
                    if (f.Idrealizador == r.Idrealizador)
                        Console.WriteLine(" - " + f.Titulo);
                        temfilme = true;
                }
                if (!temfilme)
                {
                    Console.WriteLine("----Sem filmes associados----");
                }
            }
        }
        static int LerAnoValido()
        {
            int ano = 0;
            bool valido = false;

            while (valido == false)
            {
                Console.Write("Ano: ");
                string n = Console.ReadLine();
                try
                {
                    ano = Convert.ToInt32(n);
                    if (ano >= 1850)
                    {
                        valido = true;
                    }
                    else
                    {
                        Console.WriteLine("Ano deve ser maior ou igual a 1850.");
                    }
                }
                catch
                {
                    Console.WriteLine("Por favor, insira um número válido.");
                }
            }

            return ano;
        }
        static void ListarFilmesPorAno(List<Filme> filmes)
        {
            Console.WriteLine("----- FILMES POR ANO -----");

            int anoProcurado = LerAnoValido();

            bool encontrou = false;
            Console.WriteLine("Filmes desse ano: ");

            foreach (Filme f in filmes)
            {
                if (f.Ano == anoProcurado)
                {
                    Console.WriteLine(" - " + f.Titulo);
                    encontrou = true;
                }
            }

            if (encontrou == false)
            {
                Console.WriteLine("Nenhum filme encontrado para esse ano.");
            }
        }
        static void ListarFilmesPorGenero(List<Filme> filmes, List<string> generosAdmissiveis)
        {
            string generoProcurado;
            bool generoValido = false;
            do
            {
                Console.WriteLine("Escholhe um dos géneros:");
                foreach (string g in generosAdmissiveis)
                {
                    Console.WriteLine(" - " + g);
                }

                generoProcurado = Console.ReadLine().Trim().ToUpper();

                foreach (string g in generosAdmissiveis)
                {
                    if (g.Trim().ToUpper() == generoProcurado.Trim().ToUpper())
                    {
                        generoValido = true;
                        break;
                    }
                }

                if (generoValido == false)
                {
                    Console.WriteLine("Género inválido. Tenta novamente.");
                }

            } while (generoValido == false);

            bool encontrou = false;
            Console.WriteLine("Filmes de " + generoProcurado + ":");

            foreach (Filme f in filmes)
            {
                foreach (string g in f.Genero)
                {
                    if (g.Trim().ToUpper() == generoProcurado.Trim().ToUpper())
                    {
                        Console.WriteLine(" - " + f.Titulo);
                        encontrou = true;
                        break;
                    }
                }
            }

            if (encontrou == false)
            {
                Console.WriteLine("Nenhum filme encontrado com esse género.");
            }
        }
        static void ListarRealizadoresPorPais(List<Realizador> realizadores)
        {
            Console.Write("País: ");
            string paisProcurado = Console.ReadLine().Trim().ToUpper();

            bool encontrou = false;
            Console.WriteLine("Realizadores do país " + paisProcurado);

            foreach (Realizador r in realizadores)
            {
                if (r.Pais.Trim().ToUpper() == paisProcurado)
                {
                    Console.WriteLine(" - " + r.Nome);
                    encontrou = true;
                }
            }

            if (encontrou == false)
            {
                Console.WriteLine("Nenhum realizador encontrado para esse país.");
            }
        }
        static void ListarFilmesPorGeneroEPais(List<Filme> filmes, List<Realizador> realizadores, List<string> generosAdmissiveis)
        {
            // Ler género
            string generoProcurado;
            bool generoValido = false;

            do
            {
                Console.WriteLine("Escolhe um género da lista:");
                foreach (string g in generosAdmissiveis)
                {
                    Console.WriteLine(" - " + g);
                }

                generoProcurado = Console.ReadLine().Trim().ToUpper();

                foreach (string g in generosAdmissiveis)
                {
                    if (g.Trim().ToUpper() == generoProcurado.Trim().ToUpper())
                    {
                        generoValido = true;
                        break;
                    }
                }

                if (generoValido == false)
                {
                    Console.WriteLine("Género inválido. Tenta novamente.");
                }

            } while (generoValido == false);

            // Ler país
            Console.Write("Insira o país do realizador: ");
            string paisProcurado = Console.ReadLine().Trim().ToUpper();

            Console.WriteLine("Filmes do género " + generoProcurado + " e realizador do país: " + paisProcurado);

            bool encontrou = false;

            foreach (Filme f in filmes)
            {
                // Verifica se o género está na lista de géneros do filme
                bool temGenero = false;
                foreach (string g in f.Genero)
                {
                    if (g.Trim().ToUpper() == generoProcurado.Trim().ToUpper())
                    {
                        temGenero = true;
                        break;
                    }
                }

                if (temGenero == true)
                {
                    // Encontrar o realizador do filme
                    foreach (Realizador r in realizadores)
                    {
                        if (r.Idrealizador == f.Idrealizador && r.Pais.Trim().ToUpper() == paisProcurado.Trim().ToUpper())
                        {
                            Console.WriteLine(" -" + f.Titulo + ", " + r.Nome);
                            encontrou = true;
                            break;
                        }
                    }
                }
            }

            if (encontrou == false)
            {
                Console.WriteLine("Nenhum filme encontrado com esses critérios.");
            }
        }
        static void MostrarTop3Realizadores(List<Filme> filmes, List<Realizador> realizadores)
        {
            Dictionary<int, int> contadorFilmes = new Dictionary<int, int>();

            // Contar quantos filmes tem cada realizador
            foreach (Filme f in filmes)
            {
                if (contadorFilmes.ContainsKey(f.Idrealizador) == true)
                {
                    contadorFilmes[f.Idrealizador] = contadorFilmes[f.Idrealizador] + 1;
                }
                else
                {
                    contadorFilmes[f.Idrealizador] = 1;
                }
            }

            // guardar as chaves e valores em listas para ordenar manualmente
            List<int> listaIDs = new List<int>();
            List<int> listaContagens = new List<int>();

            foreach (int id in contadorFilmes.Keys)
            {
                listaIDs.Add(id);
                listaContagens.Add(contadorFilmes[id]);
            }

            // Ordenar manualmente por número de filmes (ordem decrescente)
            for (int i = 0; i < listaContagens.Count - 1; i++)
            {
                for (int j = i + 1; j < listaContagens.Count; j++)
                {
                    if (listaContagens[j] > listaContagens[i])
                    {
                        // Troca contagem
                        int tempContagem = listaContagens[i];
                        listaContagens[i] = listaContagens[j];
                        listaContagens[j] = tempContagem;

                        // Troca o ID correspondente
                        int tempID = listaIDs[i];
                        listaIDs[i] = listaIDs[j];
                        listaIDs[j] = tempID;
                    }
                }
            }

            // Mostrar os 3 primeiros
            Console.WriteLine("Top 3 Realizadores com mais filmes:");
            int max = 3;
            if (listaIDs.Count < 3)
            {
                max = listaIDs.Count;
            }

            for (int i = 0; i < max; i++)
            {
                int id = listaIDs[i];
                int totalFilmes = listaContagens[i];
                string nome = "";

                // Procurar o nome do realizador pelo ID
                foreach (Realizador r in realizadores)
                {
                    if (r.Idrealizador == id)
                    {
                        nome = r.Nome;
                        break;
                    }
                }

                Console.WriteLine(" - " + nome + " (" + totalFilmes + " filmes)");
            }
        }
        static void ListarFestivaisComFilmes(List<Festival> festivais, List<Filme> filmes)
        {

            foreach (Festival f in festivais)
            {
                Console.WriteLine("Festival: " + f.Nome + " (" + f.Local + ")");
                
                if (f.Filmesinscritos.Count == 0)
                {
                    Console.WriteLine("  (Sem filmes inscritos)");
                   
                }
                else
                
                    foreach (string titulo in f.Filmesinscritos)
                    {
                        
                            Console.WriteLine(" - " + titulo);
                            
                        
                    }
                

                Console.WriteLine();
            }
        }
        static void ListarFestivaisFilmesPorPais(List<Festival> festivais, List<Filme> filmes, List<Realizador> realizadores)
        {
            foreach (Festival festival in festivais)
            {
                Console.WriteLine("Festival: " + festival.Nome + " (" + festival.Local + ") ");

                Dictionary<string, List<string>> filmesPorPais = new Dictionary<string, List<string>>();

                foreach (string tituloFilme in festival.Filmesinscritos)
                {
                    Filme filmeEncontrado = null;
                    foreach (Filme f in filmes)
                    {
                        if (f.Titulo.Trim().ToUpper() == tituloFilme.Trim().ToUpper())
                        {
                            filmeEncontrado = f;
                            break;
                        }
                    }

                    if (filmeEncontrado != null)
                    {
                        Realizador realizadorFilme = null;
                        foreach (Realizador r in realizadores)
                        {
                            if (r.Idrealizador == filmeEncontrado.Idrealizador)
                            {
                                realizadorFilme = r;
                                break;
                            }
                        }

                        string paisRealizador = "(Pais desconhecido)";
                        string nomeRealizador = "(Nome desconhecido)";

                        if (realizadorFilme != null)
                        {
                            if (realizadorFilme.Pais != null && realizadorFilme.Pais.Trim() != "")
                                paisRealizador = realizadorFilme.Pais;

                            if (realizadorFilme.Nome != null && realizadorFilme.Nome.Trim() != "")
                                nomeRealizador = realizadorFilme.Nome;
                        }

                        if (!filmesPorPais.ContainsKey(paisRealizador))
                            filmesPorPais[paisRealizador] = new List<string>();
                        

                        filmesPorPais[paisRealizador].Add(filmeEncontrado.Titulo + " - " + nomeRealizador);
                    }
                }

                foreach (KeyValuePair<string, List<string>> entrada in filmesPorPais)
                {
                    Console.WriteLine("País: " + entrada.Key);
                    foreach (string titulo in entrada.Value)
                    {
                        Console.WriteLine(" - " + titulo);
                    }
                }

                Console.WriteLine();
            }
        }
        static void ListarFestivaisPorRealizadorEPeriodo(List<Festival> festivais, List<Filme> filmes, List<Realizador> realizadores)
        {
            string nomeRealizador;
            Console.WriteLine("Lista de realizadores:");
            foreach (Realizador r in realizadores)
            {
                Console.WriteLine(" - " + r.Nome);
            }
            Console.WriteLine("Nome do Realizador:");
            nomeRealizador = Console.ReadLine().Trim().ToUpper();
            // Encontrar o realizador pelo nome
            Realizador realizadorSelecionado = null;
            foreach (Realizador r in realizadores)
            {
                if (r.Nome.ToUpper() == nomeRealizador.ToUpper())
                {
                    realizadorSelecionado = r;
                    break;
                }
            }

            if (realizadorSelecionado == null)
            {
                Console.WriteLine("Realizador não encontrado.");
                return;
            }

            Console.WriteLine("Data inicial (dd-MM-yyyy):");
            DateTime dataInicio = DateTime.ParseExact(Console.ReadLine().Trim(), "dd-MM-yyyy", null);

            Console.WriteLine("Data final (dd-MM-yyyy):");
            DateTime dataFim = DateTime.ParseExact(Console.ReadLine().Trim(), "dd-MM-yyyy", null);

            bool encontrouFestivais = false;

            foreach (Festival f in festivais)
            {
                // Verifica se o festival está no intervalo
                if (f.Datainicio >= dataInicio && f.Datafim <= dataFim)
                {
                    // Verifica se algum filme do festival é do realizador escolhido
                    foreach (string tituloFilme in f.Filmesinscritos)
                    {
                        // Encontra o filme na lista geral pelo título
                        Filme filmeEncontrado = null;
                        foreach (Filme fl in filmes)
                        {
                            if (fl.Titulo.ToUpper() == tituloFilme.ToUpper())
                            {
                                filmeEncontrado = fl;
                                break;
                            }
                        }

                        if (filmeEncontrado != null && filmeEncontrado.Idrealizador == realizadorSelecionado.Idrealizador)
                        {
                            Console.WriteLine("Festival: " + f.Nome + " (" + f.Local + ")");
                            foreach (string titulo in f.Filmesinscritos)
                            {
                                foreach (Filme fi in filmes)
                                {
                                    if (fi.Titulo.ToUpper() == titulo.ToUpper() && fi.Idrealizador == realizadorSelecionado.Idrealizador)
                                    {
                                        Console.WriteLine(" - " + fi.Titulo);
                                    }
                                }
                            }
                            encontrouFestivais = true;
                            break;
                        }
                    }
                }
            }

            if (encontrouFestivais == false)
            {
                Console.WriteLine("Nenhum festival encontrado com filmes deste realizador no período indicado.");
            }
        }
        static void GuardarRealizadoresComFilmesTxt(List<Realizador> realizadores, List<Filme> filmes, string nomeF)
        {
            StreamWriter F = null;

            try
            {
                F = new StreamWriter(nomeF);

                foreach (Realizador r in realizadores)
                {
                    F.WriteLine(r.Nome + ":");
                    bool temFilme = false;

                    foreach (Filme f in filmes)
                    {
                        if (f.Idrealizador == r.Idrealizador)
                        {
                            F.WriteLine(" - " + f.Titulo);
                            temFilme = true;
                        }
                    }

                    if (!temFilme)
                    {
                        F.WriteLine("------(Sem filmes associados)------");
                    }

                    F.WriteLine(); // linha em branco entre realizadores
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ficheiro não encontrado!");
            }
            catch (IOException erro)
            {
                Console.WriteLine("Erro de Input/Output: " + erro.Message);
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro genérico: " + erro.Message);
            }
            finally
            {
                if (F != null)
                    F.Close();

                Console.WriteLine("Realizadores e respetivos filmes gravados com sucesso!");
            }
        }
        static void AdicionarFilmeAFestival(List<Festival> festivais, List<Filme> filmes, List<Realizador> realizadores)
        {
            // Escolher festival
            Console.WriteLine("Festivais disponíveis:");
            foreach (Festival f in festivais)
            {
                Console.WriteLine(" - " + f.Nome);
            }

            Console.WriteLine("Insere o nome do festival onde queres adicionar o filme:");
            string nomeFestival = Console.ReadLine().Trim();

            Festival festivalSelecionado = null;
            foreach (Festival f in festivais)
            {
                if (f.Nome.Trim().ToUpper() == nomeFestival.ToUpper())
                {
                    festivalSelecionado = f;
                    break;
                }
            }

            if (festivalSelecionado == null)
            {
                Console.WriteLine("Festival não encontrado.");
                return;
            }

            // Escolher filme
            Console.WriteLine("Filmes disponíveis:");
            foreach (Filme f in filmes)
            {
                Console.WriteLine(" - " + f.Titulo);
            }

            Console.WriteLine("Insere o título do filme que queres adicionar:");
            string tituloFilme = Console.ReadLine().Trim();

            Filme filmeSelecionado = null;
            foreach (Filme f in filmes)
            {
                if (f.Titulo.Trim().ToUpper() == tituloFilme.ToUpper())
                {
                    filmeSelecionado = f;
                    break;
                }
            }

            if (filmeSelecionado == null)
            {
                Console.WriteLine("Filme não encontrado.");
                return;
            }

            //Verificar se o filme está inscrito noutro festival no mesmo período
            foreach (Festival f in festivais)
            {
                if (f.Filmesinscritos.Contains(filmeSelecionado.Titulo) &&
                    f.Idfestival != festivalSelecionado.Idfestival)
                {
                    // Verificar se há sobreposição de datas
                    if (!(festivalSelecionado.Datafim < f.Datainicio || festivalSelecionado.Datainicio > f.Datafim))
                    {
                        Console.WriteLine("Este filme já está inscrito noutro festival que ocorre no mesmo período.");
                        return;
                    }
                }
            }

            // Vereficar se não existem mais do que 2 filmes do mesmo realizador neste festival
            int contador = 0;
            foreach (string titulo in festivalSelecionado.Filmesinscritos)
            {
                foreach (Filme f in filmes)
                {
                    if (f.Titulo.ToUpper() == titulo.ToUpper() &&
                        f.Idrealizador == filmeSelecionado.Idrealizador)
                    {
                        contador++;
                    }
                }
            }

            if (contador >= 2)
            {
                Console.WriteLine("Este realizador já tem 2 filmes neste festival. Não é possível adicionar outro.");
                return;
            }

            // Adicionar o filme ao festival
            if (festivalSelecionado.Filmesinscritos.Contains(filmeSelecionado.Titulo))
            {
                Console.WriteLine("Este filme já está inscrito neste festival.");
            }
            else
            {
                festivalSelecionado.Filmesinscritos.Add(filmeSelecionado.Titulo);
                Console.WriteLine("Filme adicionado com sucesso!");
                GuardarFestivalTxt(festivais, "../../../Ficheiros/Festivais.txt");
            }
        }

    }

}


