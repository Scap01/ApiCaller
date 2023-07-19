using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApiCaller
{
    public class AppBase
    {
        public async Task<T> GetSingleElementFromApiAsync<T>(string baseUrl, string endPoint, string customPoint = "", Dictionary<string, string> queryStringElements = null)
        {
            T element;
            var queryString = queryStringElements != null ? queryStringElements.ToString() : "";
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            //HttpClientHandler clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            try
            {
                //using (var client = new HttpClient(clientHandler))
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(userToken.token_type, userToken.access_token);
                    //DEV
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDUxMTQyOTEsImV4cCI6MTcwNTExNDI5MSwiaXNzIjoiaHR0cHM6Ly9kZXYuaWRlbnRpdHkuZGl0ZWNoLmNhIiwiYXVkIjoiRGl0ZWNoLkRldi5BcGkiLCJjbGllbnRfaWQiOiI2NTU2OGIwYzhhY2U0NTUwOTM3OWRlNzM1Yzg2NjFkNiIsImp0aSI6IjE3NDM0Njg1RjYxQTkyODI4NzU2NkVEMUU4MUEzQzZFIiwiaWF0IjoxNjQ1MTE0MjkxLCJzY29wZSI6WyJEaXRlY2guRGV2LkFwaS5EZXYuUmVhZCIsIkRpdGVjaC5EZXYuQXBpLkRldi5Xcml0ZSJdfQ.DGFuQGX6vm9JgcnX86UtP2U08eEn0MwoA5yJYkyBMBlt2rI4Alw99xOy3Dvvd2JAy9Mk6qwSTWe4bb9HrPtSpTAIGah4uri_olr5Z8xjJh6E07LU6-eZ9Mg6TAQRJgIwRDvbxULeNEhBqA0EBkQ534pgRwf3T0X4f44Xr-uBBbuG4_g4CeaYe1ijwvxTkf1DyNUKpzijGl4PKrtdSuFU8E39Y2FkQkNnXeFMU2xG5GamG8sokPZpgyclHdXB7HGwEzb6t8MQTlx6CPxgqu2N1n6pnSVZ73WD5csxZRZt58MZM9fwV8Yj5bbEiKiwndtSlsT6JiKGGgaJR7fMAn09Jg");
                    //PROD
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MzQ1MjI2NjgsImV4cCI6MTY5NDUyMjY2OCwiaXNzIjoiaHR0cHM6Ly9pZGVudGl0eS5kaXRlY2guY2EiLCJhdWQiOiJEaXRlY2guTWFpbi5BcGkiLCJjbGllbnRfaWQiOiIyZmY2OGI5NTc3NmY0MGJlOGY5YTM3NWVhNzg0ZDg4YiIsImp0aSI6IjMzMDhBQkI0MDEwRTdDNEM1M0E3MkM3MDBEMTUyMkVGIiwiaWF0IjoxNjM0NTIyNjY4LCJzY29wZSI6WyJEaXRlY2guTWFpbi5BcGkuUmVhZCIsIkRpdGVjaC5NYWluLkFwaS5Xcml0ZSJdfQ.gGRmEDY5Y9MZMxsl5626oObVRZwKCWOLd2S0pDwcc-JI8k8LqUwnuCxlnVcKPjgeKEEEP1skJTfJdSpyjAthEboodmVjMf2UgQw9TsS1PDHVqXUmjN4-5IIENYl-x41M3PyjIOEXAdClsknhpFhUMxfcvAxNaRU3rUqN-DQHyeOIqGEbSQhwxoeKnPtkFHb8f0slUOoTOIWUTrg9jpT9UJMjQx4Wli6Sjm_KUeqQEI2X6OFgzQdtXl9alI1hhTuAnFODLZbjQHR3RD87i6zNej2NsmIKl5Oh95GoxA7_l-DvK0Tws0QuOzv1RdVaI508hmzKbykOIXPA-tTDTwxsgQ");

                    Console.WriteLine("Pinging : " + "api/" + endPoint + customPoint + queryString);

                    // HTTP GET
                    var response = client.GetAsync("api/" + endPoint + customPoint + queryString).Result;
                    var content = response.Content;

                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync();
                        element = JsonConvert.DeserializeObject<T>(data.Result);
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }

                    return element;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return elements;
            }
        }

        public async Task<List<T>> GetListElementFromApiAsync<T>(string baseUrl, string endPoint, string customPoint = "", Dictionary<string, string> queryStringElements = null)
        {
            List<T> element;
            var queryString = queryStringElements != null ? queryStringElements.ToString() : "";
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            //HttpClientHandler clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            try
            {
                //using (var client = new HttpClient(clientHandler))
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(userToken.token_type, userToken.access_token);
                    //Dev
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDUxMTQyOTEsImV4cCI6MTcwNTExNDI5MSwiaXNzIjoiaHR0cHM6Ly9kZXYuaWRlbnRpdHkuZGl0ZWNoLmNhIiwiYXVkIjoiRGl0ZWNoLkRldi5BcGkiLCJjbGllbnRfaWQiOiI2NTU2OGIwYzhhY2U0NTUwOTM3OWRlNzM1Yzg2NjFkNiIsImp0aSI6IjE3NDM0Njg1RjYxQTkyODI4NzU2NkVEMUU4MUEzQzZFIiwiaWF0IjoxNjQ1MTE0MjkxLCJzY29wZSI6WyJEaXRlY2guRGV2LkFwaS5EZXYuUmVhZCIsIkRpdGVjaC5EZXYuQXBpLkRldi5Xcml0ZSJdfQ.DGFuQGX6vm9JgcnX86UtP2U08eEn0MwoA5yJYkyBMBlt2rI4Alw99xOy3Dvvd2JAy9Mk6qwSTWe4bb9HrPtSpTAIGah4uri_olr5Z8xjJh6E07LU6-eZ9Mg6TAQRJgIwRDvbxULeNEhBqA0EBkQ534pgRwf3T0X4f44Xr-uBBbuG4_g4CeaYe1ijwvxTkf1DyNUKpzijGl4PKrtdSuFU8E39Y2FkQkNnXeFMU2xG5GamG8sokPZpgyclHdXB7HGwEzb6t8MQTlx6CPxgqu2N1n6pnSVZ73WD5csxZRZt58MZM9fwV8Yj5bbEiKiwndtSlsT6JiKGGgaJR7fMAn09Jg");
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDU1NTMyMzQsImV4cCI6MTcwNTU1MzIzNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDQiLCJhdWQiOiJEaXRlY2guTG9jLkFwaSIsImNsaWVudF9pZCI6IjhjYmVkYWY2NGNhMzQ0MjM4ODlkYWI5OWI3MGRiNWE5IiwianRpIjoiREQ5NERCM0Q1NEE1NzhEQ0MwMzMxNEY4RDkxMjc0OTAiLCJpYXQiOjE2NDU1NTMyMzQsInNjb3BlIjpbIkRpdGVjaC5Mb2MuQXBpLkxPQy5SZWFkIiwiRGl0ZWNoLkxvYy5BcGkuTE9DLldyaXRlIl19.WsSaoBjBHfY8Ig5MlO05QweNu38EZpbNGcHC3Opx-RSXOMpW_tiTTtdZKGlN9U_985oEupAagvKPCyGJ5vn9S6uZxkOQ_cQu5_LeF3BD-eGnSDDzFsbADCSk2We6ilraMgOX2LE_TgLnDVOHvi1VKLt93vazADsWsd-6pdbCiJrXL4WPp7lowPgraCTnPXrOuPp1T_yKLXkShQfNa7tlMHrPr8lNkSchdnFiIHQztbjS1574_AAhB4HEuNi6kpBTN2A2GensD3J4Ehu-Wa7z0Vs6NMcPIE1VMwCPeW6iamarf5uz2YxNT6-WXJKKJz4nERZhCRg-DryeNebIaUkPdg");
                    //PROD
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MzQ1MjI2NjgsImV4cCI6MTY5NDUyMjY2OCwiaXNzIjoiaHR0cHM6Ly9pZGVudGl0eS5kaXRlY2guY2EiLCJhdWQiOiJEaXRlY2guTWFpbi5BcGkiLCJjbGllbnRfaWQiOiIyZmY2OGI5NTc3NmY0MGJlOGY5YTM3NWVhNzg0ZDg4YiIsImp0aSI6IjMzMDhBQkI0MDEwRTdDNEM1M0E3MkM3MDBEMTUyMkVGIiwiaWF0IjoxNjM0NTIyNjY4LCJzY29wZSI6WyJEaXRlY2guTWFpbi5BcGkuUmVhZCIsIkRpdGVjaC5NYWluLkFwaS5Xcml0ZSJdfQ.gGRmEDY5Y9MZMxsl5626oObVRZwKCWOLd2S0pDwcc-JI8k8LqUwnuCxlnVcKPjgeKEEEP1skJTfJdSpyjAthEboodmVjMf2UgQw9TsS1PDHVqXUmjN4-5IIENYl-x41M3PyjIOEXAdClsknhpFhUMxfcvAxNaRU3rUqN-DQHyeOIqGEbSQhwxoeKnPtkFHb8f0slUOoTOIWUTrg9jpT9UJMjQx4Wli6Sjm_KUeqQEI2X6OFgzQdtXl9alI1hhTuAnFODLZbjQHR3RD87i6zNej2NsmIKl5Oh95GoxA7_l-DvK0Tws0QuOzv1RdVaI508hmzKbykOIXPA-tTDTwxsgQ");

                    Console.WriteLine("Pinging : " + "api/" + endPoint + customPoint + queryString);

                    // HTTP GET
                    var response = client.GetAsync("api/" + endPoint + customPoint + queryString).Result;
                    var content = response.Content;

                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync();
                        element = JsonConvert.DeserializeObject<List<T>>(data.Result);
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }

                    return element;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return elements;
            }
        }

        public async Task<(string rawResponse, HttpStatusCode statusCode, T2 returnedElement)> PostToApiAsync<T1, T2>
            (string apiPath, string endpoint, T1 element, bool isPut = false)
        {
            (string, HttpStatusCode, T2) result;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var returnedElement = (T2)Activator.CreateInstance(typeof(T2));
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiPath);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Dev
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDgxMjkwMDMsImV4cCI6MTcwODEyOTAwMywiaXNzIjoiaHR0cHM6Ly9kZXYuaWRlbnRpdHkuZGl0ZWNoLmNhIiwiYXVkIjoiRGl0ZWNoLkRldi5BcGkiLCJjbGllbnRfaWQiOiJiYWVmZWE2NGUzNmM0NTk2ODQzODU4N2FjYWE3M2E4OCIsImp0aSI6IjdGNkRFODVFNEYwQTgxN0I1ODhFNzRENjJDNEQ3OTM0IiwiaWF0IjoxNjQ4MTI5MDAzLCJzY29wZSI6WyJEaXRlY2guRGV2LkFwaS5EZXYuUmVhZCIsIkRpdGVjaC5EZXYuQXBpLkRldi5Xcml0ZSJdfQ.ltvwP-uHXkObMECWjaPxCqGWtQmDWhEF3NM4lKbftNXBTAkrooiowiTTqqqlyjNxb2gPKMV0qsP5deh68i8S1tNGPJMAnGXld6esxLBf1UkP6zXZHYyNP4nRIemAEF8durdZeHvXWeeXmibp0ak0KVLuyODc-X22CM_uvL7s6TXYaEgJ8Ic44fC4jyaIC39i42bVvJ5WKKQ_v3k3OW_4npSGMU-D1Vq_wCQxtIrSn2v-H3y6KHCDSK4lSOgLpQd4kEQpkcC6BgK5BAHpwwjBL5Ot9VJoDDNBGzEa2TWs3bvkkM1CN7ch5EfjRAlCoTmMYmNdTBzkiErcU8VLvZcjAw");
                    //PROD
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MzQ1MjI2NjgsImV4cCI6MTY5NDUyMjY2OCwiaXNzIjoiaHR0cHM6Ly9pZGVudGl0eS5kaXRlY2guY2EiLCJhdWQiOiJEaXRlY2guTWFpbi5BcGkiLCJjbGllbnRfaWQiOiIyZmY2OGI5NTc3NmY0MGJlOGY5YTM3NWVhNzg0ZDg4YiIsImp0aSI6IjMzMDhBQkI0MDEwRTdDNEM1M0E3MkM3MDBEMTUyMkVGIiwiaWF0IjoxNjM0NTIyNjY4LCJzY29wZSI6WyJEaXRlY2guTWFpbi5BcGkuUmVhZCIsIkRpdGVjaC5NYWluLkFwaS5Xcml0ZSJdfQ.gGRmEDY5Y9MZMxsl5626oObVRZwKCWOLd2S0pDwcc-JI8k8LqUwnuCxlnVcKPjgeKEEEP1skJTfJdSpyjAthEboodmVjMf2UgQw9TsS1PDHVqXUmjN4-5IIENYl-x41M3PyjIOEXAdClsknhpFhUMxfcvAxNaRU3rUqN-DQHyeOIqGEbSQhwxoeKnPtkFHb8f0slUOoTOIWUTrg9jpT9UJMjQx4Wli6Sjm_KUeqQEI2X6OFgzQdtXl9alI1hhTuAnFODLZbjQHR3RD87i6zNej2NsmIKl5Oh95GoxA7_l-DvK0Tws0QuOzv1RdVaI508hmzKbykOIXPA-tTDTwxsgQ");
                    Console.WriteLine("Pinging : " + "api/" + endpoint);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(element), Encoding.UTF8, "application/json");
                    var x = content.ReadAsStringAsync();
                    HttpResponseMessage response;
                    if (!isPut)
                    {
                        response = client.PostAsync(endpoint, content).Result;

                    }
                    else
                    {
                        response = client.PutAsync(endpoint, content).Result;
                    }
                    var data = await response.Content.ReadAsStringAsync();
                    result = (data, response.StatusCode, returnedElement);
                    //if (!response.IsSuccessStatusCode)
                    //{
                    //    throw new Exception(data);
                    //}
                    result = (data, response.StatusCode, JsonConvert.DeserializeObject<T2>(data));
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return elements;
            }
        }

        public async Task<(string rawResponse, HttpStatusCode statusCode)> DeleteApiAsync
            (string apiPath, string endpoint)
        {
            (string, HttpStatusCode) result;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiPath);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Dev
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDgxMjkwMDMsImV4cCI6MTcwODEyOTAwMywiaXNzIjoiaHR0cHM6Ly9kZXYuaWRlbnRpdHkuZGl0ZWNoLmNhIiwiYXVkIjoiRGl0ZWNoLkRldi5BcGkiLCJjbGllbnRfaWQiOiJiYWVmZWE2NGUzNmM0NTk2ODQzODU4N2FjYWE3M2E4OCIsImp0aSI6IjdGNkRFODVFNEYwQTgxN0I1ODhFNzRENjJDNEQ3OTM0IiwiaWF0IjoxNjQ4MTI5MDAzLCJzY29wZSI6WyJEaXRlY2guRGV2LkFwaS5EZXYuUmVhZCIsIkRpdGVjaC5EZXYuQXBpLkRldi5Xcml0ZSJdfQ.ltvwP-uHXkObMECWjaPxCqGWtQmDWhEF3NM4lKbftNXBTAkrooiowiTTqqqlyjNxb2gPKMV0qsP5deh68i8S1tNGPJMAnGXld6esxLBf1UkP6zXZHYyNP4nRIemAEF8durdZeHvXWeeXmibp0ak0KVLuyODc-X22CM_uvL7s6TXYaEgJ8Ic44fC4jyaIC39i42bVvJ5WKKQ_v3k3OW_4npSGMU-D1Vq_wCQxtIrSn2v-H3y6KHCDSK4lSOgLpQd4kEQpkcC6BgK5BAHpwwjBL5Ot9VJoDDNBGzEa2TWs3bvkkM1CN7ch5EfjRAlCoTmMYmNdTBzkiErcU8VLvZcjAw");
                    //PROD
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFENENGRDRCRDdERDhERkE5MkJBNUIwNEE0N0EwQkQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MzQ1MjI2NjgsImV4cCI6MTY5NDUyMjY2OCwiaXNzIjoiaHR0cHM6Ly9pZGVudGl0eS5kaXRlY2guY2EiLCJhdWQiOiJEaXRlY2guTWFpbi5BcGkiLCJjbGllbnRfaWQiOiIyZmY2OGI5NTc3NmY0MGJlOGY5YTM3NWVhNzg0ZDg4YiIsImp0aSI6IjMzMDhBQkI0MDEwRTdDNEM1M0E3MkM3MDBEMTUyMkVGIiwiaWF0IjoxNjM0NTIyNjY4LCJzY29wZSI6WyJEaXRlY2guTWFpbi5BcGkuUmVhZCIsIkRpdGVjaC5NYWluLkFwaS5Xcml0ZSJdfQ.gGRmEDY5Y9MZMxsl5626oObVRZwKCWOLd2S0pDwcc-JI8k8LqUwnuCxlnVcKPjgeKEEEP1skJTfJdSpyjAthEboodmVjMf2UgQw9TsS1PDHVqXUmjN4-5IIENYl-x41M3PyjIOEXAdClsknhpFhUMxfcvAxNaRU3rUqN-DQHyeOIqGEbSQhwxoeKnPtkFHb8f0slUOoTOIWUTrg9jpT9UJMjQx4Wli6Sjm_KUeqQEI2X6OFgzQdtXl9alI1hhTuAnFODLZbjQHR3RD87i6zNej2NsmIKl5Oh95GoxA7_l-DvK0Tws0QuOzv1RdVaI508hmzKbykOIXPA-tTDTwxsgQ");
                    Console.WriteLine("Pinging : " + "api/" + endpoint);
                    var response = client.DeleteAsync(endpoint).Result;
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = (data, response.StatusCode);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return elements;
            }
        }
    }
}

