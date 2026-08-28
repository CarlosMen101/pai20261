using EjemploMVVM.Commands;
using EjemploMVVM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EjemploMVVM.ViewModels
{
    public class LoginViewModel
    {
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;

        public ICommand LoginCommand { get; set; }

        // Se agrega '?' para corregir las advertencias CS8618
        public Action? OnLoginValido { get; set; }
        public Action<string>? OnLoginFallido { get; set; }

        private ILogin login;

        public LoginViewModel()
        {
            login = new LoginImpl();
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            bool esValido = login.ValidarUsuario(usuario, password);
            if (esValido)
            {
                OnLoginValido?.Invoke();
            }
            else
            {
                OnLoginFallido?.Invoke("Usuario o contraseña son inválidos");
            }
        }
    }
}