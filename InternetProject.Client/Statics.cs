using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetProject.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Browser.Interop;

namespace InternetProject.Client
{
    public enum ToastType
    {
        Warning,
        Sucess,
        Error
    }
    public enum ModalType
    {
        Block,
        None
    }
    public delegate Task Scroll();
    public static class Statics
    {
        public static bool IsAdmin;
        public static Guid Token;
        public static bool IsLogin = false;
        public static string LoginName = string.Empty;
        public static event Scroll OnScroll;
        public static void Toast(string Message, ToastType type)
        {
            switch (type)
            {
                case ToastType.Warning:
                    RegisteredFunction.Invoke<bool>("Toast", Message, "warning");
                    break;
                case ToastType.Sucess:
                    RegisteredFunction.Invoke<bool>("Toast", Message, "success");
                    break;
                case ToastType.Error:
                    RegisteredFunction.Invoke<bool>("Toast", Message, "error");
                    break;
                default:
                    break;
            }
        }
        public static void Modal(string id, ModalType type)
        {
            switch (type)
            {
                case ModalType.Block:
                    RegisteredFunction.Invoke<string>("ModalDisplay", id, "block");
                    break;
                case ModalType.None:
                    RegisteredFunction.Invoke<string>("ModalDisplay", id, "none");
                    break;
                default:
                    break;
            }
        }
        const string CookieName = "register";
        public static void SetCookie(string data) =>
            RegisteredFunction.Invoke<string>("setCookie", CookieName, data, 3600);
        public static void ClearCookie() =>
            RegisteredFunction.Invoke<string>("setCookie", CookieName, "", 1);
        public static string GetCookie() =>
            RegisteredFunction.Invoke<string>("getCookie", CookieName);
        public static string ToBase64(Guid G) =>
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(G.ToString()));
        public static async Task<string> DownloadImageAsync(this System.Net.Http.HttpClient http, string name) =>
            await http.GetJsonAsync<string>("api/Car/Download/" + name);
        //public static string DownloadImageAsync(this System.Net.Http.HttpClient http, string name)
        //{
        //    var t = DownloadImage(http, name);
        //    t.Start();
        //    t.Wait();
        //    return t.Result;
        //}
        private static IList<CarBrand> Brands;
        public static async Task<IList<CarBrand>> GetBrands(this System.Net.Http.HttpClient http)
        {
            if (Brands == null) Brands = await http.GetJsonAsync<IList<CarBrand>>("api/Car/Brands");
            return Brands;
        }
        public static void Scroll()
        {
            OnScroll?.Invoke();
        }
    }
}
