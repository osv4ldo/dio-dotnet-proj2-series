﻿using System;

namespace dotNet.Proj2Series{
    class Program{
        static SerieRepositorio repositorio = new SerieRepositorio();

		static string separador = "-------------------------";
        static void Main(string[] args){

            string opcaoUsuario = LerOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X"){
				switch (opcaoUsuario){
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = LerOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void ExcluirSerie(){
			Console.WriteLine(separador);
			Console.WriteLine("Excluir série");
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			repositorio.Exclui(indiceSerie);
		}

        private static void VisualizarSerie(){
			Console.WriteLine(separador);
			Console.WriteLine("Visualizar série");
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie(){
			Console.WriteLine(separador);
			Console.WriteLine("Atualizar série");
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Genero))){
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o título da série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o ano de início da série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a descrição da série: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void ListarSeries(){
			Console.WriteLine(separador);
			Console.WriteLine("Listar séries");

			var lista = repositorio.Lista();

			if (lista.Count == 0){
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista){
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirSerie(){
			Console.WriteLine("Inserir nova série");

			foreach (int i in Enum.GetValues(typeof(Genero))){
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int leGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o título da série: ");
			string leTitulo = Console.ReadLine();

			Console.Write("Digite o ano de início da série: ");
			int leAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a descrição da série: ");
			string leDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)leGenero,
										titulo: leTitulo,
										ano: leAno,
										descricao: leDescricao);

			repositorio.Insere(novaSerie);
		}

        private static string LerOpcaoUsuario(){
			Console.WriteLine(separador);
			Console.WriteLine("Bem-vindo(a) à DIO Séries!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
