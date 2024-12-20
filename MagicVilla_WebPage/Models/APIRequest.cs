using Microsoft.AspNetCore.Mvc;
using static MagicVilla_utility.SD;

namespace MagicVilla_WebPage.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
