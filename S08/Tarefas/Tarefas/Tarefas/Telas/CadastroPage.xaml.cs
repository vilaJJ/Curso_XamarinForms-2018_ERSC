﻿using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tarefas.Data;
using Tarefas.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarefas.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPage : ContentPage
    {
        private byte Prioridade { get; set; }

        public CadastroPage()
        {
            InitializeComponent();

            Prioridade = 4;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var stackLayoutClicado = sender as StackLayout;
            var stacksLayout = StackLayout_Prioridades.Children;

            foreach (var stackLayout in stacksLayout.Cast<StackLayout>())
            {
                var labelPrioridade = stackLayout.Children[1] as Label;

                if (stackLayoutClicado.Equals(stackLayout))
                {
                    var imagePrioridade = stackLayout.Children[0] as Image;

                    Prioridade = ObterValorPrioridade(imagePrioridade);
                    labelPrioridade.TextColor = Color.Black;
                    continue;
                }

                labelPrioridade.TextColor = Color.LightGray;
            }
        }

        private byte ObterValorPrioridade(Image imagePrioridade)
        {
            var source = imagePrioridade.Source as FileImageSource;
            var sourceFile = source.File;

            if (sourceFile.Contains("red"))
            {
                return 4;
            }
            else if (sourceFile.Contains("orange"))
            {
                return 3;
            }
            else if (sourceFile.Contains("yellow"))
            {
                return 2;
            }
            else if (sourceFile.Contains("green"))
            {
                return 1;
            }
            else
            {
                throw new InvalidOperationException("Prioridade desconhecida.");
            }
        }

        private void Button_AdicionarTarefa_Clicked(object sender, EventArgs e)
        {
            var nomeTarefa = Entry_NomeTarefa.Text?.Trim();
            var prioridade = Prioridade;

            if (string.IsNullOrEmpty(nomeTarefa) is true)
            {
                DisplayAlert(
                    "Erro ao adicionar tarefa",
                    "É necessário preencher o campo de Nome",
                    "Ok"
                    );

                return;
            }

            var tarefa = new Tarefa(nomeTarefa, prioridade);

            tarefa.Salvar();

            DisplayAlert("Tarefa salva.", "Tarefa salva com sucesso.", "Ok");
            Navigation.PopAsync();
        }
    }
}