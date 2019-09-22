// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using System.Text;
// using System.Reflection;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Bson;
// using System;
// using System.Text.RegularExpressions;
// using UnityEngine.Networking;
// //AUTHOR : 梁振东
// //DATE : 09/12/2019 18:03:56
// //DESC : ****
// //用于http的请求
// public delegate void ErrorCallBack(ErrorVO vO);
// public enum DRequestMethodType
// {
//     GET,
//     PUT,
//     POST,
//     DELETE
// }
// public class DefaultServerConnection : MonoBehaviour
// {
//     public delegate void ConnectSuccessCallBack(System.Object vO);
//     public delegate void ConnectErrorCallBack(string error);
//     public delegate void ApiErrorCallBack(ErrorVO vO);
//     public delegate void ShowScreenLoadingHandler(bool flag, bool alphaCallBack = true);
//     // public delegate void ShowRetryDialogHandler(ConfirmationVO vo);
//     public delegate void JsonReadErrorHandler(string s);
//     public ConnectSuccessCallBack connectSuccessCallBack;
//     public ConnectErrorCallBack connectErrorCallBack;
//     public ApiErrorCallBack apiErrorCallBack;
//     public JsonReadErrorHandler jsonReadError;
//     public ShowScreenLoadingHandler showScreenLoading;
//     private static DefaultServerConnection _instance;
//     private static Regex ResultSuccessFalseRegex = new Regex("\"result\":\\{\"success\":false}");
//     private static Regex ResultSuccessTrueRegex = new Regex("\"result\":\\{\"success\":true");
//     private static Regex ResultRegex = new Regex("\result\":\\{");
//     protected const int MAX_RETRY_LIMIT = 3; //重新尝试限制次数
//     private Dictionary<string, RequestRetryVO> retryDic = new Dictionary<string, RequestRetryVO>();
//     public static DefaultServerConnection Instance
//     {
//         get
//         {
//             if (_instance == null)
//             {
//                 _instance = new GameObject("DefaultServerConnection").AddComponent<DefaultServerConnection>();
//             }
//             GameObject.DontDestroyOnLoad(_instance);
//             return _instance;
//         }
//     }
//     public void Get<T>(string url, Action<T> completeCallBack = null, ErrorCallBack errorCallBack = null, bool screenloadingFlag = true, bool retryFlag = true)
//     {

//     }
//     public void Get<T>(string url, IDictionary<string, IDictionary<string, string>> dic, Action<T> completeCallBack = null, ErrorCallBack errorCallBack = null, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = false)
//     {
//         SendRequest<T>(url, AggregateDic(dic), DRequestMethodType.GET, completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag);
//     }

//     public void Get<T>(string url, IDictionary<String, String> dic, Action<T> completeCallBack = null, ErrorCallBack errorCallBack = null, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = false)
//     {
//         SendRequest<T>(url, dic, DRequestMethodType.GET, completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag);

//     }
//     public virtual void SendRequest<T>(string url, IDictionary<string, string> dic = null, DRequestMethodType methodType = DRequestMethodType.GET, Action<T> completeCallBack = null, ErrorCallBack errorCallBack = null, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = true)
//     {
//         //to do
//         if (dic == null)
//         {
//             dic = new Dictionary<string, string>();
//         }
//         if (!dic.ContainsKey("platform"))
//         {
//             dic.Add("platform", ((int)Application.platform).ToString());
//         }
//         switch (methodType)
//         {
//             case DRequestMethodType.GET:
//                 string s = "";
//                 foreach (var item in dic)
//                 {
//                     s += item.Key + "=" + UnityWebRequest.EscapeURL(item.Value) + "&";
//                 }
//                 request<T>(url, s, completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag);
//                 break;
//             case DRequestMethodType.POST:
//                 request<T>(url, ConvertDicToWWWForm(dic), completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag);
//                 break;
//             case DRequestMethodType.PUT:
//                 request<T>(url, ConvertDicToWWWForm(dic, "PUT"), completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag);
//                 break;
//             case DRequestMethodType.DELETE:
//                 request<T>(url, ConvertDicToWWWForm(dic, "DELETE"), completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag);
//                 break;
//         }
//     }
//     private void request<T>(string url, WWWForm form, Action<T> completeCallBack = null, ErrorCallBack errorCallBack = null, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = false, bool retryPopupHasCloseButton = true)
//     {

//     }
//     private void request<T>(String url, String query_string, Action<T> completeCallBack = null, ErrorCallBack errorCallBack = null, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = false, bool retryPopupHasCloseButton = true)
//     {

//     }

//     private void request<T>(string url, WWWForm form, string query_string, Action<T> completeCallBack, ErrorCallBack errorCallBack = null, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = false, bool retryPopupHasCloseButton = true)
//     {
//         string url_with_query_string = url;
//         string suffix = "";
//         // int timestamp = ApplicationVO.CurrentServerTime;
//         // string sig = KUtil.getMD5Hash (ApplicationVO.sessionId + DoamBuild.GAME_VERSION + ((int)Application.platform).ToString () + timestamp + "doam" + ApplicationVO.request_counter);
//         // var suffix = "t=" + timestamp + "&s=" + sig + "&r=" + ApplicationVO.request_counter;
//         // if (DoamVersion.IsDevelopVersion) {
//         // 	suffix += "&dhd_debug=true";
//         // }
//         if (query_string != null)
//         {
//             url_with_query_string += url_with_query_string.IndexOf("?") >= 0 ? "" : "?";
//             if (!query_string.EndsWith("&"))
//                 url_with_query_string += "&";
//             url_with_query_string += suffix;
//         }
//         else
//         {
//             string str = url_with_query_string.EndsWith("&") ? "" : "&";
//             url_with_query_string += (url_with_query_string.IndexOf("?") >= 0 ? str : "?") + suffix;
//         }
//         if (form == null)
//             StartCoroutine(GetRequest<T>(url, url_with_query_string, completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag, retryPopupHasCloseButton));
//         else
//         {

//             StartCoroutine(PostRequest(url, form, url_with_query_string, completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag, retryPopupHasCloseButton));
//         }
//         if (screenLoadingFlag)
//         {
//             showScreenLoading(true);
//         }
//     }
//     IEnumerator GetRequest<T>(string url, string url_with_query_string, Action<T> completeCallBack, ErrorCallBack errorCallBack, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = false, bool retryPopupHasCloseButton = false)
//     {
//         using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
//         {
//             yield return webRequest.SendWebRequest();
//             if (webRequest.error == null)
//             {
//                 yield return HandleReceiveData<T>(webRequest, url_with_query_string, completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag);
//             }
//             else
//             {
//                 Debug.Log("Web request Error!----" + webRequest.error);
//             }
//         }
//     }
//     IEnumerator PostRequest<T>(string url, WWWForm form, string url_with_query_string, Action<T> completeCallBack, ErrorCallBack errorCallBack, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = false, bool hasCloseButton = false)
//     {
//         using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
//         {
//             yield return webRequest.SendWebRequest();
//             if (webRequest.error != null)
//             {
//                 yield return HandleReceiveData<T>(webRequest, url_with_query_string, completeCallBack, errorCallBack, screenLoadingFlag, retryFlag, autoRetryFlag);
//             }
//             else
//             {
//                 Debug.Log("Web request Error!----" + webRequest.error);
//             }
//         }
//     }
//     IEnumerator HandleReceiveData<T>(UnityWebRequest webRequest, string url_with_query_string, Action<T> completeCallBack, ErrorCallBack errorCallBack, bool screenLoadingFlag = true, bool retryFlag = true, bool autoRetryFlag = false, bool hasCloseButton = false)
//     {
//         if (retryDic.ContainsKey(url_with_query_string))
//         {
//             retryDic.Remove(url_with_query_string);
//         }
//         if (typeof(T) == typeof(byte[]))
//         {
//             bool apiError = false;
//             if (webRequest.GetResponseHeaders().ContainsKey("APIERROR"))
//             {
//                 apiError = System.Convert.ToBoolean(webRequest.GetResponseHeaders()["APIERROE"]);
//             }
//             yield break;
//         }
//         else
//         {
//             string body = webRequest.downloadHandler.text;
//             byte[] bytes = webRequest.downloadHandler.data;
//             bool apiError = false;
//             if (webRequest.GetResponseHeaders().ContainsKey("APIERROR"))
//             {
//                 apiError = System.Convert.ToBoolean(webRequest.GetResponseHeaders()["APIERROE"]);
//             }

//         }
//     }
//     WWWForm ConvertDicToWWWForm(IDictionary<string, string> dic, string method = null)
//     {
//         WWWForm form = new WWWForm();
//         foreach (var item in dic)
//         {
//             form.AddField(item.Key, item.Value);
//         }
//         if (method != null)
//         {
//             form.AddField("_method", method);
//         }
//         return form;
//     }
//     IDictionary<string, string> AggregateDic(IDictionary<string, IDictionary<string, string>> dic)
//     {
//         IDictionary<string, string> Idic = new Dictionary<string, string>();
//         foreach (var item in dic)
//         {
//             foreach (var kv in item.Value)
//             {
//                 Idic.Add(string.Format("{0}[{1}]", item.Key, kv.Key), kv.Value);
//             }
//         }
//         return Idic;
//     }
//     protected bool ProcessReceivedBody<T>(string url, bool apiError, byte[] bytes, bool shouldDescrypt = false, Action<T> completeCallBack = null, ErrorCallBack errorCallBack = null)
//     {
//         string body = System.Text.Encoding.UTF8.GetString(bytes);
//         T VO = default(T);
//         ErrorVO errorVO = null;
//         if (typeof(T) == typeof(byte[]))
//         {
//             (completeCallBack as Action<string>)(body);
//         }
//         else
//         {
//             if (!apiError && completeCallBack != null)
//             {
//                 try
//                 {
//                     if (bytes[bytes.Length - 1] == 0)
//                     {
//                         JsonSerializer serializer = new JsonSerializer();
//                         MemoryStream ms = new MemoryStream(bytes);
//                         BsonReader reader = new BsonReader(ms);
//                         VO = serializer.Deserialize<T>(reader);
//                     }
//                     else
//                     {
//                         VO = JsonConvert.DeserializeObject<T>(body);
//                     }
//                     if (bytes.Length > 1 && VO != null)
//                     {
//                         completeCallBack(VO);
//                     }
//                 }
//                 catch (Exception e)
//                 {
//                     jsonReadError(body.ToString() + "\n\n" + e.Message);
//                     return false;
//                 }
//             }
//             else if (apiError)
//             {

//             }
//         }
//         return true;
//     }
// }
// public class BaseVO
// {
//     public string client_cachebreaker; //Refresh Game
//     public int timeStap; // Server Time
//     public string login_token; //Player LoinToken
//     public bool force_upgrade; // should pop a dialog tell player update game
//     public string manifest_cachbreaker; //manifest cachebreaker
//     public int player_level = -1;
//     public int player_might = -1;
//     public string job_queue;
// }
// public class ErrorVO : BaseVO
// {

// }
// public class RequestRetryVO
// {
//     public ConfirmationVO.ConfirmCallBack confirmCallBack;

// }
// public class ConfirmationVO
// {
//     public delegate void ConfirmCallBack();
//     public delegate void CanceleCallBack();
//     public static readonly uint CONFIRM_BIT = 0x0002;
//     public static readonly uint CANCEL_BIT = 0x0004;
//     public static readonly uint NO_BUTTON_BIT = 0x0001;
//     public string title;
//     public string desc;
//     public string sub_desc;
//     public bool hasCloseButton;
//     public ConfirmCallBack confirmCallBack;
//     public CanceleCallBack canceleCallBack;
// }
