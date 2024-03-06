using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace WpfAPIHoadon.Models
{
    internal class CXulyHanghoa
    {
        private static HttpClient hc = new HttpClient();
        private static string strUrl = @"https://localhost:44377/api/Hanghoa";
        public static List<Hanghoa> getDSHanghoa()
        {
            try
            {
                var conn = hc.GetAsync(strUrl);
                conn.Wait();
                if (conn.Result.IsSuccessStatusCode == false)
                    return null;
                var kq = conn.Result.Content.ReadAsAsync<List<Hanghoa>>();
                kq.Wait();
                return kq.Result;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public static Hanghoa getHanghoa(string mahang)
        {
            try
            {
                string str = strUrl + @"/"+mahang;
                var conn = hc.GetAsync(str);
                conn.Wait();
                if (conn.Result.IsSuccessStatusCode == false)
                    return null;
                var kq = conn.Result.Content.ReadAsAsync<Hanghoa>();
                kq.Wait();
                return kq.Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool themHanghoa(Hanghoa h)
        {
            try
            {
                var kq=hc.PostAsJsonAsync<Hanghoa>(strUrl, h);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool xoaHanghoa(string mahang)
        {
            try
            {
                string str = strUrl + @"/" + mahang;
                var kq=hc.DeleteAsync(str);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            } 
        }
        public static bool suaHanghoa(Hanghoa h)
        {
            try
            {
                var kq=hc.PutAsJsonAsync<Hanghoa>(strUrl,h);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Hoadon> getDSHoadon()
        {
            try
            {
                string str = strUrl + @"/hoadon";
                var conn = hc.GetAsync(str);
                conn.Wait();
                if (conn.Result.IsSuccessStatusCode == false)
                    return null;
                var kq = conn.Result.Content.ReadAsAsync<List<Hoadon>>();
                kq.Wait();
                return kq.Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool themHoadon(Hoadon h)
        {
            try
            {
                string str = strUrl + @"/hoadon";
                var kq = hc.PostAsJsonAsync<Hoadon>(str, h);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Chitiethoadon> getDSChitietHoadon(string sohd)
        {
            try
            {
                string str = strUrl + @"/chitiethoadon/" + sohd;
                var conn = hc.GetAsync(str);
                conn.Wait();
                if (conn.Result.IsSuccessStatusCode == false)
                    return null;
                var kq = conn.Result.Content.ReadAsAsync<List<Chitiethoadon>>();
                kq.Wait();
                return kq.Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
