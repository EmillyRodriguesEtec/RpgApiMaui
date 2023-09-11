using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService uService;
        public ICommand RegistrarCommand { get; set; }

        //As propriedades serão chamadas na View futuramente
        private string login = string.Empty;
        public string Login
        {
            get { return login; } 
            set 
            {
                login = value;
                OnPropertyChanged();
            }
        }

        private string senha = string.Empty;
        public string Senha
        { 
            get { return senha; }
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }

        //Métodos para registrar um usuário
        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = senha;

                Usuario uRegistrado = await uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado.Id != 0) 
                { 
                    string mensagem = $"Usuario Id {uRegistrado.Id} registrado com sucesso.";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    await Application.Current.MainPage
                        .Navigation.PopAsync();//Remove a página da pilha de visualização
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
